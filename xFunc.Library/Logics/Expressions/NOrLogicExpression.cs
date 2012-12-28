using System;

namespace xFunc.Library.Logics.Expressions
{

    public class NOrLogicExpression : BinaryLogicExpression
    {

        public NOrLogicExpression()
            : base(null, null)
        {

        }

        public NOrLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("↓");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return !(firstOperand.Calculate(parameters) | secondOperand.Calculate(parameters));
        }

    }

}
