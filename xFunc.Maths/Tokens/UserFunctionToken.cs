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

        public override bool Equals(object obj)
        {
            UserFunctionToken token = obj as UserFunctionToken;
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

        public string Function
        {
            get
            {
                return function;
            }
        }

    }

}
