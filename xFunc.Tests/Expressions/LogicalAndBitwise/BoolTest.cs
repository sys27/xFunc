// Copyright 2012-2020 Dmytro Kyshchenko
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
            var exp = Bool.False;

            Assert.False((bool)exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = Bool.False;

            Assert.False((bool)exp.Execute(null));
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = Bool.False;

            Assert.False(exp);
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = (Bool)false;

            Assert.False((bool)exp.Execute());
        }

        [Fact]
        public void NotEqualsTest()
        {
            var exp1 = Bool.False;
            var exp2 = Bool.True;

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualsTest()
        {
            var exp1 = Bool.True;
            var exp2 = Bool.True;

            Assert.True(exp1.Equals(exp2));
        }

        [Fact]
        public void DifferentTypesEqualsTest()
        {
            var exp1 = Bool.True;
            var exp2 = new Number(2);

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void EqualsBoolNullTest()
        {
            var boolean = Bool.True;

            Assert.False(boolean.Equals(null));
        }

        [Fact]
        public void EqualsObjectNullTest()
        {
            var boolean = Bool.True;

            Assert.False(boolean.Equals((object)null));
        }

        [Fact]
        public void EqualsBoolThisTest()
        {
            var boolean = Bool.True;

            Assert.True(boolean.Equals(boolean));
        }

        [Fact]
        public void EqualsObjectThisTest()
        {
            var boolean = Bool.True;

            Assert.True(boolean.Equals((object)boolean));
        }

        [Fact]
        public void ImplicitNullToString()
        {
            Bool x = null;

            Assert.Throws<ArgumentNullException>(() => (bool)x);
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = Bool.True;

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = Bool.True;

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }

        [Fact]
        public void ToStringTest()
        {
            var trueExp = Bool.True;
            var falseExp = Bool.False;

            Assert.Equal("True", trueExp.ToString());
            Assert.Equal("False", falseExp.ToString());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = Bool.False;
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}