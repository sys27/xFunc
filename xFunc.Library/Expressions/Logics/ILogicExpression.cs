using System;

namespace xFunc.Library.Expressions.Logics
{
    
    public interface ILogicExpression
    {

        bool Calculate(LogicParameterCollection parameters);

    }

}
