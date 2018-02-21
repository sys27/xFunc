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
    /// Represents a user-function token.
    /// </summary>
    public class UserFunctionToken : FunctionToken
    {

        private readonly string functionName;

        /// <summary>
        /// Initializes the <see cref="UserFunctionToken"/> class.
        /// </summary>
        /// <param name="functionName">A name of function.</param>
        public UserFunctionToken(string functionName) : this(functionName, -1) { }

        /// <summary>
        /// Initializes the <see cref="UserFunctionToken"/> class.
        /// </summary>
        /// <param name="functionName">A name of function.</param>
        /// <param name="countOfParams">A count of parameters.</param>
        public UserFunctionToken(string functionName, int countOfParams)
            : base(Functions.UserFunction, countOfParams)
        {
            this.functionName = functionName;
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

            if (typeof(UserFunctionToken) != obj.GetType())
                return false;

            var token = (UserFunctionToken)obj;

            return this.functionName == token.functionName && this.CountOfParams == token.CountOfParams;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 4909;

            hash = hash * 1877 + functionName.GetHashCode();
            hash = hash * 1877 + CountOfParams.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"User Function: {functionName}";
        }

        /// <summary>
        /// Gets the name of function.
        /// </summary>
        public string FunctionName
        {
            get
            {
                return functionName;
            }
        }

    }

}
