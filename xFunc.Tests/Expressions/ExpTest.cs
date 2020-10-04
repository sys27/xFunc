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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class ExpTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Exp(Number.Two);
            var expected = NumberValue.Exp(new NumberValue(2));

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var expected = new Complex(4, 2);
            var exp = new Exp(new ComplexNumber(expected));

            Assert.Equal(Complex.Exp(expected), exp.Execute());
        }

        [Fact]
        public void ExecuteExceptionTest()
            => TestNotSupported(new Exp(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Exp(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}