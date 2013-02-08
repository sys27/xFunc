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

namespace xFunc.Maths
{

    public class MathLexer : ILexer
    {

        private readonly HashSet<string> notVar;

        public MathLexer()
        {
            notVar = new HashSet<string> { "and", "or", "xor" };
        }

        public IEnumerable<MathToken> Tokenize(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException("function", Resource.NotSpecifiedFunction);

            function = function.ToLower().Replace(" ", "");
            var tokens = new List<MathToken>();

            for (int i = 0; i < function.Length; )
            {
                char letter = function[i];
                var token = new MathToken();
                if (letter == '(')
                {
                    token.Type = MathTokenType.OpenBracket;
                }
                else if (letter == ')')
                {
                    token.Type = MathTokenType.CloseBracket;
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

                    token.Type = MathTokenType.Addition;
                }
                else if (letter == '-')
                {
                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (pre == '(')
                        {
                            token.Type = MathTokenType.UnaryMinus;
                            tokens.Add(token);

                            i++;
                            continue;
                        }
                    }
                    else
                    {
                        token.Type = MathTokenType.UnaryMinus;
                        tokens.Add(token);

                        i++;
                        continue;
                    }

                    token.Type = MathTokenType.Subtraction;
                }
                else if (letter == '*')
                {
                    token.Type = MathTokenType.Multiplication;
                }
                else if (letter == '/')
                {
                    token.Type = MathTokenType.Division;
                }
                else if (letter == '^')
                {
                    token.Type = MathTokenType.Exponentiation;
                }
                else if (letter == ',')
                {
                    token.Type = MathTokenType.Comma;
                }
                else if (letter == '~')
                {
                    token.Type = MathTokenType.Not;
                }
                else if (letter == '&')
                {
                    token.Type = MathTokenType.And;
                }
                else if (letter == '|')
                {
                    token.Type = MathTokenType.Or;
                }
                else if (letter == ':' && i + 1 < function.Length && function[i + 1] == '=')
                {
                    token.Type = MathTokenType.Assign;
                    tokens.Add(token);
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

                    var number = function.Substring(i, length);
                    token.Type = MathTokenType.Number;
                    token.Number = double.Parse(number, CultureInfo.InvariantCulture);
                    tokens.Add(token);

                    if (i + length < function.Length && char.IsLetter(function[i + length]) && !notVar.Any(s => function.Substring(i + length).StartsWith(s)))
                    {
                        token = new MathToken(MathTokenType.Multiplication);
                        tokens.Add(token);
                    }

                    i += length;
                    continue;
                }
                else if (char.IsLetter(letter))
                {
                    var sub = function.Substring(i);
                    if (sub.StartsWith("pi"))
                    {
                        token.Type = MathTokenType.Variable;
                        token.Variable = 'π';
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("exp"))
                    {
                        token.Type = MathTokenType.E;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("abs"))
                    {
                        token.Type = MathTokenType.Absolute;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sh"))
                    {
                        token.Type = MathTokenType.Sineh;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sinh"))
                    {
                        token.Type = MathTokenType.Sineh;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("ch"))
                    {
                        token.Type = MathTokenType.Cosineh;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cosh"))
                    {
                        token.Type = MathTokenType.Cosineh;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("th"))
                    {
                        token.Type = MathTokenType.Tangenth;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tanh"))
                    {
                        token.Type = MathTokenType.Tangenth;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("cth"))
                    {
                        token.Type = MathTokenType.Cotangenth;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("coth"))
                    {
                        token.Type = MathTokenType.Cotangenth;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sech"))
                    {
                        token.Type = MathTokenType.Secanth;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("csch"))
                    {
                        token.Type = MathTokenType.Cosecanth;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsinh"))
                    {
                        token.Type = MathTokenType.Arsineh;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcosh"))
                    {
                        token.Type = MathTokenType.Arcosineh;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("artanh"))
                    {
                        token.Type = MathTokenType.Artangenth;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcoth"))
                    {
                        token.Type = MathTokenType.Arcotangenth;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arsech"))
                    {
                        token.Type = MathTokenType.Arsecanth;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsch"))
                    {
                        token.Type = MathTokenType.Arcosecanth;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sin"))
                    {
                        token.Type = MathTokenType.Sine;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cosec"))
                    {
                        token.Type = MathTokenType.Cosecant;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("csc"))
                    {
                        token.Type = MathTokenType.Cosecant;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cos"))
                    {
                        token.Type = MathTokenType.Cosine;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("tg"))
                    {
                        token.Type = MathTokenType.Tangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tan"))
                    {
                        token.Type = MathTokenType.Tangent;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cot") || sub.StartsWith("ctg"))
                    {
                        token.Type = MathTokenType.Cotangent;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sec"))
                    {
                        token.Type = MathTokenType.Secant;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("arcsin"))
                    {
                        token.Type = MathTokenType.Arcsine;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccosec"))
                    {
                        token.Type = MathTokenType.Arccosecant;
                        tokens.Add(token);
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("arccsc"))
                    {
                        token.Type = MathTokenType.Arccosecant;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccos"))
                    {
                        token.Type = MathTokenType.Arccosine;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arctg"))
                    {
                        token.Type = MathTokenType.Arctangent;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan"))
                    {
                        token.Type = MathTokenType.Arctangent;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccot") || sub.StartsWith("arcctg"))
                    {
                        token.Type = MathTokenType.Arccotangent;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsec"))
                    {
                        token.Type = MathTokenType.Arcsecant;
                        tokens.Add(token);
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sqrt"))
                    {
                        token.Type = MathTokenType.Sqrt;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("root"))
                    {
                        token.Type = MathTokenType.Root;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("lg"))
                    {
                        token.Type = MathTokenType.Lg;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("ln"))
                    {
                        token.Type = MathTokenType.Ln;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("log"))
                    {
                        token.Type = MathTokenType.Log;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("deriv"))
                    {
                        token.Type = MathTokenType.Derivative;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }

                    if (sub.StartsWith("not"))
                    {
                        token.Type = MathTokenType.Not;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("and"))
                    {
                        token.Type = MathTokenType.And;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("or"))
                    {
                        token.Type = MathTokenType.Or;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("xor"))
                    {
                        token.Type = MathTokenType.XOr;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }

                    token.Type = MathTokenType.Variable;
                    token.Variable = letter;
                    tokens.Add(token);
                    
                    if (i + 1 < function.Length && char.IsLetter(function[i + 1]) && !notVar.Any(s => function.Substring(i + 1).StartsWith(s)))
                    {
                        token = new MathToken(MathTokenType.Multiplication);
                        tokens.Add(token);
                    }

                    i++;
                    continue;
                }
                else
                {
                    throw new MathLexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                tokens.Add(token);
                i++;
            }

            return tokens;
        }

    }

}
