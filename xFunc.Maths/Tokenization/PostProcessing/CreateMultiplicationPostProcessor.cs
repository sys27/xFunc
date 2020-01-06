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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.PostProcessing
{

    /// <summary>
    /// Inserts the multiplication operation between number and function or open bracket.
    /// </summary>
    public class CreateMultiplicationPostProcessor : LexerPostProcessorBase
    {

        private readonly OperationToken mul = new OperationToken(Operations.Multiplication);

        /// <summary>
        /// The method for post processing of tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        public override void Process(IList<IToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token is FunctionToken ||
                    token is VariableToken ||
                    (token is SymbolToken symbol && symbol.IsOpenSymbol()))
                {
                    var previousToken = GetPreviousToken(tokens, i);
                    if (previousToken is NumberToken)
                    {
                        tokens.Insert(i, mul);
                        i++;
                    }
                }
            }
        }

    }

}