﻿// Copyright 2012-2020 Dmytro Kyshchenko
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

        private readonly ITokenFactory[] factories;

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
                new OperationTokenFactory(),
                new NumberHexTokenFactory(),
                new NumberBinTokenFactory(),
                new NumberOctTokenFactory(),
                new NumberTokenFactory(),
                new IdTokenFactory()
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="factories">The factories to create tokens.</param>
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
            var brackets = 0;
            var braces = 0;

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
        /// <exception cref="TokenizeException">Throws when <paramref name="function"/> has the not supported symbol.</exception>
        public IEnumerable<IToken> Tokenize(string function)
        {
            if (string.IsNullOrWhiteSpace(function))
                throw new ArgumentNullException(nameof(function), Resource.NotSpecifiedFunction);
            if (!IsBalanced(function))
                throw new TokenizeException(Resource.NotBalanced);

            function = function.ToLower();

            var tokens = new List<IToken>();
            for (var i = 0; i < function.Length;)
            {
                FactoryResult result = null;
                foreach (var factory in factories)
                {
                    result = factory.CreateToken(function, i);
                    if (result == null)
                        continue;

                    i += result.ProcessedLength;
                    if (result.Token != null)
                        tokens.Add(result.Token);

                    break;
                }

                if (result == null)
                    throw new TokenizeException(string.Format(Resource.NotSupportedSymbol, function));
            }

            return tokens;
        }

    }

}
