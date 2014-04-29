// Copyright 2012-2014 Dmitry Kischenko
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
using System.Globalization;
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    public class Lexer : ILexer
    {

#if NET35_OR_GREATER || PORTABLE
        private readonly HashSet<string> notVar;
#elif NET20_OR_GREATER
        private readonly List<string> notVar;
#endif

        /// <summary>
        /// Initializes a new instance of <see cref="Lexer"/>.
        /// </summary>
        public Lexer()
        {
#if NET35_OR_GREATER || PORTABLE
            notVar = new HashSet<string> { "and", "or", "xor" };
#elif NET20_OR_GREATER
            notVar = new List<string> { "and", "or", "xor" };
#endif
        }

        private bool IsBalanced(string str)
        {
            int brackets = 0;
            int braces = 0;

            foreach (var item in str)
            {
                if (item == '(') brackets++;
                else if (item == ')') brackets--;
                else if (item == '{') braces++;
                else if (item == '}') braces--;

                if (brackets < 0)
                    return false;
                if (braces < 0)
                    return false;
            }

            return brackets == 0 && braces == 0;
        }

        /// <summary>
        /// Converts the string into a sequence of tokens.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The sequence of tokens.</returns>
        /// <seealso cref="IToken"/>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="function"/> parameter is null or empty.</exception>
        /// <exception cref="LexerException">Throws when <paramref name="function"/> has the not supported symbol.</exception>
        public IEnumerable<IToken> Tokenize(string function)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(function))
#elif NET20_OR_GREATER
            if (StringExtention.IsNullOrWhiteSpace(function))
#endif
                throw new ArgumentNullException("function", Resource.NotSpecifiedFunction);

            function = function.ToLower().Replace(" ", "");
            if (!IsBalanced(function))
                throw new LexerException(Resource.NotBalanced);
            var tokens = new List<IToken>();

            for (int i = 0; i < function.Length; )
            {
                char letter = function[i];
                if (letter == '(')
                {
                    tokens.Add(new SymbolToken(Symbols.OpenBracket));
                }
                else if (letter == '{')
                {
                    if (!(tokens.LastOrDefault() is FunctionToken))
                        tokens.Add(new FunctionToken(Functions.Vector));

                    tokens.Add(new SymbolToken(Symbols.OpenBrace));
                }
                else if (letter == ')')
                {
                    tokens.Add(new SymbolToken(Symbols.CloseBracket));
                }
                else if (letter == '}')
                {
                    tokens.Add(new SymbolToken(Symbols.CloseBrace));
                }
                else if (letter == '+')
                {
                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (pre == '(' || pre == '{')
                        {
                            i++;
                            continue;
                        }
                    }
                    else
                    {
                        i++;
                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.Addition));
                }
                else if (letter == '-')
                {
                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (pre == '(' || pre == '{')
                        {
                            tokens.Add(new OperationToken(Operations.UnaryMinus));

                            i++;
                            continue;
                        }
                    }
                    else
                    {
                        tokens.Add(new OperationToken(Operations.UnaryMinus));

                        i++;
                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.Subtraction));
                }
                else if (letter == '*')
                {
                    tokens.Add(new OperationToken(Operations.Multiplication));
                }
                else if (letter == '/')
                {
                    tokens.Add(new OperationToken(Operations.Division));
                }
                else if (letter == '^')
                {
                    tokens.Add(new OperationToken(Operations.Exponentiation));
                }
                else if (letter == ',')
                {
                    tokens.Add(new SymbolToken(Symbols.Comma));
                }
                else if (letter == '~')
                {
                    var lastToken = tokens.LastOrDefault();
                    if (lastToken != null)
                    {
                        var symbol = lastToken as SymbolToken;
                        if ((symbol != null && symbol.Symbol == Symbols.CloseBracket) || lastToken is NumberToken || lastToken is VariableToken)
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                    }

                    tokens.Add(new OperationToken(Operations.BitwiseNot));
                }
                else if (letter == '&')
                {
                    if (i + 1 < function.Length && function[i + 1] == '&')
                    {
                        tokens.Add(new OperationToken(Operations.ConditionalAnd));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.BitwiseAnd));
                }
                else if (letter == '|')
                {
                    if (i + 1 < function.Length && function[i + 1] == '|')
                    {
                        tokens.Add(new OperationToken(Operations.ConditionalOr));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.BitwiseOr));
                }
                else if (letter == ':' && i + 1 < function.Length && function[i + 1] == '=')
                {
                    tokens.Add(new OperationToken(Operations.Assign));
                    i += 2;

                    continue;
                }
                else if (letter == '=' && i + 1 < function.Length && function[i + 1] == '=')
                {
                    tokens.Add(new OperationToken(Operations.Equal));
                    i += 2;

                    continue;
                }
                else if (letter == '!')
                {
                    var lastToken = tokens.LastOrDefault();
                    if (lastToken != null)
                    {
                        var symbol = lastToken as SymbolToken;
                        if ((symbol != null && symbol.Symbol == Symbols.CloseBracket) || lastToken is NumberToken)
                        {
                            tokens.Add(new OperationToken(Operations.Factorial));
                            i++;

                            continue;
                        }
                    }

                    if (i + 1 < function.Length && function[i + 1] == '=')
                    {
                        tokens.Add(new OperationToken(Operations.NotEqual));
                        i += 2;

                        continue;
                    }

                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }
                else if (letter == '<')
                {
                    if (i + 1 < function.Length && function[i + 1] == '=')
                    {
                        tokens.Add(new OperationToken(Operations.LessOrEqual));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.LessThan));
                }
                else if (letter == '>')
                {
                    if (i + 1 < function.Length && function[i + 1] == '=')
                    {
                        tokens.Add(new OperationToken(Operations.GreaterOrEqual));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.GreaterThan));
                }
                else if (char.IsDigit(letter))
                {
                    int length;
                    int j;
                    string strNumber;
                    double number;

                    if (letter == '0' && i + 1 < function.Length)
                    {
                        var nextLetter = function[i + 1];
                        if (nextLetter == 'x' && i + 2 < function.Length)
                        {
                            i += 2;

                            length = 1;
                            for (j = i + 1; j < function.Length && (char.IsDigit(function[j]) || (function[j] >= 97 && function[j] <= 102)); j++)
                                length++;

                            strNumber = function.Substring(i, length);
                            number = Convert.ToInt64(strNumber, 16);
                            tokens.Add(new NumberToken(number));

                            i += length;
                            continue;
                        }
                        if (nextLetter == 'b' && i + 2 < function.Length)
                        {
                            i += 2;

                            length = 1;
                            for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
                                length++;

                            strNumber = function.Substring(i, length);
                            number = Convert.ToInt64(strNumber, 2);
                            tokens.Add(new NumberToken(number));

                            i += length;
                            continue;
                        }
                        if (char.IsDigit(nextLetter))
                        {
                            length = 1;
                            for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
                                length++;

                            strNumber = function.Substring(i, length);
                            number = Convert.ToInt64(strNumber, 8);
                            tokens.Add(new NumberToken(number));

                            i += length;
                            continue;
                        }
                    }

                    length = 1;
                    for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
                        length++;

                    if (j < function.Length && function[j] == '.')
                    {
                        length++;
                        for (j += 1; j < function.Length && char.IsDigit(function[j]); j++)
                            length++;
                    }

                    strNumber = function.Substring(i, length);
                    number = double.Parse(strNumber, CultureInfo.InvariantCulture);
                    tokens.Add(new NumberToken(number));

                    i += length;

                    var f = function.Substring(i);
                    if (i < function.Length && char.IsLetter(function[i]) && !notVar.Any(f.StartsWith))
                    {
                        tokens.Add(new OperationToken(Operations.Multiplication));
                    }

                    continue;
                }
                else if (char.IsLetter(letter))
                {
                    var sub = function.Substring(i);
                    if (sub.StartsWith("pi"))
                    {
                        tokens.Add(new VariableToken("π"));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("vector{"))
                    {
                        tokens.Add(new FunctionToken(Functions.Vector));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("matrix{"))
                    {
                        tokens.Add(new FunctionToken(Functions.Matrix));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("exp("))
                    {
                        tokens.Add(new FunctionToken(Functions.Exp));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("abs("))
                    {
                        tokens.Add(new FunctionToken(Functions.Absolute));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sinh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("ch("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cosh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("th("))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tanh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("cth("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("coth("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sech("))
                    {
                        tokens.Add(new FunctionToken(Functions.Secanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("csch("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsinh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arch("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arcosh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arth("))
                    {
                        tokens.Add(new FunctionToken(Functions.Artangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("artanh("))
                    {
                        tokens.Add(new FunctionToken(Functions.Artangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcth("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcotangenth));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arcoth("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcotangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arsch("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsecanth));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arsech("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsch("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sin("))
                    {
                        tokens.Add(new FunctionToken(Functions.Sine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cosec("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("csc("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cos("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("tg("))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tan("))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cot(") || sub.StartsWith("ctg("))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sec("))
                    {
                        tokens.Add(new FunctionToken(Functions.Secant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("arcsin("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccosec("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("arccsc("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccos("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arctg("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccot(") || sub.StartsWith("arcctg("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccotangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsec("))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sqrt("))
                    {
                        tokens.Add(new FunctionToken(Functions.Sqrt));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("root("))
                    {
                        tokens.Add(new FunctionToken(Functions.Root));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("lg("))
                    {
                        tokens.Add(new FunctionToken(Functions.Lg));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("ln("))
                    {
                        tokens.Add(new FunctionToken(Functions.Ln));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("log("))
                    {
                        tokens.Add(new FunctionToken(Functions.Log));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("deriv("))
                    {
                        tokens.Add(new FunctionToken(Functions.Derivative));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("simplify("))
                    {
                        tokens.Add(new FunctionToken(Functions.Simplify));
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("def("))
                    {
                        tokens.Add(new FunctionToken(Functions.Define));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("undef("))
                    {
                        tokens.Add(new FunctionToken(Functions.Undefine));
                        i += 5;

                        continue;
                    }

                    if (sub.StartsWith("not("))
                    {
                        tokens.Add(new OperationToken(Operations.BitwiseNot));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("and"))
                    {
                        tokens.Add(new OperationToken(Operations.BitwiseAnd));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("or"))
                    {
                        tokens.Add(new OperationToken(Operations.BitwiseOr));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("xor"))
                    {
                        tokens.Add(new OperationToken(Operations.BitwiseXOr));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("gcd(") || sub.StartsWith("gcf(") || sub.StartsWith("hcf("))
                    {
                        tokens.Add(new FunctionToken(Functions.GCD));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("lcm(") || sub.StartsWith("scm("))
                    {
                        tokens.Add(new FunctionToken(Functions.LCM));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("fact("))
                    {
                        tokens.Add(new FunctionToken(Functions.Factorial));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sum("))
                    {
                        tokens.Add(new FunctionToken(Functions.Sum));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("product("))
                    {
                        tokens.Add(new FunctionToken(Functions.Product));
                        i += 7;

                        continue;
                    }
                    if (sub.StartsWith("round("))
                    {
                        tokens.Add(new FunctionToken(Functions.Round));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("floor("))
                    {
                        tokens.Add(new FunctionToken(Functions.Floor));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("ceil("))
                    {
                        tokens.Add(new FunctionToken(Functions.Ceil));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("transpose("))
                    {
                        tokens.Add(new FunctionToken(Functions.Transpose));
                        i += 9;

                        continue;
                    }
                    if (sub.StartsWith("determinant("))
                    {
                        tokens.Add(new FunctionToken(Functions.Determinant));
                        i += 11;

                        continue;
                    }
                    if (sub.StartsWith("det("))
                    {
                        tokens.Add(new FunctionToken(Functions.Determinant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("inverse("))
                    {
                        tokens.Add(new FunctionToken(Functions.Inverse));
                        i += 7;

                        continue;
                    }
                    if (sub.StartsWith("if("))
                    {
                        tokens.Add(new FunctionToken(Functions.If));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("for("))
                    {
                        tokens.Add(new FunctionToken(Functions.For));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("while("))
                    {
                        tokens.Add(new FunctionToken(Functions.While));
                        i += 5;

                        continue;
                    }

                    int j = i + 1;
                    for (; j < function.Length && char.IsLetter(function[j]) && !notVar.Any(s => function.Substring(j).StartsWith(s)); j++) ;

                    var str = function.Substring(i, j - i);
                    i = j;

                    if (i < function.Length && function[i] == '(')
                        tokens.Add(new UserFunctionToken(str));
                    else
                        tokens.Add(new VariableToken(str));

                    continue;
                }
                else
                {
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                i++;
            }

            return CountParams(tokens);
        }

        private int _CountParams(List<IToken> tokens, int index)
        {
            var func = (FunctionToken)tokens[index];

            int countOfParams = 0;
            int brackets = 1;
            bool hasBraces = false;
            bool oneParam = true;
            int i = index + 2;
            for (; i < tokens.Count; )
            {
                var token = tokens[i];
                if (token is SymbolToken)
                {
                    var symbol = token as SymbolToken;
                    if (symbol.Symbol == Symbols.OpenBrace)
                        hasBraces = true;

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
                else if (token is FunctionToken)
                {
                    if (oneParam)
                    {
                        countOfParams++;
                        oneParam = false;
                    }

                    var f = (FunctionToken)token;
                    if (f.Function == Functions.Matrix || f.Function == Functions.Vector)
                        hasBraces = true;

                    i = _CountParams(tokens, i) + 1;
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

        private IEnumerable<IToken> CountParams(List<IToken> tokens)
        {
            for (int i = 0; i < tokens.Count; )
            {
                if (tokens[i] is FunctionToken)
                    i = _CountParams(tokens, i) + 1;
                else
                    i++;
            }

            return tokens;
        }

    }

}
