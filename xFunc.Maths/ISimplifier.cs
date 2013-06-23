using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{
   
    public interface ISimplifier
    {

        /// <summary>
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        IMathExpression Simplify(IMathExpression expression);

    }

}
