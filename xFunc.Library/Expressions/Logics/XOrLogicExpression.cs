using System;

namespace xFunc.Library.Expressions.Logics
{

    public class XOrLogicExpression : BinaryLogicExpression
    {

        public XOrLogicExpression()
            : base(null, null)
        {

        }

        public XOrLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("⊕");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return !(firstOperand.Calculate(parameters) & secondOperand.Calculate(parameters)) & (firstOperand.Calculate(parameters) | secondOperand.Calculate(parameters));
        }

    }

}
