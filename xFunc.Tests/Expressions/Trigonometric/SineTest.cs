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
    public class SineTest
    {
        [Theory]
        [InlineData(0.0, 0.0)] // 0
        [InlineData(30.0, 0.5)] // 1 / 2
        [InlineData(45.0, 0.70710678118654757)] // sqrt(2) / 2
        [InlineData(60.0, 0.86602540378443864)] // sqrt(3) / 2
        [InlineData(90.0, 1.0)] // 1
        [InlineData(120.0, 0.86602540378443864)] // sqrt(3) / 2
        [InlineData(135.0, 0.70710678118654757)] // sqrt(2) / 2
        [InlineData(150.0, 0.5)] // 1 / 2
        [InlineData(180.0, 0.0)] // 0
        [InlineData(210.0, -0.5)] // -1 / 2
        [InlineData(225.0, -0.70710678118654757)] // -sqrt(2) / 2
        [InlineData(240.0, -0.86602540378443864)] // -sqrt(3) / 2
        [InlineData(270.0, -1.0)] // -1
        [InlineData(300.0, -0.86602540378443864)] // -sqrt(3) / 2
        [InlineData(315.0, -0.70710678118654757)] // -sqrt(2) / 2
        [InlineData(330.0, -0.5)] // -1 / 2
        [InlineData(360.0, 0)] // 0
        [InlineData(1110.0, 0.5)] // 1 / 2
        [InlineData(1770.0, -0.5)] // -1 / 2
        [InlineData(-390.0, -0.5)] // -1 / 2
        public void ExecuteNumberTest(double degree, double expected)
        {
            var exp = new Sin(new Number(degree));
            var result = (double)exp.Execute();

            Assert.Equal(expected, result, 15);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Sin(AngleValue.Radian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(0.8414709848078965, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Sin(AngleValue.Degree(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(0.017452406437283512, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Sin(AngleValue.Gradian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(0.015707317311820675, result, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Sin(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.53092108624851986, result.Real, 15);
            Assert.Equal(-3.59056458998578, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Sin(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Sin(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}