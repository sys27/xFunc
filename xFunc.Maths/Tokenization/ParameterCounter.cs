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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{
    internal class ParameterCounter
    {
        private int CountParametersInternal(IList<IToken> tokens, int index)
        {
            var func = (FunctionToken)tokens[index];

            int countOfParams = 0;
            int brackets = 1;
            bool hasBraces = false;
            bool oneParam = true;
            int i = index + 2;
            for (; i < tokens.Count;)
            {
                var token = tokens[i];
                if (token is SymbolToken symbol)
                {
                    if (symbol.Symbol == Symbols.OpenBrace)
                    {
                        hasBraces = true;
                    }

                    if (symbol.Symbol == Symbols.CloseBracket || symbol.Symbol == Symbols.CloseBrace)
                    {
                        brackets--;

                        if (brackets == 0)
                            break;
                    }
                    else if (symbol.Symbol == Symbols.OpenBracket || symbol.Symbol == Symbols.OpenBrace)
                    {
                        brackets++;

                        if (oneParam)
                        {
                            countOfParams++;
                            oneParam = false;
                        }
                    }
                    else if (symbol.Symbol == Symbols.Comma)
                    {
                        oneParam = true;
                    }

                    i++;
                }
                else if (token is FunctionToken function)
                {
                    if (oneParam)
                    {
                        countOfParams++;
                        oneParam = false;
                    }

                    if (function.Function == Functions.Matrix || function.Function == Functions.Vector)
                        hasBraces = true;

                    i = CountParametersInternal(tokens, i) + 1;
                }
                else
                {
                    if (oneParam)
                    {
                        countOfParams++;
                        oneParam = false;
                    }

                    i++;
                }
            }

            if (func.Function == Functions.Vector && hasBraces)
                tokens[index] = new FunctionToken(Functions.Matrix, countOfParams);
            else
                func.CountOfParams = countOfParams;

            return i;
        }

        /// <summary>
        /// Calculates the number of parametes of functions.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        /// <returns>The list of tokens.</returns>
        public IEnumerable<IToken> CountParameters(IList<IToken> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
                if (tokens[i] is FunctionToken)
                    i = CountParametersInternal(tokens, i);

            return tokens;
        }
    }
}