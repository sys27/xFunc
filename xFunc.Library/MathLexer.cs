using System;
using System.Collections.Generic;
using xFunc.Library.Exceptions;
using xFunc.Library.Resources;

namespace xFunc.Library
{

    public class MathLexer
    {

        public IEnumerable<Token> MathTokenization(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentException(Resource.NotSpecifiedFuncation, "function");

            function = function.ToLower().Replace(" ", "");
            List<Token> tokens = new List<Token>();

            int index = 0;
            bool isDigit = false;
            for (int i = 0; i < function.Length; i++)
            {
                if (!isDigit)
                    index = i;

                char letter = function[i];
                Token token = new Token();
                if (letter == '(')
                {
                    token.Type = TokenType.OpenBracket;
                }
                else if (letter == ')')
                {
                    token.Type = TokenType.CloseBracket;
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

                    token.Type = TokenType.Addition;
                }
                else if (letter == '-')
                {
                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (pre == '(')
                        {
                            token.Type = TokenType.UnaryMinus;
                            tokens.Add(token);
                            continue;
                        }
                    }
                    else
                    {
                        token.Type = TokenType.UnaryMinus;
                        tokens.Add(token);
                        continue;
                    }

                    token.Type = TokenType.Subtraction;
                }
                else if (letter == '*')
                {
                    token.Type = TokenType.Multiplication;
                }
                else if (letter == '/')
                {
                    token.Type = TokenType.Division;
                }
                else if (letter == '^')
                {
                    token.Type = TokenType.Exponentiation;
                }
                else if (letter == ',')
                {
                    token.Type = TokenType.Comma;
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

                    token.Type = TokenType.Number;
                    string number = function.Substring(index, i - index + 1);
                    token.Number = double.Parse(number, System.Globalization.CultureInfo.InvariantCulture);
                    isDigit = false;
                }
                else if (char.IsLetter(letter))
                {
                    string sub = function.Substring(i);
                    if (sub.StartsWith("pi"))
                    {
                        token.Type = TokenType.Variable;
                        token.Variable = 'π';
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("exp"))
                    {
                        token.Type = TokenType.E;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("abs"))
                    {
                        token.Type = TokenType.Absolute;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sin"))
                    {
                        token.Type = TokenType.Sine;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cos"))
                    {
                        token.Type = TokenType.Cosine;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tan"))
                    {
                        token.Type = TokenType.Tangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cot"))
                    {
                        token.Type = TokenType.Cotangent;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("arcsin"))
                    {
                        token.Type = TokenType.Arcsin;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccos"))
                    {
                        token.Type = TokenType.Arccos;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan"))
                    {
                        token.Type = TokenType.Arctan;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arccot"))
                    {
                        token.Type = TokenType.Arccot;
                        tokens.Add(token);
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("sqrt"))
                    {
                        token.Type = TokenType.Sqrt;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("root"))
                    {
                        token.Type = TokenType.Root;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("lg"))
                    {
                        token.Type = TokenType.Lg;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("ln"))
                    {
                        token.Type = TokenType.Ln;
                        tokens.Add(token);
                        i++;

                        continue;
                    }
                    if (sub.StartsWith("log"))
                    {
                        token.Type = TokenType.Log;
                        tokens.Add(token);
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("plot"))
                    {
                        token.Type = TokenType.Plot;
                        tokens.Add(token);
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("deriv"))
                    {
                        token.Type = TokenType.Derivative;
                        tokens.Add(token);
                        i += 4;

                        continue;
                    }

                    if (i != (function.Length - 1) && char.IsLetter(function[i + 1]))
                    {
                        function = function.Insert(i + 1, "*");
                    }

                    token.Type = TokenType.Variable;
                    token.Variable = letter;
                }
                else
                {
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter));
                }

                tokens.Add(token);
            }

            return tokens;
        }

    }

}
