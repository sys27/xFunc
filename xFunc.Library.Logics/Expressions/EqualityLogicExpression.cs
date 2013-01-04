using System;

namespace xFunc.Library.Logics.Expressions
{

    public class EqualityLogicExpression : BinaryLogicExpression
    {

        public EqualityLogicExpression()
            : base(null, null)
        {

        }

        public EqualityLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
            : base(firstOperand, secondOperand)
        {

        }

        public override string ToString()
        {
            return ToString("<->");
        }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            return (firstOperand.Calculate(parameters) & secondOperand.Calculate(parameters)) | (!firstOperand.Calculate(parameters) & !secondOperand.Calculate(parameters));
        }

    }

}
