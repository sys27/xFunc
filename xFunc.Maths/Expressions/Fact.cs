using System;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Factorial function.
    /// </summary>
    public class Fact : UnaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Fact"/> class.
        /// </summary>
        public Fact()
            : base(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fact"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        public Fact(IMathExpression argument)
            : base(argument)
        {

        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("fact({0})");
        }

        /// <summary>
        /// Calculates this mathemarical expression. Don't use this method if your expression has variables or functions.
        /// </summary>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        public override double Calculate()
        {
            return MathExtentions.Fact(Math.Round(argument.Calculate()));
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Fact(Math.Round(argument.Calculate(parameters)));
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <param name="functions">A collection of functions that are used in the expression.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="MathParameterCollection" />
        /// <seealso cref="MathFunctionCollection" />
        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return MathExtentions.Fact(Math.Round(argument.Calculate(parameters, functions)));
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IMathExpression" /> that is a clone of this instance.
        /// </returns>
        public override IMathExpression Clone()
        {
            return new Fact(argument.Clone());
        }

        /// <summary>
        /// Always throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns>
        /// Throws an exception.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="NotSupportedException">Always.</exception>
        protected override IMathExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
