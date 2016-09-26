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
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{
    
    public class FactTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var fact = new Fact(new Number(4));

            Assert.Equal(24.0, fact.Execute());
        }

        [Fact]
        public void CalculateTest2()
        {
            var fact = new Fact(new Number(0));

            Assert.Equal(1.0, fact.Execute());
        }

        [Fact]
        public void CalculateTest3()
        {
            var fact = new Fact(new Number(1));

            Assert.Equal(1.0, fact.Execute());
        }

        [Fact]
        public void CalculateTest4()
        {
            var fact = new Fact(new Number(-1));

            Assert.Equal(double.NaN, fact.Execute());
        }

    }

}
