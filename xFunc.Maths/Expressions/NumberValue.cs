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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NumberValue left, NumberValue right)
            => left.Equals(right);

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
        /// is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(NumberValue left, NumberValue right)
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
        /// Multiplies two objects of <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue operator *(NumberValue left, NumberValue right)
            => new NumberValue(left.Value * right.Value);

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
        /// <param name="a">The first numbers.</param>
        /// <param name="b">The second numbers.</param>
        /// <returns>The greatest common divisor.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue GCD(NumberValue a, NumberValue b)
        {
            return new NumberValue(GCD(a.Value, b.Value));

            static double GCD(double a, double b)
            {
                while (!Equals(b, 0.0))
                    b = a % (a = b);

                return a;
            }
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
        /// <param name="numberValue">A specified number.</param>
        /// <returns>The binary logarithm.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumberValue Lb(NumberValue numberValue)
            => new NumberValue(Math.Log(numberValue.Value, 2));

        /// <summary>
        /// Gets a value.
        /// </summary>
        public double Value { get; }
    }
}