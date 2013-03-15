using System;

namespace xFunc.Logics.Expressions
{
    
    public class Undefine : ILogicExpression
    {

        private Variable variable;

        public Undefine()
            : this(null)
        {

        }

        public Undefine(Variable variable)
        {
            this.variable = variable;
        }

        public bool Calculate(LogicParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            parameters.Remove(variable.Character);

            return false;
        }

        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

    }

}
