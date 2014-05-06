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
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Subtraction operation.
    /// </summary>
    public class Sub : BinaryExpression
    {

        internal Sub() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <param name="left">The minuend.</param>
        /// <param name="right">The subtrahend.</param>
        public Sub(IExpression left, IExpression right) : base(left, right) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(5987, 4703);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryExpression)
            {
                return ToString("({0} - {1})");
            }

            return ToString("{0} - {1}");
        }

        /// <summary>
        /// Calculates this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Calculate(ExpressionParameters parameters)
        {
            if (ResultIsMatrix)
            {
                if (left is Vector && right is Vector)
                    return MatrixExtentions.Sub((Vector)left, (Vector)right, parameters);
                if (left is Matrix && right is Matrix)
                    return MatrixExtentions.Sub((Matrix)left, (Matrix)right, parameters);
                if ((left is Vector && right is Matrix) || (right is Vector && left is Matrix))
                    throw new NotSupportedException();

                if (!(left is Vector || left is Matrix))
                {
                    var l = left.Calculate(parameters);

                    if (l is Vector)
                        return MatrixExtentions.Sub((Vector)l, (Vector)right, parameters);
                    if (l is Matrix)
                        return MatrixExtentions.Sub((Matrix)l, (Matrix)right, parameters);

                    throw new NotSupportedException();
                }

                if (!(right is Vector || right is Matrix))
                {
                    var r = right.Calculate(parameters);

                    if (r is Vector)
                        return MatrixExtentions.Sub((Vector)left, (Vector)r, parameters);
                    if (r is Matrix)
                        return MatrixExtentions.Sub((Matrix)left, (Matrix)r, parameters);

                    throw new NotSupportedException();
                }

                throw new NotSupportedException();
            }

            return (double)left.Calculate(parameters) - (double)right.Calculate(parameters);
        }
        
        /// <summary>
        /// Clones this instance of the <see cref="Sub"/> class.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Sub(left.Clone(), right.Clone());
        }

        /// <summary>
        /// Gets a value indicating whether result is a matrix.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result is a matrix; otherwise, <c>false</c>.
        /// </value>
        public override bool ResultIsMatrix
        {
            get
            {
                return left.ResultIsMatrix && right.ResultIsMatrix;
            }
        }

    }

}
