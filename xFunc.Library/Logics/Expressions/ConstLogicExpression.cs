using System;

namespace xFunc.Library.Logics.Expressions
{
    
    public class ConstLogicExpression : ILogicExpression
    {

        private bool value;

        public ConstLogicExpression()
        {
            
        }

        public ConstLogicExpression(bool value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public bool Calculate(LogicParameterCollection parameters)
        {
            return value;
        }

    }

}
