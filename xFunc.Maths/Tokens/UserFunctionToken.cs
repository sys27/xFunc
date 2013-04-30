using System;

namespace xFunc.Maths.Tokens
{
    
    public class UserFunctionToken : IToken
    {

        private string function;

        public UserFunctionToken(string function)
        {
            this.function = function;
        }

        public string Function
        {
            get
            {
                return function;
            }
        }

    }

}
