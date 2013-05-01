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

        public override bool Equals(object obj)
        {
            FunctionToken token = obj as FunctionToken;
            if (token != null && this.Function == token.Function)
            {
                return true;
            }

            return false;
        }

        public int Priority
        {
            get
            {
                return 100;
            }
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
