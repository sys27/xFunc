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
    
    public class AbsTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Abs(new Number(-1));

            Assert.Equal(1.0, exp.Execute());
        }

        [Fact]
        public void EqualsTest1()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 2;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.True(abs1.Equals(abs2));
            Assert.True(abs1.Equals(abs1));
        }

        [Fact]
        public void EqualsTest2()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 3;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.False(abs1.Equals(abs2));
            Assert.False(abs1.Equals(mul2));
            Assert.False(abs1.Equals(null));
        }

    }

}
