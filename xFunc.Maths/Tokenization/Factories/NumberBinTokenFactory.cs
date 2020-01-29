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
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates number tokens (from binary format).
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class NumberBinTokenFactory : FactoryBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberBinTokenFactory"/> class.
        /// </summary>
        public NumberBinTokenFactory() : base(new Regex(@"\G0b([01]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) { }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>
        /// The token.
        /// </returns>
        protected override FactoryResult CreateTokenInternal(Match match)
        {
            var token = new NumberToken(Convert.ToInt64(match.Groups[1].Value, 2));

            return new FactoryResult(token, match.Length);
        }

    }

}