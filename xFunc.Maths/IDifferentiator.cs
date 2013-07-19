using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public interface IDifferentiator
    {

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        IMathExpression Differentiate(IMathExpression expression);
        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        IMathExpression Differentiate(IMathExpression expression, Variable variable);

    }

}
