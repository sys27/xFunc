using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    /// <summary>
    /// Represents a bitwise NOT operation.
    /// </summary>
    public class Not : UnaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        public Not()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        /// <param name="firstMathExpression">The argument of function.</param>
        /// <seealso cref="IMathExpression"/>
        public Not(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("not({0})");
        }

        /// <summary>
        /// Calculates this bitwise NOT expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        public override double Calculate(MathParameterCollection parameters)
        {
            return ~(int)firstMathExpression.Calculate(parameters);
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Not"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Not(firstMathExpression.Clone());
        }

        /// <summary>
        /// This method always throws the <see cref="NotSupportedException"/> exception.
        /// </summary>
        /// <param name="variable">This method always throws the <see cref="NotSupportedException"/> exception.</param>
        /// <returns>This method always throws the <see cref="NotSupportedException"/> exception.</returns>
        /// <exception cref="NotSupportedException">Always is thrown.</exception>
        protected override IMathExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
