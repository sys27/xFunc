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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;
using static xFunc.Maths.Tokenization.Tokens.SymbolToken;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    public partial class Lexer : ILexer
    {
        /// <summary>
        /// Converts the string into a sequence of tokens.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The sequence of tokens.</returns>
        /// <seealso cref="IToken"/>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="function"/> parameter is null or empty.</exception>
        /// <exception cref="TokenizeException">Throws when <paramref name="function"/> has the not supported symbol.</exception>
        public IEnumerable<IToken> Tokenize(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException(nameof(function), Resource.NotSpecifiedFunction);

            var memory = function.AsMemory();

            while (memory.Length > 0)
            {
                var result = SkipWhiteSpaces(ref memory) ??
                             CreateNumberToken(ref memory) ??
                             CreateIdToken(ref memory) ??
                             CreateOperatorToken(ref memory) ??
                             CreateSymbol(ref memory);

                if (result == null)
                    throw new TokenizeException(string.Format(CultureInfo.InvariantCulture, Resource.NotSupportedSymbol, memory.Span[0]));

                yield return result;
            }
        }

        private IToken? SkipWhiteSpaces(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            var index = 0;
            while (char.IsWhiteSpace(span[index]))
                index++;

            if (index > 0)
                function = function[index..];

            return null;
        }

        private IToken? CreateSymbol(ref ReadOnlyMemory<char> function)
        {
            var symbol = function.Span[0] switch
            {
                '(' => OpenParenthesis,
                ')' => CloseParenthesis,
                '{' => OpenBrace,
                '}' => CloseBrace,
                ',' => Comma,
                '∠' => Angle,
                '°' => Degree,
                ':' => Colon,
                '?' => QuestionMark,
                _ => null,
            };

            if (symbol == null)
                return null;

            function = function[1..];

            return symbol;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Compare(ReadOnlySpan<char> id, string str)
            => id.Equals(str, StringComparison.OrdinalIgnoreCase);
    }
}