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
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Multiplication operation.
    /// </summary>
    public class Mul : BinaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        internal Mul() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="left">The first (left) operand.</param>
        /// <param name="right">The second (right) operand.</param>
        public Mul(IExpression left, IExpression right) : base(left, right) { }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(7537, 1973);
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parent is BinaryExpression)
            {
                return ToString("({0} * {1})");
            }

            return ToString("{0} * {1}");
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override object Calculate(ExpressionParameters parameters)
        {
            // todo: tests
            if (left is Vector)
            {
                if (right is Matrix)
                    return MatrixExtentions.Mul((Vector)left, (Matrix)right, parameters);

                return MatrixExtentions.Mul((Vector)left, right, parameters);
            }
            if (right is Vector)
            {
                if (left is Matrix)
                    return MatrixExtentions.Mul((Matrix)left, (Vector)right, parameters);

                return MatrixExtentions.Mul((Vector)right, left, parameters);
            }

            if (left is Matrix && right is Matrix)
                return MatrixExtentions.Mul((Matrix)left, (Matrix)right, parameters);
            if (left is Matrix)
                return MatrixExtentions.Mul((Matrix)left, right, parameters);
            if (right is Matrix)
                return MatrixExtentions.Mul((Matrix)right, left, parameters);

            return (double)left.Calculate(parameters) * (double)right.Calculate(parameters);
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        public override IExpression Differentiate(Variable variable)
        {
            var first = Parser.HasVar(left, variable);
            var second = Parser.HasVar(right, variable);

            if (first && second)
            {
                var mul1 = new Mul(left.Clone().Differentiate(variable), right.Clone());
                var mul2 = new Mul(left.Clone(), right.Clone().Differentiate(variable));
                var add = new Add(mul1, mul2);

                return add;
            }
            if (first)
            {
                return new Mul(left.Clone().Differentiate(variable), right.Clone());
            }
            if (second)
            {
                return new Mul(left.Clone(), right.Clone().Differentiate(variable));
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Mul(left.Clone(), right.Clone());
        }

    }

}
