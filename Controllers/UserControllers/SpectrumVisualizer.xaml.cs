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
namespace SpectrumVisualizer
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SpectrumVisualizer : UserControl
    {
        public SpectrumVisualizer()
        {
            InitializeComponent();
            bar0.mode3();
            bar1.mode1();
            bar2.mode2();
            bar3.mode3();
            bar4.mode4();
            bar5.mode5();
            bar6.mode6();
            bar7.mode2();
            bar8.mode3();
            bar9.mode5();

        }
        public static Task<float[]> calculateFFT(Complex[] fftResults)
        {
            return Task.Run(() => {
                float[] buckets = new float[13];
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
        public async void updateBars(Complex[] fftResults)
        {
            float[] buckets = await calculateFFT(fftResults);
            bar0.Height = buckets[0] * 2000;
            bar1.Height = buckets[1] * 2000;
            bar2.Height = buckets[2] * 4000;
            bar3.Height = buckets[3] * 8000;
            bar4.Height = buckets[4] * 10000;
            bar5.Height = buckets[5] * 15000;
            bar6.Height = buckets[6] * 9000;
            bar7.Height = buckets[7] * 50000;
            bar8.Height = buckets[8] * 80000;
            bar9.Height = buckets[9] * 2000;
        }
    }
}
