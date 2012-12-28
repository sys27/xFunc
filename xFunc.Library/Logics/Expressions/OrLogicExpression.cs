using System;

namespace xFunc.Library.Logics.Expressions
{

    public class OrLogicExpression : BinaryLogicExpression
    {

        public OrLogicExpression()
            : base(null, null)
        {

        }

        public OrLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("|");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return firstOperand.Calculate(parameters) | secondOperand.Calculate(parameters);
        }

    }

}
