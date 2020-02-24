// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Globalization;
using System.Runtime.Serialization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Analyzers.TypeAnalyzers
{
    /// <summary>
    /// Represents an exception when the type of the actual argument does not match the expected parameter type.
    /// </summary>
    [Serializable]
    public class DifferentParameterTypeMismatchException : ParameterTypeMismatchException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
        /// </summary>
        public DifferentParameterTypeMismatchException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException" /> class.
        /// </summary>
        /// <param name="expected">The expected parameter type.</param>
        /// <param name="actual">The actual parameter type.</param>
        /// <param name="index">The index of parameter.</param>
        public DifferentParameterTypeMismatchException(ResultTypes expected, ResultTypes actual, int index)
            : base(expected, actual, string.Format(CultureInfo.InvariantCulture, Resource.DifferentParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString(), index + 1))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DifferentParameterTypeMismatchException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DifferentParameterTypeMismatchException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DifferentParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}