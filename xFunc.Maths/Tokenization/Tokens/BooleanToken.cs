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

namespace xFunc.Maths.Tokenization.Tokens
{

    /// <summary>
    /// Represents the boolean token.
    /// </summary>
    public class BooleanToken : IToken
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToken"/> class.
        /// </summary>
        /// <param name="value">The value of this token.</param>
        public BooleanToken(bool value)
        {
            this.Value = value;
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
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (typeof(BooleanToken) != obj.GetType())
                return false;

            var token = (BooleanToken)obj;

            return this.Value == token.Value;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Boolean: {Value}";
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority => 101;

        /// <summary>
        /// Gets a value.
        /// </summary>
        /// <value>
        /// The value of this token.
        /// </value>
        public bool Value { get; }

    }

}
