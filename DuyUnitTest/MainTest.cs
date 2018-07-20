using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveIO_Main;
using UserControlLibrary;
using SpectrumVisualizer;

namespace UnitTest
{
    [TestClass]
    public class MainTest
    {

        [TestMethod]
        public void Duy_TestMethod1()
        {

            AudioEngine a = new AudioEngine();
            TimeSpan time = a.CurrentTime;

            Assert.AreEqual(time, new TimeSpan(0));
        }

        [TestMethod]
        public void Duy_TestMethod2()
        {
            SpectrumVisualizer.SpectrumVisualizer a = new SpectrumVisualizer.SpectrumVisualizer();
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            rect.Height = 1;
            a.animateBar(rect, 2);
            Assert.AreEqual(rect.Height, 1);
        }
    }
}
