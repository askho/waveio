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

namespace UserControlLibrary {
    /// <summary>
    /// Interaction logic for JensMenu.xaml
    /// </summary>
    public partial class Menu : UserControl {

        /// <summary>
        /// Constructor sets up Click event handlers for individual buttons.
        /// </summary>
        public Menu() {
            InitializeComponent();
            openBtn.Click += new RoutedEventHandler(onOpen);
            playlistBtn.Click += new RoutedEventHandler(onPlaylist);
            exitBtn.Click += new RoutedEventHandler(onExit);
        }

        /// <summary>
        /// Handles the Exit button.
        /// </summary>
        public event RoutedEventHandler Exit;

        /// <summary>
        /// Handles the Open button.
        /// </summary>
        public event RoutedEventHandler OpenFile;

        public event RoutedEventHandler OpenPlaylist;

        /// <summary>
        /// Calls Exit event handler.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void onExit(object sender, RoutedEventArgs e) {

            if (Exit != null) {
                Exit(sender, e);
            }
        }

        /// <summary>
        /// Calls open event handler.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void onOpen(object sender, RoutedEventArgs e) 
        {
            if (OpenFile != null) {
                OpenFile(sender, e);
            }
        }

        private void onPlaylist(object sender, RoutedEventArgs e)
        {
            if (OpenPlaylist != null)
            {
                OpenPlaylist(sender, e);
            }
        }

        /// <summary>
        /// Sets the background for the buttons.
        /// </summary>
        public SolidColorBrush BgColor {
            get {
                return (SolidColorBrush)Resources["comboBoxBg"];
            }
            set {
                Resources["comboBoxBg"] = value;
            }
        }
    }
}
