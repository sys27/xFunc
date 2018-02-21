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
    /// Represents a operation token.
    /// </summary>
    public class OperationToken : IToken
    {

        /// <summary>
        /// Initializes the <see cref="OperationToken"/> class.
        /// </summary>
        /// <param name="operation">A operation.</param>
        public OperationToken(Operations operation)
        {
            this.Operation = operation;
            this.Priority = GetPriority();
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
            return Operation.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Operation: {Operation}";
        }

        private int GetPriority()
        {
            switch (Operation)
            {
                case Operations.ConditionalAnd:
                case Operations.ConditionalOr:
                    return 8;
                case Operations.Equal:
                case Operations.NotEqual:
                case Operations.LessThan:
                case Operations.LessOrEqual:
                case Operations.GreaterThan:
                case Operations.GreaterOrEqual:
                    return 9;
                case Operations.Addition:
                case Operations.Subtraction:
                    return 10;
                case Operations.Multiplication:
                case Operations.Division:
                case Operations.Modulo:
                    return 11;
                case Operations.Exponentiation:
                    return 12;
                case Operations.Factorial:
                    return 13;
                case Operations.UnaryMinus:
                    return 14;
                case Operations.Assign:
                    return 0;
                case Operations.Not:
                case Operations.And:
                case Operations.Or:
                case Operations.XOr:
                case Operations.Implication:
                case Operations.Equality:
                case Operations.NOr:
                case Operations.NAnd:
                    return 7;
                case Operations.AddAssign:
                case Operations.SubAssign:
                case Operations.MulAssign:
                case Operations.DivAssign:
                    return 16;
                case Operations.Increment:
                case Operations.Decrement:
                    return 17;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Gets the operation.
        /// </summary>
        public Operations Operation { get; }

    }

}
