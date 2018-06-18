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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;
using Xunit;

namespace xFunc.Tests.Expressionss.Statistical
{

    public class ProductTest
    {

        [Fact]
        public void TwoNumbersTest()
        {
            var sum = new Product(new[] { new Number(3), new Number(2) }, 2);

            Assert.Equal(6.0, sum.Execute());
        }

        [Fact]
        public void OneNumberTest()
        {
            var sum = new Product(new[] { new Number(2) }, 1);

            Assert.Equal(2.0, sum.Execute());
        }

        [Fact]
        public void VectorTest()
        {
            var sum = new Product(new[] { new Vector(new[] { new Number(4), new Number(2) }) }, 1);

            Assert.Equal(8.0, sum.Execute());
        }

        [Fact]
        public void NotSupportedException()
        {
            var exp = new Product(new[] { new Bool(false), new Bool(false) }, 2);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Product(new[] { new Number(1), new Number(2) }, 2);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
