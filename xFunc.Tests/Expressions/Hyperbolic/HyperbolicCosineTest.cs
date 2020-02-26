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

using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicCosineTest
    {
        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Cosh(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Radian);

            Assert.Equal(1.5430806348152437, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Cosh(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Degree);

            Assert.Equal(1.0001523125762564, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Cosh(new Number(1));
            var result = (double)exp.Execute(AngleMeasurement.Gradian);

            Assert.Equal(1.0001233725917296, result, 15);
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
        {
            var exp = new Cosh(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Cosh(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}