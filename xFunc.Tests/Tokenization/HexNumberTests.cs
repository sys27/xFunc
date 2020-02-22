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
using Xunit;

namespace xFunc.Tests.Tokenization
{
    public class HexNumberTests : BaseLexerTests
    {
        [Fact]
        public void HexTest()
        {
            var tokens = lexer.Tokenize("0xff00");
            var expected = Builder()
                .Number(0xff00)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexNumberUpperCase()
        {
            var tokens = lexer.Tokenize("0XFF");
            var expected = Builder()
                .Number(0xFF)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexErrorTest()
        {
            var tokens = lexer.Tokenize("0x");
            var expected = Builder()
                .Number(0)
                .VariableX()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }
    }
}