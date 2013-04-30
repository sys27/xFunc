using System;

namespace xFunc.Maths.Tokens
{
   
    public class SymbolToken : IToken
    {

        private Symbols symbol;

        public SymbolToken(Symbols symbol)
        {
            this.symbol = symbol;
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

        public Symbols Symbol
        {
            get
            {
                return symbol;
            }
        }

    }

}
