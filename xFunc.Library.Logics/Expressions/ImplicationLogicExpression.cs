using System;

namespace xFunc.Library.Logics.Expressions
{

    public class ImplicationLogicExpression : BinaryLogicExpression
    {

        public ImplicationLogicExpression()
            : base(null, null)
        {

        }

        public ImplicationLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("->");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return !firstOperand.Calculate(parameters) | secondOperand.Calculate(parameters);
        }

    }

}
