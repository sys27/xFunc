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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    /// <summary>
    /// The factory which creates variable tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    internal class IdTokenFactory : ITokenFactory // TODO: rename class?
    {
        private readonly Dictionary<string, KeywordToken> keywords;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdTokenFactory"/> class.
        /// </summary>
        public IdTokenFactory()
        {
            keywords = new Dictionary<string, KeywordToken>
            {
                { "true", new KeywordToken(Keywords.True) },
                { "false", new KeywordToken(Keywords.False) },

                // TODO: copies?
                { "def", new KeywordToken(Keywords.Define) },
                { "define", new KeywordToken(Keywords.Define) },
                { "undef", new KeywordToken(Keywords.Undefine) },
                { "undefine", new KeywordToken(Keywords.Undefine) },

                { "if", new KeywordToken(Keywords.If) },
                { "for", new KeywordToken(Keywords.For) },
                { "while", new KeywordToken(Keywords.While) },

                { "nand", new KeywordToken(Keywords.NAnd) },
                { "nor", new KeywordToken(Keywords.NOr) },
                { "and", new KeywordToken(Keywords.And) },
                { "or", new KeywordToken(Keywords.Or) },
                { "xor", new KeywordToken(Keywords.XOr) },
                { "not", new KeywordToken(Keywords.Not) },
                { "eq", new KeywordToken(Keywords.Eq) },
                { "impl", new KeywordToken(Keywords.Impl) },
                { "mod", new KeywordToken(Keywords.Mod) },
            };
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="index">The start index.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public IToken CreateToken(string function, ref int index)
        {
            if (!char.IsLetter(function[index]))
                return null;

            var endIndex = index + 1;
            while (endIndex < function.Length && char.IsLetterOrDigit(function[endIndex]))
                endIndex++;

            var id = function.Substring(index, endIndex - index); // TODO: span

            index = endIndex;

            if (keywords.TryGetValue(id, out var keyword))
                return keyword;

            return new IdToken(id);
        }
    }
}