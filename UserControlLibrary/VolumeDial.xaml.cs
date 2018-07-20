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
    /// Interaction logic for VolumeDial.xaml
    /// </summary>
    public partial class VolumeDial : UserControl {
        public VolumeDial() {
            InitializeComponent();
            rotateTransform.Changed += new EventHandler(onValueChanged);
        }

        


        public double VolumeAngle {
            get {
                return rotateTransform.Angle;
            }
            set {
                rotateTransform.Angle = value;
            }
        }

        public double Value {
            get {
                if ((rotateTransform.Angle - 45) / 270 < 0) {
                    return 0;
                } else if ((rotateTransform.Angle - 45) / 270 > 1) {
                    return 1;
                } else {
                    return (rotateTransform.Angle - 45) / 270;
                }
            }
            set {
                rotateTransform.Angle = (value - 45) * 270;
                onValueChanged(this, new EventArgs());
            }
        }

        public event EventHandler ValueChanged;

        private void onValueChanged(object sender, EventArgs e) {

            if (ValueChanged != null) {
                ValueChanged(sender, e);
            }
        }

        private bool mouseIsDown = false;

        int prevX;
        private void onMouseMove(object sender, MouseEventArgs e) {
            if (mouseIsDown) {
                if (prevX < System.Windows.Forms.Control.MousePosition.X) {
                    if (rotateTransform.Angle <= 315) {
                        rotateTransform.Angle += 10;
                    }
                } else if (prevX > System.Windows.Forms.Control.MousePosition.X) {
                    if (rotateTransform.Angle >= 45) {
                        rotateTransform.Angle -= 10;
                    }
                }
                prevX = System.Windows.Forms.Control.MousePosition.X;
            }
        }

        private void volumeDial_MouseDown(object sender, MouseButtonEventArgs e) {
            prevX = System.Windows.Forms.Control.MousePosition.X;
            mouseIsDown = true;
        }

        private void onMouseUp(object sender, MouseButtonEventArgs e) {
            mouseIsDown = false;
        }

       
    }
}
