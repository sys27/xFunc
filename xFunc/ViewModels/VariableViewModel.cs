using System;

namespace xFunc.ViewModels
{

    public class VariableViewModel
    {

        private string variable;
        private string value;
        private bool isReadOnly;

        public VariableViewModel(string variable, string value, bool isReadOnly)
        {
            this.variable = variable;
            this.value = value;
            this.isReadOnly = isReadOnly;
        }

        public string Variable
        {
            get
            {
                return variable;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
        }

    }

}
