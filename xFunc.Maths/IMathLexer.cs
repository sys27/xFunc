using System;
using System.Collections.Generic;

namespace xFunc.Maths
{

    public interface IMathLexer
    {

        IEnumerable<MathToken> Tokenize(string function);

    }

}
