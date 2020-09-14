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

using System.Diagnostics;

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a number token.
    /// </summary>
    [DebuggerDisplay("Number: {" + nameof(Number) + "}")]
    internal sealed class NumberToken : IToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberToken"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public NumberToken(double number) => Number = number;

        /// <summary>
        /// Gets the number.
        /// </summary>
        public double Number { get; }
    }
}