using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{
    
    public interface IDifferentiator
    {

        IMathExpression Differentiate(IMathExpression expression);
        IMathExpression Differentiate(IMathExpression expression, Variable variable);

    }

}
