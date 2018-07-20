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

namespace UserControlLibrary
{
    /// <summary>
    /// Interaction logic for VolumeBar.xaml
    /// </summary>
    public partial class VolumeBar : UserControl
    {
        public VolumeBar()
        {
            InitializeComponent();
            volumeBar.Minimum = 0;
            volumeBar.Maximum = 1;
            volumeBar.Value = 1;
        }


        /// <summary>
        /// The property for the color of the current progress of the progress bar.
        /// </summary>
        public SolidColorBrush ProgressbarColor1
        {
            get { return (SolidColorBrush)Resources["pbColor1"]; }
            set { Resources["pbColor1"] = value; }

        }

        /// <summary>
        /// The property for the color of the progress bar.
        /// </summary>
        public SolidColorBrush ProgressbarColor2
        {
            get { return (SolidColorBrush)Resources["pbColor2"]; }
            set { Resources["pbColor2"] = value; }

        }

        public SolidColorBrush ThumbColor
        {
            get { return (SolidColorBrush)Resources["thumbColor"]; }
            set { Resources["thumbColor"] = value; }
        }

        public double Max
        {
            get { return volumeBar.Maximum; }
            set { volumeBar.Maximum = value; }
        }

        public double Min
        {
            get { return volumeBar.Minimum; }
            set { volumeBar.Minimum = value; }
        }

        public double Val
        {
            get { return volumeBar.Value; }
            set { volumeBar.Value = value; }
        }

        /// <summary>
        /// User Control event. Triggered on thumb drag.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<double> userEvent;
        private void progressBar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (userEvent != null)
            {
                userEvent(this, e);
            }
        }
    }
}
