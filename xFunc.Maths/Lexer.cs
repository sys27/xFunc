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
using System.Numerics;
using System.Text.RegularExpressions;
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

        private Regex regexSymbols;
        private Regex regexOperations;
        private Regex regexFunctions;
        private Regex regexConst;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        public Lexer()
        {
            notVar = new HashSet<string> { "nand", "nor", "and", "or", "xor", "mod" };
            unaryMinusOp = new HashSet<char> { '(', '{', '*', '/', '^', '=', ',' };

            regexSymbols = new Regex(@"\G(\(|\)|{|}|,)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            regexOperations = new Regex(@"\G([^a-zA-Z0-9(){},°\s]+|nand|nor|and|or|xor|not|eq|impl|mod)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            regexFunctions = new Regex(@"\G([a-z][0-9a-z]*)(\(|{)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            regexConst = new Regex(@"\G(true|false)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private void CreateSymbol(string match, IList<IToken> tokens)
        {
            if (match == "(")
            {
                tokens.Add(new SymbolToken(Symbols.OpenBracket));
            }
            else if (match == ")")
            {
                var lastToken = tokens.LastOrDefault() as SymbolToken;
                if (lastToken != null && lastToken.Symbol == Symbols.Comma)
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
        }

        private void CreateOperations(string match, IList<IToken> tokens)
        {
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
                var lastToken = tokens.LastOrDefault();
                if (lastToken != null)
                {
                    var symbol = lastToken as SymbolToken;
                    if ((symbol != null && symbol.Symbol == Symbols.CloseBracket) || lastToken is NumberToken || lastToken is VariableToken)
                    {
                        tokens.Add(new OperationToken(Operations.Factorial));
                        return;
                    }
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
                var lastToken = tokens.LastOrDefault();
                if (lastToken == null)
                {
                    return;
                }
                else
                {
                    var symbolToken = lastToken as SymbolToken;
                    if (symbolToken != null && (symbolToken.Symbol == Symbols.OpenBracket ||
                                                symbolToken.Symbol == Symbols.OpenBrace))
                        return;
                }

                tokens.Add(new OperationToken(Operations.Addition));
            }
            else if (match == "-" || match == "−")
            {
                var lastToken = tokens.LastOrDefault();
                if (lastToken == null)
                {
                    tokens.Add(new OperationToken(Operations.UnaryMinus));
                }
                else
                {
                    var symbolToken = lastToken as SymbolToken;
                    if (symbolToken != null && (symbolToken.Symbol == Symbols.OpenBracket ||
                                                symbolToken.Symbol == Symbols.OpenBrace ||
                                                symbolToken.Symbol == Symbols.Comma))
                    {
                        tokens.Add(new OperationToken(Operations.UnaryMinus));
                    }
                    else
                    {
                        var operationToken = lastToken as OperationToken;
                        if (operationToken != null && (operationToken.Operation == Operations.Exponentiation ||
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
                var lastToken = tokens.LastOrDefault();
                if (lastToken != null)
                {
                    var symbol = lastToken as SymbolToken;
                    if ((symbol != null && symbol.Symbol == Symbols.CloseBracket) || lastToken is NumberToken || lastToken is VariableToken)
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
        }

        private void CreateFunction(string match, IList<IToken> tokens)
        {
            if (match == "add")
            {
                tokens.Add(new FunctionToken(Functions.Add));
            }
            else if (match == "sub")
            {
                tokens.Add(new FunctionToken(Functions.Sub));
            }
            else if (match == "mul")
            {
                tokens.Add(new FunctionToken(Functions.Mul));
            }
            else if (match == "div")
            {
                tokens.Add(new FunctionToken(Functions.Div));
            }
            else if (match == "pow")
            {
                tokens.Add(new FunctionToken(Functions.Pow));
            }
            else if (match == "abs")
            {
                tokens.Add(new FunctionToken(Functions.Absolute));
            }
            else if (match == "sin")
            {
                tokens.Add(new FunctionToken(Functions.Sine));
            }
            else if (match == "cos")
            {
                tokens.Add(new FunctionToken(Functions.Cosine));
            }
            else if (match == "tg" || match == "tan")
            {
                tokens.Add(new FunctionToken(Functions.Tangent));
            }
            else if (match == "ctg" || match == "cot")
            {
                tokens.Add(new FunctionToken(Functions.Cotangent));
            }
            else if (match == "sec")
            {
                tokens.Add(new FunctionToken(Functions.Secant));
            }
            else if (match == "csc" || match == "cosec")
            {
                tokens.Add(new FunctionToken(Functions.Cosecant));
            }
            else if (match == "arcsin")
            {
                tokens.Add(new FunctionToken(Functions.Arcsine));
            }
            else if (match == "arccos")
            {
                tokens.Add(new FunctionToken(Functions.Arccosine));
            }
            else if (match == "arctg" || match == "arctan")
            {
                tokens.Add(new FunctionToken(Functions.Arctangent));
            }
            else if (match == "arcctg" || match == "arccot")
            {
                tokens.Add(new FunctionToken(Functions.Arccotangent));
            }
            else if (match == "arcsec")
            {
                tokens.Add(new FunctionToken(Functions.Arcsecant));
            }
            else if (match == "arccsc" || match == "arccosec")
            {
                tokens.Add(new FunctionToken(Functions.Arccosecant));
            }
            else if (match == "sqrt")
            {
                tokens.Add(new FunctionToken(Functions.Sqrt));
            }
            else if (match == "root")
            {
                tokens.Add(new FunctionToken(Functions.Root));
            }
            else if (match == "ln")
            {
                tokens.Add(new FunctionToken(Functions.Ln));
            }
            else if (match == "lg")
            {
                tokens.Add(new FunctionToken(Functions.Lg));
            }
            else if (match == "lb" || match == "log2")
            {
                tokens.Add(new FunctionToken(Functions.Lb));
            }
            else if (match == "log")
            {
                tokens.Add(new FunctionToken(Functions.Log));
            }
            else if (match == "sh" || match == "sinh")
            {
                tokens.Add(new FunctionToken(Functions.Sineh));
            }
            else if (match == "ch" || match == "cosh")
            {
                tokens.Add(new FunctionToken(Functions.Cosineh));
            }
            else if (match == "th" || match == "tanh")
            {
                tokens.Add(new FunctionToken(Functions.Tangenth));
            }
            else if (match == "cth" || match == "coth")
            {
                tokens.Add(new FunctionToken(Functions.Cotangenth));
            }
            else if (match == "sech")
            {
                tokens.Add(new FunctionToken(Functions.Secanth));
            }
            else if (match == "csch")
            {
                tokens.Add(new FunctionToken(Functions.Cosecanth));
            }
            else if (match == "arsh" || match == "arsinh")
            {
                tokens.Add(new FunctionToken(Functions.Arsineh));
            }
            else if (match == "arch" || match == "arcosh")
            {
                tokens.Add(new FunctionToken(Functions.Arcosineh));
            }
            else if (match == "arth" || match == "artanh")
            {
                tokens.Add(new FunctionToken(Functions.Artangenth));
            }
            else if (match == "arcth" || match == "arcoth")
            {
                tokens.Add(new FunctionToken(Functions.Arcotangenth));
            }
            else if (match == "arsch" || match == "arsech")
            {
                tokens.Add(new FunctionToken(Functions.Arsecanth));
            }
            else if (match == "arcsch")
            {
                tokens.Add(new FunctionToken(Functions.Arcosecanth));
            }
            else if (match == "exp")
            {
                tokens.Add(new FunctionToken(Functions.Exp));
            }
            else if (match == "gcd" || match == "gcf" || match == "hcf")
            {
                tokens.Add(new FunctionToken(Functions.GCD));
            }
            else if (match == "lcm" || match == "scm")
            {
                tokens.Add(new FunctionToken(Functions.LCM));
            }
            else if (match == "fact")
            {
                tokens.Add(new FunctionToken(Functions.Factorial));
            }
            else if (match == "sum")
            {
                tokens.Add(new FunctionToken(Functions.Sum));
            }
            else if (match == "product")
            {
                tokens.Add(new FunctionToken(Functions.Product));
            }
            else if (match == "round")
            {
                tokens.Add(new FunctionToken(Functions.Round));
            }
            else if (match == "floor")
            {
                tokens.Add(new FunctionToken(Functions.Floor));
            }
            else if (match == "ceil")
            {
                tokens.Add(new FunctionToken(Functions.Ceil));
            }
            else if (match == "if")
            {
                tokens.Add(new FunctionToken(Functions.If));
            }
            else if (match == "for")
            {
                tokens.Add(new FunctionToken(Functions.For));
            }
            else if (match == "while")
            {
                tokens.Add(new FunctionToken(Functions.While));
            }
            else if (match == "del" || match == "nabla")
            {
                tokens.Add(new FunctionToken(Functions.Del));
            }
            else if (match == "deriv")
            {
                tokens.Add(new FunctionToken(Functions.Derivative));
            }
            else if (match == "simplify")
            {
                tokens.Add(new FunctionToken(Functions.Simplify));
            }
            else if (match == "def")
            {
                tokens.Add(new FunctionToken(Functions.Define));
            }
            else if (match == "undef")
            {
                tokens.Add(new FunctionToken(Functions.Undefine));
            }
            else if (match == "transpose")
            {
                tokens.Add(new FunctionToken(Functions.Transpose));
            }
            else if (match == "det" || match == "determinant")
            {
                tokens.Add(new FunctionToken(Functions.Determinant));
            }
            else if (match == "inverse")
            {
                tokens.Add(new FunctionToken(Functions.Inverse));
            }
            else if (match == "vector")
            {
                tokens.Add(new FunctionToken(Functions.Vector));
            }
            else if (match == "matrix")
            {
                tokens.Add(new FunctionToken(Functions.Matrix));
            }
            else if (match == "re" || match == "real")
            {
                tokens.Add(new FunctionToken(Functions.Re));
            }
            else if (match == "im" || match == "imaginary")
            {
                tokens.Add(new FunctionToken(Functions.Im));
            }
            else if (match == "phase")
            {
                tokens.Add(new FunctionToken(Functions.Phase));
            }
            else if (match == "conjugate")
            {
                tokens.Add(new FunctionToken(Functions.Conjugate));
            }
            else if (match == "reciprocal")
            {
                tokens.Add(new FunctionToken(Functions.Reciprocal));
            }
            else if (match == "min")
            {
                tokens.Add(new FunctionToken(Functions.Min));
            }
            else if (match == "max")
            {
                tokens.Add(new FunctionToken(Functions.Max));
            }
            else if (match == "avg")
            {
                tokens.Add(new FunctionToken(Functions.Avg));
            }
            else if (match == "count")
            {
                tokens.Add(new FunctionToken(Functions.Count));
            }
            else if (match == "var")
            {
                tokens.Add(new FunctionToken(Functions.Var));
            }
            else if (match == "varp")
            {
                tokens.Add(new FunctionToken(Functions.Varp));
            }
            else if (match == "stdev")
            {
                tokens.Add(new FunctionToken(Functions.Stdev));
            }
            else if (match == "stdevp")
            {
                tokens.Add(new FunctionToken(Functions.Stdevp));
            }
            else
            {
                tokens.Add(new UserFunctionToken(match));
            }
        }

        private void CreateConst(string match, IList<IToken> tokens)
        {
            if (match == "true")
            {
                tokens.Add(new BooleanToken(true));
            }
            else if (match == "false")
            {
                tokens.Add(new BooleanToken(false));
            }
            else
            {
                throw new LexerException(string.Format(Resource.NotSupportedSymbol, match));
            }
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
                               .Replace("\n", "")
                               .Replace("\r", "");

            var tokens = new List<IToken>();
            Match match = null;

            for (int i = 0; i < function.Length;)
            {
                var letter = function[i];
                if (letter == ' ' || letter == '\t')
                {
                    i++;
                    continue;
                }

                // symbols
                match = regexSymbols.Match(function, i);
                if (match.Success)
                {
                    CreateSymbol(match.Value, tokens);

                    i += match.Length;
                    continue;
                }

                // operations
                match = regexOperations.Match(function, i);
                if (match.Success)
                {
                    CreateOperations(match.Value, tokens);

                    i += match.Length;
                    continue;
                }

                if (char.IsDigit(letter))
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
                    continue;
                }

                if (letter == '°')
                {
                    var magnitude = 0.0;
                    var phase = 1.0;

                    var phaseToken = tokens.LastOrDefault() as NumberToken;
                    if (phaseToken == null)
                        throw new LexerException(string.Format(Resource.NotSupportedSymbol, letter.ToString()));

                    phase = phaseToken.Number;
                    tokens.Remove(phaseToken);

                    // binary +, - or unary -
                    var operationToken = tokens.LastOrDefault() as OperationToken;
                    if (operationToken != null)
                    {
                        if (tokens.Count >= 2 && (operationToken.Operation == Operations.Addition || operationToken.Operation == Operations.Subtraction))
                        {
                            var magnitudeToken = tokens[tokens.Count - 2] as NumberToken;
                            if (magnitudeToken != null)
                            {
                                magnitude = magnitudeToken.Number;

                                if (operationToken.Operation == Operations.Subtraction)
                                    phase = -phase;

                                if (tokens.Count >= 3)
                                {
                                    var unaryRealToken = tokens[tokens.Count - 3] as OperationToken;
                                    if (unaryRealToken != null && unaryRealToken.Operation == Operations.UnaryMinus)
                                    {
                                        magnitude = -magnitude;

                                        tokens.Remove(unaryRealToken);
                                    }
                                }

                                tokens.Remove(operationToken);
                                tokens.Remove(magnitudeToken);
                            }
                        }
                        else if (operationToken.Operation == Operations.UnaryMinus)
                        {
                            phase = -phase;

                            tokens.Remove(operationToken);
                        }
                    }

                    tokens.Add(new ComplexNumberToken(Complex.FromPolarCoordinates(magnitude, phase)));

                    i++;
                    continue;
                }

                // consts
                match = regexConst.Match(function, i);
                if (match.Success)
                {
                    CreateConst(match.Value, tokens);

                    i += match.Length;
                    continue;
                }

                // functions
                match = regexFunctions.Match(function, i);
                if (match.Success)
                {
                    if (!string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        var funcName = match.Groups[1].Value;
                        CreateFunction(funcName, tokens);

                        i += funcName.Length;
                    }
                    else
                    {
                        var lastToken = tokens.LastOrDefault() as NumberToken;
                        if (lastToken != null)
                            tokens.Add(new OperationToken(Operations.Multiplication));

                        if (match.Value == "i")
                            tokens.Add(new ComplexNumberToken(Complex.ImaginaryOne));
                        else if (match.Value == "pi")
                            tokens.Add(new VariableToken("π"));
                        else
                            tokens.Add(new VariableToken(match.Value));

                        i += match.Length;
                    }

                    continue;
                }

                //i++;
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
