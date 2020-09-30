// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Trigonometric
{
    public class CosecantTest : BaseExpressionTests
    {
        [Theory]
        [InlineData(0.0, double.PositiveInfinity)] // -
        [InlineData(30.0, 2.0)] // 2
        [InlineData(45.0, 1.4142135623730951)] // sqrt(2)
        [InlineData(60.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(90.0, 1.0)] // 1
        [InlineData(120.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(135.0, 1.4142135623730951)] // sqrt(2)
        [InlineData(150.0, 2.0)] // 2
        [InlineData(180.0, double.PositiveInfinity)] // -
        [InlineData(210.0, -2.0)] // -2
        [InlineData(225.0, -1.4142135623730951)] // -sqrt(2)
        [InlineData(240.0, -1.1547005383792515)] // -2sqrt(3) / 3
        [InlineData(270.0, -1.0)] // -1
        [InlineData(300.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(315.0, 1.4142135623730951)] // sqrt(2)
        [InlineData(330.0, 2.0)] // 2
        [InlineData(360.0, double.PositiveInfinity)] // -
        [InlineData(1110.0, 2.0)] // 2
        [InlineData(1770.0, 2.0)] // 2
        [InlineData(-390.0, 2.0)] // 2
        public void ExecuteNumberTest(double degree, double expected)
        {
            var exp = new Csc(new Number(degree));
            var result = (double)exp.Execute();

            Assert.Equal(expected, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Csc(AngleValue.Degree(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(57.298688498550185, result, 15);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Csc(AngleValue.Radian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(1.1883951057781212, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Csc(AngleValue.Gradian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(63.664595306000564, result, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Csc(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.040300578856891527, result.Real, 15);
            Assert.Equal(0.27254866146294021, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Csc(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Csc(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}