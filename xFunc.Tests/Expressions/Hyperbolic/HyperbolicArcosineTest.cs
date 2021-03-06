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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Hyperbolic
{
    public class HyperbolicArcosineTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Arcosh(new Number(7));
            var result = exp.Execute();
            var expected = AngleValue.Radian(2.6339157938496336);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Arcosh(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(1.9686379257930964, result.Real, 15);
            Assert.Equal(0.606137822387294, result.Imaginary, 15);
        }

        [Fact]
        public void ExecuteTestException()
        => TestNotSupported(new Arcosh(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Arcosh(Number.One);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}