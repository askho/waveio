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

namespace UserControlLibrary {
    /// <summary>
    /// Interaction logic for TrackBackwardButton.xaml
    /// </summary>
    public partial class TrackBackwardButton : UserControl {

        private bool isEnabled = false;
        public TrackBackwardButton() {
            InitializeComponent();
            this.MouseUp += new MouseButtonEventHandler(onClicked);
            this.MouseDoubleClick += new MouseButtonEventHandler(onDoubleClicked);
        }

        [Description("Fires when the button is clicked"), Category("Jens Custom Design")]
        public event MouseButtonEventHandler TrackRestart;

        public event MouseButtonEventHandler TrackBack;

        private void onClicked(object sender, MouseButtonEventArgs e) {
            if (TrackRestart != null) {
                TrackRestart(sender, e);
            }
        }

        private void onDoubleClicked(object sender, MouseButtonEventArgs e) {
            if (TrackBack != null) {
                TrackBack(sender, e);
            }
        }

        public double IconWidth {
            get {

                return iconScale.ScaleX;
            }
            set {

                iconScale.ScaleX = value;
            }
        }

        public double IconHeight {
            get {
                return iconScale.ScaleY;
            }
            set {
                iconScale.ScaleY = value;
            }
        }

        public new bool IsEnabled {
            get {
                return isEnabled;
            }
            set {
                isEnabled = value;
                if (isEnabled == false) {
                    Resources["brush"] = new SolidColorBrush(Color.FromArgb(128, 41, 108, 166));
                } else {
                    Resources["brush"] = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF296CA6"));
                }
                base.IsEnabled = value;
            }
        }


    }


}
