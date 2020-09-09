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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class FloorTest
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var floor = new Floor(new Number(5.55555555));
            var result = floor.Execute();
            var expected = 5.0;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteAngleTest()
        {
            var floor = new Floor(AngleValue.Degree(5.55555555).AsExpression());
            var result = floor.Execute();
            var expected = AngleValue.Degree(5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Floor(Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Floor(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}