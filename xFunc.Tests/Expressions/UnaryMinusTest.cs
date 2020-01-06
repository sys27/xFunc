﻿// Copyright 2012-2020 Dmytro Kyshchenko
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
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class UnaryMinusTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new UnaryMinus(new Number(10));

            Assert.Equal(-10.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var complex = new Complex(2, 3);
            var exp = new UnaryMinus(new ComplexNumber(complex));

            Assert.Equal(Complex.Negate(complex), exp.Execute());
        }

        [Fact]
        public void NotSupportedException()
        {
            var exp = new UnaryMinus(new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new UnaryMinus(new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
