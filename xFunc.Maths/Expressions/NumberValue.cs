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

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the number value.
    /// </summary>
    public readonly struct NumberValue : IEquatable<NumberValue>, IComparable<NumberValue>, IComparable
    {
        /// <summary>
        /// The constant which is used to compare two double numbers.
        /// </summary>
        public const double Epsilon = 1E-14;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberValue"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public NumberValue(double value)
            => Value = value;

        /// <inheritdoc />
        public bool Equals(NumberValue other)
            => Math.Abs(Value - other.Value) < Epsilon;

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is NumberValue other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(Value);

        /// <inheritdoc />
        public int CompareTo(NumberValue other)
            => Value.CompareTo(other.Value);

        /// <inheritdoc />
        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return 1;

            return obj is NumberValue other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(NumberValue)}");
        }

        /// <inheritdoc />
        public override string ToString()
            => Value.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Determines whether two specified instances of <see cref="NumberValue"/> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(NumberValue left, NumberValue right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances of <see cref="NumberValue"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(NumberValue left, NumberValue right)
            => !left.Equals(right);

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <(NumberValue left, NumberValue right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >(NumberValue left, NumberValue right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <=(NumberValue left, NumberValue right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >=(NumberValue left, NumberValue right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Produces the negative of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="numberValue">The number value.</param>
        /// <returns>The negative of <paramref name="numberValue"/>.</returns>
        public static NumberValue operator -(NumberValue numberValue)
            => new NumberValue(-numberValue.Value);

        /// <summary>
        /// Adds two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static NumberValue operator +(NumberValue left, NumberValue right)
            => new NumberValue(left.Value + right.Value);

        /// <summary>
        /// Subtracts two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static NumberValue operator -(NumberValue left, NumberValue right)
            => new NumberValue(left.Value - right.Value);

        /// <summary>
        /// Multiplies two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static NumberValue operator *(NumberValue left, NumberValue right)
            => new NumberValue(left.Value * right.Value);

        /// <summary>
        /// Divides two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static NumberValue operator /(NumberValue left, NumberValue right)
            => new NumberValue(left.Value / right.Value);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public double Value { get; }
    }
}