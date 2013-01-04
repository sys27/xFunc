using System;
using System.Collections.Generic;
using xFunc.Library.Maths.Exceptions;
using xFunc.Library.Maths.Resources;

namespace xFunc.Library.Maths
{

    public class MathLexer
    {

        public IEnumerable<MathToken> MathTokenization(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentException(Resource.NotSpecifiedFuncation, "function");

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
                    if (sub.StartsWith("sin"))
                    {
                        token.Type = MathTokenType.Sine;
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
                    if (sub.StartsWith("tan"))
                    {
                        token.Type = MathTokenType.Tangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cot"))
                    {
                        token.Type = MathTokenType.Cotangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("arcsin"))
                    {
                        token.Type = MathTokenType.Arcsin;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccos"))
                    {
                        token.Type = MathTokenType.Arccos;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan"))
                    {
                        token.Type = MathTokenType.Arctan;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccot"))
                    {
                        token.Type = MathTokenType.Arccot;
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
                    if (sub.StartsWith("plot"))
                    {
                        token.Type = MathTokenType.Plot;
                        tokens.Add(token);
                        i += 3;

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
