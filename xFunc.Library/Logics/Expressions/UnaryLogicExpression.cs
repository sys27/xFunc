using System;

namespace xFunc.Library.Logics.Expressions
{

    public abstract class UnaryLogicExpression : ILogicExpression
    {

        protected ILogicExpression firstMathExpression;

        public UnaryLogicExpression(ILogicExpression firstMathExpression)
        {
            this.firstMathExpression = firstMathExpression;
        }

        protected string ToString(string operand)
        {
            if (firstMathExpression is VariableLogicExpression)
                return string.Format("{0}{1}", operand, firstMathExpression);

            return string.Format("{0}({1})", operand, firstMathExpression);
        }

        public abstract bool Calculate(LogicParameterCollection parameters);

        public ILogicExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
            }
        }

    }

}
