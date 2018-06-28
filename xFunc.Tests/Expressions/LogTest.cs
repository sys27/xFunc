// Copyright 2012-2018 Dmitry Kischenko
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

namespace xFunc.Tests.Expressions
{

    public class LogTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Log(new Number(10), new Number(2));

            Assert.Equal(Math.Log(10, 2), exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var complex = new Complex(2, 3);
            var exp = new Log(new ComplexNumber(complex), new Number(4));

            Assert.Equal(Complex.Log(complex, 4), exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Log(new Number(5), new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
