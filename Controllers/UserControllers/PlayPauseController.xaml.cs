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
    /// This control is used to set the play pause 
    /// </summary>
    public partial class PlayPauseControl : UserControl
    {
        public enum PlayBackStatus { Play, Pause };
        private PlayBackStatus playBackStatus = PlayBackStatus.Pause;
        [Description("Fires when the status changes"), Category("Leon Custom Design")]
        public event EventHandler<PlayPauseEventArgs> StatusChanged;
        public PlayPauseControl()
        {
            InitializeComponent();
            playButton.Visibility = Visibility.Hidden;
            this.MouseUp+=onClicked;

        }

        private void onClicked(object sender, MouseButtonEventArgs e)
        {
            if (playBackStatus == PlayBackStatus.Play)
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
            updateHandler(playBackStatus);
        }
        protected virtual void updateHandler(PlayBackStatus status)
        {
            EventHandler<PlayPauseEventArgs> handler = StatusChanged;
            if (StatusChanged != null) {
                handler(this, new PlayPauseEventArgs(status));
            }
        }
    }
    public class PlayPauseEventArgs : EventArgs
    {
        public PlayPause.PlayPauseControl.PlayBackStatus status;
        public PlayPauseEventArgs(PlayPause.PlayPauseControl.PlayBackStatus status)
        {
            this.status = status;
        }
    }
}
