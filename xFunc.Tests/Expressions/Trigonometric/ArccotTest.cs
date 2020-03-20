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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;
using xFunc.Maths.Expressions.ComplexNumbers;
using System.Numerics;
using xFunc.Maths.Expressions.LogicalAndBitwise;

namespace xFunc.Tests.Expressions.Trigonometric
{
    public class ArccotTest
    {
        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Arccot(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Radian);

            Assert.Equal(0.7853981633974483, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Arccot(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Degree);

            Assert.Equal(45, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Arccot(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Gradian);

            Assert.Equal(50, result, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Arccot(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.23182380450040308, result.Real, 15);
            Assert.Equal(-0.14694666622552988, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Arccot(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Arccot(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}