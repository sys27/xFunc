// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using xFunc.Maths.Resources;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal ref partial struct Lexer
    {
        private ReadOnlySpan<char> function;
        private IToken? current;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> struct.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <exception cref="ArgumentNullException">Throws when the <paramref name="function"/> parameter is null or empty.</exception>
        public Lexer(string function)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(function), Resource.NotSpecifiedFunction);

            current = default;
            this.function = function.AsSpan();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <exception cref="TokenizeException">Thrown when input string has the not supported symbol.</exception>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element;
        /// <c>false</c> if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            while (function.Length > 0)
            {
                current = SkipWhiteSpaces(ref function) ??
                          CreateNumberToken(ref function) ??
                          CreateIdToken(ref function) ??
                          CreateOperatorToken(ref function) ??
                          CreateSymbol(ref function);

                if (current == null)
                    throw new TokenizeException(string.Format(CultureInfo.InvariantCulture, Resource.NotSupportedSymbol, function[0]));

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Compare(ReadOnlySpan<char> id, string str)
            => id.Equals(str, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <returns>
        /// The element in the collection at the current position of the enumerator.
        /// </returns>
        public IToken? Current => current;
    }
}