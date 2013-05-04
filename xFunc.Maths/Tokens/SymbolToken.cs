using System;

namespace xFunc.Maths.Tokens
{

    public class SymbolToken : IToken
    {

        private Symbols symbol;
        private int priority;

        public SymbolToken(Symbols symbol)
        {
            this.symbol = symbol;

            SetPriority();
        }

        public override bool Equals(object obj)
        {
            SymbolToken token = obj as SymbolToken;
            if (token != null && this.Symbol == token.Symbol)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Symbol: " + symbol;
        }

        private void SetPriority()
        {
            switch (symbol)
            {
                case Symbols.OpenBracket:
                    priority = 1;
                    break;
                case Symbols.CloseBracket:
                    priority = 2;
                    break;
                case Symbols.Comma:
                    priority = 3;
                    break;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }
        }

        public Symbols Symbol
        {
            get
            {
                return symbol;
            }
        }

    }

}
