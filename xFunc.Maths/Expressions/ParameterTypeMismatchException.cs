// Copyright 2012-2015 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using System.Runtime.Serialization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents an exception when the type of the actual argument does not match the expected parameter type.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class ParameterTypeMismatchException : Exception
    {

        private ExpressionResultType expected;
        private ExpressionResultType actual;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
        /// </summary>
        public ParameterTypeMismatchException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="expected">The expected parameter type.</param>
        /// <param name="actual">The actual parameter type.</param>
        public ParameterTypeMismatchException(ExpressionResultType expected, ExpressionResultType actual)
            : base(string.Format(Resource.ParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString()))
        {
            this.expected = expected;
            this.actual = actual;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ParameterTypeMismatchException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ParameterTypeMismatchException(string message, Exception inner) : base(message, inner) { }

#if !PORTABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif

        /// <summary>
        /// Gets the expected parameter type.
        /// </summary>
        /// <value>
        /// The expected parameter type.
        /// </value>
        public ExpressionResultType Expected
        {
            get
            {
                return expected;
            }
        }

        /// <summary>
        /// Gets the actual parameter type.
        /// </summary>
        /// <value>
        /// The actual parameter type.
        /// </value>
        public ExpressionResultType Actual
        {
            get
            {
                return actual;
            }
        }

    }

}
