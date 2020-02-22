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

using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths
{
    public partial class Parser
    {
        private class TokenEnumerator
        {
            private readonly IToken[] list;
            private int index;

            public TokenEnumerator(IToken[] list)
            {
                this.list = list;
            }

            private bool MoveNext()
            {
                if (index >= list.Length)
                {
                    index = list.Length;

                    return false;
                }

                ++index;

                return true;
            }

            private TToken Peek<TToken>() where TToken : class, IToken
            {
                if (index >= list.Length)
                    return null;

                return list[index] as TToken;
            }

            public TToken GetCurrent<TToken>() where TToken : class, IToken
            {
                var token = Peek<TToken>();
                if (token != null)
                    MoveNext();

                return token;
            }

            public bool Symbol(SymbolToken symbolToken)
            {
                var token = Peek<SymbolToken>();
                var result = token == symbolToken;
                if (result)
                    MoveNext();

                return result;
            }

            public OperatorToken Operator(OperatorToken operatorToken)
            {
                var token = Peek<OperatorToken>();
                if (token == operatorToken)
                {
                    MoveNext();

                    return token;
                }

                return null;
            }

            public KeywordToken Keyword(KeywordToken keywordToken)
            {
                var token = Peek<KeywordToken>();
                if (token == keywordToken)
                {
                    MoveNext();

                    return token;
                }

                return null;
            }

            public Scope CreateScope() => new Scope(this);

            public void Rollback(Scope scope) => scope.Rollback(this);

            public bool IsEnd => index >= list.Length;

            public readonly struct Scope
            {
                private readonly int position;

                public Scope(TokenEnumerator tokenEnumerator)
                {
                    position = tokenEnumerator.index;
                }

                public void Rollback(TokenEnumerator tokenEnumerator)
                {
                    tokenEnumerator.index = this.position;
                }
            }
        }
    }
}