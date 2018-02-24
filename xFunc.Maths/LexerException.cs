// Copyright 2012-2018 Dmitry Kischenko
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
    /// The exception that is thrown in <see cref="xFunc.Maths.Tokenization.Lexer" />.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class LexerException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class.
        /// </summary>
        public LexerException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A <see cref="String"/> that describes the error.</param>
        public LexerException(string message) : base(message) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A <see cref="String"/> that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public LexerException(string message, Exception inner) : base(message, inner) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected LexerException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}