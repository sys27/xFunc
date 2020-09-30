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
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class LogTests : BaseParserTests
    {
        [Fact]
        public void ParseLog()
            => ParseTest("log(9, 3)", new Log(new Number(9), new Number(3)));

        [Fact]
        public void ParseLogWithOneParam()
            => ParseErrorTest("log(9)");

        [Theory]
        [InlineData("lb(2)")]
        [InlineData("log2(2)")]
        public void LbTest(string function)
            => ParseTest(function, new Lb(Number.Two));

        [Fact]
        public void LgTest()
            => ParseTest("lg(2)", new Lg(Number.Two));

        [Fact]
        public void LnTest()
            => ParseTest("ln(2)", new Ln(Number.Two));
    }
}