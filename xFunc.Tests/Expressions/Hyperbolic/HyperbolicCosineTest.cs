// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units.AngleUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicCosineTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Cosh(Number.One);
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(1.0001523125762564);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Cosh(AngleValue.Radian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(1.5430806348152437);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Cosh(AngleValue.Degree(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(1.0001523125762564);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Cosh(AngleValue.Gradian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(1.0001233725917296);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Cosh(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(-4.189625690968807230132555, result.Real, 15);
            Assert.Equal(9.10922789375533659797919, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Cosh(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Cosh(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}