// Copyright 2012-2021 Dmytro Kyshchenko
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

using xFunc.Maths.Resources;

namespace xFunc.Maths.Tokenization
{
    /// <summary>
    /// The lexer for mathematical expressions.
    /// </summary>
    internal ref partial struct Lexer
    {
        private bool CreateStringToken(char quote)
        {
            if (function[0] != quote)
                return false;

            var endIndex = 1;
            var foundClosingQuote = false;
            while (endIndex < function.Length)
            {
                if (function[endIndex] == quote)
                {
                    foundClosingQuote = true;
                    break;
                }

                endIndex++;
            }

            if (!foundClosingQuote)
                throw new TokenizeException(Resource.StringTokenizeException);

            var stringValue = function[1..endIndex];

            Current = Token.String(stringValue.ToString());

            function = function[(endIndex + 1)..];

            return true;
        }

        private bool CreateStringToken()
            => CreateStringToken('"') ||
               CreateStringToken('\'');
    }
}