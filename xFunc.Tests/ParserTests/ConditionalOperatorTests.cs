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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class ConditionalOperatorTests : BaseParserTests
    {
        [Fact]
        public void ConditionalAndTest()
        {
            var expected = new ConditionalAnd(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(new Variable("y"), Number.Zero)
            );

            ParseTest("x == 0 && y != 0", expected);
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var expected = new ConditionalOr(
                new Equal(Variable.X, Number.Zero),
                new NotEqual(new Variable("y"), Number.Zero)
            );

            ParseTest("x == 0 || y != 0", expected);
        }

        [Fact]
        public void ConditionalPrecedenceTest()
        {
            var expected = new ConditionalOr(
                Variable.X,
                new ConditionalAnd(new Variable("y"), new Variable("z"))
            );

            ParseTest("x || y && z", expected);
        }

        [Theory]
        [InlineData("x == 0 &&")]
        [InlineData("x == 0 ||")]
        public void ConditionalMissingSecondOperand(string function)
            => ParseErrorTest(function);
    }
}