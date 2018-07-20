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

using System.ComponentModel;
using System.ComponentModel.Design;

namespace UserControlLibrary {
    /// <summary>
    /// Interaction logic for RepeatRandomButton.xaml
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class RepeatRandomButton : UserControl {

        public enum PlaybackType { NONE, REPEAT_ONE, REPEAT_ALL, RANDOM }
        private PlaybackType playbackType;

        public RepeatRandomButton() {
            InitializeComponent();
            this.MouseUp += new MouseButtonEventHandler(onClick);
            this.MouseUp += new MouseButtonEventHandler(onPlaybackStateChanged);
            //playbackType = PlaybackType.NONE;
        }

        public event EventHandler Change;

        public PlaybackType PlaybackMode {
            get {
                return playbackType;
            }
        }

        private void onPlaybackStateChanged(object sender, EventArgs e) {
            switch (playbackType) {
                case PlaybackType.NONE:
                    repeatIcon.Visibility = Visibility.Visible;
                    repeatOneIcon.Visibility = Visibility.Hidden;
                    randomIcon.Visibility = Visibility.Hidden;
                    noneIcon.Visibility = Visibility.Hidden;
                    playbackType = PlaybackType.REPEAT_ALL;
                    break;
                case PlaybackType.REPEAT_ALL:
                    repeatIcon.Visibility = Visibility.Hidden;
                    repeatOneIcon.Visibility = Visibility.Visible;
                    randomIcon.Visibility = Visibility.Hidden;
                    noneIcon.Visibility = Visibility.Hidden;
                    playbackType = PlaybackType.REPEAT_ONE;
                    break;
                case PlaybackType.REPEAT_ONE:
                    repeatIcon.Visibility = Visibility.Hidden;
                    repeatOneIcon.Visibility = Visibility.Hidden;
                    randomIcon.Visibility = Visibility.Visible;
                    noneIcon.Visibility = Visibility.Hidden;
                    playbackType = PlaybackType.RANDOM;
                    break;
                case PlaybackType.RANDOM:
                    repeatIcon.Visibility = Visibility.Hidden;
                    repeatOneIcon.Visibility = Visibility.Hidden;
                    randomIcon.Visibility = Visibility.Hidden;
                    noneIcon.Visibility = Visibility.Visible;
                    playbackType = PlaybackType.NONE;
                    break;
                
            }
        }

        private void onClick(object sender, EventArgs e) {

            if (Change != null) {
                Change(sender, e);
            }
        }





    }
}
