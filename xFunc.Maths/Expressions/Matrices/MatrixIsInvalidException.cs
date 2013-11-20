using System;
#if !PORTABLE
using System.Runtime.Serialization;
#endif

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Thrown in matrix building.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class MatrixIsInvalidException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixIsInvalidException"/> class.
        /// </summary>
        public MatrixIsInvalidException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixIsInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MatrixIsInvalidException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixIsInvalidException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="String"/> that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public MatrixIsInvalidException(string message, Exception inner) : base(message, inner) { }

#if !PORTABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixIsInvalidException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected MatrixIsInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif

    }

}
