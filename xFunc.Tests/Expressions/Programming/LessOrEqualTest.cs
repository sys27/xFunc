// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class LessOrEqualTest
    {
        [Fact]
        public void CalculateLessTrueTest1()
        {
            var parameters = new ParameterCollection { new Parameter("x", 0) };
            var lessOrEqual = new LessOrEqual(Variable.X, new Number(10));

            Assert.True((bool)lessOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateLessTrueTest2()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var lessOrEqual = new LessOrEqual(Variable.X, new Number(10));

            Assert.True((bool)lessOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateLessFalseTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", 666) };
            var lessOrEqual = new LessOrEqual(Variable.X, new Number(10));

            Assert.False((bool)lessOrEqual.Execute(parameters));
        }

        [Fact]
        public void LessOrEqualAngleTest()
        {
            var exp = new LessOrEqual(
                AngleValue.Degree(10).AsExpression(),
                AngleValue.Degree(12).AsExpression()
            );
            var result = (bool)exp.Execute();

            Assert.True(result);
        }

        [Fact]
        public void LessOrEqualPowerTest()
        {
            var exp = new LessOrEqual(
                PowerValue.Watt(10).AsExpression(),
                PowerValue.Watt(12).AsExpression()
            );
            var result = (bool)exp.Execute();

            Assert.True(result);
        }

        [Fact]
        public void CalculateInvalidTypeTest()
        {
            var lessOrEqual = new LessOrEqual(Bool.True, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => lessOrEqual.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new LessOrEqual(Number.Two, new Number(3));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}