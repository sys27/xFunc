using System;

namespace xFunc.Library.Logics.Exceptions
{

    [Serializable]
    public class LogicParserException : Exception
    {

        public LogicParserException() { }

        public LogicParserException(string message) : base(message) { }

        public LogicParserException(string message, Exception inner) : base(message, inner) { }

        protected LogicParserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

}
