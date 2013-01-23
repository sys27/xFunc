// Copyright 2012 Dmitry Kischenko
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
using xFunc.Maths.Exceptions;
using xFunc.Maths.Resources;

namespace xFunc.Maths
{

    public class MathLexer
    {

        public IEnumerable<MathToken> Tokenization(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentException(Resource.NotSpecifiedFunction, "function");

            function = function.ToLower().Replace(" ", "");
            List<MathToken> tokens = new List<MathToken>();

            int index = 0;
            bool isDigit = false;
            for (int i = 0; i < function.Length; i++)
            {
                if (!isDigit)
                    index = i;

                char letter = function[i];
                string sub = function.Substring(i);
                MathToken token = new MathToken();
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
                            continue;
                        }
                    }
                    else
                    {
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
                            continue;
                        }
                    }
                    else
                    {
                        token.Type = MathTokenType.UnaryMinus;
                        tokens.Add(token);
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
                else if (sub.StartsWith(":="))
                {
                    token.Type = MathTokenType.Assign;
                    tokens.Add(token);
                    i++;

                    continue;
                }
                else if (char.IsDigit(letter))
                {
                    if (i != (function.Length - 1))
                    {
                        if (char.IsDigit(function[i + 1]))
                        {
                            isDigit = true;
                            continue;
                        }
                        if (function[i + 1] == '.')
                        {
                            isDigit = true;
                            i++;
                            continue;
                        }
                        if (char.IsLetter(function[i + 1]))
                        {
                            function = function.Insert(i + 1, "*");
                        }
                    }

                    token.Type = MathTokenType.Number;
                    string number = function.Substring(index, i - index + 1);
                    token.Number = double.Parse(number, System.Globalization.CultureInfo.InvariantCulture);
                    isDigit = false;
                }
                else if (char.IsLetter(letter))
                {
                    if (sub.StartsWith("pi"))
                    {
                        token.Type = MathTokenType.Variable;
                        token.Variable = 'π';
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("exp"))
                    {
                        token.Type = MathTokenType.E;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("abs"))
                    {
                        token.Type = MathTokenType.Absolute;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sh"))
                    {
                        token.Type = MathTokenType.Sineh;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("sinh"))
                    {
                        token.Type = MathTokenType.Sineh;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("ch"))
                    {
                        token.Type = MathTokenType.Cosineh;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("cosh"))
                    {
                        token.Type = MathTokenType.Cosineh;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("th"))
                    {
                        token.Type = MathTokenType.Tangenth;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("tanh"))
                    {
                        token.Type = MathTokenType.Tangenth;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cth"))
                    {
                        token.Type = MathTokenType.Cotangenth;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("coth"))
                    {
                        token.Type = MathTokenType.Cotangenth;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sin"))
                    {
                        token.Type = MathTokenType.Sine;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cosec"))
                    {
                        token.Type = MathTokenType.Cosecant;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("csc"))
                    {
                        token.Type = MathTokenType.Cosecant;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cos"))
                    {
                        token.Type = MathTokenType.Cosine;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tg"))
                    {
                        token.Type = MathTokenType.Tangent;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("tan"))
                    {
                        token.Type = MathTokenType.Tangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cot") || sub.StartsWith("ctg"))
                    {
                        token.Type = MathTokenType.Cotangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sec"))
                    {
                        token.Type = MathTokenType.Secant;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("arcsin"))
                    {
                        token.Type = MathTokenType.Arcsine;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccosec"))
                    {
                        token.Type = MathTokenType.Arccosecant;
                        tokens.Add(token);
                        i += 7;

                        continue;
                    }
                    if (sub.StartsWith("arccsc"))
                    {
                        token.Type = MathTokenType.Arccosecant;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccos"))
                    {
                        token.Type = MathTokenType.Arccosine;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctg"))
                    {
                        token.Type = MathTokenType.Arctangent;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arctan"))
                    {
                        token.Type = MathTokenType.Arctangent;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccot") || sub.StartsWith("arcctg"))
                    {
                        token.Type = MathTokenType.Arccotangent;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arcsec"))
                    {
                        token.Type = MathTokenType.Arcsecant;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("sqrt"))
                    {
                        token.Type = MathTokenType.Sqrt;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("root"))
                    {
                        token.Type = MathTokenType.Root;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("lg"))
                    {
                        token.Type = MathTokenType.Lg;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("ln"))
                    {
                        token.Type = MathTokenType.Ln;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("log"))
                    {
                        token.Type = MathTokenType.Log;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("deriv"))
                    {
                        token.Type = MathTokenType.Derivative;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }

                    if (i != (function.Length - 1) && char.IsLetter(function[i + 1]))
                    {
                        function = function.Insert(i + 1, "*");
                    }

                    token.Type = MathTokenType.Variable;
                    token.Variable = letter;
                }
                else
                {
                    throw new MathLexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                tokens.Add(token);
            }

            return tokens;
        }

    }

}
