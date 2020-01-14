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
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates variable tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class IdTokenFactory : FactoryBase
    {

        private readonly Dictionary<string, KeywordToken> keywords;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdTokenFactory"/> class.
        /// </summary>
        public IdTokenFactory()
            : base(new Regex(@"\G([a-zα-ω][0-9a-zα-ω]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            // TODO: lazy
            keywords = new Dictionary<string, KeywordToken>
            {
                { "true", new KeywordToken(Keywords.True) },
                { "false", new KeywordToken(Keywords.False) },

                // TODO: copies?
                { "def", new KeywordToken(Keywords.Define) },
                { "define", new KeywordToken(Keywords.Define) },
                { "undef", new KeywordToken(Keywords.Undefine) },
                { "undefine", new KeywordToken(Keywords.Undefine) },
            };
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>
        /// The token.
        /// </returns>
        protected override FactoryResult CreateTokenInternal(Match match)
        {
            var result = new FactoryResult();
            var id = match.Value;

            if (keywords.TryGetValue(id, out var keyword))
                return new FactoryResult(keyword, id.Length);

            return new FactoryResult(new IdToken(id), id.Length);
        }

    }

}