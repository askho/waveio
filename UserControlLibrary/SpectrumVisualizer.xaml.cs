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
using NAudio.Dsp;
using System.Windows.Media.Animation;
using System.Diagnostics;
namespace SpectrumVisualizer
{
    /// <summary>
    /// This is a spectrum visualizer controller. It requires the user to
    /// give it a fall duration, and call the updateBars() method when fft
    /// results are ready. 
    /// 
    /// Created By  : Leon Ho
    /// Student Num : A00879122
    /// Date        : March 10 2015
    /// </summary>
    public partial class SpectrumVisualizer : UserControl
    {
        //This stopwatch is used to time the animations so that we don't 
        //update the bars 300 times a second. 
        private Stopwatch stopWatch;
        /// <summary>
        /// This constructor intializes the component, and starts a stop
        /// watch that makes sure that the animations arn't updated too
        /// frequently. (There is no point in updating the bars 300 times
        /// a second).
        /// </summary>
        public SpectrumVisualizer()
        {
            InitializeComponent();
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        /// <summary>
        /// This is the fall duration of the bars. The larger the value
        /// the longer it takes for the bars to reach a height of 0.
        /// </summary>
        public TimeSpan fallDuration
        {
            get;
            set;
        }
        /// <summary>
        /// We want to start a spawn a new thread and calculate how much 
        /// to move the individual bars. I am using the Task class to make
        /// it a little simpler. As calculating the individual bars it time
        /// depanding, we don't want to lag out the UI thread and cause the
        /// music to stutter. 
        /// </summary>
        /// <param name="fftResults">This is a set of complex numbres that are returned from the
        /// fft calculations.</param>
        /// <returns>This returns a set of "buckets" which will be used for each bar.</returns>
        private static Task<float[]> calculateFFT(Complex[] fftResults)
        {
            return Task.Run(() =>
            {
                float[] buckets = new float[10];
                if (fftResults != null)
                {
                    int size = fftResults.Length / 2;
                    int bucketSize = size / 10;
                    int limit = 1, j = 1, bucketIndex = 0, averageCount = 0;
                    for (int i = 1; i < 511; i++)
                    {
                        ++averageCount;
                        buckets[bucketIndex] += Math.Abs(fftResults[i].X);
                        if (j == limit)
                        {
                            buckets[bucketIndex] /= averageCount;
                            averageCount = 0;
                            limit *= 2;
                            ++bucketIndex;
                        }
                        ++j;

                    }
                }
                return buckets;
            });
        }
        private static double GetYPosLog(Complex c)
        {
            // not entirely sure whether the multiplier should be 10 or 20 in this case.
            // going with 10 from here http://stackoverflow.com/a/10636698/7532
            double intensityDB = 10 * Math.Log10(Math.Sqrt(c.X * c.X + c.Y * c.Y));
            double minDB = -90;
            if (intensityDB < minDB) intensityDB = minDB;
            double percent = intensityDB / minDB;
            // we want 0dB to be at the top (i.e. yPos = 0)
            double yPos = percent * 170;
            return yPos;
        }
        /// <summary>
        /// This method is called by whatever instantiated this control.
        /// It takes in a set of complex numbers and we use it to animate
        /// the bars. 
        /// We use the stopwatch to make sure that we don't update the bars too often
        /// as it both looks weird, and is a waste of system resources. 
        /// </summary>
        /// <param name="fftResults">A set of complex numbers that we will be
        /// using to determine the heights of bars. </param>
        public async void updateBars(Complex[] fftResults)
        {

            float height = (float)170;
            stopWatch.Stop();
            if (stopWatch.ElapsedMilliseconds > 30)
            {
                stopWatch.Reset();
                float[] buckets = await calculateFFT(fftResults);
                //The numbers multiplying the buckets are arbitrary to try
                //to normalize the values. 
                animateBar(bar0, buckets[0] * 3000);
                animateBar(bar1, buckets[1] * 3000);
                animateBar(bar2, buckets[2] * 4000);
                animateBar(bar3, buckets[3] * 8000);
                animateBar(bar4, buckets[4] * 10000);
                animateBar(bar5, buckets[5] * 13000);
                animateBar(bar6, buckets[6] * 9000);
                animateBar(bar7, buckets[7] * 6000);
                animateBar(bar8, buckets[8] * 60000);
                animateBar(bar9, buckets[9] * 2000);

            }
            stopWatch.Start(); 
        }
        /// <summary>
        /// To make the bars a little smoother we animate the falling
        /// down determined by the property fallduration. 
        /// It will also not update the bars if the deviation is too great as
        /// sometimes the fft results are not always accurate and causes some spikes. 
        /// </summary>
        /// <param name="bar">The rectangle that we will be animating.</param>
        /// <param name="height">The height to animate to. </param>
        public void animateBar(Rectangle bar, float height) {
            if (height > bar.Height)
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = height;
                anim.To = 1;
                anim.Duration = fallDuration;
                bar.BeginAnimation(Rectangle.HeightProperty, anim);
            }
        }
    }
}
