// Copyright 2012-2014 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Matrices
{

    [Serializable]
    public class VectorIsInvalidException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorIsInvalidException"/> class.
        /// </summary>
        public VectorIsInvalidException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorIsInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public VectorIsInvalidException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorIsInvalidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner exception.</param>
        public VectorIsInvalidException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorIsInvalidException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected VectorIsInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }

}
