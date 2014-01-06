using System;
using System.Runtime.Serialization;

namespace xFunc.Maths
{

    [Serializable]
    public class InvalidResultException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
        /// </summary>
        public InvalidResultException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidResultException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidResultException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected InvalidResultException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }

}
