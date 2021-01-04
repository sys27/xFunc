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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class EqualityOperatorTests : BaseParserTests
    {
        [Fact]
        public void EqualTest()
            => ParseTest("x == 0", new Equal(Variable.X, Number.Zero));

        [Fact]
        public void NotEqualTest()
            => ParseTest("x != 0", new NotEqual(Variable.X, Number.Zero));

        [Fact]
        public void PrecedenceTest()
        {
            var expected = new And(
                new Variable("a"),
                new NotEqual(new Variable("b"), new Variable("c"))
            );

            ParseTest("a & b != c", expected);
        }
    }
}