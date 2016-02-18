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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{
    
    public class ProductTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var sum = new Product(new Variable("i"), new Number(8));

            Assert.Equal(40320.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var sum = new Product(new Variable("i"), new Number(4), new Number(8));

            Assert.Equal(6720.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var sum = new Product(new Variable("i"), new Number(4), new Number(8), new Number(2));

            Assert.Equal(192.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var sum = new Product(new Variable("k"), new Number(4), new Number(8), new Number(2), new Variable("k"));

            Assert.Equal(192.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest5()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(4));

            Assert.Equal(1024.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest6()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(2), new Number(5));

            Assert.Equal(16384.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest7()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(4), new Number(8), new Number(2));

            Assert.Equal(262144.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest8()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("k")), new Number(4), new Number(8), new Number(2), new Variable("k"));

            Assert.Equal(262144.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

    }

}
