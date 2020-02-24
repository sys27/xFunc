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

namespace xFunc.Maths
{
    /// <summary>
    /// The exception that is thrown in the process of tokenization.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class TokenizeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizeException"/> class.
        /// </summary>
        public TokenizeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        public TokenizeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A <see cref="string"/> that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public TokenizeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizeException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected TokenizeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}