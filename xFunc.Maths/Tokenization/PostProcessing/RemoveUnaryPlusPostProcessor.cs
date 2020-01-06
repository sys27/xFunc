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
    /// Removes unnecessary unary plus tokens.
    /// </summary>
    public class RemoveUnaryPlusPostProcessor : LexerPostProcessorBase
    {

        /// <summary>
        /// The method for post processing of tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        public override void Process(IList<IToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i] as OperationToken;
                if (token?.Operation != Operations.Addition)
                    continue;

                var previousToken = GetPreviousToken(tokens, i);
                if (previousToken == null || IsOpenSymbol(previousToken))
                {
                    tokens.RemoveAt(i);
                    i--;
                }
            }
        }

        private bool IsOpenSymbol(IToken token)
        {
            return token is SymbolToken symbolToken && symbolToken.IsOpenSymbol();
        }

    }

}