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

using System.Collections.Generic;

namespace xFunc.Maths.Tokenization.Factories
{
    /// <summary>
    /// The factory which creates empty result (without token).
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.ITokenFactory" />
    public class EmptyTokenFactory : ITokenFactory
    {
        private readonly HashSet<char> symbols = new HashSet<char>
        {
            ' ', '\n', '\r', '\t', '\v', '\f'
        };

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="function">The string to scan for tokens.</param>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The token.</returns>
        public FactoryResult CreateToken(string function, int startIndex)
        {
            var index = startIndex;
            while (symbols.Contains(function[index]))
                index++;

            var processedLength = index - startIndex;

            return processedLength > 0 ? new FactoryResult(null, processedLength) : null;
        }
    }
}