using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public interface ISolver
    {

        double Solve(IMathExpression expression);

    }

}
