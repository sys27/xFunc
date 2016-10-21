// Copyright 2012-2016 Dmitry Kischenko
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
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class SqrtTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Sqrt(new Number(4));

            Assert.Equal(Math.Sqrt(4), exp.Execute());
        }

        [Fact]
        public void NegativeNumberExecuteTest1()
        {
            var exp = new Sqrt(new Number(-25));

            Assert.Equal(double.NaN, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var complex = new Complex(5, 3);
            var exp = new Sqrt(new ComplexNumber(complex));

            Assert.Equal(Complex.Sqrt(complex), exp.Execute());
        }

        [Fact]
        public void NegativeNumberExecuteTest2()
        {
            var complex = new Complex(-25, 13);
            var exp = new Sqrt(new ComplexNumber(complex));

            Assert.Equal(Complex.Sqrt(complex), exp.Execute());
        }

    }

}
