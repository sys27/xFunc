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

    public class BinaryTest
    {

        [Fact]
        public void EqualsTest1()
        {
            var add1 = new Add(new Number(2), new Number(3));
            var add2 = new Add(new Number(2), new Number(3));

            Assert.Equal(add1, add2);
        }

        [Fact]
        public void EqualsTest2()
        {
            var add = new Add(new Number(2), new Number(3));
            var sub = new Sub(new Number(2), new Number(3));

            Assert.NotEqual<IExpression>(add, sub);
        }

        [Fact]
        public void EqualsSameTest()
        {
            var exp = new Add(new Number(1), new Number(1));

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualsNullTest()
        {
            var exp = new Add(new Number(1), new Number(1));

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void LeftNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Add(null, new Number(1)));
        }

        [Fact]
        public void RightNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Add(new Number(1), null));
        }

    }

}
