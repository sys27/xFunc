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
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class GCDTest
    {

        [Fact]
        public void CalcucateTest1()
        {
            var exp = new GCD(new Number(12), new Number(16));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void CalcucateTest2()
        {
            var exp = new GCD(new IExpression[] { new Number(64), new Number(16), new Number(8) }, 3);

            Assert.Equal(8.0, exp.Execute());
        }

        [Fact]
        public void DifferentArgsParentTest()
        {
            var num1 = new Number(64);
            var num2 = new Number(16);
            var gcd = new GCD(new[] { num1, num2 }, 2);

            Assert.Equal(gcd, num1.Parent);
            Assert.Equal(gcd, num2.Parent);
        }

        [Fact]
        public void NullArgTest()
        {
            Assert.Throws<ArgumentNullException>(() => new GCD(null, 0));
        }


        [Fact]
        public void ArgCountTest()
        {
            Assert.Throws<ArgumentException>(() => new GCD(new IExpression[] { new Number(1) }, 0));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new GCD(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
