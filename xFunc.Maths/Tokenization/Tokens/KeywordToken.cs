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

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a keyword token.
    /// </summary>
    [DebuggerDisplay("Keyword: {" + nameof(keyword) + "}")]
    public sealed class KeywordToken : IToken
    {
        private readonly string keyword;

        private KeywordToken(string keyword)
        {
            this.keyword = keyword;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => keyword;

        /// <summary>
        /// Gets the 'true' token.
        /// </summary>
        public static KeywordToken True { get; } = new KeywordToken("true");

        /// <summary>
        /// Gets the 'false' token.
        /// </summary>
        public static KeywordToken False { get; } = new KeywordToken("false");

        /// <summary>
        /// Gets the 'def, define' token.
        /// </summary>
        public static KeywordToken Define { get; } = new KeywordToken("def");

        /// <summary>
        /// Gets the 'undef, undefine' token.
        /// </summary>
        public static KeywordToken Undefine { get; } = new KeywordToken("undef");

        /// <summary>
        /// Gets the 'if' token.
        /// </summary>
        public static KeywordToken If { get; } = new KeywordToken("if");

        /// <summary>
        /// Gets the 'for' token.
        /// </summary>
        public static KeywordToken For { get; } = new KeywordToken("for");

        /// <summary>
        /// Gets the 'while' token.
        /// </summary>
        public static KeywordToken While { get; } = new KeywordToken("while");

        /// <summary>
        /// Gets the 'nand' token.
        /// </summary>
        public static KeywordToken NAnd { get; } = new KeywordToken("nand");

        /// <summary>
        /// Gets the 'nor' token.
        /// </summary>
        public static KeywordToken NOr { get; } = new KeywordToken("nor");

        /// <summary>
        /// Gets the 'and' token.
        /// </summary>
        public static KeywordToken And { get; } = new KeywordToken("and");

        /// <summary>
        /// Gets the 'Or' token.
        /// </summary>
        public static KeywordToken Or { get; } = new KeywordToken("or");

        /// <summary>
        /// Gets the 'xor' token.
        /// </summary>
        public static KeywordToken XOr { get; } = new KeywordToken("xor");

        /// <summary>
        /// Gets the 'not' token.
        /// </summary>
        public static KeywordToken Not { get; } = new KeywordToken("not");

        /// <summary>
        /// Gets the 'eq' token.
        /// </summary>
        public static KeywordToken Eq { get; } = new KeywordToken("eq");

        /// <summary>
        /// Gets the 'impl' token.
        /// </summary>
        public static KeywordToken Impl { get; } = new KeywordToken("impl");

        /// <summary>
        /// Gets the 'mod' token.
        /// </summary>
        public static KeywordToken Mod { get; } = new KeywordToken("mod");
    }
}