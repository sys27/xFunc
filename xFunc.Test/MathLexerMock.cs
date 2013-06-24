using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Tokens;

namespace xFunc.Test
{

    public class MathLexerMock : ILexer
    {

        private List<IToken> tokens;

        public MathLexerMock()
        {
        }

        public MathLexerMock(IEnumerable<IToken> tokens)
        {
            this.tokens = new List<IToken>(tokens);
        }

        public IEnumerable<IToken> Tokenize(string function)
        {
            return tokens;
        }

        public IEnumerable<IToken> Tokens
        {
            get
            {
                return tokens;
            }
            set
            {
                tokens = new List<IToken>(value);
            }
        }

    }

}
