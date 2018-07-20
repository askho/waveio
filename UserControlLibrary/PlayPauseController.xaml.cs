using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PlayPause
{
    /// <summary>
    /// This control is used for the play pause functionality in WaveIO. 
    /// It has its own custom event args that define the current state of the
    /// button. 
    /// 
    /// Created By  : Leon Ho
    /// Student ID  : A00879122
    /// Date        : March 10 2015
    /// </summary>
    public partial class PlayPauseControl : UserControl
    {
        //An enumerated type to easier understand the status of the control. 
        public enum PlayBackStatus { Play, Pause };
        private bool isEnabled;
        private PlayBackStatus playBackStatus = PlayBackStatus.Pause;
        //Used to let others know the status of the controller. 
        //If we are setting it, we will update the controller to 
        //reflect the state. 
        public PlayBackStatus status
        {
            set
            {
                if (value == PlayBackStatus.Play)
                {
                    setIcon(PlayBackStatus.Play);
                }
                else
                {
                    setIcon(PlayBackStatus.Pause);
                }
            }
            get
            {
                return playBackStatus;
            }
        }
        public new bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                if (isEnabled == false)
                {
                    Resources["brush"] = new SolidColorBrush(Color.FromArgb(128,41, 108, 166));
                }
                else
                {
                    Resources["brush"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF296CA6"));
                }
                base.IsEnabled = value;
            }
        }
        [Description("Fires when the status changes"), Category("Leon Custom Design")]
        public event EventHandler<PlayPauseEventArgs> StatusChanged;
        /// <summary>
        /// This constructor sets the default visibility of the buttons, and sets up 
        /// an onclick listener to update the icon and handler. 
        /// </summary>
        public PlayPauseControl()
        {
            InitializeComponent();
            playButton.Visibility = Visibility.Hidden;
            this.MouseUp+=onClicked;
        }
        /// <summary>
        /// This handler changes the view of the button, and then updates the 
        /// event status. 
        /// </summary>
        /// <param name="sender">Not used. </param>
        /// <param name="e">Not used. </param>
        public void onClicked(object sender, MouseButtonEventArgs e)
        {
            if (playBackStatus == PlayBackStatus.Play)
                setIcon(PlayBackStatus.Pause);
            else
                setIcon(PlayBackStatus.Play);
            updateHandler(playBackStatus);
        }
        /// <summary>
        /// This updates the external handler so that everybody can know that
        /// we've been clicked. 
        /// </summary>
        /// <param name="status">A custom event args that returns the playback status. </param>
        protected virtual void updateHandler(PlayBackStatus status)
        {
            EventHandler<PlayPauseEventArgs> handler = StatusChanged;
            if (StatusChanged != null) {
                handler(this, new PlayPauseEventArgs(status));
            }
        }
        /// <summary>
        /// Show and hide the corresponding shapes so to accurately reflect 
        /// the statue of the coontrol. 
        /// </summary>
        /// <param name="status">The playback status to reflect.</param>
        private void setIcon(PlayBackStatus status)
        {
            if (status == PlayBackStatus.Pause)
            {
                playBackStatus = PlayBackStatus.Pause;
                playButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Visible;
            }
            else
            {
                playBackStatus = PlayBackStatus.Play;
                playButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Hidden;
            }
        }
    }
    /// <summary>
    /// A custom event args that gives the outside the status
    /// of our button. 
    /// </summary>
    public class PlayPauseEventArgs : EventArgs
    {
        public PlayPause.PlayPauseControl.PlayBackStatus status;
        public PlayPauseEventArgs(PlayPause.PlayPauseControl.PlayBackStatus status)
        {
            this.status = status;
        }
    }
}
