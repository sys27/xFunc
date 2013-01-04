using System;

namespace xFunc.Library.Maths
{

    public class MathToken
    {

        private MathTokenType type;
        private double number;
        private char variable;

        public MathToken() { }

        public MathToken(MathTokenType type) : this(type, 0, ' ') { }

        public MathToken(MathTokenType type, double number) : this(type, number, ' ') { }

        public MathToken(MathTokenType type, char variable) : this(type, 0, variable) { }

        public MathToken(MathTokenType type, double number, char variable)
        {
            this.type = type;
            this.number = number;
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            MathToken token = obj as MathToken;
            if (token != null && token.Type == type)
            {
                if (token.Type == MathTokenType.Variable && token.Variable != variable)
                    return false;
                if (token.Type == MathTokenType.Number && token.Number != number)
                    return false;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (type == MathTokenType.Number)
            {
                return string.Format("Number: {0}", number);
            }
            if (type == MathTokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

        public MathTokenType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public double Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public char Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

    }

}
