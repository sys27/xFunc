// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths.Tokens
{

    /// <summary>
    /// Represents a function token.
    /// </summary>
    public class FunctionToken : IToken
    {

        private Functions function;
        /// <summary>
        /// The count of parameters
        /// </summary>
        protected int countOfParams;

        /// <summary>
        /// Initializes the <see cref="FunctionToken" /> class.
        /// </summary>
        /// <param name="function">A function.</param>
        public FunctionToken(Functions function) :
            this(function, -1)
        {
        }

        /// <summary>
        /// Initializes the <see cref="FunctionToken" /> class.
        /// </summary>
        /// <param name="function">A function.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        public FunctionToken(Functions function, int countOfParams)
        {
            this.function = function;
            this.countOfParams = countOfParams;
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

            if (typeof(FunctionToken) != obj.GetType())
                return false;

            var token = (FunctionToken)obj;

            return this.function == token.function && this.countOfParams == token.countOfParams;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 6949;

            hash = hash * 5437 + function.GetHashCode();
            hash = hash * 5437 + countOfParams.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Function: {0} ({1})", function, countOfParams);
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// Gets the function.
        /// </summary>
        public Functions Function
        {
            get
            {
                return function;
            }
        }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int CountOfParams
        {
            get
            {
                return countOfParams;
            }
            internal set
            {
                countOfParams = value;
            }
        }

    }

}
