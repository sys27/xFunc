using System;
using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions.Matrices
{

    [Serializable]
    public class VectorIsInvalidException : Exception
    {

        public VectorIsInvalidException() { }

        public VectorIsInvalidException(string message) : base(message) { }

        public VectorIsInvalidException(string message, Exception inner) : base(message, inner) { }

        protected VectorIsInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }

}
