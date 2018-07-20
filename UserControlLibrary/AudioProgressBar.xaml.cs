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
    /// CREATED BY DUY LE
    /// Interaction logic for AudioProgressBar.xaml
    /// The progress bar. Displays the current progress of the song currently playing.
    /// The progress bar has the ability scrub.
    /// </summary>
    public partial class AudioProgressBar : UserControl
    {
        public AudioProgressBar()
        {
            InitializeComponent();
            
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

        /// <summary>
        /// The property for the color of the progress bar's thumb
        /// </summary>
        public SolidColorBrush ThumbColor
        {
            get { return (SolidColorBrush)Resources["thumbColor"]; }
            set { Resources["thumbColor"] = value; }
        }


        public String ThumbText
        {
            get { return (String)Resources["timeLabel"]; }
            set { Resources["timeLabel"] = value; }
        }

        /// <summary>
        /// The property for the maximum value of the progress bar
        /// </summary>
        public double Max
        {
            get { return progressBar.Maximum; }
            set { progressBar.Maximum = value; }
        }

        /// <summary>
        /// The property for the minimum value of the progress bar
        /// </summary>
        public double Min
        {
            get { return progressBar.Minimum; }
            set { progressBar.Minimum = value; }
        }

        /// <summary>
        /// The property for the current value of the progress bar
        /// </summary>
        public double Val
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }

        /// <summary>
        /// User Control event. Triggered on thumb drag.
        /// </summary>
        public event EventHandler userEvent;
        private void progressBar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (userEvent != null)
            {
                userEvent(this, new EventArgs());
            }
        }
    }
}
