using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WaveIO_Main
{
    /// <summary>
    /// This is the audio engine that is responsible for the music playback.
    /// Created By Leon Ho, Jens Christiansen
    /// Date: March 4 2015
    /// Resources used: 
    /// http://naudio.codeplex.com/SourceControl/latest#NAudioWpfDemo/AudioPlaybackDemo/AudioPlayback.cs
    /// https://msdn.microsoft.com/en-us/library/edzehd2t%28v=vs.110%29.aspx
    /// 
    /// </summary>
    public class AudioEngine : IDisposable
    {
        // Event handler for when we finish calculating a fft
        public event EventHandler<FftEventArgs> FftCalculated;
        public event EventHandler<StoppedEventArgs> SongCompleted;
        //Object that will be playing our audio.
        private WaveOut output;
        #region properties
        public float Volume
        {
            get
            {
                if(fileStream != null)
                    return fileStream.Volume;
                return 0;
            }
            set
            {
                if(fileStream != null)
                    fileStream.Volume = value;
            }
        }
        public TimeSpan CurrentTime
        {
            get
            {
                if(fileStream != null)
                    return fileStream.CurrentTime;
                return new TimeSpan(0);
            }
            set
            {
                if (fileStream != null && value < fileStream.TotalTime)
                {
                    fileStream.CurrentTime = value;
                }
            }
        }
        public long Length
        {
            get
            {
                if (fileStream != null)
                    return fileStream.Length;
                return 0;
            }
        }
        public long Position
        {
            get {
                if (fileStream != null)
                    return fileStream.Position;
                return 0;
            }
        }
        public TimeSpan TotalTime
        {
            get
            {
                if (fileStream != null)
                    return fileStream.TotalTime;
                return new TimeSpan(0);
            }
        }
        public PlaybackState PlaybackState
        {
            get
            {
                return output.PlaybackState;
            }
        }
        // File to be played
        public string FileName
        {
            get;
            set;
        }
        /*
        public Boolean repeatSong
        {
            get;
            set;
        }
        */
        #endregion
        /// <summary>
        /// When an FFT is calculated we cascade the event to the calling class 
        /// through this even handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">FFT event args - e.Result is a Complex array for the fft result</param>
        private void asp_FftCalculated(object sender, FftEventArgs e)
        {
            EventHandler<FftEventArgs> handler = FftCalculated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //Stream we will be playing from
        private AudioFileReader fileStream;
        /// <summary>
        /// Setup our output device. We will be using a WaveOut as
        /// it seems to have a better compatibility than DirectOut.
        /// </summary>
        public AudioEngine()
        {
            output = new WaveOut();
            //repeatSong = false;
        }
        /// <summary>
        /// Initialize the NAudio library with the new information.
        /// </summary>
        /// <returns>True or false depnding if we managed to load the audio. </returns>
        public bool Load()
        {
            try
            {
                Stop();
                output = new WaveOut();
                output.PlaybackStopped += output_PlaybackStopped;
                output.DesiredLatency = 90;
                fileStream = new AudioFileReader(FileName);
                AggregateSampleProvider asp = new AggregateSampleProvider(fileStream);
                asp.PerformFFT = true;
                asp.FftCalculated += asp_FftCalculated;
                output.Init(asp);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Fire off the song completed event when the song has completed naturally.
        /// </summary>
        /// <param name="sender">Object that is calling</param>
        /// <param name="e">Stopped event args </param>
        void output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //if (repeatSong == false)
            //{
                EventHandler<StoppedEventArgs> handler = SongCompleted;
                if (handler != null)
                {
                    handler(this, e);
                }
            //}
            //else
            //{
                //fileStream.CurrentTime = new TimeSpan();
                //output.Play();
            //}
            
        }
        /// <summary>
        /// Play the audio.
        /// </summary>
        public void Play()
        {
            output.Play();
        }
        /// <summary>
        /// Checks if the audio is playing and fires off the FFTCalculated
        /// event with a null response so that the visualizer knows we 
        /// have paused. 
        /// </summary>
        public void Pause()
        {
            if (output.PlaybackState == PlaybackState.Playing)
            {
                output.Pause();
                EventHandler<FftEventArgs> handler = FftCalculated;
                if (handler != null)
                {
                    handler(this, null);
                }
            }
        }
        /// <summary>
        /// Checks if the audio is playing and fires off the FFTCalculated
        /// event with a null response so that the visualizer knows we 
        /// have stoped audio playback. 
        /// </summary>
        public void Stop()
        {
            if (output.PlaybackState == PlaybackState.Playing)
            {
                output.Stop();
                EventHandler<FftEventArgs> handler = FftCalculated;
                if (handler != null)
                {
                    handler(this, null);
                }
            }
                
        }
        /// <summary>
        /// Close all the outputs. 
        /// </summary>
        public void CloseFile()
        {
            if(output.PlaybackState == PlaybackState.Playing)
                output.Stop();
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
        }
        /// <summary>
        /// Scrub the audio to the given time.
        /// </summary>
        /// <param name="time"></param>
        public void Scrub(TimeSpan time)
        {
            fileStream.CurrentTime = time;
        }
        /// <summary>
        /// Close the files, and kill the output. 
        /// </summary>
        public void Dispose()
        {
            CloseFile();
            if (output != null)
            {
                output.Dispose();
            }
        }
    }
}
