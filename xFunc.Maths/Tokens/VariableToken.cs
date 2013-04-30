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

        public string Variable
        {
            get
            {
                return variable;
            }
        }

    }

}
