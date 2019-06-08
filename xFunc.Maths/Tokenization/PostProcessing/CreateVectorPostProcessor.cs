// Copyright 2012-2019 Dmitry Kischenko
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
    /// Inserts the vector token.
    /// </summary>
    public class CreateVectorPostProcessor : LexerPostProcessorBase
    {

        private readonly FunctionToken vector = new FunctionToken(Functions.Vector);

        /// <summary>
        /// The method for post processing of tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        public override void Process(IList<IToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token is SymbolToken symbolToken && symbolToken.Is(Symbols.OpenBrace))
                {
                    var previousToken = GetPreviousToken(tokens, i);
                    if (previousToken is FunctionToken)
                        continue;

                    tokens.Insert(i, vector);
                    i++;
                }
            }
        }

    }

}