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
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .OpenParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NotBalancedClose()
        {
            var tokens = Builder()
                .Id("sin")
                .CloseParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NotBalancedFirstClose()
        {
            var tokens = Builder()
                .Id("sin")
                .CloseParenthesis()
                .Number(2)
                .OpenParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NotBalancedBracesOpen()
        {
            var tokens = Builder()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NotBalancedBracesClose()
        {
            var tokens = Builder()
                .CloseBrace()
                .Number(2)
                .Comma()
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NoCloseParen()
        {
            var tokens = Builder()
                .OpenParenthesis()
                .Number(2)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void NoCloseParenInFunc()
        {
            var tokens = Builder()
                .Id("fund")
                .OpenParenthesis()
                .Number(2)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}