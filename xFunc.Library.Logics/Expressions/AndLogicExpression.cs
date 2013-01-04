using System;

namespace xFunc.Library.Logics.Expressions
{

    public class AndLogicExpression : BinaryLogicExpression
    {

        public AndLogicExpression() : base(null, null) { }

        public AndLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("&");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return firstOperand.Calculate(parameters) & secondOperand.Calculate(parameters);
        }

    }

}
