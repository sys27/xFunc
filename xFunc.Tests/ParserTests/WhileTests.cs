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
    public class WhileTests : BaseParserTests
    {
        [Fact]
        public void WhileTest()
        {
            var expected = new While(
                new Define(Variable.X, new Add(Variable.X, Number.One)),
                new Equal(Number.One, Number.One)
            );

            ParseTest("while(x := x + 1, 1 == 1)", expected);
        }

        [Theory]
        [InlineData("while x := x + 1, 1 == 1)")]
        [InlineData("while(, 1 == 1)")]
        [InlineData("while(x := x + 1 1 == 1)")]
        [InlineData("while(x := x + 1, )")]
        [InlineData("while(x := x + 1, 1 == 1")]
        public void WhileMissingPartsTest(string function)
            => ParseErrorTest(function);
    }
}