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
using System.Linq;
using System.Runtime.Serialization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents an exception which is thrown if expression doesn't support result type of own argument.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ResultIsNotSupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
        /// </summary>
        public ResultIsNotSupportedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="that">The that.</param>
        /// <param name="result">The result.</param>
        public ResultIsNotSupportedException(object that, params object[] result)
            : this(string.Format(
                CultureInfo.InvariantCulture,
                Resource.ResultIsNotSupported,
                that?.GetType().Name,
                string.Join(", ", result.Select(x => x.GetType().Name))))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResultIsNotSupportedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ResultIsNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResultIsNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}