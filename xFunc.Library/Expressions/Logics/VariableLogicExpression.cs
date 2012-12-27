using System;

namespace xFunc.Library.Expressions.Logics
{
    
    public class VariableLogicExpression : ILogicExpression
    {

        private char variable;

        public VariableLogicExpression(char variable)
        {
            this.variable = variable;
        }

        public override string ToString()
        {
            return variable.ToString();
        }

        public bool Calculate(LogicParameterCollection parameters)
        {
            return parameters[variable];
        }

    }

}
