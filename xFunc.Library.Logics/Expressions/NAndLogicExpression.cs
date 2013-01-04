using System;

namespace xFunc.Library.Logics.Expressions
{
    
    public class NAndLogicExpression : BinaryLogicExpression
    {

        public NAndLogicExpression()
            : base(null, null)
        {

        }

        public NAndLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("↑");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return !(firstOperand.Calculate(parameters) & secondOperand.Calculate(parameters));
        }

    }

}
