using System;

namespace xFunc.Library.Logics.Expressions
{

    public abstract class BinaryLogicExpression : ILogicExpression
    {

        protected ILogicExpression firstOperand;
        protected ILogicExpression secondOperand;

        public BinaryLogicExpression(ILogicExpression firstOperand, ILogicExpression secondOperand)
        {
            this.firstOperand = firstOperand;
            this.secondOperand = secondOperand;
        }

        protected string ToString(string operand)
        {
            string first;
            string second;

            if (firstOperand is VariableLogicExpression || firstOperand is ConstLogicExpression)
                first = firstOperand.ToString();
            else
                first = "(" + firstOperand + ")";

            if (secondOperand is VariableLogicExpression || secondOperand is ConstLogicExpression)
                second = secondOperand.ToString();
            else
                second = "(" + secondOperand + ")";

            return first + " " + operand + " " + second;
        }

        public abstract bool Calculate(LogicParameterCollection parameters);

        public ILogicExpression FirstOperand
        {
            get
            {
                return firstOperand;
            }
            set
            {
                firstOperand = value;
            }
        }

        public ILogicExpression SecondOperand
        {
            get
            {
                return secondOperand;
            }
            set
            {
                secondOperand = value;
            }
        }

    }

}
