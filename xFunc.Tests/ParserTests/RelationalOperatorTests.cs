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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class RelationalOperatorTests : BaseParserTests
    {
        [Fact]
        public void LessThenTest()
        {
            var expected = new LessThan(Variable.X, Number.Zero);

            ParseTest("x < 0", expected);
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var expected = new LessOrEqual(Variable.X, Number.Zero);

            ParseTest("x <= 0", expected);
        }

        [Fact]
        public void GreaterThenTest()
        {
            var expected = new GreaterThan(Variable.X, Number.Zero);

            ParseTest("x > 0", expected);
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var expected = new GreaterOrEqual(Variable.X, Number.Zero);

            ParseTest("x >= 0", expected);
        }

        [Fact]
        public void PrecedenceTest()
        {
            var expected = new NotEqual(
                new Variable("a"),
                new LessThan(new Variable("b"), new Variable("c"))
            );

            ParseTest("a != b < c", expected);
        }
    }
}