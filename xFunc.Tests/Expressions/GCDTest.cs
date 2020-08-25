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

namespace xFunc.Tests.Expressions
{
    public class GCDTest
    {
        [Fact]
        public void CalculateTest1()
        {
            var exp = new GCD(new Number(12), new Number(16));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void CalculateTest2()
        {
            var exp = new GCD(new IExpression[] { new Number(64), new Number(16), new Number(8) });

            Assert.Equal(8.0, exp.Execute());
        }

        [Fact]
        public void CalculateWrongArgumentTypeTest()
        {
            var exp = new GCD(new IExpression[] { new Bool(true), new Number(16), new Number(8) });

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void NullArgTest()
        {
            Assert.Throws<ArgumentNullException>(() => new GCD(null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new GCD(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

        [Fact]
        public void EqualsSameTest()
        {
            var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

            Assert.True(exp.Equals(exp));
        }

        [Fact]
        public void EqualsNullTest()
        {
            var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

            Assert.False(exp.Equals(null));
        }

        [Fact]
        public void EqualsDiffTypesTest()
        {
            var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });
            var number = new Number(2);

            Assert.False(exp.Equals(number));
        }

        [Fact]
        public void EqualsDiffCountTest()
        {
            var exp1 = new GCD(new IExpression[] { new Number(16), new Number(8) });
            var exp2 = new GCD(new IExpression[] { new Number(16), new Number(8), new Number(2) });

            Assert.False(exp1.Equals(exp2));
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }
    }
}