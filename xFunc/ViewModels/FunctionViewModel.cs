using System;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{
    
    public class FunctionViewModel
    {

        private UserFunction function;
        private IMathExpression value;

        public FunctionViewModel(UserFunction function, IMathExpression value)
        {
            this.function = function;
            this.value = value;
        }

        public UserFunction Function
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
                return value.ToString();
            }
        }

    }

}
