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

    }

}
