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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Factories;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{

    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    public class Lexer : ILexer
    {

        private ITokenFactory[] factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        public Lexer()
        {
            factories = new ITokenFactory[]
            {
                new EmptyTokenFactory(),
                new SymbolTokenFactory(),
                new ComplexNumberTokenFactory(),
                new OpeartionTokenFactory(),
                new NumberHexTokenFactory(),
                new NumberBinTokenFactory(),
                new NumberOctTokenFactory(),
                new NumberTokenFactory(),
                new ConstantTokenFactory(),
                new FunctionTokenFactory(),
                new VariableTokenFactory()
            };
        }

        public Lexer(ITokenFactory[] factories)
        {
            this.factories = factories;
        }

        /// <summary>
        /// Determines whether brackets in the specified string is balanced.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is balanced; otherwise, <c>false</c>.
        /// </returns>
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

            function = function.Replace("\n", "").Replace("\r", "");

            var tokens = new List<IToken>();
            for (var i = 0; i < function.Length;)
            {
                FactoryResult result = null;
                foreach (var factory in factories)
                {
                    result = factory.CreateToken(function, i, tokens);
                    if (result != null)
                    {
                        i += result.ProcessedLength;
                        if (result.Token != null)
                            tokens.Add(result.Token);

                        break;
                    }
                }

                if (result == null)
                    throw new LexerException(string.Format(Resource.NotSupportedSymbol, function));
            }

            return CountParams(tokens);
        }

        private int CountParamsInternal(List<IToken> tokens, int index)
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
                if (token is SymbolToken symbol)
                {
                    if (symbol.Symbol == Symbols.OpenBrace)
                    {
                        hasBraces = true;
                    }

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
                else if (token is FunctionToken function)
                {
                    if (oneParam)
                    {
                        countOfParams++;
                        oneParam = false;
                    }

                    if (function.Function == Functions.Matrix || function.Function == Functions.Vector)
                        hasBraces = true;

                    i = CountParamsInternal(tokens, i) + 1;
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

        /// <summary>
        /// Calculates the number of parametes of functions.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        /// <returns>The list of tokens.</returns>
        private IEnumerable<IToken> CountParams(List<IToken> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is FunctionToken)
                {
                    i = CountParamsInternal(tokens, i);
                }
            }

            return tokens;
        }

    }

}
