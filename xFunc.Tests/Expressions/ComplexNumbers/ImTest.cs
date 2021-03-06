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
using xFunc.Maths.Expressions.ComplexNumbers;
using Xunit;

namespace xFunc.Tests.Expressions.ComplexNumbers
{
    public class ImTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Im(new ComplexNumber(complex));
            var expected = new NumberValue(complex.Imaginary);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteExceptionTest()
            => TestNotSupported(new Im(Number.Two));

        [Fact]
        public void CloneTest()
        {
            var exp = new Im(new ComplexNumber(new Complex(2, 2)));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}