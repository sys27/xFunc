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
    /// Inserts the unary minus token.
    /// </summary>
    public class CreateUnaryMinusPostProcessor : LexerPostProcessorBase
    {

        private readonly OperationToken unaryMinus = new OperationToken(Operations.UnaryMinus);

        /// <summary>
        /// The method for post processing of tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        public override void Process(IList<IToken> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i] as OperationToken;
                if (token?.Operation != Operations.Subtraction)
                    continue;

                var previousToken = GetPreviousToken(tokens, i);
                if (previousToken == null ||
                    IsOpenSymbolOrComma(previousToken) ||
                    IsOperationToken(previousToken))
                {
                    tokens[i] = unaryMinus;
                }
            }
        }

        private bool IsOpenSymbolOrComma(IToken token)
        {
            return token is SymbolToken symbolToken &&
                   (symbolToken.IsOpenSymbol() || symbolToken.IsComma());
        }

        private bool IsOperationToken(IToken token)
        {
            return token is OperationToken operationToken &&
                   (operationToken.Operation == Operations.Exponentiation ||
                    operationToken.Operation == Operations.Multiplication ||
                    operationToken.Operation == Operations.Division ||
                    operationToken.Operation == Operations.Assign ||
                    operationToken.Operation == Operations.AddAssign ||
                    operationToken.Operation == Operations.SubAssign ||
                    operationToken.Operation == Operations.MulAssign ||
                    operationToken.Operation == Operations.DivAssign);
        }

    }

}