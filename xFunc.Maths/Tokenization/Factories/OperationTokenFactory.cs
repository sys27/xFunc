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
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class OperationTokenFactory : IAbstractTokenFactory
    {
        public IList<IToken> CreateToken(string match, IToken lastToken)
        {
            var tokens = new List<IToken>();

            if (match == "+=")
            {
                tokens.Add(new OperationToken(Operations.AddAssign));
            }
            else if (match == "-=" || match == "−=")
            {
                tokens.Add(new OperationToken(Operations.SubAssign));
            }
            else if (match == "*=" || match == "×=")
            {
                tokens.Add(new OperationToken(Operations.MulAssign));
            }
            else if (match == "*" || match == "×")
            {
                tokens.Add(new OperationToken(Operations.Multiplication));
            }
            else if (match == "/")
            {
                tokens.Add(new OperationToken(Operations.Division));
            }
            else if (match == "^")
            {
                tokens.Add(new OperationToken(Operations.Exponentiation));
            }
            else if (match == "!")
            {
                if (lastToken != null &&
                    ((lastToken is SymbolToken symbol && symbol.Symbol == Symbols.CloseBracket) ||
                    lastToken is NumberToken || lastToken is VariableToken))
                {
                    tokens.Add(new OperationToken(Operations.Factorial));
                }

                throw new LexerException(string.Format(Resource.NotSupportedSymbol, match));
            }
            else if (match == "%" || match == "mod")
            {
                tokens.Add(new OperationToken(Operations.Modulo));
            }
            else if (match == "&&")
            {
                tokens.Add(new OperationToken(Operations.ConditionalAnd));
            }
            else if (match == "||")
            {
                tokens.Add(new OperationToken(Operations.ConditionalOr));
            }
            else if (match == "==")
            {
                tokens.Add(new OperationToken(Operations.Equal));
            }
            else if (match == "!=")
            {
                tokens.Add(new OperationToken(Operations.NotEqual));
            }
            else if (match == "<=")
            {
                tokens.Add(new OperationToken(Operations.LessOrEqual));
            }
            else if (match == "<")
            {
                tokens.Add(new OperationToken(Operations.LessThan));
            }
            else if (match == ">=")
            {
                tokens.Add(new OperationToken(Operations.GreaterOrEqual));
            }
            else if (match == ">")
            {
                tokens.Add(new OperationToken(Operations.GreaterThan));
            }
            else if (match == "++")
            {
                tokens.Add(new OperationToken(Operations.Increment));
            }
            else if (match == "--" || match == "−−")
            {
                tokens.Add(new OperationToken(Operations.Decrement));
            }
            else if (match == "+")
            {
                if (lastToken != null ||
                    !(lastToken is SymbolToken symbolToken) ||
                    (symbolToken.Symbol != Symbols.OpenBracket && symbolToken.Symbol != Symbols.OpenBrace))
                    tokens.Add(new OperationToken(Operations.Addition));
            }
            else if (match == "-" || match == "−")
            {
                if (lastToken == null)
                {
                    tokens.Add(new OperationToken(Operations.UnaryMinus));
                }
                else
                {
                    if (lastToken is SymbolToken symbolToken && (symbolToken.Symbol == Symbols.OpenBracket ||
                            symbolToken.Symbol == Symbols.OpenBrace ||
                            symbolToken.Symbol == Symbols.Comma))
                    {
                        tokens.Add(new OperationToken(Operations.UnaryMinus));
                    }
                    else
                    {
                        if (lastToken is OperationToken operationToken &&
                            (operationToken.Operation == Operations.Exponentiation ||
                            operationToken.Operation == Operations.Multiplication ||
                            operationToken.Operation == Operations.Division ||
                            operationToken.Operation == Operations.Assign ||
                            operationToken.Operation == Operations.AddAssign ||
                            operationToken.Operation == Operations.SubAssign ||
                            operationToken.Operation == Operations.MulAssign ||
                            operationToken.Operation == Operations.DivAssign))
                        {
                            tokens.Add(new OperationToken(Operations.UnaryMinus));
                        }
                        else
                        {
                            tokens.Add(new OperationToken(Operations.Subtraction));
                        }
                    }
                }
            }
            else if (match == "/=")
            {
                tokens.Add(new OperationToken(Operations.DivAssign));
            }
            else if (match == ":=")
            {
                tokens.Add(new OperationToken(Operations.Assign));
            }
            else if (match == "not" || match == "~")
            {
                if (lastToken != null)
                {
                    if ((lastToken is SymbolToken symbol && symbol.Symbol == Symbols.CloseBracket) ||
                        lastToken is NumberToken || lastToken is VariableToken)
                        throw new LexerException(string.Format(Resource.NotSupportedSymbol, match));
                }

                tokens.Add(new OperationToken(Operations.Not));
            }
            else if (match == "and" || match == "&")
            {
                tokens.Add(new OperationToken(Operations.And));
            }
            else if (match == "or" || match == "|")
            {
                tokens.Add(new OperationToken(Operations.Or));
            }
            else if (match == "xor")
            {
                tokens.Add(new OperationToken(Operations.XOr));
            }
            else if (match == "impl" || match == "->" || match == "−>" || match == "=>")
            {
                tokens.Add(new OperationToken(Operations.Implication));
            }
            else if (match == "eq" || match == "<->" || match == "<−>" || match == "<=>")
            {
                tokens.Add(new OperationToken(Operations.Equality));
            }
            else if (match == "nor")
            {
                tokens.Add(new OperationToken(Operations.NOr));
            }
            else if (match == "nand")
            {
                tokens.Add(new OperationToken(Operations.NAnd));
            }
            else
            {
                throw new LexerException(string.Format(Resource.NotSupportedSymbol, match));
            }

            return tokens;
        }
    }
}