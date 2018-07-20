using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;
using System.Windows.Media.Animation;
using System.Windows.Forms;
using UserControlLibrary;

namespace WaveIO_Main
{
    /// <summary>
    /// CREATED BY DUY LE & JENS Christiansen
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A timer for the progress of the progress bar
        /// </summary>
        private System.Windows.Threading.DispatcherTimer timer;

        /// <summary>
        /// A timer for the animation of opening/closing the playlist
        /// </summary>
        private System.Windows.Threading.DispatcherTimer titleAnimationTimer;

        private System.Windows.Threading.DispatcherTimer appTimer;
        private int animationFadeIn = 0;


        private AudioEngine audioEngine;

        private String folderPath = "";

        // Indicates if the progress bar is being scrubbed.
        private bool isScrubbing = false;

        public int xpos = 0;
        private bool openPlaylist = true;

        private static int delay = 0;
        private static byte colorOpacity = 255;
        private const int VISUALIZER_MULTIPLIER = 10000;

        private List<System.IO.FileInfo> files;
        private int currentIndex;

        public bool repeatToggled = false;
        public bool shuffleToggled = false;
        public bool playlistRepeatToggled = false;

        private System.Windows.Controls.ListViewItem selectedItem;

        /// <summary>
        /// Handle logic for the current index of the song being played from the list of 'files'
        /// </summary>
        public int CurrentIndex {
            get {
                return currentIndex;
            }
            set {

                if (value < 0) {
                    value = 0;
                }

                if (value > files.Count - 1) {
                    value = files.Count;
                }
                currentIndex = value;
                handleTrackForwardButton(currentIndex);
            }
        }

        private void handleTrackForwardButton(int value) {
            if (value >= files.Count - 1) {
                if (this.shuffleToggled || this.playlistRepeatToggled) {
                    this.trackForwardBtn.IsEnabled = true;
                } else {
                    this.trackForwardBtn.IsEnabled = false;
                }
            } else {
                this.trackForwardBtn.IsEnabled = true;
            }
        }

        /// <summary>
        /// This constructor initializes all of the timers which will be
        /// used for the animations, and syncing. This constructor also 
        /// sets up some a couple of even handlers. 
        /// </summary>
        public MainWindow()
        {
            files = new List<System.IO.FileInfo>();
            audioEngine = new AudioEngine();
            audioEngine.FftCalculated += audioEngine_FftCalculated;
            audioEngine.SongCompleted += audioEngine_SongCompleted;

            InitializeComponent();

            mainForm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            const int SPLASH_TIME = 700;
            const int SPLASH_FADE_TIME = 2000;

            SplashScreen splash = new SplashScreen("Resources/logo.png");

#if (!DEBUG)
            splash.Show(false, true);
            System.Threading.Thread.Sleep(SPLASH_TIME);

            splash.Close(TimeSpan.FromMilliseconds(SPLASH_FADE_TIME));
#endif

            playPause.IsEnabled = false;
            playPause.status = PlayPause.PlayPauseControl.PlayBackStatus.Play;
            trackForwardBtn.IsEnabled = false;
            trackBackwardBtn.IsEnabled = false;


            // Set up the timer.
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(updateProgressBar);
            timer.Interval = new TimeSpan(100000);
            timer.Start();

            titleAnimationTimer = new System.Windows.Threading.DispatcherTimer();
            titleAnimationTimer.Tick += new EventHandler(titleAnimation);
            titleAnimationTimer.Interval = new TimeSpan(165000);
            titleAnimationTimer.Start();

            /*
             *Lambda expressions to allow user to scrub the progress bar.
             * */
            progressBar.PreviewMouseDown += (a, e) =>
            {
                isScrubbing = true;
            };
            progressBar.PreviewMouseUp += (a, e) =>
            {
                audioEngine.CurrentTime = TimeSpan.FromSeconds(progressBar.Val);
                isScrubbing = false;
            };

            mainForm.Width = 700;

            progressBar.userEvent += new EventHandler(onThumbDrag);

            menu.OpenPlaylist += new RoutedEventHandler(OpenPlaylist);
        }

       

        void audioEngine_SongCompleted(object sender, StoppedEventArgs e)
        {
            if (audioEngine.Position >= audioEngine.Length)
            {


                selectedItem = listView.ItemContainerGenerator.ContainerFromIndex(CurrentIndex) as System.Windows.Controls.ListViewItem;
                selectedItem.IsSelected = false;
                SongItem path;

                if (shuffleToggled)
                {
                    Random rnd = new Random();
                    int shuffleIndex = rnd.Next(0, files.Count);
                    CurrentIndex = shuffleIndex;
                    path = (SongItem)listView.Items[CurrentIndex];
                    
                }

                else if (playlistRepeatToggled && CurrentIndex == files.Count - 1)
                {
                    CurrentIndex = 0;
                    path = (SongItem)listView.Items[CurrentIndex];
                }

                else if (repeatToggled)
                {
                    path = (SongItem)listView.Items[CurrentIndex];
                }

               

                else if (CurrentIndex < files.Count - 1 || shuffleToggled)
                {
                    ++CurrentIndex;
                    path = (SongItem)listView.Items[CurrentIndex];
                } else {
                    artistName.Content = "";
                    songName.Content = "";
                    audioEngine.Stop();
                    audioEngine.Dispose();
                    return;
                }

                playSong(path.directory + path.fileName);
                
                selectedItem = listView.ItemContainerGenerator.ContainerFromIndex(CurrentIndex) as System.Windows.Controls.ListViewItem;
                selectedItem.IsSelected = true;
                
            }
        }

        /// <summary>
        /// Event handler for when the audio engine is done calculating 
        /// FFT results. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void audioEngine_FftCalculated(object sender, FftEventArgs e)
        {
            try
            {
                spectrumVisualizer.updateBars(e.Result);
            }
            catch (NullReferenceException) { }
        }

        /// <summary>
        /// This method updates the progress bar according to the 
        /// audio engines current time property
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void updateProgressBar(object sender, EventArgs e)
        {
            if (audioEngine != null)
            {
                if (!isScrubbing)
                {
                    progressBar.Val = audioEngine.CurrentTime.TotalSeconds;
                }
                if (audioEngine.Position == audioEngine.Length)
                {
                    audioEngine.Stop();
                }
            }
        }

        private void onThumbDrag(object sender, EventArgs e)
        {
            String seconds = TimeSpan.FromSeconds(progressBar.Val).Seconds.ToString("00");
            String minutes = TimeSpan.FromSeconds(progressBar.Val).Minutes.ToString();
            progressBar.ThumbText = minutes + ":" + seconds;
        }

        private void appAnimation(object sender, EventArgs e)
        {
            ++animationFadeIn;
        }

        /// <summary>
        /// This method animates the tile bar to scroll left.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used. </param>
        private void titleAnimation(object sender, EventArgs e)
        {

            if (delay <= 0)
            {

                int titleWidth = 0;
                if (artistName.ActualWidth > songName.ActualWidth)
                {
                    titleWidth = (int)artistName.ActualWidth;
                }
                else
                {
                    titleWidth = (int)songName.ActualWidth;
                }

                Canvas.SetLeft(artistName, --xpos);
                Canvas.SetLeft(songName, --xpos);

                if ((int)xpos <= ((int)(titleWidth - 100) * -1))
                {
                    colorOpacity = 0;
                    artistName.Foreground = new SolidColorBrush(Color.FromArgb(colorOpacity, 182, 225, 242));
                    songName.Foreground = new SolidColorBrush(Color.FromArgb(colorOpacity, 182, 225, 242));
                    delay = 100;
                    xpos = 350 - (int)(artistName.ActualWidth / 2);
                    Canvas.SetLeft(artistName, xpos);
                    Canvas.SetLeft(songName, xpos);
                }
            }
            --delay;

            if (colorOpacity < 255)
            {
                colorOpacity = (byte)(colorOpacity + 5);
                artistName.Foreground = new SolidColorBrush(Color.FromArgb(colorOpacity, 182, 225, 242));
                songName.Foreground = new SolidColorBrush(Color.FromArgb(colorOpacity, 182, 225, 242));
            }

        }
        
        /// <summary>
        /// This event handler activates when the list view is clicked and starts playing
        /// the song that was clicked. 
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void listviewOnClick(object sender, MouseButtonEventArgs e)
        {
            CurrentIndex = listView.SelectedIndex;
            SongItem item = (SongItem)listView.SelectedItems[0];
            if (item != null)
            {
                playSong(item.directory + item.fileName);
            }
            }

        /// <summary>
        /// This method calls the audio engine to play the song that is given in the pat
        /// </summary>
        /// <param name="path">The path to the audio file to open. </param>
        public void playSong(string path)
        {
            audioEngine.FileName = path;
            
            if (audioEngine.Load())
            {
                playPause.IsEnabled = true;

                playPause.status = PlayPause.PlayPauseControl.PlayBackStatus.Pause;
                progressBar.Max = audioEngine.TotalTime.TotalSeconds;
                audioEngine.Play();
                volumeSlider.IsEnabled = true;

                try
                {
                    TagLib.File file = TagLib.File.Create(path);
                    artistName.Content = file.Tag.Performers[0];
                    songName.Content = file.Tag.Title;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    artistName.Content = path;
                }

                trackForwardBtn.IsEnabled = true;
                trackBackwardBtn.IsEnabled = true;
            }
            xpos = -1000;//Move the title
        }

        /// <summary>
        /// This method opens up a file browser for the user to select a single song.
        /// It then tells the audio engine to play the song selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "MP3 Files|*.mp3;";
            if (dialog.ShowDialog() != true) return;
            playSong(dialog.FileName);
            
            volumeSlider.userEvent += new RoutedPropertyChangedEventHandler<double>(volumeChanged);

            listViewLabel.Visibility = Visibility.Hidden;
            trackForwardBtn.IsEnabled = false;
            trackBackwardBtn.IsEnabled = false;
        }

        /// <summary>
        /// This event handler updates the audio engine volume. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            audioEngine.Volume = (float)volumeSlider.Val;
        }
        /// <summary>
        /// Play 
        /// </summary>
        private void play()
        {
            audioEngine.Play();
        }
        /// <summary>
        /// Pause
        /// </summary>
        private void pause()
        {
            audioEngine.Pause();
        }
        /// <summary>
        /// Clean up when we are closing the application
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            audioEngine.Dispose();
            this.Close();
        }


        private void OpenPlaylist(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK) return;

            try
            {
                listView.Items.Clear();
                folderPath = fbd.SelectedPath + "\\";
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(folderPath);
                files = new List<FileInfo>(directory.GetFiles("*.mp3"));

                foreach (System.IO.FileInfo f in files)
                {
                    SongItem item = new SongItem(folderPath, f.Name);
                    listView.Items.Add(item);
                }

                CurrentIndex = 0;
                if (listView.Items[CurrentIndex] != null)
                {
                    this.playSong(folderPath + listView.Items[CurrentIndex]);
                }
               
                listViewLabel.Visibility = Visibility.Hidden;
                
                
               /*
                selectedItem = this.listView.ItemContainerGenerator.ContainerFromIndex(0) as System.Windows.Controls.ListViewItem;
                selectedItem.IsSelected = true;*/
                //var test = listView.Items[0];
                //test.IsSelected = true;

            }
            catch (IOException)
            {
                System.Windows.MessageBox.Show("Cannot open file");
            }

            openPlaylist = true;
            trackForwardBtn.IsEnabled = true;
            trackBackwardBtn.IsEnabled = true;
            playlistButton_Click(null, null);
        }

        /// <summary>
        /// This drags the UI when the header block is selected and dragged. 
        /// </summary>
        /// <param name="sender">Not used. </param>
        /// <param name="e">Used to check what the mouse button is clicked</param>
        private void headerBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /// <summary>
        /// Handler for the playpause button. It checks the status of the button and
        /// updates the methods audio engine accordingly. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playPause_StatusChanged(object sender, PlayPause.PlayPauseEventArgs e)
        {
            if (e.status == PlayPause.PlayPauseControl.PlayBackStatus.Pause)
            {
                play();
            }
            else
            {
                pause();
            }
        }

        private void playlistButton_Click(object sender, RoutedEventArgs e)
        {
            QuinticEase ease = new QuinticEase();

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;


            if (openPlaylist == true)
            {
                animation.From = mainForm.Width;
                animation.To = 900;
                openPlaylist = false;
            }
            else if (openPlaylist == false)
            {
                animation.From = 900;
                animation.To = 700;
                openPlaylist = true;
            }

            animation.Duration = TimeSpan.FromSeconds(0.3);

            mainForm.BeginAnimation(Window.WidthProperty, animation);
        }

        /// <summary>
        /// Event handler for when a file is dropped into the main form
        /// It checks if the file is a mp3 file, if it is a mp3 file it will
        /// begin to play the file. 
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">DrageEventArgs, used to get the file that was draged over. </param>
        private void audioForm_Drop(object sender, System.Windows.DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
            if (a != null)
            {
                string s = a.GetValue(0).ToString();
                if (System.IO.Path.GetExtension(s) == ".mp3")
                {
                    this.playSong(s);
                    trackBackwardBtn.IsEnabled = true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("That is not an mp3 file");
                }
            }
        }
        /// <summary>
        /// Event handler for when the file is dropped into the playlist area.
        /// It checks if the files are mp3s if they are we add them to the play list. 
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Used to get the file name</param>
        private void playList_Drop(object sender, System.Windows.DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
            if (a != null)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    string s = a.GetValue(i).ToString();
                    if (System.IO.Path.GetExtension(s) == ".mp3")
                    {
                        FileInfo temp = new FileInfo(s);
                        
                        files.Add(new System.IO.FileInfo(s));
                    }
                }
                listView.Items.Clear();
                foreach (System.IO.FileInfo f in files)
                {
                    System.Windows.Controls.ListViewItem fileItem = new System.Windows.Controls.ListViewItem();
                    SongItem item = new SongItem(f.DirectoryName+"\\", f.Name);
                    listView.Items.Add(item);
                }
                
            }

            listViewLabel.Visibility = Visibility.Hidden;

        }

        private void onTrackForward(object sender, MouseButtonEventArgs e) {
            audioEngine.CurrentTime = TimeSpan.FromSeconds(progressBar.Max - .1);
        }

        private void onTrackRestart(object sender, MouseButtonEventArgs e) {
            audioEngine.CurrentTime = TimeSpan.Zero;
            }

        private void onTrackBack(object sender, MouseButtonEventArgs e) {
            selectedItem = this.listView.ItemContainerGenerator.ContainerFromIndex(CurrentIndex) as System.Windows.Controls.ListViewItem;
            selectedItem.IsSelected = false;
            SongItem path = (SongItem)listView.Items[--CurrentIndex];
            playSong(path.directory + path.fileName);
            selectedItem = this.listView.ItemContainerGenerator.ContainerFromIndex(CurrentIndex) as System.Windows.Controls.ListViewItem;
            selectedItem.IsSelected = true;
        }

        private void onPlaybackStateChanged(object sender, EventArgs e) {
            switch (modeBtn.PlaybackMode) {
                case RepeatRandomButton.PlaybackType.REPEAT_ALL:
                    Console.WriteLine("repeat one");
                    repeatToggled = true;
                    shuffleToggled = false;
                    playlistRepeatToggled = false;
                    break;
                case RepeatRandomButton.PlaybackType.REPEAT_ONE:
                    Console.WriteLine("repeat random");
                    repeatToggled = false;
                    shuffleToggled = true;
                    playlistRepeatToggled = false;
                    break;
                case RepeatRandomButton.PlaybackType.RANDOM:
                    Console.WriteLine("none");
                    repeatToggled = false;
                    shuffleToggled = false;
                    playlistRepeatToggled = false;
                    break;
                case RepeatRandomButton.PlaybackType.NONE:
                    Console.WriteLine("repeat all");
                    repeatToggled = false;
                    shuffleToggled = false;
                    playlistRepeatToggled = true;
                    break;
            }
        }


    } // End class MainWindow
}
