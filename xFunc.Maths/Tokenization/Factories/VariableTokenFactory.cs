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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class VariableTokenFactory : FactoryBase
    {
        public VariableTokenFactory()
            : base(new Regex(@"\G([a-zα-ω][0-9a-zα-ω]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase))
        { }

        protected override FactoryResult CreateTokenInternal(Match match, IReadOnlyList<IToken> tokens)
        {
            if (tokens.LastOrDefault() is NumberToken)
                return new FactoryResult(new OperationToken(Operations.Multiplication), 0);

            var result = new FactoryResult();
            var variable = match.Value;

            if (variable == "i")
                result.Token = new ComplexNumberToken(Complex.ImaginaryOne);
            else if (variable == "pi")
                result.Token = new VariableToken("π");
            else
                result.Token = new VariableToken(variable);

            result.ProcessedLength = variable.Length;
            return result;
        }
    }
}