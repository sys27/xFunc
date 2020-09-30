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
    public class SecantTest : BaseExpressionTests
    {
        [Theory]
        [InlineData(0.0, 1.0)] // 1
        [InlineData(30.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(45.0, 1.4142135623730951)] // sqrt(2)
        [InlineData(60.0, 2.0)] // 2
        [InlineData(90.0, double.PositiveInfinity)] // -
        [InlineData(120.0, -2.0)] // -2
        [InlineData(135.0, -1.4142135623730951)] // -sqrt(2)
        [InlineData(150.0, -1.1547005383792515)] // -2sqrt(3) / 3
        [InlineData(180.0, -1.0)] // -1
        [InlineData(210.0, -1.1547005383792515)] // -2sqrt(3) / 3
        [InlineData(225.0, -1.4142135623730951)] // -sqrt(2)
        [InlineData(240.0, -2.0)] // -2
        [InlineData(270.0, double.PositiveInfinity)] // -
        [InlineData(300.0, -2.0)] // -2
        [InlineData(315.0, 1.4142135623730951)] // sqrt(2)
        [InlineData(330.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(360.0, 1.0)] // 1
        [InlineData(1110.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(1770.0, 1.1547005383792515)] // 2sqrt(3) / 3
        [InlineData(-390.0, 1.1547005383792515)] // 2sqrt(3) / 3
        public void ExecuteNumberTest(double degree, double expected)
        {
            var exp = new Sec(new Number(degree));
            var result = (double)exp.Execute();

            Assert.Equal(expected, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Sec(AngleValue.Degree(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(1.0001523280439077, result, 15);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Sec(AngleValue.Radian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(1.8508157176809255, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Sec(AngleValue.Gradian(1).AsExpression());
            var actual = (double)exp.Execute();

            Assert.Equal(1.0001233827397618, actual, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Sec(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(-0.26351297515838928, result.Real, 15);
            Assert.Equal(0.036211636558768523, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Sec(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Sec(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}