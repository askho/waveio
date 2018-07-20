using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Dsp;
using NAudio.Wave;

namespace WaveIO_Main
{
    /// <summary>
    /// Created By Leon & Jens
    /// Provides the samples to be played back by the output 
    /// Some references used 
    /// http://naudio.codeplex.com/SourceControl/latest#NAudioWpfDemo/AudioPlaybackDemo/SampleAggregator.cs
    /// http://channel9.msdn.com/coding4fun/articles/AutotuneNET
    /// </summary>
    class AggregateSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        //We never actually use this but we it can be used for other visualizers.
        public event EventHandler<MaxSampleEventArgs> MaximumCalculated;
        private float maxValue;
        private float minValue;
        public int NotificationCount { get; set; }
        int count;
        // FFT has been calculated. 
        public event EventHandler<FftEventArgs> FftCalculated;
        public bool PerformFFT { get; set; }
        private readonly Complex[] fftBuffer;
        private readonly FftEventArgs fftArgs;
        private int fftPos;
        private int fftLength;
        private int m;
        private readonly int channels;
        /// <summary>
        /// This constructor checks if the fftLength is a power of two,
        /// and instantiates the buffers.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fftLength"></param>
        public AggregateSampleProvider(ISampleProvider source, int fftLength = 1024)
        {
            channels = source.WaveFormat.Channels;
            if (!IsPowerOfTwo(fftLength))
            {
                throw new ArgumentException("FFT Length must be a power of two");
            }
            this.m = (int)Math.Log(fftLength, 2.0);
            this.fftLength = fftLength;
            this.fftBuffer = new Complex[fftLength];
            this.fftArgs = new FftEventArgs(fftBuffer);
            this.source = source;
        }
        /// <summary>
        /// This property allows people to get the WaveFormat which is used
        /// to get the number of channels that is in the audio
        /// </summary>
        public WaveFormat WaveFormat { get { return source.WaveFormat; } }
        /// <summary>
        /// Used to accumulate the read in samples, and will automatically
        /// do a fourier transform when whenever ready.
        /// </summary>
        /// <param name="value">The sample</param>
        private void Add(float value)
        {
            if (PerformFFT && FftCalculated != null)
            {
                fftBuffer[fftPos].X = (float)(value * FastFourierTransform.HammingWindow(fftPos, fftLength));
                fftBuffer[fftPos].Y = 0;
                fftPos++;
                if (fftPos >= fftBuffer.Length)
                {
                    fftPos = 0;
                    FastFourierTransform.FFT(true, m, fftBuffer);
                    FftCalculated(this, fftArgs);
                }
            }

            maxValue = Math.Max(maxValue, value);
            minValue = Math.Min(minValue, value);
            count++;
            if (count >= NotificationCount && NotificationCount > 0)
            {
                if (MaximumCalculated != null)
                {
                    MaximumCalculated(this, new MaxSampleEventArgs(minValue, maxValue));
                }
                Reset();
            }
        }
        /// <summary>
        /// Called by a object in the NAudio library. 
        /// This is required to gather the number of samples read, and
        /// to build a fft result. 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Read(float[] buffer, int offset, int count)
        {
            var samplesRead = source.Read(buffer, offset, count);
            for (int n = 0; n < samplesRead; n += channels)
            {
                Add(buffer[n + offset]);
            }
            return samplesRead;
        }
        /// <summary>
        /// Verify that if something is a power of two. 
        /// </summary>
        /// <param name="x">The number we are calculating. </param>
        /// <returns>True or false if the item is a power</returns>
        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        /// <summary>
        /// Reset the values for the next bunch of samples. 
        /// </summary>
        public void Reset()
        {
            count = 0;
            maxValue = minValue = 0;
        }
    }
    /// <summary>
    /// Event args that outputs the max volume of a given set of samples. 
    /// Not used in our application.
    /// </summary>
    public class MaxSampleEventArgs : EventArgs
    {
        public MaxSampleEventArgs(float minValue, float maxValue)
        {
            this.MaxSample = maxValue;
            this.MinSample = minValue;
        }
        public float MaxSample { get; private set; }
        public float MinSample { get; private set; }
    }
    /// <summary>
    /// Event args that outputs a set of complex numbers that hold 
    /// the FFT results. 
    /// </summary>
    public class FftEventArgs : EventArgs
    {
        public FftEventArgs(Complex[] result)
        {
            this.Result = result;
        }
        public Complex[] Result { get; private set; }
    }
}
