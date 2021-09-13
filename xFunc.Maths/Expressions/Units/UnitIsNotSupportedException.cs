// Copyright 2012-2021 Dmytro Kyshchenko
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

namespace xFunc.Maths.Expressions.Units
{
    /// <summary>
    /// Represents the exception that is thrown when converter uses unsupported unit.
    /// </summary>
    [Serializable]
    public class UnitIsNotSupportedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="unit">The unsupported unit.</param>
        public UnitIsNotSupportedException(string? unit)
            : this(unit, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="unit">The unsupported unit.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UnitIsNotSupportedException(string? unit, Exception? inner)
            : base(string.Format(Resource.UnitIsNotSupportedException, unit), inner)
        {
            Unit = unit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsNotSupportedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected UnitIsNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the unsupported unit.
        /// </summary>
        public string? Unit { get; }
    }
}