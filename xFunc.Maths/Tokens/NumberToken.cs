using System;

namespace xFunc.Maths.Tokens
{
    
    public class NumberToken : IToken
    {

        private double number;

        public NumberToken(double number)
        {
            this.number = number;
        }

        public double Number
        {
            get
            {
                return number;
            }
        }

    }

}
