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
using System.Text.RegularExpressions;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates empty result (without token).
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class EmptyTokenFactory : FactoryBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTokenFactory"/> class.
        /// </summary>
        public EmptyTokenFactory() : base(new Regex(@"\G\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) { }

        /// <summary>
        /// Creates the empty result (without token).
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>
        /// The token.
        /// </returns>
        protected override FactoryResult CreateTokenInternal(Match match)
        {
            return new FactoryResult(null, match.Length);
        }

    }

}