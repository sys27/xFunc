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

namespace xFunc.Maths.Tokenization.Tokens
{

    /// <summary>
    /// Describes keywords.
    /// </summary>
    public enum Keywords
    {
        /// <summary>
        /// true
        /// </summary>
        True,
        /// <summary>
        /// false
        /// </summary>
        False,

        /// <summary>
        /// def, define
        /// </summary>
        Define,
        /// <summary>
        /// undef, undefine
        /// </summary>
        Undefine
    }

    /// <summary>
    /// Represents a keyword token.
    /// </summary>
    public class KeywordToken : IToken
    {

        /// <summary>
        /// Initializes the <see cref="KeywordToken"/> class.
        /// </summary>
        /// <param name="keyword">A keyword.</param>
        public KeywordToken(Keywords keyword)
        {
            this.Keyword = keyword;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (typeof(KeywordToken) != obj.GetType())
                return false;

            var token = (KeywordToken)obj;

            return this.Keyword == token.Keyword;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Keyword.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Keyword: {Keyword}";
        }

        /// <summary>
        /// Gets a keyword.
        /// </summary>
        public Keywords Keyword { get; }

    }

}