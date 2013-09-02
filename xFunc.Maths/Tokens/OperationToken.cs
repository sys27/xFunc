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
    /// Represents a operation token.
    /// </summary>
    public class OperationToken : IToken
    {

        private Operations operation;
        private int priority;

        /// <summary>
        /// Initializes the <see cref="OperationToken"/> class.
        /// </summary>
        /// <param name="operation">A operation.</param>
        public OperationToken(Operations operation)
        {
            this.operation = operation;

            SetPriority();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (typeof(OperationToken) != obj.GetType())
                return false;

            var token = obj as OperationToken;
            
            return this.Operation == token.Operation;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return operation.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "Operation: " + operation;
        }

        private void SetPriority()
        {
            switch (operation)
            {
                case Operations.Addition:
                    priority = 10;
                    break;
                case Operations.Subtraction:
                    priority = 10;
                    break;
                case Operations.Multiplication:
                    priority = 11;
                    break;
                case Operations.Division:
                    priority = 11;
                    break;
                case Operations.Exponentiation:
                    priority = 12;
                    break;
                case Operations.UnaryMinus:
                    priority = 13;
                    break;
                case Operations.Assign:
                    priority = 0;
                    break;
                case Operations.Not:
                    priority = 15;
                    break;
                case Operations.And:
                    priority = 15;
                    break;
                case Operations.Or:
                    priority = 15;
                    break;
                case Operations.XOr:
                    priority = 15;
                    break;
            }
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority
        {
            get
            {
                return priority;
            }
        }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        public Operations Operation
        {
            get
            {
                return operation;
            }
        }

    }

}
