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
using System.Globalization;
using System.Numerics;

namespace xFunc.Maths.Tokenization.Tokens
{

    /// <summary>
    /// Represent complex number token.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Tokens.IToken" />
    public class ComplexNumberToken : IToken
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumberToken"/> class.
        /// </summary>
        /// <param name="complex">The complex number.</param>
        public ComplexNumberToken(Complex complex)
        {
            this.Number = complex;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var num = obj as ComplexNumberToken;
            if (num == null)
                return false;

            return Number.Equals(num.Number);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Number.Real == 0)
            {
                if (Number.Imaginary == 1)
                    return "Complex Number: i";
                if (Number.Imaginary == -1)
                    return "Complex Number: -i";

                return $"Complex Number: {Number.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
            }

            if (Number.Imaginary == 0)
                return $"Complex Number: {Number.Real.ToString(CultureInfo.InvariantCulture)}";

            if (Number.Imaginary > 0)
                return $"Complex Number: {Number.Real.ToString(CultureInfo.InvariantCulture)}+{Number.Imaginary.ToString(CultureInfo.InvariantCulture)}i";

            return $"Complex Number: {Number.Real.ToString(CultureInfo.InvariantCulture)}{Number.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority => 101;

        /// <summary>
        /// Gets the complex number.
        /// </summary>
        /// <value>
        /// The complex number.
        /// </value>
        public Complex Number { get; }

    }

}
