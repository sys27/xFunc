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

        public override bool Equals(object obj)
        {
            NumberToken token = obj as NumberToken;
            if (token != null && this.Number == token.Number)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Number: " + number;
        }

        public int Priority
        {
            get
            {
                return 101;
            }
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
