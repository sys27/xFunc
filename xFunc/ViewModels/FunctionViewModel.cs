using System;
using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{
    
    public class FunctionViewModel
    {

        private UserFunction function;
        private IExpression value;

        public FunctionViewModel(UserFunction function, IExpression value)
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
