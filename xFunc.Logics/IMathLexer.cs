using System;
using System.Collections.Generic;

namespace xFunc.Logics
{

    public interface IMathLexer
    {

        IEnumerable<LogicToken> Tokenize(string function);

    }

}
