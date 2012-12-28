using System;

namespace xFunc.Library.Logics
{

    public class LogicToken
    {

        private LogicTokenType type;
        private double number;
        private char variable;

        public LogicToken() { }

        public LogicToken(LogicTokenType type) : this(type, 0, ' ') { }

        public LogicToken(LogicTokenType type, double number) : this(type, number, ' ') { }

        public LogicToken(LogicTokenType type, char variable) : this(type, 0, variable) { }

        public LogicToken(LogicTokenType type, double number, char variable)
        {
            this.type = type;
            this.number = number;
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            LogicToken token = obj as LogicToken;
            if (token != null && token.Type == type)
            {
                if (token.Type == LogicTokenType.Variable && token.Variable != variable)
                    return false;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (type == LogicTokenType.Variable)
            {
                return string.Format("Var: {0}", variable);
            }

            return type.ToString();
        }

        public LogicTokenType Type
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
