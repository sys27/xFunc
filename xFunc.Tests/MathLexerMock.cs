// Copyright 2012-2015 Dmitry Kischenko
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
using xFunc.Maths;
using xFunc.Maths.Tokens;

namespace xFunc.Test
{

    public class MathLexerMock : ILexer
    {

        private List<IToken> tokens;

        public MathLexerMock()
        {
        }

        public MathLexerMock(IEnumerable<IToken> tokens)
        {
            this.tokens = new List<IToken>(tokens);
        }

        public IEnumerable<IToken> Tokenize(string function)
        {
            return tokens;
        }

        public IEnumerable<IToken> Tokens
        {
            get
            {
                return tokens;
            }
            set
            {
                tokens = new List<IToken>(value);
            }
        }

    }

}
