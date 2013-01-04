using System;

namespace xFunc.Library.Logics.Exceptions
{

    [Serializable]
    public class LogicLexerException : Exception
    {

        public LogicLexerException() { }

        public LogicLexerException(string message) : base(message) { }
        
        public LogicLexerException(string message, Exception inner) : base(message, inner) { }
        
        protected LogicLexerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}