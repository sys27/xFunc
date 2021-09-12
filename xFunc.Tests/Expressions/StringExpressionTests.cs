// Copyright 2012-2021 Dmytro Kyshchenko
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

namespace xFunc.Tests.Expressions
{
    public class StringExpressionTests : BaseExpressionTests
    {
        [Fact]
        public void NullCtor()
        {
            Assert.Throws<ArgumentNullException>(() => new StringExpression(null));
        }

        [Fact]
        public void ExecuteTest()
        {
            var exp = new StringExpression("hello");
            var expected = "hello";

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteWithParamsTest()
        {
            var exp = new StringExpression("hello");
            var parameters = new ParameterCollection();
            var expected = "hello";

            Assert.Equal(expected, exp.Execute(parameters));
        }

        [Fact]
        public void EqualsNumberNullTest()
        {
            var exp = new StringExpression("hello");

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualsObjectNullTest()
        {
            var exp = new StringExpression("hello");

            Assert.False(exp.Equals((object)null));
        }

        [Fact]
        public void EqualsNumberThisTest()
        {
            var exp = new StringExpression("hello");

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualsObjectThisTest()
        {
            var exp = new StringExpression("hello");

            Assert.True(exp.Equals((object)exp));
        }

        [Fact]
        public void EqualsTest()
        {
            var left = new StringExpression("hello");
            var right = new StringExpression("hello");

            Assert.True(left.Equals(right));
        }

        [Fact]
        public void NotEqualsTest()
        {
            var left = new StringExpression("hello");
            var right = new StringExpression("hello1");

            Assert.False(left.Equals(right));
        }

        [Fact]
        public void EqualsWithDifferentTypeTest()
        {
            var left = new StringExpression("hello");
            var right = Number.One;

            Assert.False(left.Equals(right));
        }

        [Fact]
        public void EqualsAsObjectTest()
        {
            var left = new StringExpression("hello");
            var right = new StringExpression("hello") as object;

            Assert.True(left.Equals(right));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new StringExpression("hello");

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new StringExpression("hello");

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}