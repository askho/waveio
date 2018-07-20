using NAudio.Dsp;
using NAudio.Wave;
using System;
/// <summary>
/// Created By Leon
/// Provides the samples to be played back by the output 
/// Some references used 
/// http://naudio.codeplex.com/SourceControl/latest#NAudioWpfDemo/AudioPlaybackDemo/SampleAggregator.cs
/// http://channel9.msdn.com/coding4fun/articles/AutotuneNET
/// </summary>
public class SampleWaveProvider : ISampleProvider
{
        private readonly ISampleProvider source;
        public event EventHandler<MaxSampleEventArgs> MaximumCalculated;
        private float maxValue;
        private float minValue;
        public int NotificationCount { get; set; }
        int count;

        // FFT
        public event EventHandler<FftEventArgs> FftCalculated;
        public bool PerformFFT { get; set; }
        private readonly Complex[] fftBuffer;
        private readonly FftEventArgs fftArgs;
        private int fftPos;
        private int fftLength;
        private int m;
        public System.Windows.Forms.RichTextBox textBox
        {
            get;
            set;
        }
        private readonly int channels;
        public SampleWaveProvider(ISampleProvider source, int fftLength = 1024)
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
        public WaveFormat WaveFormat { get { return source.WaveFormat; } }
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
                    // 1024 = 2^10
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
        public int Read(float[] buffer, int offset, int count)
        {
            var samplesRead = source.Read(buffer, offset, count);
            for (int n = 0; n < samplesRead; n += channels)
            {
                Add(buffer[n + offset]);
            }
            return samplesRead;
        }
        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }


        public void Reset()
        {
            count = 0;
            maxValue = minValue = 0;
        }
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

        public class FftEventArgs : EventArgs
        {
            public FftEventArgs(Complex[] result)
            {
                this.Result = result;
            }
            public Complex[] Result { get; private set; }
        }
}
