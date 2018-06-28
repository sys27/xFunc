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

namespace xFunc.Tests.Expressions.ComplexNumbers
{

    public class ReciprocalTest
    {

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Reciprocal(new ComplexNumber(complex));

            Assert.Equal(Complex.Reciprocal(complex), exp.Execute());
        }

        [Fact]
        public void ExecuteExeptionTest()
        {
            var exp = new Reciprocal(new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Reciprocal(new ComplexNumber(new Complex(2, 2)));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
