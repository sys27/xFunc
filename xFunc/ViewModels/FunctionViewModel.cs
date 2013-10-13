using System;

namespace xFunc.ViewModels
{
    
    public class FunctionViewModel
    {

        private string function;
        private string value;

        public FunctionViewModel(string function, string value)
        {
            this.function = function;
            this.value = value;
        }

        public string Function
        {
            get
            {
                return function;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

    }

}
