// Copyright 2012-2014 Dmitry Kischenko
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

        private readonly Operations operation;
        private readonly int priority;

        /// <summary>
        /// Initializes the <see cref="OperationToken"/> class.
        /// </summary>
        /// <param name="operation">A operation.</param>
        public OperationToken(Operations operation)
        {
            this.operation = operation;
            this.priority = GetPriority();
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

            if (typeof(OperationToken) != obj.GetType())
                return false;

            var token = (OperationToken)obj;

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

        private int GetPriority()
        {
            switch (operation)
            {
                case Operations.ConditionalAnd:
                    return 8;
                case Operations.ConditionalOr:
                    return 8;
                case Operations.Equal:
                    return 9;
                case Operations.NotEqual:
                    return 9;
                case Operations.LessThen:
                    return 9;
                case Operations.LessOrEqual:
                    return 9;
                case Operations.GreaterThen:
                    return 9;
                case Operations.GreaterOrEqual:
                    return 9;
                case Operations.Addition:
                    return 10;
                case Operations.Subtraction:
                    return 10;
                case Operations.Multiplication:
                    return 11;
                case Operations.Division:
                    return 11;
                case Operations.Exponentiation:
                    return 12;
                case Operations.Factorial:
                    return 13;
                case Operations.UnaryMinus:
                    return 14;
                case Operations.Assign:
                    return 0;
                case Operations.BitwiseNot:
                    return 15;
                case Operations.BitwiseAnd:
                    return 15;
                case Operations.BitwiseOr:
                    return 15;
                case Operations.BitwiseXOr:
                    return 15;
                default:
                    return -1;
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
