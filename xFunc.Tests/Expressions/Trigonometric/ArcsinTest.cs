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
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressions.Trigonometric
{
    public class ArcsinTest
    {
        [Fact]
        public void CalculateRadianTest()
        {
            var exp = new Arcsin(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Radian);

            Assert.Equal(1.5707963267948966, result, 15);
        }

        [Fact]
        public void CalculateDegreeTest()
        {
            var exp = new Arcsin(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Degree);

            Assert.Equal(90, result, 15);
        }

        [Fact]
        public void CalculateGradianTest()
        {
            var exp = new Arcsin(new Number(1));
            var actual = (double)exp.Execute(AngleMeasurement.Gradian);

            Assert.Equal(100, actual, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Arcsin(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.96465850440760248, result.Real, 14);
            Assert.Equal(1.9686379257930975, result.Imaginary, 14);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Arcsin(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Arcsin(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}