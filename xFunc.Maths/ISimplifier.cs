using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{
   
    public interface ISimplifier
    {

        IMathExpression Simplify(IMathExpression expression);

    }

}
