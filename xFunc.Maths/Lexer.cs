// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Resources;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    public class Lexer : ILexer
    {

        private readonly HashSet<string> notVar;
        private readonly HashSet<char> unaryMinusOp;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        public Lexer()
        {
            notVar = new HashSet<string> { "nand", "nor", "and", "or", "xor" };
            unaryMinusOp = new HashSet<char> { '(', '{', '*', '/', '^', '=', ',' };
        }

        private static bool IsBalanced(string str)
        {
            int brackets = 0;
            int braces = 0;

            foreach (var item in str)
            {
                if (item == '(') brackets++;
                else if (item == ')') brackets--;
                else if (item == '{') braces++;
                else if (item == '}') braces--;

                if (brackets < 0 || braces < 0)
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
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException(nameof(function), Resource.NotSpecifiedFunction);
            if (!IsBalanced(function))
                throw new LexerException(Resource.NotBalanced);

            function = function.ToLower()
                               .Replace(" ", "")
                               .Replace("\t", "")
                               .Replace("\n", "")
                               .Replace("\r", "");

            var tokens = new List<IToken>();

            for (int i = 0; i < function.Length;)
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
                    if (CheckNextSymbol(function, i, '+'))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.Increment));
                        i += 2;

                        continue;
                    }
                    if (CheckNextSymbol(function, i, '='))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.AddAssign));
                        i += 2;

                        continue;
                    }

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
                    if (CheckNextSymbol(function, i, '-'))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.Decrement));
                        i += 2;

                        continue;
                    }
                    if (CheckNextSymbol(function, i, '='))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.SubAssign));
                        i += 2;

                        continue;
                    }
                    if (CheckNextSymbol(function, i, '>'))
                    {
                        tokens.Add(new OperationToken(Operations.Implication));
                        i += 2;

                        continue;
                    }

                    if (i - 1 >= 0)
                    {
                        char pre = function[i - 1];
                        if (unaryMinusOp.Contains(pre))
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
                    if (CheckNextSymbol(function, i, '='))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.MulAssign));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.Multiplication));
                }
                else if (letter == '/')
                {
                    if (CheckNextSymbol(function, i, '='))
                    {
                        var lastToken = tokens.LastOrDefault();
                        if (!(lastToken is VariableToken))
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                        tokens.Add(new OperationToken(Operations.DivAssign));
                        i += 2;

                        continue;
                    }

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
                            throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));
                    }

                    tokens.Add(new OperationToken(Operations.Not));
                }
                else if (letter == '&')
                {
                    if (CheckNextSymbol(function, i, '&'))
                    {
                        tokens.Add(new OperationToken(Operations.ConditionalAnd));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.And));
                }
                else if (letter == '|')
                {
                    if (CheckNextSymbol(function, i, '|'))
                    {
                        tokens.Add(new OperationToken(Operations.ConditionalOr));
                        i += 2;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.Or));
                }
                else if (letter == ':' && CheckNextSymbol(function, i, '='))
                {
                    tokens.Add(new OperationToken(Operations.Assign));
                    i += 2;

                    continue;
                }
                else if (letter == '=')
                {
                    if (CheckNextSymbol(function, i, '='))
                    {
                        tokens.Add(new OperationToken(Operations.Equal));
                        i += 2;

                        continue;
                    }
                    if (CheckNextSymbol(function, i, '>'))
                    {
                        tokens.Add(new OperationToken(Operations.Implication));
                        i += 2;

                        continue;
                    }
                }
                else if (letter == '!')
                {
                    if (CheckNextSymbol(function, i, '='))
                    {
                        tokens.Add(new OperationToken(Operations.NotEqual));
                        i += 2;

                        continue;
                    }

                    var lastToken = tokens.LastOrDefault();
                    if (lastToken != null)
                    {
                        var symbol = lastToken as SymbolToken;
                        if ((symbol != null && symbol.Symbol == Symbols.CloseBracket) || lastToken is NumberToken || lastToken is VariableToken)
                        {
                            tokens.Add(new OperationToken(Operations.Factorial));
                            i++;

                            continue;
                        }
                    }

                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));
                }
                else if (letter == '<')
                {
                    if (CheckNextSymbol(function, i, '='))
                    {
                        if (CheckNextSymbol(function, i + 1, '>'))
                        {
                            tokens.Add(new OperationToken(Operations.Equality));
                            i += 3;

                            continue;
                        }

                        tokens.Add(new OperationToken(Operations.LessOrEqual));
                        i += 2;

                        continue;
                    }
                    if (CheckNextSymbol(function, i, '-') && CheckNextSymbol(function, i + 1, '>'))
                    {
                        tokens.Add(new OperationToken(Operations.Equality));
                        i += 3;

                        continue;
                    }

                    tokens.Add(new OperationToken(Operations.LessThan));
                }
                else if (letter == '>')
                {
                    if (CheckNextSymbol(function, i, '='))
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
                        if (nextLetter == 'x' && i + 2 < function.Length) // hex
                        {
                            i += 2;

                            length = 1;
                            for (j = i + 1; j < function.Length && (char.IsDigit(function[j]) || (function[j] >= 97 && function[j] <= 102)); j++) // from 'a' to 'f'
                                length++;

                            strNumber = function.Substring(i, length);
                            number = Convert.ToInt64(strNumber, 16);
                            tokens.Add(new NumberToken(number));

                            i += length;
                            continue;
                        }
                        if (nextLetter == 'b' && i + 2 < function.Length) // bin
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
                        if (char.IsDigit(nextLetter)) // oct
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

                    // normal number (dec)

                    length = 1;
                    for (j = i + 1; j < function.Length && char.IsDigit(function[j]); j++)
                        length++;

                    if (j < function.Length && function[j] == '.')
                    {
                        var temp = ++length;
                        for (j += 1; j < function.Length && char.IsDigit(function[j]); j++)
                            length++;

                        if (temp == length)
                            throw new LexerException(string.Format(Resource.FormatException, function.Substring(i, length)));
                    }

                    if (CheckNextSymbol(function, i + length - 1, 'e')) // exp notation
                    {
                        length++;
                        if (CheckNextSymbol(function, i + length - 1, '-'))
                            length++;

                        for (j = i + length; j < function.Length && char.IsDigit(function[j]); j++)
                            length++;
                    }

                    strNumber = function.Substring(i, length);
                    number = double.Parse(strNumber, CultureInfo.InvariantCulture);
                    tokens.Add(new NumberToken(number));

                    i += length;

                    var f = function.Substring(i);
                    if (i < function.Length && char.IsLetter(function[i]) && !notVar.Any(s => f.StartsWith(s, StringComparison.Ordinal)))
                        tokens.Add(new OperationToken(Operations.Multiplication));

                    continue;
                }
                else if (char.IsLetter(letter))
                {
                    var sub = function.Substring(i);
                    if (sub.StartsWith("pi", StringComparison.Ordinal))
                    {
                        tokens.Add(new VariableToken("π"));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("true", StringComparison.Ordinal))
                    {
                        tokens.Add(new BooleanToken(true));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("false", StringComparison.Ordinal))
                    {
                        tokens.Add(new BooleanToken(false));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("vector{", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Vector));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("matrix{", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Matrix));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("exp(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Exp));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("abs(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Absolute));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("sinh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Sineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("ch(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("cosh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("th(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tanh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("cth(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("coth(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sech(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Secanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("csch(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecanth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arsinh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arch(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosineh));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("arcosh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosineh));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arth(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Artangenth));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("artanh(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Artangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcth(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcotangenth));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arcoth(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcotangenth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arsch(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsecanth));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arsech(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arsecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsch(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcosecanth));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sin(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Sine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cosec(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("csc(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosecant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cos(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cosine));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("tg(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("tan(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Tangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("cot(") || sub.StartsWith("ctg(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Cotangent));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("sec(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Secant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("arcsin(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccosec(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("arccsc(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccos(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccosine));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arctg(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("arctan(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arctangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arccot(") || sub.StartsWith("arcctg(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arccotangent));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("arcsec(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Arcsecant));
                        i += 6;

                        continue;
                    }
                    if (sub.StartsWith("sqrt(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Sqrt));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("root(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Root));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("lg(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Lg));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("ln(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Ln));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("lb(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Lb));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("log2(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Lb));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("log(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Log));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("deriv(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Derivative));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("simplify(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Simplify));
                        i += 8;

                        continue;
                    }
                    if (sub.StartsWith("def(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Define));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("undef(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Undefine));
                        i += 5;

                        continue;
                    }

                    if (sub.StartsWith("not(", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.Not));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("and", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.And));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("or", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.Or));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("xor", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.XOr));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("impl", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.Implication));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("eq", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.Equality));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("nor", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.NOr));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("nand", StringComparison.Ordinal))
                    {
                        tokens.Add(new OperationToken(Operations.NAnd));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("gcd(") || sub.StartsWith("gcf(") || sub.StartsWith("hcf(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.GCD));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("lcm(") || sub.StartsWith("scm(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.LCM));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("fact(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Factorial));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("sum(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Sum));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("product(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Product));
                        i += 7;

                        continue;
                    }
                    if (sub.StartsWith("round(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Round));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("floor(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Floor));
                        i += 5;

                        continue;
                    }
                    if (sub.StartsWith("ceil(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Ceil));
                        i += 4;

                        continue;
                    }
                    if (sub.StartsWith("transpose(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Transpose));
                        i += 9;

                        continue;
                    }
                    if (sub.StartsWith("determinant(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Determinant));
                        i += 11;

                        continue;
                    }
                    if (sub.StartsWith("det(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Determinant));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("inverse(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.Inverse));
                        i += 7;

                        continue;
                    }
                    if (sub.StartsWith("if(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.If));
                        i += 2;

                        continue;
                    }
                    if (sub.StartsWith("for(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.For));
                        i += 3;

                        continue;
                    }
                    if (sub.StartsWith("while(", StringComparison.Ordinal))
                    {
                        tokens.Add(new FunctionToken(Functions.While));
                        i += 5;

                        continue;
                    }

                    int j = i + 1;
                    for (; j < function.Length && char.IsLetter(function[j]) && !notVar.Any(s => function.Substring(j).StartsWith(s, StringComparison.Ordinal)); j++) { }

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
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));
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
            for (; i < tokens.Count;)
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
            for (int i = 0; i < tokens.Count;)
            {
                if (tokens[i] is FunctionToken)
                    i = _CountParams(tokens, i) + 1;
                else
                    i++;
            }

            return tokens;
        }

        private bool CheckNextSymbol(string str, int index, char symbol)
        {
            return index + 1 < str.Length && str[index + 1] == symbol;
        }

    }

}
