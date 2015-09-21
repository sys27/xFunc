using System;
using xFunc.UnitConverters;
using Xunit;

namespace xFunc.Test.Converters
{
    
    public class PowerTest
    {

        private PowerConverter conv = new PowerConverter();

        [Fact]
        public void FromWtoK()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Kilowatts);

            Assert.Equal(14, value);
        }

        [Fact]
        public void FromKtoW()
        {
            var value = conv.Convert(14, PowerUnits.Kilowatts, PowerUnits.Watts);

            Assert.Equal(14000, value);
        }

        [Fact]
        public void FromWtoHP()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Horsepower);

            Assert.Equal(18.7743092543304, value, 4);
        }

        [Fact]
        public void FromHPtoW()
        {
            var value = conv.Convert(18.7743092543304, PowerUnits.Horsepower, PowerUnits.Watts);

            Assert.Equal(14000, value, 4);
        }

    }

}
