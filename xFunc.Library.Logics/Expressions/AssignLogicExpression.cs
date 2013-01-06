using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Logics.Expressions
{

    public class AssignLogicExpression : ILogicExpression
    {

        private VariableLogicExpression variable;
        private ILogicExpression value;

        public AssignLogicExpression()
        {

        }

        public AssignLogicExpression(VariableLogicExpression variable, ILogicExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} := {1}", variable, value);
        }

        public bool Calculate(LogicParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var localValue = value.Calculate(parameters);
            parameters.Add(variable.Variable);
            parameters[variable.Variable] = localValue;

            return false;
        }

        public VariableLogicExpression Variable
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

        public ILogicExpression Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

    }

}
