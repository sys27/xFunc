using System;

namespace xFunc.Library.Logics.Expressions
{
    
    public interface ILogicExpression
    {

        bool Calculate(LogicParameterCollection parameters);

    }

}
