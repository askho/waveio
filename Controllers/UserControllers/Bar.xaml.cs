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

namespace Bar
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Bar : UserControl
    {
        public Bar()
        {
            InitializeComponent();
            
        }
        public void mode1()
        {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
        public void mode2()
        {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF8AB3D6");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
        public void mode3()
        {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF7CB3E2");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
        public void mode4()
        {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF296CA6");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
        public void mode5() {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF88AFD1");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
        public void mode6()
        {
            LinearGradientBrush mode1 = new LinearGradientBrush();
            mode1.StartPoint = new Point(0.5, 1);
            mode1.EndPoint = new Point(0.5, 0);
            GradientStop stop1 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF296CA6");
            stop1.Offset = 0.961;
            mode1.GradientStops.Add(stop1);
            GradientStop stop2 = new GradientStop();
            stop1.Color = Colors.Black;
            mode1.GradientStops.Add(stop2);
            GradientStop stop3 = new GradientStop();
            stop1.Color = (Color)ColorConverter.ConvertFromString("#FF004E91");
            stop1.Offset = 0.27;
            mode1.GradientStops.Add(stop3);
            GradientStop stop4 = new GradientStop();
            stop1.Color = Colors.Black;
            stop1.Offset = 0;
            mode1.GradientStops.Add(stop4);
            bar.Fill = mode1;
        }
    }
}
