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
using System.Numerics;
using System.Runtime.CompilerServices;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents the number value.
    /// </summary>
    public readonly struct NumberValue :
        IEquatable<NumberValue>,
        IEquatable<double>,
        IComparable<NumberValue>,
        IComparable<double>,
        IComparable
    {
        /// <summary>
        /// The constant which is used to compare two double numbers.
        /// </summary>
        private const double Epsilon = 1E-14;

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
        public bool Equals(double other)
            => Math.Abs(Value - other) < Epsilon;

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
        public int CompareTo(double other)
            => Value.CompareTo(other);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NumberValue left, NumberValue right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NumberValue left, double right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double left, NumberValue right)
            => right.Equals(left);

        /// <summary>
        /// Determines whether two specified instances of <see cref="NumberValue"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NumberValue left, NumberValue right)
            => !left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NumberValue left, double right)
            => !left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double left, NumberValue right)
            => !right.Equals(left);

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(NumberValue left, NumberValue right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(NumberValue left, double right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(NumberValue left, NumberValue right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(NumberValue left, double right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(NumberValue left, NumberValue right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is less than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(NumberValue left, double right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(NumberValue left, NumberValue right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter
        /// is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(NumberValue left, double right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Produces the negative of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="numberValue">The number value.</param>
        /// <returns>The negative of <paramref name="numberValue"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator -(NumberValue numberValue)
            => new NumberValue(-numberValue.Value);

        /// <summary>
        /// Adds two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator +(NumberValue left, NumberValue right)
            => new NumberValue(left.Value + right.Value);

        /// <summary>
        /// Adds <see cref="NumberValue"/> and <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator +(NumberValue left, double right)
            => new NumberValue(left.Value + right);

        /// <summary>
        /// Adds <see cref="double"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator +(double left, NumberValue right)
            => new NumberValue(left + right.Value);

        /// <summary>
        /// Adds <see cref="AngleValue"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator +(AngleValue left, NumberValue right)
            => new AngleValue(left.Value + right.Value, left.Unit);

        /// <summary>
        /// Adds <see cref="NumberValue"/> and <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator +(NumberValue left, AngleValue right)
            => new AngleValue(left.Value + right.Value, right.Unit);

        /// <summary>
        /// Adds <see cref="Complex"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator +(Complex left, NumberValue right)
            => left + right.Value;

        /// <summary>
        /// Adds <see cref="NumberValue"/> and <see cref="Complex"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator +(NumberValue left, Complex right)
            => left.Value + right;

        /// <summary>
        /// Subtracts two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator -(NumberValue left, NumberValue right)
            => new NumberValue(left.Value - right.Value);

        /// <summary>
        /// Subtracts <see cref="NumberValue"/> and <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator -(NumberValue left, double right)
            => new NumberValue(left.Value - right);

        /// <summary>
        /// Subtracts <see cref="double"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator -(double left, NumberValue right)
            => new NumberValue(left - right.Value);

        /// <summary>
        /// Subtracts <see cref="NumberValue"/> and <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator -(NumberValue left, AngleValue right)
            => new AngleValue(left.Value - right.Value, right.Unit);

        /// <summary>
        /// Subtracts <see cref="AngleValue"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator -(AngleValue left, NumberValue right)
            => new AngleValue(left.Value - right.Value, left.Unit);

        /// <summary>
        /// Subtracts <see cref="NumberValue"/> and <see cref="Complex"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator -(NumberValue left, Complex right)
            => left.Value - right;

        /// <summary>
        /// Subtracts <see cref="Complex"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator -(Complex left, NumberValue right)
            => left - right.Value;

        /// <summary>
        /// Multiplies two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator *(NumberValue left, NumberValue right)
            => new NumberValue(left.Value * right.Value);

        /// <summary>
        /// Multiplies <see cref="NumberValue"/> and <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator *(NumberValue left, double right)
            => new NumberValue(left.Value * right);

        /// <summary>
        /// Multiplies <see cref="double"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator *(double left, NumberValue right)
            => new NumberValue(left * right.Value);

        /// <summary>
        /// Multiplies <see cref="AngleValue"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator *(AngleValue left, NumberValue right)
            => new AngleValue(left.Value * right.Value, left.Unit);

        /// <summary>
        /// Multiplies <see cref="NumberValue"/> and <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator *(NumberValue left, AngleValue right)
            => new AngleValue(left.Value * right.Value, right.Unit);

        /// <summary>
        /// Multiplies <see cref="Complex"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator *(Complex left, NumberValue right)
            => left * right.Value;

        /// <summary>
        /// Multiplies <see cref="NumberValue"/> and <see cref="Complex"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator *(NumberValue left, Complex right)
            => left.Value * right;

        /// <summary>
        /// Divides two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator /(NumberValue left, NumberValue right)
            => new NumberValue(left.Value / right.Value);

        /// <summary>
        /// Divides <see cref="NumberValue"/> and <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator /(NumberValue left, double right)
            => new NumberValue(left.Value / right);

        /// <summary>
        /// Divides <see cref="double"/> and <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator /(double left, NumberValue right)
            => new NumberValue(left / right.Value);

        /// <summary>
        /// Divides <see cref="AngleValue"/> by <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator /(AngleValue left, NumberValue right)
            => new AngleValue(left.Value / right.Value, left.Unit);

        /// <summary>
        /// Divides <see cref="NumberValue"/> by <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AngleValue operator /(NumberValue left, AngleValue right)
            => new AngleValue(left.Value / right.Value, right.Unit);

        /// <summary>
        /// Divides <see cref="Complex"/> by <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator /(Complex left, NumberValue right)
            => left / right.Value;

        /// <summary>
        /// Divides <see cref="NumberValue"/> by <see cref="Complex"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex operator /(NumberValue left, Complex right)
            => left.Value / right;

        /// <summary>
        /// Returns the remainder of a division.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>The remainder of a division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator %(NumberValue left, NumberValue right)
            => new NumberValue(left.Value % right.Value);

        /// <summary>
        /// Returns the absolute value of a double-precision floating-point number.
        /// </summary>
        /// <param name="numberValue">A number that is greater than or equal to <see cref="double.MinValue"/>, but less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="double.MaxValue"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Abs(NumberValue numberValue)
            => new NumberValue(Math.Abs(numberValue.Value));

        /// <summary>
        /// Returns the smallest integral value that is greater than
        /// or equal to the specified double-precision floating-point number.
        /// </summary>
        /// <param name="numberValue">A double-precision floating-point number.</param>
        /// <returns>The smallest integral value that is greater than or equal to <paramref name="numberValue"/>. If <paramref name="numberValue"/> is equal to <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/>, or <see cref="double.PositiveInfinity"/>, that value is returned. Note that this method returns a Double instead of an integral type.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Ceiling(NumberValue numberValue)
            => new NumberValue(Math.Ceiling(numberValue.Value));

        /// <summary>
        /// Returns <c>e</c> raised to the specified power.
        /// </summary>
        /// <param name="numberValue">A number specifying a power.</param>
        /// <returns>The number <c>e</c> raised to the power <paramref name="numberValue"/>. If <paramref name="numberValue"/> equals <see cref="double.NaN"/> or <see cref="double.PositiveInfinity"/>, that value is returned. If <paramref name="numberValue"/> equals <see cref="double.NegativeInfinity"/>, 0 is returned.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Exp(NumberValue numberValue)
            => new NumberValue(Math.Exp(numberValue.Value));

        /// <summary>
        /// Returns a factorial of specified number.
        /// </summary>
        /// <param name="numberValue">A specified number.</param>
        /// <returns>The factorial.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Factorial(NumberValue numberValue)
        {
            return new NumberValue(Fact(Math.Round(numberValue.Value)));

            static double Fact(double n)
            {
                if (n < 0)
                    return double.NaN;

                var result = 1.0;

                for (var i = n; i > 0; i--)
                    result *= i;

                return result;
            }
        }

        /// <summary>
        /// Returns the largest integral value less than
        /// or equal to the specified double-precision floating-point number.
        /// </summary>
        /// <param name="numberValue">A double-precision floating-point number.</param>
        /// <returns>The largest integral value less than or equal to <paramref name="numberValue"/>. If <paramref name="numberValue"/> is equal to <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/>, or <see cref="double.PositiveInfinity"/>, that value is returned.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Floor(NumberValue numberValue)
            => new NumberValue(Math.Floor(numberValue.Value));

        /// <summary>
        /// Returns the fractional part of the number.
        /// </summary>
        /// <param name="numberValue">The number value.</param>
        /// <returns>The fractional part.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Frac(NumberValue numberValue)
        {
            // TODO: use custom operator
            if (numberValue.Value >= 0)
                return numberValue - Floor(numberValue);

            return numberValue - Ceiling(numberValue);
        }

        /// <summary>
        /// Returns the polynomial greatest common divisor.
        /// </summary>
        /// <param name="left">The first numbers.</param>
        /// <param name="right">The second numbers.</param>
        /// <returns>The greatest common divisor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue GCD(NumberValue left, NumberValue right)
        {
            var a = left.Value;
            var b = right.Value;

            while (!Equals(b, 0.0))
                b = a % (a = b);

            return new NumberValue(a);
        }

        /// <summary>
        /// Computes the polynomial greatest common divisor.
        /// </summary>
        /// <param name="a">The first numbers.</param>
        /// <param name="b">The second numbers.</param>
        /// <returns>The least common multiple.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue LCM(NumberValue a, NumberValue b)
            => Abs(a * b) / GCD(a, b);

        /// <summary>
        /// Returns the base 2 logarithm of a specified number.
        /// </summary>
        /// <param name="numberValue">A number whose logarithm is to be found.</param>
        /// <returns>The binary logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Lb(NumberValue numberValue)
            => new NumberValue(Math.Log(numberValue.Value, 2));

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="numberValue">A number whose logarithm is to be found.</param>
        /// <returns>The base 10 logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Lg(NumberValue numberValue)
            => new NumberValue(Math.Log10(numberValue.Value));

        /// <summary>
        /// Returns the natural (base <c>e</c>) logarithm of a specified number.
        /// </summary>
        /// <param name="numberValue">The number whose logarithm is to be found.</param>
        /// <returns>The natural (base <c>e</c>) logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Ln(NumberValue numberValue)
            => new NumberValue(Math.Log(numberValue.Value));

        /// <summary>
        /// Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="number">The number whose logarithm is to be found.</param>
        /// <param name="base">The base of the logarithm.</param>
        /// <returns>The logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Log(NumberValue number, NumberValue @base)
            => new NumberValue(Math.Log(number.Value, @base.Value));

        /// <summary>
        /// Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="number">The number whose logarithm is to be found.</param>
        /// <param name="base">The base of the logarithm.</param>
        /// <returns>The logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Log(Complex number, NumberValue @base)
            => Complex.Log(number, @base.Value);

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="power">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The <paramref name="number"/> raised to the <paramref name="power"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Pow(NumberValue number, NumberValue power)
        {
            if (number < 0)
            {
                if ((BitConverter.DoubleToInt64Bits(power.Value) & 1) == 1)
                    return new NumberValue(-Math.Pow(-number.Value, power.Value));

                if (power > 0 && power < 1)
                    return Complex.Pow(number.Value, power.Value);

                if (power < 0 && power > -1)
                    return new Complex(0, -Math.Pow(-number.Value, power.Value));
            }

            return new NumberValue(Math.Pow(number.Value, power.Value));
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="power">A double-precision floating-point number that specifies a power.</param>
        /// <returns>The <paramref name="number"/> raised to the <paramref name="power"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Pow(Complex number, NumberValue power)
            => Complex.Pow(number, power.Value);

        /// <summary>
        /// Rounds a double-precision floating-point value to a specified number of fractional digits,
        /// and uses the specified rounding convention for midpoint values.
        /// </summary>
        /// <param name="number">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <returns>The number nearest to <paramref name="number"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="number"/> is returned unchanged.</returns>
        /// <exception cref="InvalidOperationException">The value is not an integer.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Round(NumberValue number, NumberValue digits)
        {
            if (!digits.Value.IsInt())
                throw new InvalidOperationException(Resource.ValueIsNotInteger);

            var rounded = Math.Round(number.Value, (int)digits.Value, MidpointRounding.AwayFromZero);

            return new NumberValue(rounded);
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="numberValue">The number whose square root is to be found.</param>
        /// <returns>The square root.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Sqrt(NumberValue numberValue)
        {
            if (numberValue < 0)
                return Complex.Sqrt(numberValue.Value);

            return new NumberValue(Math.Sqrt(numberValue.Value));
        }

        private static string PadNumber(string number, int padding)
        {
            var padLength = number.Length % padding;
            if (padLength > 0)
                padLength = number.Length + (padding - padLength);

            return number.PadLeft(padLength, '0');
        }

        /// <summary>
        /// Converts <paramref name="numberValue"/> to the binary number.
        /// </summary>
        /// <param name="numberValue">The number.</param>
        /// <returns>String that contains the number in the new numeral system.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToBin(NumberValue numberValue)
        {
            // TODO:
            var number = numberValue.Value;
            if (!number.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

            var result = Convert.ToString((int)number, 2);
            result = PadNumber(result, 8);

            return $"0b{result}";
        }

        /// <summary>
        /// Converts <paramref name="numberValue"/> to the octal number.
        /// </summary>
        /// <param name="numberValue">The number.</param>
        /// <returns>String that contains the number in the new numeral system.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToOct(NumberValue numberValue)
        {
            var number = numberValue.Value;
            if (!number.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

            return $"0{Convert.ToString((int)number, 8)}";
        }

        /// <summary>
        /// Converts <paramref name="numberValue"/> to the hexadecimal number.
        /// </summary>
        /// <param name="numberValue">The number.</param>
        /// <returns>String that contains the number in the new numeral system.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHex(NumberValue numberValue)
        {
            var number = numberValue.Value;
            if (!number.IsInt())
                throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

            var result = Convert.ToString((int)number, 16).ToUpperInvariant();
            result = PadNumber(result, 2);

            return $"0x{result}";
        }

        /// <summary>
        /// Calculates the integral part of a specified double-precision floating-point number.
        /// </summary>
        /// <param name="number">A number to truncate.</param>
        /// <returns>The integral part.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Truncate(NumberValue number)
            => new NumberValue(Math.Truncate(number.Value));

        /// <summary>
        /// Gets a value indicating whether the current value is not a number (NaN).
        /// </summary>
        public bool IsNaN => double.IsNaN(Value);

        /// <summary>
        /// Gets a value indicating whether the current number evaluates to infinity.
        /// </summary>
        public bool IsInfinity => double.IsInfinity(Value);

        /// <summary>
        /// Gets a value indicating whether the current number evaluates to positive infinity.
        /// </summary>
        public bool IsPositiveInfinity => double.IsPositiveInfinity(Value);

        /// <summary>
        /// Gets a value indicating whether the current number evaluates to negative infinity.
        /// </summary>
        public bool IsNegativeInfinity => double.IsNegativeInfinity(Value);

        /// <summary>
        /// Gets a sign of number.
        /// </summary>
        public double Sign => Math.Sign(Value);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public double Value { get; }
    }
}