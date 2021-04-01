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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class TruncTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteNumberTest()
        {
            var exp = new Trunc(new Number(5.55555555));
            var result = exp.Execute();
            var expected = new NumberValue(5.0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteAngleTest()
        {
            var exp = new Trunc(AngleValue.Degree(5.55555555).AsExpression());
            var result = exp.Execute();
            var expected = AngleValue.Degree(5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecutePowerTest()
        {
            var exp = new Trunc(PowerValue.Watt(5.55555555).AsExpression());
            var result = exp.Execute();
            var expected = PowerValue.Watt(5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Trunc(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Trunc(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}