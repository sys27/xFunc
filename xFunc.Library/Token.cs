using System;

namespace xFunc.Library
{

    public class Token
    {

        private TokenType type;
        private double number;
        private char variable;

        public Token() { }

        public Token(TokenType type) : this(type, 0, ' ') { }

        public Token(TokenType type, double number) : this(type, number, ' ') { }

        public Token(TokenType type, char variable) : this(type, 0, variable) { }

        public Token(TokenType type, double number, char variable)
        {
            this.type = type;
            this.number = number;
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            Token token = obj as Token;
            if (token != null && token.Type == type)
            {
                if (token.Type == TokenType.Variable && token.Variable != variable)
                    return false;
                if (token.Type == TokenType.Number && token.Number != number)
                    return false;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (type == TokenType.Number)
            {
                return string.Format("Number: {0}", number);
            }
            if (type == TokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

        public TokenType Type
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
