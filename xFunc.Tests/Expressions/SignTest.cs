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
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class SignTest : BaseExpressionTests
    {
        [Fact]
        public void PositiveSignTest()
        {
            var exp = new Sign(new Number(5));
            var result = exp.Execute();

            Assert.Equal(new NumberValue(1.0), result);
        }

        [Fact]
        public void NegativeSignTest()
        {
            var exp = new Sign(new Number(-5));
            var result = exp.Execute();

            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void AngleSignTest()
        {
            var exp = new Sign(AngleValue.Degree(10).AsExpression());
            var result = exp.Execute();

            Assert.Equal(new NumberValue(1.0), result);
        }

        [Fact]
        public void PowerSignTest()
        {
            var exp = new Sign(PowerValue.Watt(10).AsExpression());
            var result = exp.Execute();

            Assert.Equal(new NumberValue(1.0), result);
        }

        [Fact]
        public void InvalidParameterTest()
            => TestNotSupported(new Sign(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Sign(new Number(-5));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}