using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{

    /// <summary>
    /// Factory of expressions.
    /// </summary>
    public interface IExpressionFactory
    {

        /// <summary>
        /// Creates a expression from specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The expression.</returns>
        IMathExpression Create(IToken token);

    }

}
