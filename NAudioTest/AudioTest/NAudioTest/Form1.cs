using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using NAudio.Wave.SampleProviders;
using NAudio.Dsp;


namespace NAudioTest {
    public partial class Form1 : Form {
        DirectSoundOut output;
        AudioFileReader audioFileReader;

        public Form1() {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e) {
            this.disposeWave();
        }

        private void play(object sender, EventArgs e) {
            if (output.PlaybackState == PlaybackState.Playing) {
                output.Pause();
                playButton.Text = "Play";
            } else {
                output.Play();
                playButton.Text = "Pause";
            }
        }

        private void stop(object sender, EventArgs e) {
            output.Stop();
            audioFileReader.CurrentTime = TimeSpan.Zero;
            playButton.Text = "Play";
        }

        private void timer1_Tick(object sender, EventArgs e) {
            
            if (audioFileReader != null) {
                int seconds = audioFileReader.CurrentTime.Seconds;
                int minutes = audioFileReader.CurrentTime.Minutes;
                int milliseconds = audioFileReader.CurrentTime.Milliseconds;
                label1.Text = minutes.ToString() + ":" + seconds.ToString() + ":" + milliseconds.ToString();
                progressBar.Value = (int)audioFileReader.CurrentTime.TotalSeconds;

                if (audioFileReader.Position == audioFileReader.Length) {
                    stop(sender, e);
                }
            }
        }

        private void open(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3 Files|*.mp3";
            if (dialog.ShowDialog() != DialogResult.OK) return;

            if (audioFileReader != null) audioFileReader.Dispose();
            audioFileReader = new AudioFileReader(dialog.FileName);
            progressBar.Maximum = (int)audioFileReader.TotalTime.TotalSeconds;

            // For wave painter
            //waveChannel = new WaveChannel32(audioFileReader);
            //postVolumeMeter.StreamVolume += OnPostVolumeMeter;
            /*output = new DirectSoundOut();
            output.Init(new SampleToWaveProvider(postVolumeMeter));*/
            SampleWaveProvider swp = new SampleWaveProvider(audioFileReader);
            swp.PerformFFT = true;
            swp.MaximumCalculated += swp_MaximumCalculated;
            swp.FftCalculated += swp_FftCalculated;
            output = new DirectSoundOut();
            output.Init(swp);
            // Enable playback buttons
            playButton.Enabled = true;
            stopButton.Enabled = true;

            output.Play();
            playButton.Text = "Pause";
        }

        void swp_FftCalculated(object sender, SampleWaveProvider.FftEventArgs e)
        {
            Complex[] fftResults = e.Result;
            for (int n = 0; n < fftResults.Length / 2; n += 2 )
            {
                // averaging out bins
                updateText((GetYPosLog(fftResults[n])).ToString());
            }
        }
        private void updateText(string text)
        {
            fft.Text = text;
        } 
        private double GetYPosLog(Complex c)
        {
            // not entirely sure whether the multiplier should be 10 or 20 in this case.
            // going with 10 from here http://stackoverflow.com/a/10636698/7532
            double intensityDB = 10 * Math.Log10(Math.Sqrt(c.X * c.X + c.Y * c.Y));
            double minDB = -90;
            if (intensityDB < minDB) intensityDB = minDB;
            double percent = intensityDB / minDB;
            // we want 0dB to be at the top (i.e. yPos = 0)
            double yPos = percent * 2;
            return yPos;
        }
        private void swp_MaximumCalculated(object sender, SampleWaveProvider.MaxSampleEventArgs e)
        {
            throw new NotImplementedException();
        } 
        void OnPreVolumeMeter(object sender, StreamVolumeEventArgs e) {
            // we know it is stereo
            waveformPainter1.AddMax(e.MaxSampleValues[0]);
            waveformPainter2.AddMax(e.MaxSampleValues[1]);
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e) {
            // we know it is stereo
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues[1];
        }

        private void scrub(object sender, EventArgs e) {
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(progressBar.Value);
        }

        private void volumeSlider1_VolumeChanged(object sender, EventArgs e) {
            audioFileReader.Volume = volumeSlider1.Volume;
        }

        private void disposeWave() {
            if (output != null) {
                if (output.PlaybackState == PlaybackState.Playing) {
                    output.Stop();
                }
                output.Dispose();
                output = null;
                playButton.Enabled = false;
                stopButton.Enabled = false;
            }
            if (audioFileReader != null) {
                audioFileReader.Dispose();
                audioFileReader = null;
                playButton.Enabled = false;
                stopButton.Enabled = false;
            }
        }


    }

}
