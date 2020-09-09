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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicCotangentTest
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Coth(Number.One);
            var result = (double)exp.Execute();

            Assert.Equal(57.30159715911299, result, 15);
        }

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Coth(AngleValue.Radian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(1.3130352854993312, result, 15);
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Coth(AngleValue.Degree(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(57.30159715911299, result, 15);
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Coth(AngleValue.Gradian(1).AsExpression());
            var result = (double)exp.Execute();

            Assert.Equal(63.66721313838742, result, 15);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Coth(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(0.99675779656935837, result.Real, 15);
            Assert.Equal(0.0037397103763368955, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Coth(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Coth(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}