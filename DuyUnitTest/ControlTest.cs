using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserControlLibrary;

namespace UnitTest
{
    [TestClass]
    public class ControlTest
    {
        /// <summary>
        /// Check default status of playPause controller.
        /// </summary>
        [TestMethod, TestCategory("PlayPauseController")]
        public void DefaultStatus()
        {
            PlayPause.PlayPauseControl play = new PlayPause.PlayPauseControl();
            Assert.AreEqual(play.IsEnabled, false);
        }
        [TestMethod, TestCategory("PlayPauseController")]
        public void PlayClicked()
        {
            PlayPause.PlayPauseControl play = new PlayPause.PlayPauseControl();
            play.IsEnabled = true;
            play.onClicked(null, null);
            Assert.AreEqual(play.status, PlayPause.PlayPauseControl.PlayBackStatus.Play);
        }
        [TestMethod, TestCategory("PlayPauseController")]
        public void PlayDoubleClicked()
        {
            PlayPause.PlayPauseControl play = new PlayPause.PlayPauseControl();
            play.IsEnabled = true;
            play.onClicked(null, null);
            play.onClicked(null, null);
            Assert.AreEqual(play.status, PlayPause.PlayPauseControl.PlayBackStatus.Pause);
        }
        [TestMethod, TestCategory("PlayPauseController")]
        public void PlayEventTest()
        {
            PlayPause.PlayPauseControl play = new PlayPause.PlayPauseControl();
            play.IsEnabled = true;
            PlayPause.PlayPauseControl.PlayBackStatus input;
            play.StatusChanged += (a, e) =>
            {
                input = e.status;
            };
            play.onClicked(null, null);
            play.onClicked(null, null);
            Assert.AreEqual(play.status, PlayPause.PlayPauseControl.PlayBackStatus.Pause);
        }

        [TestMethod, TestCategory("Track")]
        public void TrackForwardTest() {
            TrackForwardButton btn = new TrackForwardButton();

            btn.IsEnabled = true;

            Assert.AreEqual(btn.IsEnabled, true);
        }

        [TestMethod, TestCategory("Track")]
        public void TrackBackwardTest() {
            TrackBackwardButton btn = new TrackBackwardButton();

            btn.IsEnabled = true;

            Assert.AreEqual(btn.IsEnabled, true);
        }
    }
}
