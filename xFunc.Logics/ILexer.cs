using System;
using System.Collections.Generic;

namespace xFunc.Logics
{

    public interface ILexer
    {

        IEnumerable<LogicToken> Tokenize(string function);

    }

}
