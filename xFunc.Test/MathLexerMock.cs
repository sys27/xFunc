using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test
{

    public class MathLexerMock : IMathLexer
    {

        private List<MathToken> tokens;

        public MathLexerMock()
        {
        }

        public MathLexerMock(IEnumerable<MathToken> tokens)
        {
            this.tokens = new List<MathToken>(tokens);
        }

        public IEnumerable<MathToken> Tokenize(string function)
        {
            return tokens;
        }

        public IEnumerable<MathToken> Tokens
        {
            get
            {
                return tokens;
            }
            set
            {
                tokens = new List<MathToken>(value);
            }
        }

    }

}
