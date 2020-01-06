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
    /// Counts the amount of parameters.
    /// </summary>
    public class ParameterCounter : LexerPostProcessorBase
    {

        private int CountParametersInternal(IList<IToken> tokens, int index)
        {
            var func = (FunctionToken)tokens[index];

            var countOfParameters = 0;
            var brackets = 0;
            var i = index + 1;
            for (; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token is SymbolToken symbol)
                {
                    if (symbol.IsOpenSymbol())
                    {
                        brackets++;
                    }
                    else if (symbol.IsCloseSymbol())
                    {
                        brackets--;

                        if (brackets == 0)
                            break;
                    }
                    else if (symbol.IsComma())
                    {
                        countOfParameters++;
                    }
                }
                else
                {
                    if (countOfParameters == 0)
                        countOfParameters++;

                    if (token is FunctionToken)
                        i = CountParametersInternal(tokens, i);
                }
            }

            func.CountOfParameters = countOfParameters;

            return i;
        }

        /// <summary>
        /// The method for post processing of tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        public override void Process(IList<IToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
                if (tokens[i] is FunctionToken)
                    i = CountParametersInternal(tokens, i);
        }

    }

}