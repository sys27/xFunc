using System;
using System.Collections.Generic;

namespace xFunc.Logics
{

    public interface IMathLexer
    {

        /// <summary>
        /// Converts the string into a sequence of tokens.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The sequence of tokens.</returns>
        IEnumerable<LogicToken> Tokenize(string function);

    }

}
