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
using System.Collections.Generic;
using System.Numerics;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{

    /// <summary>
    /// Builder of token list.
    /// </summary>
    public class TokensBuilder
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TokensBuilder"/> class.
        /// </summary>
        public TokensBuilder()
        {
            Tokens = new List<IToken>();
        }

        /// <summary>
        /// Adds boolean token with <c>true</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder True()
        {
            return Keyword(Keywords.True);
        }

        /// <summary>
        /// Adds boolean token with <c>false</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder False()
        {
            return Keyword(Keywords.False);
        }

        /// <summary>
        /// Adds a keyword token with <c>def</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Def()
        {
            return Keyword(Keywords.Define);
        }

        /// <summary>
        /// Adds a keyword token with <c>undef</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Undef()
        {
            return Keyword(Keywords.Undefine);
        }

        /// <summary>
        /// Adds a keyword token with <c>if</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder If()
        {
            return Keyword(Keywords.If);
        }

        /// <summary>
        /// Adds a keyword token with <c>for</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder For()
        {
            return Keyword(Keywords.For);
        }

        /// <summary>
        /// Adds a keyword token with <c>while</c> value.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder While()
        {
            return Keyword(Keywords.While);
        }

        /// <summary>
        /// Adds a keyword token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Keyword(Keywords keyword)
        {
            Tokens.Add(new KeywordToken(keyword));

            return this;
        }

        /// <summary>
        /// Adds number token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        /// <param name="number">The value of number token.</param>
        public TokensBuilder Number(double number)
        {
            Tokens.Add(new NumberToken(number));

            return this;
        }

        /// <summary>
        /// Adds operation token.
        /// </summary>
        /// <returns>The current instance of builder..</returns>
        /// <param name="operation">The type of operation.</param>
        public TokensBuilder Operation(Operations operation)
        {
            Tokens.Add(new OperationToken(operation));

            return this;
        }

        /// <summary>
        /// Adds symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        /// <param name="symbol">The type of symbol.</param>
        public TokensBuilder Symbol(Symbols symbol)
        {
            Tokens.Add(new SymbolToken(symbol));

            return this;
        }

        /// <summary>
        /// Adds open brace symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder OpenBrace()
        {
            return Symbol(Symbols.OpenBrace);
        }

        /// <summary>
        /// Adds close brace symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder CloseBrace()
        {
            return Symbol(Symbols.CloseBrace);
        }

        /// <summary>
        /// Adds open parenthesis symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder OpenParenthesis()
        {
            return Symbol(Symbols.OpenParenthesis);
        }

        /// <summary>
        /// Adds close parenthesis symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder CloseParenthesis()
        {
            return Symbol(Symbols.CloseParenthesis);
        }

        /// <summary>
        /// Adds comma symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Comma()
        {
            return Symbol(Symbols.Comma);
        }

        /// <summary>
        /// Adds angle symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Angle()
        {
            return Symbol(Symbols.Angle);
        }

        /// <summary>
        /// Adds degree symbol token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder Degree()
        {
            return Symbol(Symbols.Degree);
        }

        /// <summary>
        /// Adds variable token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        /// <param name="id">The name of variable.</param>
        public TokensBuilder Id(string id)
        {
            Tokens.Add(new IdToken(id));

            return this;
        }

        /// <summary>
        /// Adds the 'x' variable token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder VariableX()
        {
            Tokens.Add(new IdToken("x"));

            return this;
        }

        /// <summary>
        /// Adds the 'y' variable token.
        /// </summary>
        /// <returns>The current instance of builder.</returns>
        public TokensBuilder VariableY()
        {
            Tokens.Add(new IdToken("y"));

            return this;
        }

        /// <summary>
        /// Gets token list.
        /// </summary>
        /// <value>
        /// The token list.
        /// </value>
        public IList<IToken> Tokens { get; }
    }

}