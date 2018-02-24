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
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class NumberHexTokenFactory : FactoryBase
    {
        public NumberHexTokenFactory() : base(new Regex(@"\G[+-]?0x[0-9a-f]+", RegexOptions.Compiled | RegexOptions.IgnoreCase)) { }

        protected override FactoryResult CreateTokenInternal(Match match, ReadOnlyCollection<IToken> tokens)
        {
            var token = new NumberToken(Convert.ToInt64(match.Value, 16));

            return new FactoryResult(token, match.Length);
        }
    }
}