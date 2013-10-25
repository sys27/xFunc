using System;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{

    public class VariableViewModel
    {

        private MathParameter parameter;

        public VariableViewModel(MathParameter parameter)
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

        public string Value
        {
            get
            {
                return parameter.Value.ToString();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return parameter.IsReadOnly;
            }
        }

    }

}
