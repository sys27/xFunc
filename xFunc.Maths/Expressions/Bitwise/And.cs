using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    /// <summary>
    /// Represents a bitwise AND operation.
    /// </summary>
    public class And : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        public And()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The left operand.</param>
        /// <param name="secondMathExpression">The right operand.</param>
        /// <seealso cref="IMathExpression"/>
        public And(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} and {1})");
            }

            return ToString("{0} and {1}");
        }

        /// <summary>
        /// Calculates this bitwise AND expression. Don't use this method if your expression has variables.
        /// </summary>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate()
        {
            return (int)firstMathExpression.Calculate() & (int)secondMathExpression.Calculate();
        }

        /// <summary>
        /// Calculates this bitwise AND expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)firstMathExpression.Calculate(parameters) & (int)secondMathExpression.Calculate(parameters);
        }

        /// <summary>
        /// Clones this instanse of the <see cref="And"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new And(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        /// <summary>
        /// This method always throws the <see cref="NotSupportedException"/> exception.
        /// </summary>
        /// <param name="variable">This method always throws the <see cref="NotSupportedException"/> exception.</param>
        /// <returns>This method always throws the <see cref="NotSupportedException"/> exception.</returns>
        /// <exception cref="NotSupportedException">Always is thrown.</exception>
        public override IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
