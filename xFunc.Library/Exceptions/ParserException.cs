using System;

namespace xFunc.Library.Exceptions
{

    [Serializable]
    public class ParserException : Exception
    {

        public ParserException() { }

        public ParserException(string message) : base(message) { }

        public ParserException(string message, Exception inner) : base(message, inner) { }

        protected ParserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}
