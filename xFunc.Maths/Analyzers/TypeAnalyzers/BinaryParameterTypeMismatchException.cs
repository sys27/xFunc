// Copyright 2012-2017 Dmitry Kischenko
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

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{

    /// <summary>
    /// Type of binary parameter.
    /// </summary>
    public enum BinaryParameterType
    {
        /// <summary>
        /// The left parameter.
        /// </summary>
        Left,
        /// <summary>
        /// The right parameter.
        /// </summary>
        Right
    }

    /// <summary>
    /// Represents an exception when the type of the actual argument does not match the expected parameter type.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.TypeAnalyzers.ParameterTypeMismatchException" />
    [Serializable]
    public class BinaryParameterTypeMismatchException : ParameterTypeMismatchException
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
        /// </summary>
        public BinaryParameterTypeMismatchException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        public BinaryParameterTypeMismatchException(
            ResultType expected,
            ResultType actual,
            BinaryParameterType parameterType)
            : base(expected, actual,
                  string.Format(parameterType == BinaryParameterType.Left ?
                      Resource.LeftParameterTypeMismatchExceptionError :
                      Resource.RightParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="expected">The expected parameter type.</param>
        /// <param name="actual">The actual parameter type.</param>
        /// <param name="message">The error message.</param>
        public BinaryParameterTypeMismatchException(ResultType expected, ResultType actual, string message) : base(expected, actual, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public BinaryParameterTypeMismatchException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BinaryParameterTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
