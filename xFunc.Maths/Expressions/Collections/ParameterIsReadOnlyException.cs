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
using System.Runtime.Serialization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Collections
{
    /// <summary>
    /// Trying to change a read-only variable.
    /// </summary>
    [Serializable]
    public class ParameterIsReadOnlyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        public ParameterIsReadOnlyException()
            : this(Resource.ReadOnlyError)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ParameterIsReadOnlyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ParameterIsReadOnlyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameterName">The parameter name.</param>
        public ParameterIsReadOnlyException(string message, string parameterName)
            : this(message, parameterName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <param name="inner">The inner.</param>
        public ParameterIsReadOnlyException(string message, string parameterName, Exception? inner)
           : base(message, inner)
        {
            ParameterName = parameterName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ParameterIsReadOnlyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string? ParameterName { get; }
    }
}