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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicArsecantTest
    {
        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Arsech(new Number(0.5));
            var result = (double)exp.Execute(AngleMeasurement.Radian);

            Assert.Equal(1.3169578969248166, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Arsech(new Number(0.5));
            var result = (double)exp.Execute(AngleMeasurement.Degree);

            Assert.Equal(75.45612929021688, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Arsech(new Number(0.5));
            var result = (double)exp.Execute(AngleMeasurement.Gradian);

            Assert.Equal(83.84014365579654, result, 15);
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
        {
            var exp = new Arsech(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Arsech(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}