using System;
#if !PORTABLE
using System.Runtime.Serialization;
#endif

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Trying to change a read-only variable.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class MathParameterIsReadOnlyException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterIsReadOnlyException"/> class.
        /// </summary>
        public MathParameterIsReadOnlyException()
            : this("You cannot change the read-only variable.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterIsReadOnlyException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MathParameterIsReadOnlyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public MathParameterIsReadOnlyException(string message, Exception inner) : base(message, inner) { }

#if !PORTABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected MathParameterIsReadOnlyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif

    }

}
