using System;

namespace xFunc.Library.Maths.Exceptions
{

    [Serializable]
    public class MathParserException : Exception
    {

        public MathParserException() { }

        public MathParserException(string message) : base(message) { }

        public MathParserException(string message, Exception inner) : base(message, inner) { }

        protected MathParserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}
