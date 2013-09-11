using System;

namespace xFunc.Maths.Expressions.Hyperbolic
{

    /// <summary>
    /// The base class for hyperbolic functions.
    /// </summary>
    public abstract class HyperbolicMathExpression : UnaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicMathExpression"/> class.
        /// </summary>
        /// <param name="argument">The expression.</param>
        protected HyperbolicMathExpression(IMathExpression argument)
            : base(argument)
        {

        }

    }

}
