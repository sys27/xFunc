// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class FloorTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var floor = new Floor(new Number(5.55555555));
            var result = floor.Execute();
            var expected = new NumberValue(5.0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteAngleTest()
        {
            var floor = new Floor(AngleValue.Degree(5.55555555).AsExpression());
            var result = floor.Execute();
            var expected = AngleValue.Degree(5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecutePowerTest()
        {
            var floor = new Floor(PowerValue.Watt(5.55555555).AsExpression());
            var result = floor.Execute();
            var expected = PowerValue.Watt(5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Floor(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Floor(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}