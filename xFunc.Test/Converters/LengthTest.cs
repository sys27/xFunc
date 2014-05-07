using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class LengthTest
    {

        private LengthConverter conv = new LengthConverter();

        [TestMethod]
        public void FromMtoNano()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Nanometres);

            Assert.AreEqual(12000000000, value);
        }

        [TestMethod]
        public void FromNanotoM()
        {
            var value = conv.Convert(12000000000, LengthUnits.Nanometres, LengthUnits.Metres);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromMtoMicro()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Micrometers);

            Assert.AreEqual(12000000, value);
        }

        [TestMethod]
        public void FromMicroToM()
        {
            var value = conv.Convert(12000000, LengthUnits.Micrometers, LengthUnits.Metres);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromMtoMilli()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Millimeters);

            Assert.AreEqual(12000, value);
        }

        [TestMethod]
        public void FromMilliToM()
        {
            var value = conv.Convert(12000, LengthUnits.Millimeters, LengthUnits.Metres);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromMtoCenti()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Centimeters);

            Assert.AreEqual(1200, value);
        }

        [TestMethod]
        public void FromCentiToM()
        {
            var value = conv.Convert(1200, LengthUnits.Centimeters, LengthUnits.Metres);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromMtoKilo()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Kilometers);

            Assert.AreEqual(12, value);
        }

        [TestMethod]
        public void FromKiloToM()
        {
            var value = conv.Convert(12, LengthUnits.Kilometers, LengthUnits.Metres);

            Assert.AreEqual(12000, value);
        }

        [TestMethod]
        public void FromMtoInch()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Inches);

            Assert.AreEqual(472.44094488189, value, 0.00001);
        }

        [TestMethod]
        public void FromInchToM()
        {
            var value = conv.Convert(472.44094488189, LengthUnits.Inches, LengthUnits.Metres);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoFoot()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Feet);

            Assert.AreEqual(39.3700787401575, value, 0.00001);
        }

        [TestMethod]
        public void FromFootToM()
        {
            var value = conv.Convert(39.3700787401575, LengthUnits.Feet, LengthUnits.Metres);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoYard()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Yards);

            Assert.AreEqual(13.1233595800525, value, 0.00001);
        }

        [TestMethod]
        public void FromYardToM()
        {
            var value = conv.Convert(13.1233595800525, LengthUnits.Yards, LengthUnits.Metres);

            Assert.AreEqual(12, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoMile()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Miles);

            Assert.AreEqual(7.45645430684801, value, 0.00001);
        }

        [TestMethod]
        public void FromMileToM()
        {
            var value = conv.Convert(7.45645430684801, LengthUnits.Miles, LengthUnits.Metres);

            Assert.AreEqual(12000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoNauticalMile()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.NauticalMiles);

            Assert.AreEqual(6.47948164146868, value, 0.00001);
        }

        [TestMethod]
        public void FromNauticalMileToM()
        {
            var value = conv.Convert(6.47948164146868, LengthUnits.NauticalMiles, LengthUnits.Metres);

            Assert.AreEqual(12000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoFathom()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Fathoms);

            Assert.AreEqual(6561.67979002625, value, 0.00001);
        }

        [TestMethod]
        public void FromFathomToM()
        {
            var value = conv.Convert(6561.67979002625, LengthUnits.Fathoms, LengthUnits.Metres);

            Assert.AreEqual(12000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoChain()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Chains);

            Assert.AreEqual(596.516344547841, value, 0.00001);
        }

        [TestMethod]
        public void FromChainToM()
        {
            var value = conv.Convert(596.516344547841, LengthUnits.Chains, LengthUnits.Metres);

            Assert.AreEqual(12000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoRod()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Rods);

            Assert.AreEqual(2386.065384, value, 0.00001);
        }

        [TestMethod]
        public void FromRodToM()
        {
            var value = conv.Convert(2386.065384, LengthUnits.Rods, LengthUnits.Metres);

            Assert.AreEqual(12000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoAu()
        {
            var value = conv.Convert(224396806050, LengthUnits.Metres, LengthUnits.AstronomicalUnits);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromAuToM()
        {
            var value = conv.Convert(1.5, LengthUnits.AstronomicalUnits, LengthUnits.Metres);

            Assert.AreEqual(224396806050, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoLY()
        {
            var value = conv.Convert(14190792600000000, LengthUnits.Metres, LengthUnits.LightYears);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromLYToM()
        {
            var value = conv.Convert(1.5, LengthUnits.LightYears, LengthUnits.Metres);

            Assert.AreEqual(14190792600000000, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoP()
        {
            var value = conv.Convert(46285163700000000, LengthUnits.Metres, LengthUnits.Parsecs);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromPToM()
        {
            var value = conv.Convert(1.5, LengthUnits.Parsecs, LengthUnits.Metres);

            Assert.AreEqual(46285163700000000, value, 0.00001);
        }

    }

}
