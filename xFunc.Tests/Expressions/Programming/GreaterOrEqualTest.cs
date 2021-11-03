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
    public class GreaterOrEqualTest
    {
        [Fact]
        public void CalculateGreaterTrueTest1()
        {
            var parameters = new ParameterCollection { new Parameter("x", 463) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.True((bool)greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateGreaterTrueTest2()
        {
            var parameters = new ParameterCollection { new Parameter("x", 10) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.True((bool)greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection { new Parameter("x", 0) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.False((bool)greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void GreaterOrEqualAngleTest()
        {
            var exp = new GreaterOrEqual(
                AngleValue.Degree(12).AsExpression(),
                AngleValue.Degree(10).AsExpression()
            );
            var result = (bool)exp.Execute();

            Assert.True(result);
        }

        [Fact]
        public void GreaterOrEqualPowerTest()
        {
            var exp = new GreaterOrEqual(
                PowerValue.Watt(12).AsExpression(),
                PowerValue.Watt(10).AsExpression()
            );
            var result = (bool)exp.Execute();

            Assert.True(result);
        }

        [Fact]
        public void CalculateInvalidTypeTest()
        {
            var greaterOrEqual = new GreaterOrEqual(Bool.True, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => greaterOrEqual.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new GreaterOrEqual(Number.Two, new Number(3));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}