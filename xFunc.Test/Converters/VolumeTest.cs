using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class VolumeTest
    {

        private VolumeConverter conv = new VolumeConverter();

        [TestMethod]
        public void FromMtoCenti()
        {
            var value = conv.Convert(0.032, VolumeUnits.CubicMeters, VolumeUnits.CubicCentimeters);

            Assert.AreEqual(32000, value, 0.00001);
        }

        [TestMethod]
        public void FromCentiToM()
        {
            var value = conv.Convert(32000, VolumeUnits.CubicCentimeters, VolumeUnits.CubicMeters);

            Assert.AreEqual(0.032, value);
        }
        
        [TestMethod]
        public void FromMtoLitre()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.Litres);

            Assert.AreEqual(1500, value, 0.00001);
        }

        [TestMethod]
        public void FromLitreToM()
        {
            var value = conv.Convert(1500, VolumeUnits.Litres, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value);
        }

        [TestMethod]
        public void FromMtoInch()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicInches);

            Assert.AreEqual(91535.61, value, 0.00001);
        }

        [TestMethod]
        public void FromInchToM()
        {
            var value = conv.Convert(91535.61, VolumeUnits.CubicInches, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value);
        }

        [TestMethod]
        public void FromMtoPtUS()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.PintsUS);

            Assert.AreEqual(3170.06462829778, value, 0.00001);
        }

        [TestMethod]
        public void FromPtUSToM()
        {
            var value = conv.Convert(3170.06462829778, VolumeUnits.PintsUS, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoPtUK()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.PintsUK);

            Assert.AreEqual(2639.63097958905, value, 0.00001);
        }

        [TestMethod]
        public void FromPtUKToM()
        {
            var value = conv.Convert(2639.63097958905, VolumeUnits.PintsUK, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoGalUS()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.GallonsUS);

            Assert.AreEqual(396.258078537223, value, 0.00001);
        }

        [TestMethod]
        public void FromGalUSToM()
        {
            var value = conv.Convert(396.258078537223, VolumeUnits.GallonsUS, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoGalUK()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.GallonsUK);

            Assert.AreEqual(329.953872448632, value, 0.00001);
        }

        [TestMethod]
        public void FromGalUKToM()
        {
            var value = conv.Convert(329.953872448632, VolumeUnits.GallonsUK, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoFoot()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicFeet);

            Assert.AreEqual(52.9720000822329, value, 0.00001);
        }

        [TestMethod]
        public void FromFootToM()
        {
            var value = conv.Convert(52.9720000822329, VolumeUnits.CubicFeet, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

        [TestMethod]
        public void FromMtoYard()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicYards);

            Assert.AreEqual(1.96192592897159, value, 0.00001);
        }

        [TestMethod]
        public void FromYardToM()
        {
            var value = conv.Convert(1.96192592897159, VolumeUnits.CubicYards, VolumeUnits.CubicMeters);

            Assert.AreEqual(1.5, value, 0.00001);
        }

    }

}
