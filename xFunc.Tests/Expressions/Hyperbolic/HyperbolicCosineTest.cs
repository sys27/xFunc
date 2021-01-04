// Copyright 2012-2021 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
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