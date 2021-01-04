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
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.Angles
{
    public class ToNumberTest
    {
        [Fact]
        public void ExecuteAngleTest()
        {
            var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
            var actual = exp.Execute();
            var expected = new NumberValue(10.0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteBoolTest()
        {
            Assert.Throws<ResultIsNotSupportedException>(() => new ToNumber(Bool.False).Execute());
        }

        [Fact]
        public void NullAnalyzerTest1()
        {
            var exp = new ToNumber(new Number(10));

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
        }

        [Fact]
        public void NullAnalyzerTest2()
        {
            var exp = new ToNumber(new Number(10));

            Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}