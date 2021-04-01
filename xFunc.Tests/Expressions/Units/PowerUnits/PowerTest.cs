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
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Units.PowerUnits
{
    public class PowerTest
    {
        [Fact]
        public void EqualNullTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualNullObjectTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.False(exp.Equals((object)null));
        }

        [Fact]
        public void EqualSameTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualSameObjectTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.True(exp.Equals((object)exp));
        }

        [Fact]
        public void EqualDiffTypeTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();
            var number = Number.One;

            Assert.False(exp.Equals(number));
        }

        [Fact]
        public void ExecuteTest()
        {
            var exp = PowerValue.Watt(10).AsExpression();
            var expected = PowerValue.Watt(10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = PowerValue.Watt(10).AsExpression();
            var expected = PowerValue.Watt(10);

            Assert.Equal(expected, exp.Execute(null));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = PowerValue.Watt(10).AsExpression();

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}