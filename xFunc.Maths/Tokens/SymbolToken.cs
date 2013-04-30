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

        public Symbols Symbol
        {
            get
            {
                return symbol;
            }
        }

    }

}
