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
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates variable tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class VariableTokenFactory : FactoryBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableTokenFactory"/> class.
        /// </summary>
        public VariableTokenFactory()
            : base(new Regex(@"\G([a-zα-ω][0-9a-zα-ω]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        { }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// The token.
        /// </returns>
        protected override FactoryResult CreateTokenInternal(Match match, ReadOnlyCollection<IToken> tokens)
        {
            if (tokens.LastOrDefault() is NumberToken)
                return new FactoryResult(new OperationToken(Operations.Multiplication), 0);

            var result = new FactoryResult();
            var variable = match.Value;

            if (variable == "i")
                result.Token = new ComplexNumberToken(Complex.ImaginaryOne);
            else
                result.Token = new VariableToken(variable);

            result.ProcessedLength = variable.Length;
            return result;
        }

    }

}