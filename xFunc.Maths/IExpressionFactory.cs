using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokens;

namespace xFunc.Maths
{
    
    public interface IExpressionFactory
    {

        IMathExpression Create(IToken token);

    }

}
