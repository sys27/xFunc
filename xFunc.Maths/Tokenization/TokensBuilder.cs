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
using System.Collections.Generic;
using System.Numerics;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{

    public class TokensBuilder
    {

        private readonly IList<IToken> tokens;

        public TokensBuilder()
        {
            tokens = new List<IToken>();
        }

        public TokensBuilder Boolean(bool value)
        {
            tokens.Add(new BooleanToken(value));

            return this;
        }

        public TokensBuilder ComplexNumber(Complex complex)
        {
            tokens.Add(new ComplexNumberToken(complex));

            return this;
        }

        public TokensBuilder Function(Functions function, int countOfParams = 1)
        {
            tokens.Add(new FunctionToken(function, countOfParams));

            return this;
        }

        public TokensBuilder Number(double number)
        {
            tokens.Add(new NumberToken(number));

            return this;
        }

        public TokensBuilder Operation(Operations operation)
        {
            tokens.Add(new OperationToken(operation));

            return this;
        }

        public TokensBuilder Symbol(Symbols symbol)
        {
            tokens.Add(new SymbolToken(symbol));

            return this;
        }

        public TokensBuilder OpenBracket()
        {
            tokens.Add(new SymbolToken(Symbols.OpenBracket));

            return this;
        }

        public TokensBuilder CloseBracket()
        {
            tokens.Add(new SymbolToken(Symbols.CloseBracket));

            return this;
        }

        public TokensBuilder Comma()
        {
            tokens.Add(new SymbolToken(Symbols.Comma));

            return this;
        }

        public TokensBuilder UserFunction(string function, int countOfParams)
        {
            tokens.Add(new UserFunctionToken(function, countOfParams));

            return this;
        }

        public TokensBuilder Variable(string variable)
        {
            tokens.Add(new VariableToken(variable));

            return this;
        }

        public TokensBuilder VariableX()
        {
            tokens.Add(new VariableToken("x"));

            return this;
        }

        public TokensBuilder VariableY()
        {
            tokens.Add(new VariableToken("y"));

            return this;
        }

        public IList<IToken> Tokens
        {
            get
            {
                return tokens;
            }
        }

    }

}