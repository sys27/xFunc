using System;

namespace xFunc.Library.Exceptions
{

    [Serializable]
    public class LexerException : Exception
    {

        public LexerException() { }

        public LexerException(string message) : base(message) { }
        
        public LexerException(string message, Exception inner) : base(message, inner) { }
        
        protected LexerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}