using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.UnitConverters;

namespace xFunc.Test.Converters
{

    [TestClass]
    public class PowerTest
    {

        private PowerConverter conv = new PowerConverter();

        [TestMethod]
        public void FromWtoK()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Kilowatts);

            Assert.AreEqual(14, value);
        }

        [TestMethod]
        public void FromKtoW()
        {
            var value = conv.Convert(14, PowerUnits.Kilowatts, PowerUnits.Watts);

            Assert.AreEqual(14000, value);
        }

        [TestMethod]
        public void FromWtoHP()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Horsepower);

            Assert.AreEqual(18.7743092543304, value, 0.00001);
        }

        [TestMethod]
        public void FromHPtoW()
        {
            var value = conv.Convert(18.7743092543304, PowerUnits.Horsepower, PowerUnits.Watts);

            Assert.AreEqual(14000, value, 0.00001);
        }

    }

}
