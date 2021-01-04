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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Trigonometric
{
    public class TangentTest : BaseExpressionTests
    {
        [Theory]
        [InlineData(0.0, 0.0)] // 0
        [InlineData(30.0, 0.57735026918962573)] // sqrt(3) / 3
        [InlineData(45.0, 1.0)] // 1
        [InlineData(60.0, 1.7320508075688772)] // sqrt(3)
        [InlineData(90.0, double.PositiveInfinity)] // -
        [InlineData(120.0, -1.7320508075688772)] // -sqrt(3)
        [InlineData(135.0, -1)] // -1
        [InlineData(150.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(180.0, 0.0)] // 0
        [InlineData(210.0, 0.57735026918962573)] // sqrt(3) / 3
        [InlineData(225.0, 1.0)] // 1
        [InlineData(240.0, 1.7320508075688772)] // sqrt(3)
        [InlineData(270.0, double.PositiveInfinity)] // -
        [InlineData(300.0, -1.7320508075688772)] // -sqrt(3)
        [InlineData(315.0, -1.0)] // -1
        [InlineData(330.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(360.0, 0.0)] // 0
        [InlineData(1110.0, 0.57735026918962573)] // sqrt(3) / 3
        [InlineData(1770.0, -0.57735026918962573)] // -sqrt(3) / 3
        [InlineData(-390.0, -0.57735026918962573)] // -sqrt(3) / 3
        public void ExecuteNumberTest(double degree, double expected)
        {
            var exp = new Tan(new Number(degree));
            var result = (NumberValue)exp.Execute();

            Assert.Equal(expected, result.Number);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Tan(AngleValue.Radian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(1.5574077246549021);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Tan(AngleValue.Degree(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.017455064928217585);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Tan(AngleValue.Gradian(1).AsExpression());
            var result = (NumberValue)exp.Execute();
            var expected = new NumberValue(0.015709255323664916);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Tan(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(-0.0098843750383224935, result.Real, 15);
            Assert.Equal(0.96538587902213313, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Tan(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Tan(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}