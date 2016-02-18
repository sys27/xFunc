// Copyright 2012-2016 Dmitry Kischenko
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
#if !PORTABLE
using System.Runtime.Serialization;
#endif

namespace xFunc.Maths.Expressions.Collections
{

    /// <summary>
    /// Trying to change a read-only variable.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class ParameterIsReadOnlyException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        public ParameterIsReadOnlyException()
            : this("You cannot change the read-only variable.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ParameterIsReadOnlyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ParameterIsReadOnlyException(string message, Exception inner) : base(message, inner) { }

#if !PORTABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ParameterIsReadOnlyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
#endif

    }

}
