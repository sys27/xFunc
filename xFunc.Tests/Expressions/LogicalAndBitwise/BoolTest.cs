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
using Xunit;

namespace xFunc.Tests.Expressions.LogicalAndBitwise
{

    public class BoolTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Bool(false);

            Assert.False((bool)exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Bool(false);

            Assert.False((bool)exp.Execute(null));
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Bool(false);

            Assert.False(exp);
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = (Bool)false;

            Assert.False((bool)exp.Execute());
        }

        [Fact]
        public void EqualsTest()
        {
            var exp1 = new Bool(false);
            var exp2 = new Bool(true);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualsTest2()
        {
            var exp1 = new Bool(true);
            var exp2 = new Bool(true);

            Assert.True(exp1.Equals(exp2));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Bool(false);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
