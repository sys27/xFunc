using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths
{

    public interface ILexer
    {

        IEnumerable<MathToken> Tokenization(string function);

    }

}
