using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.ViewModels
{

    public class VariableViewModel
    {

        private Parameter parameter;

        public VariableViewModel(Parameter parameter)
        {
            this.parameter = parameter;
        }

        public string Variable
        {
            get
            {
                return parameter.Key;
            }
        }

        public double Value
        {
            get
            {
                return parameter.Value;
            }
        }

        public ParameterType Type
        {
            get
            {
                return parameter.Type;
            }
        }

    }

}
