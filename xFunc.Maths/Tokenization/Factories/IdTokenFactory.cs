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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    internal class IdTokenFactory : ITokenFactory
    {
        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public IToken CreateToken(ref ReadOnlyMemory<char> function)
        {
            var span = function.Span;

            if (!char.IsLetter(span[0]))
                return null;

            var endIndex = 1;
            while (endIndex < span.Length && char.IsLetterOrDigit(span[endIndex]))
                endIndex++;

            var id = span[..endIndex];

            IToken token;

            if (id.Equals("true", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.True;
            else if (id.Equals("false", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.False;

            else if (id.Equals("def", StringComparison.OrdinalIgnoreCase) ||
                     id.Equals("define", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Define;
            else if (id.Equals("undef", StringComparison.OrdinalIgnoreCase) ||
                     id.Equals("undefine", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Undefine;

            else if (id.Equals("if", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.If;
            else if (id.Equals("for", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.For;
            else if (id.Equals("while", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.While;

            else if (id.Equals("nand", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.NAnd;
            else if (id.Equals("nor", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.NOr;
            else if (id.Equals("and", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.And;
            else if (id.Equals("or", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Or;
            else if (id.Equals("xor", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.XOr;
            else if (id.Equals("not", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Not;
            else if (id.Equals("eq", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Eq;
            else if (id.Equals("impl", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Impl;
            else if (id.Equals("mod", StringComparison.OrdinalIgnoreCase))
                token = KeywordToken.Mod;

            else
                token = new IdToken(id.ToString().ToLowerInvariant()); // TODO:

            function = function[endIndex..];

            return token;
        }
    }
}