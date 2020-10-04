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
    public class CotangentTest : BaseExpressionTests
    {
        [Theory]
        [InlineData(0.0, double.PositiveInfinity)] // -
        [InlineData(30.0, 1.7320508075688772)] // sqrt(3)
        [InlineData(45.0, 1.0)] // 1
        [InlineData(60.0, 0.57735026918962573)] // sqrt(3) / 3
        [InlineData(90.0, 0.0)] // 0
        [InlineData(120.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(135.0, -1)] // -1
        [InlineData(150.0, -1.7320508075688772)] // -sqrt(3)
        [InlineData(180.0, double.PositiveInfinity)] // -
        [InlineData(210.0, 1.7320508075688772)] // sqrt(3)
        [InlineData(225.0, 1.0)] // 1
        [InlineData(240.0, 0.57735026918962573)] // sqrt(3) / 3
        [InlineData(270.0, 0.0)] // 0
        [InlineData(300.0, -1.7320508075688772)] // -sqrt(3)
        [InlineData(315.0, -1.0)] // -1
        [InlineData(330.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(360.0, double.PositiveInfinity)] // -
        [InlineData(1110.0, 1.7320508075688772)] // sqrt(3)
        [InlineData(1770.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(-390.0, -0.57735026918962573)] // -sqrt(3) / 3
        public void ExecuteNumberTest(double degree, double expected)
        {
            var exp = new Cot(new Number(degree));
            var actual = (NumberValue)exp.Execute();

            Assert.Equal(expected, actual.Number);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Cot(AngleValue.Radian(1).AsExpression());
            var actual = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.6420926159343308);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Cot(AngleValue.Degree(1).AsExpression());
            var actual = (NumberValue)exp.Execute();
            var expected = new NumberValue(57.28996163075943);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Cot(AngleValue.Gradian(1).AsExpression());
            var actual = (NumberValue)exp.Execute();
            var expected = new NumberValue(63.65674116287158);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Cot(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(-0.010604783470337083, result.Real, 14);
            Assert.Equal(-1.0357466377649953, result.Imaginary, 14);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Cot(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Cot(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}