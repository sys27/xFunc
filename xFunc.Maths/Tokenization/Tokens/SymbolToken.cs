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

using System.Diagnostics;
using System.Globalization;

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a symbol token.
    /// </summary>
    [DebuggerDisplay("Symbol: {" + nameof(symbol) + "}")]
    public sealed class SymbolToken : IToken
    {
        private readonly char symbol;

        private SymbolToken(char symbol) => this.symbol = symbol;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => symbol.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets the '(' token.
        /// </summary>
        public static SymbolToken OpenParenthesis { get; } = new SymbolToken('(');

        /// <summary>
        /// Gets the ')' token.
        /// </summary>
        public static SymbolToken CloseParenthesis { get; } = new SymbolToken(')');

        /// <summary>
        /// Gets the '{' token.
        /// </summary>
        public static SymbolToken OpenBrace { get; } = new SymbolToken('{');

        /// <summary>
        /// Gets the '}' token.
        /// </summary>
        public static SymbolToken CloseBrace { get; } = new SymbolToken('}');

        /// <summary>
        /// Gets the ',' token.
        /// </summary>
        public static SymbolToken Comma { get; } = new SymbolToken(',');

        /// <summary>
        /// Gets the '∠' token.
        /// </summary>
        public static SymbolToken Angle { get; } = new SymbolToken('∠');

        /// <summary>
        /// Gets the '°' token.
        /// </summary>
        public static SymbolToken Degree { get; } = new SymbolToken('°');

        /// <summary>
        /// Gets the ':' token.
        /// </summary>
        public static SymbolToken Colon { get; } = new SymbolToken(':');

        /// <summary>
        /// Gets the '?' token.
        /// </summary>
        public static SymbolToken QuestionMark { get; } = new SymbolToken('?');
    }
}