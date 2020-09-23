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
using xFunc.Maths.Expressions.Angles;
using Xunit;

namespace xFunc.Tests.Expressions.Angles
{
    public class AngleTest
    {
        [Fact]
        public void EqualNullTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualNullObjectTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.False(exp.Equals((object)null));
        }

        [Fact]
        public void EqualSameTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualSameObjectTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.True(exp.Equals((object)exp));
        }

        [Fact]
        public void EqualDiffTypeTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();
            var number = Number.One;

            Assert.False(exp.Equals(number));
        }

        [Fact]
        public void ExecuteTest()
        {
            var exp = AngleValue.Degree(10).AsExpression();
            var expected = AngleValue.Degree(10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = AngleValue.Degree(10).AsExpression();
            var expected = AngleValue.Degree(10);

            Assert.Equal(expected, exp.Execute(null));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = AngleValue.Degree(10).AsExpression();

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}