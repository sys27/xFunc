using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class TemperatureTest
    {

        private TemperatureConverter conv = new TemperatureConverter();

        [TestMethod]
        public void FromCToF()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Fahrenheit);

            Assert.AreEqual(53.6, value);
        }

        [TestMethod]
        public void FromFToC()
        {
            var value = conv.Convert(53.6, TemperatureUnits.Fahrenheit, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromCToK()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Kelvin);

            Assert.AreEqual(285.15, value);
        }

        [TestMethod]
        public void FromKToC()
        {
            var value = conv.Convert(285.15, TemperatureUnits.Kelvin, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromCToR()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Rankine);

            Assert.AreEqual(513.27, value);
        }

        [TestMethod]
        public void FromRToC()
        {
            var value = conv.Convert(513.27, TemperatureUnits.Rankine, TemperatureUnits.Celsius);

            Assert.AreEqual(12.0, value, 0.00001);
        }

        [TestMethod]
        public void FromCToDe()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Delisle);

            Assert.AreEqual(132, value);
        }

        [TestMethod]
        public void FromDeToC()
        {
            var value = conv.Convert(132, TemperatureUnits.Delisle, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromCToN()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Newton);

            Assert.AreEqual(3.96, value);
        }

        [TestMethod]
        public void FromNToC()
        {
            var value = conv.Convert(3.96, TemperatureUnits.Newton, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromCToRe()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Réaumur);

            Assert.AreEqual(9.6, value, 0.00001);
        }

        [TestMethod]
        public void FromReToC()
        {
            var value = conv.Convert(9.6, TemperatureUnits.Réaumur, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromCToRo()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Rømer);

            Assert.AreEqual(13.8, value);
        }

        [TestMethod]
        public void FromRoToC()
        {
            var value = conv.Convert(13.8, TemperatureUnits.Rømer, TemperatureUnits.Celsius);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromFToDe()
        {
            var value = conv.Convert(-146.8, TemperatureUnits.Fahrenheit, TemperatureUnits.Delisle);

            Assert.AreEqual(299, value);
        }

        [TestMethod]
        public void FromDeToF()
        {
            var value = conv.Convert(299, TemperatureUnits.Delisle, TemperatureUnits.Fahrenheit);

            Assert.AreEqual(-146.8, value, 0.00001);
        }

    }

}
