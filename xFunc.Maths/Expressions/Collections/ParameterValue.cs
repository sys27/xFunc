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
using System.Diagnostics;
using System.Numerics;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.Matrices;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions.Collections
{
    /// <summary>
    /// Represents a parameter value.
    /// </summary>
    public readonly struct ParameterValue : IEquatable<ParameterValue>
    {
        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(double value)
            : this(new NumberValue(value))
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(NumberValue value)
            : this(value as object)
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(AngleValue value)
            : this(value as object)
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(Complex value)
            : this(value as object)
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(bool value)
            : this(value as object)
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(Vector value)
            : this(value as object)
        {
        }

        /// <inheritdoc cref="ParameterValue(object)"/>
        public ParameterValue(Matrix value)
            : this(value as object)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterValue" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        internal ParameterValue(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            Debug.Assert(
                value is NumberValue ||
                value is AngleValue ||
                value is Complex ||
                value is bool ||
                value is Vector ||
                value is Matrix,
                "Unsupported parameter value.");

            Value = value;
        }

        /// <summary>
        /// Converts a value to <see cref="ParameterValue"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The parameter value.</returns>
        public static implicit operator ParameterValue(double value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(NumberValue value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(AngleValue value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(Complex value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(bool value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(Vector value)
            => new ParameterValue(value);

        /// <inheritdoc cref="ParameterValue.op_Implicit(double)"/>
        public static implicit operator ParameterValue(Matrix value)
            => new ParameterValue(value);

        /// <inheritdoc />
        public bool Equals(ParameterValue other)
            => Value.Equals(other.Value);

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is ParameterValue other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(Value);

        /// <summary>
        /// Determines whether two specified instances of <see cref="ParameterValue"/> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ParameterValue left, ParameterValue right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances of <see cref="ParameterValue"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ParameterValue left, ParameterValue right)
            => !left.Equals(right);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public object Value { get; }
    }
}