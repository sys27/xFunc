using System;
using System.Runtime.Serialization;

namespace xFunc.Maths
{

    [Serializable]
    public class InvalidResultException : Exception
    {

        public InvalidResultException() { }

        public InvalidResultException(string message) : base(message) { }

        public InvalidResultException(string message, Exception inner) : base(message, inner) { }

        protected InvalidResultException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }

}
