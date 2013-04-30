using System;

namespace xFunc.Maths.Tokens
{
    
    public class FunctionToken : IToken
    {

        private Functions function;

        public FunctionToken(Functions function)
        {
            this.function = function;
        }

        public Functions Function
        {
            get
            {
                return function;
            }
        }

    }

}
