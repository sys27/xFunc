using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class TimeTest
    {

        private TimeConverter conv = new TimeConverter();

        [TestMethod]
        public void FromSToMicro()
        {
            var value = conv.Convert(43, TimeUnits.Seconds, TimeUnits.Microseconds);

            Assert.AreEqual(43000000, value);
        }

        [TestMethod]
        public void FromMicroToS()
        {
            var value = conv.Convert(43000000, TimeUnits.Microseconds, TimeUnits.Seconds);

            Assert.AreEqual(43, value);
        }

        [TestMethod]
        public void FromSToMilli()
        {
            var value = conv.Convert(43, TimeUnits.Seconds, TimeUnits.Milliseconds);

            Assert.AreEqual(43000, value);
        }

        [TestMethod]
        public void FromMilliToS()
        {
            var value = conv.Convert(43000, TimeUnits.Milliseconds, TimeUnits.Seconds);

            Assert.AreEqual(43, value);
        }

        [TestMethod]
        public void FromSToM()
        {
            var value = conv.Convert(42, TimeUnits.Seconds, TimeUnits.Minutes);

            Assert.AreEqual(0.7, value);
        }

        [TestMethod]
        public void FromMToS()
        {
            var value = conv.Convert(0.7, TimeUnits.Minutes, TimeUnits.Seconds);

            Assert.AreEqual(42, value);
        }

        [TestMethod]
        public void FromSToH()
        {
            var value = conv.Convert(72, TimeUnits.Seconds, TimeUnits.Hours);

            Assert.AreEqual(0.02, value);
        }

        [TestMethod]
        public void FromHToS()
        {
            var value = conv.Convert(0.02, TimeUnits.Hours, TimeUnits.Seconds);

            Assert.AreEqual(72, value);
        }

        [TestMethod]
        public void FromSToD()
        {
            var value = conv.Convert(648000, TimeUnits.Seconds, TimeUnits.Days);

            Assert.AreEqual(7.5, value);
        }

        [TestMethod]
        public void FromDToS()
        {
            var value = conv.Convert(7.5, TimeUnits.Days, TimeUnits.Seconds);

            Assert.AreEqual(648000, value);
        }

        [TestMethod]
        public void FromSToW()
        {
            var value = conv.Convert(1412812.8, TimeUnits.Seconds, TimeUnits.Weeks);

            Assert.AreEqual(2.336, value, 0.00001);
        }

        [TestMethod]
        public void FromWToS()
        {
            var value = conv.Convert(2.336, TimeUnits.Weeks, TimeUnits.Seconds);

            Assert.AreEqual(1412812.8, value, 0.00001);
        }
        
    }

}
