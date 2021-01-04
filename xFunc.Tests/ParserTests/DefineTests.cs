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
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class DefineTests : BaseParserTests
    {
        [Fact]
        public void DefTest()
            => ParseTest("def(x, 2)", new Define(Variable.X, Number.Two));

        [Theory]
        [InlineData("def x, 2)")]
        [InlineData("def(, 2)")]
        [InlineData("def(x 2)")]
        [InlineData("def(x,)")]
        [InlineData("def(x, 2")]
        public void DefMissingOpenParen(string function)
            => ParseErrorTest(function);
    }
}