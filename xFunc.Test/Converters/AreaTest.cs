using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class AreaTest
    {

        private AreaConverter conv = new AreaConverter();

        [TestMethod]
        public void FromMToMilli()
        {
            var value = conv.Convert(1, AreaUnits.SquareMetres, AreaUnits.SquareMillimeters);

            Assert.AreEqual(1000000, value);
        }

        [TestMethod]
        public void FromMilliToM()
        {
            var value = conv.Convert(1000000, AreaUnits.SquareMillimeters, AreaUnits.SquareMetres);

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void FromMToCenti()
        {
            var value = conv.Convert(1, AreaUnits.SquareMetres, AreaUnits.SquareCentimeters);

            Assert.AreEqual(10000, value);
        }

        [TestMethod]
        public void FromCentiToM()
        {
            var value = conv.Convert(10000, AreaUnits.SquareCentimeters, AreaUnits.SquareMetres);

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void FromMToKilo()
        {
            var value = conv.Convert(140000, AreaUnits.SquareMetres, AreaUnits.SquareKilometers);

            Assert.AreEqual(0.14, value);
        }

        [TestMethod]
        public void FromKiloToM()
        {
            var value = conv.Convert(0.14, AreaUnits.SquareKilometers, AreaUnits.SquareMetres);

            Assert.AreEqual(140000, value);
        }

        [TestMethod]
        public void FromMToHa()
        {
            var value = conv.Convert(10000, AreaUnits.SquareMetres, AreaUnits.Hectares);

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void FromHaToM()
        {
            var value = conv.Convert(1, AreaUnits.Hectares, AreaUnits.SquareMetres);

            Assert.AreEqual(10000, value);
        }

        [TestMethod]
        public void FromMToInch()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareInches);

            Assert.AreEqual(23250.0465, value);
        }

        [TestMethod]
        public void FromInchToM()
        {
            var value = conv.Convert(23250.0465, AreaUnits.SquareInches, AreaUnits.SquareMetres);

            Assert.AreEqual(15, value);
        }

        [TestMethod]
        public void FromMToFoot()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareFeet);

            Assert.AreEqual(161.458665, value);
        }

        [TestMethod]
        public void FromFootToM()
        {
            var value = conv.Convert(161.458665, AreaUnits.SquareFeet, AreaUnits.SquareMetres);

            Assert.AreEqual(15, value);
        }

        [TestMethod]
        public void FromMToYard()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareYards);

            Assert.AreEqual(17.93985, value);
        }

        [TestMethod]
        public void FromYardToM()
        {
            var value = conv.Convert(17.93985, AreaUnits.SquareYards, AreaUnits.SquareMetres);

            Assert.AreEqual(15, value);
        }

        [TestMethod]
        public void FromMToAcre()
        {
            var value = conv.Convert(15000, AreaUnits.SquareMetres, AreaUnits.Acres);

            Assert.AreEqual(3.706580715, value);
        }

        [TestMethod]
        public void FromAcreToM()
        {
            var value = conv.Convert(3.706580715, AreaUnits.Acres, AreaUnits.SquareMetres);

            Assert.AreEqual(15000, value);
        }

        [TestMethod]
        public void FromMToMi()
        {
            var value = conv.Convert(15000, AreaUnits.SquareMetres, AreaUnits.SquareMiles);

            Assert.AreEqual(38849821655.04, value);
        }

        [TestMethod]
        public void FromMiToM()
        {
            var value = conv.Convert(38849821655.04, AreaUnits.SquareMiles, AreaUnits.SquareMetres);

            Assert.AreEqual(15000, value);
        }

    }

}
