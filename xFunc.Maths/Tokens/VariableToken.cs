using System;

namespace xFunc.Maths.Tokens
{

    public class VariableToken : IToken
    {

        private string variable;

        public VariableToken(string variable)
        {
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            VariableToken token = obj as VariableToken;
            if (token != null && this.Variable == token.Variable)
            {
                return true;
            }

            return false;
        }

        public int Priority
        {
            get
            {
                return 102;
            }
        }

        public string Variable
        {
            get
            {
                return variable;
            }
        }

    }

}
