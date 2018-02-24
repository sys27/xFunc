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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{
    public class OpeartionTokenFactory : FactoryBase
    {
        public OpeartionTokenFactory() : base(new Regex(@"\G([^a-zα-ω0-9(){},°\s]+|nand|nor|and|or|xor|not|eq|impl|mod)", RegexOptions.Compiled | RegexOptions.IgnoreCase)) { }

        protected override FactoryResult CreateTokenInternal(Match match, ReadOnlyCollection<IToken> tokens)
        {
            var result = new FactoryResult();
            var operation = match.Value.ToLower();

            if (operation == "+=")
            {
                result.Token = new OperationToken(Operations.AddAssign);
            }
            else if (operation == "-=" || operation == "−=")
            {
                result.Token = new OperationToken(Operations.SubAssign);
            }
            else if (operation == "*=" || operation == "×=")
            {
                result.Token = new OperationToken(Operations.MulAssign);
            }
            else if (operation == "*" || operation == "×")
            {
                result.Token = new OperationToken(Operations.Multiplication);
            }
            else if (operation == "/")
            {
                result.Token = new OperationToken(Operations.Division);
            }
            else if (operation == "^")
            {
                result.Token = new OperationToken(Operations.Exponentiation);
            }
            else if (operation == "!")
            {
                var lastToken = tokens.LastOrDefault();
                if (lastToken == null ||
                    !((lastToken is SymbolToken symbol && symbol.Symbol == Symbols.CloseBracket) ||
                       lastToken is NumberToken ||
                       lastToken is VariableToken))
                {
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, operation));
                }

                result.Token = new OperationToken(Operations.Factorial);
            }
            else if (operation == "%" || operation == "mod")
            {
                result.Token = new OperationToken(Operations.Modulo);
            }
            else if (operation == "&&")
            {
                result.Token = new OperationToken(Operations.ConditionalAnd);
            }
            else if (operation == "||")
            {
                result.Token = new OperationToken(Operations.ConditionalOr);
            }
            else if (operation == "==")
            {
                result.Token = new OperationToken(Operations.Equal);
            }
            else if (operation == "!=")
            {
                result.Token = new OperationToken(Operations.NotEqual);
            }
            else if (operation == "<=")
            {
                result.Token = new OperationToken(Operations.LessOrEqual);
            }
            else if (operation == "<")
            {
                result.Token = new OperationToken(Operations.LessThan);
            }
            else if (operation == ">=")
            {
                result.Token = new OperationToken(Operations.GreaterOrEqual);
            }
            else if (operation == ">")
            {
                result.Token = new OperationToken(Operations.GreaterThan);
            }
            else if (operation == "++")
            {
                result.Token = new OperationToken(Operations.Increment);
            }
            else if (operation == "--" || operation == "−−")
            {
                result.Token = new OperationToken(Operations.Decrement);
            }
            else if (operation == "+")
            {
                var lastToken = tokens.LastOrDefault();
                if (lastToken == null)
                {
                    return null;
                }
                else
                {
                    if (lastToken is SymbolToken symbolToken &&
                        (symbolToken.Symbol == Symbols.OpenBracket || symbolToken.Symbol == Symbols.OpenBrace))
                        return null;
                }

                result.Token = new OperationToken(Operations.Addition);
            }
            else if (operation == "-" || operation == "−")
            {
                var lastToken = tokens.LastOrDefault();
                if (lastToken == null)
                {
                    result.Token = new OperationToken(Operations.UnaryMinus);
                }
                else
                {
                    if (lastToken is SymbolToken symbolToken && (symbolToken.Symbol == Symbols.OpenBracket ||
                            symbolToken.Symbol == Symbols.OpenBrace ||
                            symbolToken.Symbol == Symbols.Comma))
                    {
                        result.Token = new OperationToken(Operations.UnaryMinus);
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
                            result.Token = new OperationToken(Operations.UnaryMinus);
                        }
                        else
                        {
                            result.Token = new OperationToken(Operations.Subtraction);
                        }
                    }
                }
            }
            else if (operation == "/=")
            {
                result.Token = new OperationToken(Operations.DivAssign);
            }
            else if (operation == ":=")
            {
                result.Token = new OperationToken(Operations.Assign);
            }
            else if (operation == "not" || operation == "~")
            {
                var lastToken = tokens.LastOrDefault();
                if (lastToken != null)
                {
                    if ((lastToken is SymbolToken symbol && symbol.Symbol == Symbols.CloseBracket) ||
                        lastToken is NumberToken || lastToken is VariableToken)
                        throw new LexerException(string.Format(Resource.NotSupportedSymbol, operation));
                }

                result.Token = new OperationToken(Operations.Not);
            }
            else if (operation == "and" || operation == "&")
            {
                result.Token = new OperationToken(Operations.And);
            }
            else if (operation == "or" || operation == "|")
            {
                result.Token = new OperationToken(Operations.Or);
            }
            else if (operation == "xor")
            {
                result.Token = new OperationToken(Operations.XOr);
            }
            else if (operation == "impl" || operation == "->" || operation == "−>" || operation == "=>")
            {
                result.Token = new OperationToken(Operations.Implication);
            }
            else if (operation == "eq" || operation == "<->" || operation == "<−>" || operation == "<=>")
            {
                result.Token = new OperationToken(Operations.Equality);
            }
            else if (operation == "nor")
            {
                result.Token = new OperationToken(Operations.NOr);
            }
            else if (operation == "nand")
            {
                result.Token = new OperationToken(Operations.NAnd);
            }
            else
            {
                throw new LexerException(string.Format(Resource.NotSupportedSymbol, operation));
            }

            result.ProcessedLength = match.Length;
            return result;
        }
    }
}