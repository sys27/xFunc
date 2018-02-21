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
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class SymbolTokenFactory : IAbstractTokenFactory
    {
        public IList<IToken> CreateToken(string match, IToken lastToken)
        {
            var tokens = new List<IToken>();
            if (match == "(")
            {
                tokens.Add(new SymbolToken(Symbols.OpenBracket));
            }
            else if (match == ")")
            {
                if (lastToken is SymbolToken lastTokenSymbol && lastTokenSymbol.Symbol == Symbols.Comma)
                    throw new LexerException(Resource.NotEnoughParams);

                tokens.Add(new SymbolToken(Symbols.CloseBracket));
            }
            else if (match == "{")
            {
                if (!(tokens.LastOrDefault() is FunctionToken))
                    tokens.Add(new FunctionToken(Functions.Vector));

                tokens.Add(new SymbolToken(Symbols.OpenBrace));
            }
            else if (match == "}")
            {
                tokens.Add(new SymbolToken(Symbols.CloseBrace));
            }
            else if (match == ",")
            {
                tokens.Add(new SymbolToken(Symbols.Comma));
            }
            else
            {
                throw new LexerException(string.Format(Resource.NotSupportedSymbol, match));
            }

            return tokens;
        }
    }
}