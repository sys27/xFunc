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
        /// true
        /// </summary>
        public static KeywordToken True { get; } = new KeywordToken("true");

        /// <summary>
        /// false
        /// </summary>
        public static KeywordToken False { get; } = new KeywordToken("false");

        /// <summary>
        /// def, define
        /// </summary>
        public static KeywordToken Define { get; } = new KeywordToken("def");

        /// <summary>
        /// undef, undefine
        /// </summary>
        public static KeywordToken Undefine { get; } = new KeywordToken("undef");

        /// <summary>
        /// if
        /// </summary>
        public static KeywordToken If { get; } = new KeywordToken("if");

        /// <summary>
        /// for
        /// </summary>
        public static KeywordToken For { get; } = new KeywordToken("for");

        /// <summary>
        /// while
        /// </summary>
        public static KeywordToken While { get; } = new KeywordToken("while");

        /// <summary>
        /// nand
        /// </summary>
        public static KeywordToken NAnd { get; } = new KeywordToken("nand");

        /// <summary>
        /// nor
        /// </summary>
        public static KeywordToken NOr { get; } = new KeywordToken("nor");

        /// <summary>
        /// and
        /// </summary>
        public static KeywordToken And { get; } = new KeywordToken("and");

        /// <summary>
        /// Or
        /// </summary>
        public static KeywordToken Or { get; } = new KeywordToken("or");

        /// <summary>
        /// xor
        /// </summary>
        public static KeywordToken XOr { get; } = new KeywordToken("xor");

        /// <summary>
        /// not
        /// </summary>
        public static KeywordToken Not { get; } = new KeywordToken("not");

        /// <summary>
        /// eq
        /// </summary>
        public static KeywordToken Eq { get; } = new KeywordToken("eq");

        /// <summary>
        /// impl
        /// </summary>
        public static KeywordToken Impl { get; } = new KeywordToken("impl");

        /// <summary>
        /// mod
        /// </summary>
        public static KeywordToken Mod { get; } = new KeywordToken("mod");
    }
}