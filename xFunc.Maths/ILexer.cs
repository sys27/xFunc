using System;
using System.Collections.Generic;

namespace xFunc.Maths
{

    public interface ILexer
    {

        IEnumerable<MathToken> Tokenize(string function);

    }

}
