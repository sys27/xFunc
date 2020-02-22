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

using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{
    public class NumberTests : BaseLexerTests
    {
        [Fact]
        public void ZeroTest()
        {
            var tokens = lexer.Tokenize("0");
            var expected = Builder().Number(0).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddBeforeNumber()
        {
            var tokens = lexer.Tokenize("+2");
            var expected = Builder()
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubBeforeNumber()
        {
            var tokens = lexer.Tokenize("-2");
            var expected = Builder()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberFormatTest()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("0.").First());
        }
    }
}