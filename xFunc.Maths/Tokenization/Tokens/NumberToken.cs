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
using System.Globalization;

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a number token.
    /// </summary>
    [DebuggerDisplay("Number: {" + nameof(Number) + "}")]
    public class NumberToken : IToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberToken"/> class.
        /// </summary>
        /// <param name="number">A number.</param>
        public NumberToken(double number)
        {
            Number = number;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (typeof(NumberToken) != obj.GetType())
                return false;

            var token = (NumberToken)obj;

            return MathExtensions.Equals(Number, token.Number);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Number.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets the number.
        /// </summary>
        public double Number { get; }
    }
}