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
        internal Fact() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fact"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        public Fact(IMathExpression argument)
            : base(argument)
        {

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(1453);
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
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override double Calculate(ExpressionParameters parameters)
        {
            return MathExtentions.Fact(Math.Round(argument.Calculate(parameters)));
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
