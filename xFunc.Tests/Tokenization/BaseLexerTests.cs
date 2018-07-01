// Copyright 2012-2018 Dmitry Kischenko
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using xFunc.Maths;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{

    public abstract class BaseLexerTests
    {

        protected readonly ILexer lexer;

        protected BaseLexerTests()
        {
            lexer = new Lexer();
        }

        protected TokensBuilder Builder()
        {
            return new TokensBuilder();
        }

        protected void FuncTest(string func, Functions type)
        {
            var tokens = lexer.Tokenize($"{func}(3)");
            var expected = Builder()
                .Function(type, 1)
                .OpenBracket()
                .Number(3)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        protected void FuncBinaryTest(string func, Functions type)
        {
            var tokens = lexer.Tokenize($"{func}(1, 2)");
            var expected = Builder()
                .Function(type, 2)
                .OpenBracket()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

    }

}