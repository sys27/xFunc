// Copyright 2012-2013 Dmitry Kischenko
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
using System.Linq;
using xFunc.Maths.Exceptions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    public class MathLexer : IMathLexer
    {

        private readonly HashSet<string> notVar;

        /// <summary>
        /// Initializes a new instance of <see cref="MathLexer"/>.
        /// </summary>
        public MathLexer()
        {
            notVar = new HashSet<string> { "and", "or", "xor" };
        }

        //public IEnumerable<MathToken> Tokenize(string function)
        //{
        //    if (string.IsNullOrWhiteSpace(function))
        //        throw new ArgumentNullException("function", Resource.NotSpecifiedFunction);

        //    function = function.ToLower().Replace(" ", "");
        //    var tokens = new List<MathToken>();

        //    for (int i = 0; i < function.Length; )
        //    {
        //        char letter = function[i];
        //        var token = new MathToken();
        //        if (letter == '(')
        //        {
        //            token.Type = MathTokenType.OpenBracket;
        //        }
        //        else if (letter == ')')
        //        {
        //            token.Type = MathTokenType.CloseBracket;
        //        }
        //        else if (letter == '+')
        //        {
        //            if (i - 1 >= 0)
        //            {
        //                char pre = function[i - 1];
        //                if (pre == '(')
        //                {
        //                    i++;
        //                    continue;
        //                }
        //            }
        //            else
        //            {
        //                i++;
        //                continue;
        //            }

        //            token.Type = MathTokenType.Addition;
        //        }
        //        else if (letter == '-')
        //        {
        //            if (i - 1 >= 0)
        //            {
        //                char pre = function[i - 1];
        //                if (pre == '(')
        //                {
        //                    token.Type = MathTokenType.UnaryMinus;
        //                    tokens.Add(token);

        //                    i++;
        //                    continue;
        //                }
        //            }
        //            else
        //            {
        //                token.Type = MathTokenType.UnaryMinus;
        //                tokens.Add(token);

        //                i++;
        //                continue;
        //            }

        //            token.Type = MathTokenType.Subtraction;
        //        }
        //        else if (letter == '*')
        //        {
        //            token.Type = MathTokenType.Multiplication;
        //        }
        //        else if (letter == '/')
        //        {
        //            token.Type = MathTokenType.Division;
        //        }
        //        else if (letter == '^')
        //        {
        //            token.Type = MathTokenType.Exponentiation;
        //        }
        //        else if (letter == ',')
        //        {
        //            token.Type = MathTokenType.Comma;
        //        }
        //        else if (letter == '~')
        //        {
        //            token.Type = MathTokenType.Not;
        //        }
        //        else if (letter == '&')
        //        {
        //            token.Type = MathTokenType.And;
        //        }
        //        else if (letter == '|')
        //        {
        //            token.Type = MathTokenType.Or;
        //        }
        //        else if (letter == ':' && i + 1 < function.Length && function[i + 1] == '=')
        //        {
        //            token.Type = MathTokenType.Assign;
        //            tokens.Add(token);
        //            i += 2;

        //            continue;
        //        }
        //        else if (char.IsDigit(letter))
        //        {
        //            int length = 1;
        //            int j;
        //            for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
        //                length++;

        //            if (j < function.Length && function[j] == '.')
        //            {
        //                length++;
        //                for (j += 1; j < function.Length && char.IsDigit(function[j]); j++)
        //                    length++;
        //            }

        //            var number = function.Substring(i, length);
        //            token.Type = MathTokenType.Number;
        //            token.Number = double.Parse(number, CultureInfo.InvariantCulture);
        //            tokens.Add(token);

        //            if (i + length < function.Length && char.IsLetter(function[i + length]) && !notVar.Any(s => function.Substring(i + length).StartsWith(s)))
        //            {
        //                token = new MathToken(MathTokenType.Multiplication);
        //                tokens.Add(token);
        //            }

        //            i += length;
        //            continue;
        //        }
        //        else if (char.IsLetter(letter))
        //        {
        //            var sub = function.Substring(i);
        //            if (sub.StartsWith("pi"))
        //            {
        //                token.Type = MathTokenType.Variable;
        //                token.Variable = "π";
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("exp"))
        //            {
        //                token.Type = MathTokenType.E;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("abs"))
        //            {
        //                token.Type = MathTokenType.Absolute;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("sh"))
        //            {
        //                token.Type = MathTokenType.Sineh;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("sinh"))
        //            {
        //                token.Type = MathTokenType.Sineh;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("ch"))
        //            {
        //                token.Type = MathTokenType.Cosineh;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("cosh"))
        //            {
        //                token.Type = MathTokenType.Cosineh;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("th"))
        //            {
        //                token.Type = MathTokenType.Tangenth;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("tanh"))
        //            {
        //                token.Type = MathTokenType.Tangenth;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("cth"))
        //            {
        //                token.Type = MathTokenType.Cotangenth;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("coth"))
        //            {
        //                token.Type = MathTokenType.Cotangenth;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("sech"))
        //            {
        //                token.Type = MathTokenType.Secanth;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("csch"))
        //            {
        //                token.Type = MathTokenType.Cosecanth;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("arsinh"))
        //            {
        //                token.Type = MathTokenType.Arsineh;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arcosh"))
        //            {
        //                token.Type = MathTokenType.Arcosineh;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("artanh"))
        //            {
        //                token.Type = MathTokenType.Artangenth;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arcoth"))
        //            {
        //                token.Type = MathTokenType.Arcotangenth;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arsech"))
        //            {
        //                token.Type = MathTokenType.Arsecanth;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arcsch"))
        //            {
        //                token.Type = MathTokenType.Arcosecanth;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("sin"))
        //            {
        //                token.Type = MathTokenType.Sine;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("cosec"))
        //            {
        //                token.Type = MathTokenType.Cosecant;
        //                tokens.Add(token);
        //                i += 5;

        //                continue;
        //            }
        //            if (sub.StartsWith("csc"))
        //            {
        //                token.Type = MathTokenType.Cosecant;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("cos"))
        //            {
        //                token.Type = MathTokenType.Cosine;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("tg"))
        //            {
        //                token.Type = MathTokenType.Tangent;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("tan"))
        //            {
        //                token.Type = MathTokenType.Tangent;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("cot") || sub.StartsWith("ctg"))
        //            {
        //                token.Type = MathTokenType.Cotangent;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("sec"))
        //            {
        //                token.Type = MathTokenType.Secant;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("arcsin"))
        //            {
        //                token.Type = MathTokenType.Arcsine;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arccosec"))
        //            {
        //                token.Type = MathTokenType.Arccosecant;
        //                tokens.Add(token);
        //                i += 8;

        //                continue;
        //            }
        //            if (sub.StartsWith("arccsc"))
        //            {
        //                token.Type = MathTokenType.Arccosecant;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arccos"))
        //            {
        //                token.Type = MathTokenType.Arccosine;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arctg"))
        //            {
        //                token.Type = MathTokenType.Arctangent;
        //                tokens.Add(token);
        //                i += 5;

        //                continue;
        //            }
        //            if (sub.StartsWith("arctan"))
        //            {
        //                token.Type = MathTokenType.Arctangent;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arccot") || sub.StartsWith("arcctg"))
        //            {
        //                token.Type = MathTokenType.Arccotangent;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("arcsec"))
        //            {
        //                token.Type = MathTokenType.Arcsecant;
        //                tokens.Add(token);
        //                i += 6;

        //                continue;
        //            }
        //            if (sub.StartsWith("sqrt"))
        //            {
        //                token.Type = MathTokenType.Sqrt;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("root"))
        //            {
        //                token.Type = MathTokenType.Root;
        //                tokens.Add(token);
        //                i += 4;

        //                continue;
        //            }
        //            if (sub.StartsWith("lg"))
        //            {
        //                token.Type = MathTokenType.Lg;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("ln"))
        //            {
        //                token.Type = MathTokenType.Ln;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("log"))
        //            {
        //                token.Type = MathTokenType.Log;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("deriv"))
        //            {
        //                token.Type = MathTokenType.Derivative;
        //                tokens.Add(token);
        //                i += 5;

        //                continue;
        //            }
        //            if (sub.StartsWith("undef"))
        //            {
        //                token.Type = MathTokenType.Undefine;
        //                tokens.Add(token);
        //                i += 5;

        //                continue;
        //            }

        //            if (sub.StartsWith("not"))
        //            {
        //                token.Type = MathTokenType.Not;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("and"))
        //            {
        //                token.Type = MathTokenType.And;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }
        //            if (sub.StartsWith("or"))
        //            {
        //                token.Type = MathTokenType.Or;
        //                tokens.Add(token);
        //                i += 2;

        //                continue;
        //            }
        //            if (sub.StartsWith("xor"))
        //            {
        //                token.Type = MathTokenType.XOr;
        //                tokens.Add(token);
        //                i += 3;

        //                continue;
        //            }

        //            token.Type = MathTokenType.Variable;

        //            int j = i + 1;
        //            for (; j < function.Length && char.IsLetter(function[j]) && !notVar.Any(s => function.Substring(j).StartsWith(s)); j++) ;

        //            token.Variable = function.Substring(i, j - i);
        //            tokens.Add(token);
        //            i = j;

        //            continue;
        //        }
        //        else
        //        {
        //            throw new MathLexerException(string.Format(Resource.NotSupportedSymbol, letter));
        //        }

        //        tokens.Add(token);
        //        i++;
        //    }

        //    return tokens;
        //}

        /// <summary>
        /// Converts the string into a sequence of tokens.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The sequence of tokens.</returns>
        /// <seealso cref="IToken"/>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="function"/> parameter is null or empty.</exception>
        /// <exception cref="MathLexerException">Throws when <paramref name="function"/> has the not supported symbol.</exception>
        public IEnumerable<IToken> Tokenize(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException("function", Resource.NotSpecifiedFunction);

            function = function.ToLower().Replace(" ", "");
            var tokens = new List<IToken>();

            for (int i = 0; i < function.Length; )
            {
                char letter = function[i];
                if (letter == '(')
                {
                    tokens.Add(new SymbolToken(Symbols.OpenBracket));
                }
                else if (letter == ')')
                {
                    tokens.Add(new SymbolToken(Symbols.CloseBracket));
                }
                else if (letter == '+')
                {
                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (pre == '(')
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
                        if (pre == '(')
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
                    tokens.Add(new OperationToken(Operations.Not));
                }
                else if (letter == '&')
                {
                    tokens.Add(new OperationToken(Operations.And));
                }
                else if (letter == '|')
                {
                    tokens.Add(new OperationToken(Operations.Or));
                }
                else if (letter == ':' && i + 1 < function.Length && function[i + 1] == '=')
                {
                    tokens.Add(new OperationToken(Operations.Assign));
                    i += 2;

                    continue;
                }
                else if (char.IsDigit(letter))
                {
                    int length = 1;
                    int j;
                    for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
                        length++;

                    if (j < function.Length && function[j] == '.')
                    {
                        length++;
                        for (j += 1; j < function.Length && char.IsDigit(function[j]); j++)
                            length++;
                    }

                    var strNumber = function.Substring(i, length);
                    var number = double.Parse(strNumber, CultureInfo.InvariantCulture);
                    tokens.Add(new NumberToken(number));

                    if (i + length < function.Length && char.IsLetter(function[i + length]) && !notVar.Any(s => function.Substring(i + length).StartsWith(s)))
                    {
                        tokens.Add(new OperationToken(Operations.Multiplication));
                    }

                    i += length;
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
                    if (sub.StartsWith("exp"))
                    {
                        tokens.Add(new FunctionToken(Functions.Exp));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("abs"))
                    {
                        tokens.Add(new FunctionToken(Functions.Absolute));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sinh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("ch"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cosh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("th"))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tanh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("cth"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("coth"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sech"))
                    {
                        tokens.Add(new FunctionToken(Functions.Secanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("csch"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsinh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcosh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("artanh"))
                    {
                        tokens.Add(new FunctionToken(Functions.Artangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcoth"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcotangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arsech"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsch"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sin"))
                    {
                        tokens.Add(new FunctionToken(Functions.Sine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cosec"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("csc"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cos"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("tg"))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tan"))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cot") || sub.StartsWith("ctg"))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sec"))
                    {
                        tokens.Add(new FunctionToken(Functions.Secant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("arcsin"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccosec"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("arccsc"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccos"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arctg"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccot") || sub.StartsWith("arcctg"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccotangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsec"))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sqrt"))
                    {
                        tokens.Add(new FunctionToken(Functions.Sqrt));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("root"))
                    {
                        tokens.Add(new FunctionToken(Functions.Root));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("lg"))
                    {
                        tokens.Add(new FunctionToken(Functions.Lg));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("ln"))
                    {
                        tokens.Add(new FunctionToken(Functions.Ln));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("log"))
                    {
                        tokens.Add(new FunctionToken(Functions.Log));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("deriv"))
                    {
                        tokens.Add(new FunctionToken(Functions.Derivative));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("undef"))
                    {
                        tokens.Add(new FunctionToken(Functions.Undefine));
                        i += 5;

                        continue;
                    }

                    if (sub.StartsWith("not"))
                    {
                        tokens.Add(new OperationToken(Operations.Not));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("and"))
                    {
                        tokens.Add(new OperationToken(Operations.And));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("or"))
                    {
                        tokens.Add(new OperationToken(Operations.Or));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("xor"))
                    {
                        tokens.Add(new OperationToken(Operations.XOr));
                        i += 3;

                        continue;
                    }

                    int j = i + 1;
                    for (; j < function.Length && char.IsLetter(function[j]) && !notVar.Any(s => function.Substring(j).StartsWith(s)); j++) ;

                    var variable = function.Substring(i, j - i);
                    tokens.Add(new VariableToken(variable));
                    i = j;

                    continue;
                }
                else
                {
                    throw new MathLexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                i++;
            }

            return tokens;
        }

    }

}
