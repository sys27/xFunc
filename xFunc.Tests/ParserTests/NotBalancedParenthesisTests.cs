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

using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class NotBalancedParenthesisTests : BaseParserTests
    {
        [Fact]
        public void NotBalancedOpen()
            => ParseErrorTest("sin(2(");

        [Fact]
        public void NotBalancedClose()
            => ParseErrorTest("sin)2)");

        [Fact]
        public void NotBalancedFirstClose()
            => ParseErrorTest("sin)2(");

        [Fact]
        public void NotBalancedBracesOpen()
            => ParseErrorTest("{2,1");

        [Fact]
        public void NotBalancedBracesClose()
            => ParseErrorTest("}2,1");

        [Fact]
        public void NoCloseParen()
            => ParseErrorTest("(2");

        [Fact]
        public void NoCloseParenInFunc()
            => ParseErrorTest("func(2");
    }
}