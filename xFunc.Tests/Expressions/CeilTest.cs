// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class CeilTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTestNumber()
        {
            var ceil = new Ceil(new Number(5.55555555));
            var result = (NumberValue)ceil.Execute();
            var expected = new NumberValue(6.0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestAngleNumber()
        {
            var ceil = new Ceil(AngleValue.Degree(5.55555555).AsExpression());
            var result = ceil.Execute();
            var expected = AngleValue.Degree(6);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestPowerNumber()
        {
            var ceil = new Ceil(PowerValue.Watt(5.55555555).AsExpression());
            var result = ceil.Execute();
            var expected = PowerValue.Watt(6);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Ceil(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Ceil(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}