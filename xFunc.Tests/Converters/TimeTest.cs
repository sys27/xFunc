// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.UnitConverters;
using Xunit;

namespace xFunc.Tests.Converters
{
    public class TimeTest
    {
        private readonly TimeConverter conv = new TimeConverter();

        [Fact]
        public void ConvertToSame()
        {
            var value = conv.Convert(43, TimeUnits.Seconds, TimeUnits.Seconds);

            Assert.Equal(43, value);
        }

        [Fact]
        public void FromSToMicro()
        {
            var value = conv.Convert(43, TimeUnits.Seconds, TimeUnits.Microseconds);

            Assert.Equal(43000000, value);
        }

        [Fact]
        public void FromMicroToS()
        {
            var value = conv.Convert(43000000, TimeUnits.Microseconds, TimeUnits.Seconds);

            Assert.Equal(43, value);
        }

        [Fact]
        public void FromSToMilli()
        {
            var value = conv.Convert(43, TimeUnits.Seconds, TimeUnits.Milliseconds);

            Assert.Equal(43000, value);
        }

        [Fact]
        public void FromMilliToS()
        {
            var value = conv.Convert(43000, TimeUnits.Milliseconds, TimeUnits.Seconds);

            Assert.Equal(43, value);
        }

        [Fact]
        public void FromSToM()
        {
            var value = conv.Convert(42, TimeUnits.Seconds, TimeUnits.Minutes);

            Assert.Equal(0.7, value);
        }

        [Fact]
        public void FromMToS()
        {
            var value = conv.Convert(0.7, TimeUnits.Minutes, TimeUnits.Seconds);

            Assert.Equal(42, value);
        }

        [Fact]
        public void FromSToH()
        {
            var value = conv.Convert(72, TimeUnits.Seconds, TimeUnits.Hours);

            Assert.Equal(0.02, value);
        }

        [Fact]
        public void FromHToS()
        {
            var value = conv.Convert(0.02, TimeUnits.Hours, TimeUnits.Seconds);

            Assert.Equal(72, value);
        }

        [Fact]
        public void FromSToD()
        {
            var value = conv.Convert(648000, TimeUnits.Seconds, TimeUnits.Days);

            Assert.Equal(7.5, value);
        }

        [Fact]
        public void FromDToS()
        {
            var value = conv.Convert(7.5, TimeUnits.Days, TimeUnits.Seconds);

            Assert.Equal(648000, value);
        }

        [Fact]
        public void FromSToW()
        {
            var value = conv.Convert(1412812.8, TimeUnits.Seconds, TimeUnits.Weeks);

            Assert.Equal(2.336, value, 4);
        }

        [Fact]
        public void FromWToS()
        {
            var value = conv.Convert(2.336, TimeUnits.Weeks, TimeUnits.Seconds);

            Assert.Equal(1412812.8, value, 4);
        }
    }
}