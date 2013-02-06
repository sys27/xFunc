using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Logics
{

    public interface ILexer
    {

        IEnumerable<LogicToken> Tokenize(string function);

    }

}
