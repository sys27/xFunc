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
    public class HyperbolicArsecantTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Arsech(new Number(0.5));
            var result = exp.Execute();
            var expected = AngleValue.Radian(1.3169578969248166);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Arsech(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.15735549884498526, result.Real, 15);
            Assert.Equal(-1.3408334244176887, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Arsech(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Arsech(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}