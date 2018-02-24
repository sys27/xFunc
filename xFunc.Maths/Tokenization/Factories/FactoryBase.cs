// Copyright 2012-2018 Dmitry Kischenko
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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The base for token factory.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    public abstract class FactoryBase : ITokenFactory
    {

        /// <summary>
        /// The regex.
        /// </summary>
        protected Regex regex;

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryBase"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        protected FactoryBase(Regex regex)
        {
            this.regex = regex;
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The token.</returns>
        protected abstract FactoryResult CreateTokenInternal(Match match, ReadOnlyCollection<IToken> tokens);

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public FactoryResult CreateToken(string function, int startIndex, ReadOnlyCollection<IToken> tokens)
        {
            var match = regex.Match(function, startIndex);
            if (match.Success)
                return CreateTokenInternal(match, tokens);

            return null;
        }

    }

}