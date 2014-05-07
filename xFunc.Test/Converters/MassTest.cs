using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class MassTest
    {

        private MassConverter conv = new MassConverter();

        [TestMethod]
        public void FromKiloToMilli()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Milligrams);

            Assert.AreEqual(12000000, value);
        }

        [TestMethod]
        public void FromMilliToKilo()
        {
            var value = conv.Convert(12000000, MassUnits.Milligrams, MassUnits.Kilograms);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromKiloToG()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Grams);

            Assert.AreEqual(12000, value);
        }

        [TestMethod]
        public void FromGToKilo()
        {
            var value = conv.Convert(12000, MassUnits.Grams, MassUnits.Kilograms);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromKiloToSlug()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Slugs);

            Assert.AreEqual(0.822261186743533, value, 0.00001);
        }

        [TestMethod]
        public void FromSlugToKilo()
        {
            var value = conv.Convert(0.822261186743533, MassUnits.Slugs, MassUnits.Kilograms);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromKiloToLb()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Pounds);

            Assert.AreEqual(26.4554714621853, value, 0.00001);
        }

        [TestMethod]
        public void FromLbToKilo()
        {
            var value = conv.Convert(26.4554714621853, MassUnits.Pounds, MassUnits.Kilograms);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromKiloToTonne()
        {
            var value = conv.Convert(12000, MassUnits.Kilograms, MassUnits.Tonne);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromTonneToKilo()
        {
            var value = conv.Convert(12, MassUnits.Tonne, MassUnits.Kilograms);

            Assert.AreEqual(12000, value, 0.00001);
        }

    }

}
