using System;

namespace xFunc.Library.Maths.Exceptions
{

    [Serializable]
    public class MathLexerException : Exception
    {

        public MathLexerException() { }

        public MathLexerException(string message) : base(message) { }
        
        public MathLexerException(string message, Exception inner) : base(message, inner) { }
        
        protected MathLexerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}