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

namespace xFunc.Maths.Expressions.Angles
{
    /// <summary>
    /// Represents a number with angle measurement unit.
    /// </summary>
    public readonly struct AngleValue : IEquatable<AngleValue>, IComparable<AngleValue>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AngleValue"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit of number.</param>
        public AngleValue(double value, AngleUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Degree</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Degree(double value)
            => new AngleValue(value, AngleUnit.Degree);

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Degree</c> unit.
        /// </summary>
        /// <param name="numberValue">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Degree(NumberValue numberValue)
            => new AngleValue(numberValue.Value, AngleUnit.Degree);

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Radian</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Radian(double value)
            => new AngleValue(value, AngleUnit.Radian);

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Radian</c> unit.
        /// </summary>
        /// <param name="numberValue">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Radian(NumberValue numberValue)
            => new AngleValue(numberValue.Value, AngleUnit.Radian);

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Gradian</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Gradian(double value)
            => new AngleValue(value, AngleUnit.Gradian);

        /// <summary>
        /// Creates the <see cref="AngleValue"/> struct with <c>Gradian</c> unit.
        /// </summary>
        /// <param name="numberValue">The value.</param>
        /// <returns>The angle.</returns>
        public static AngleValue Gradian(NumberValue numberValue)
            => new AngleValue(numberValue.Value, AngleUnit.Gradian);

        /// <inheritdoc />
        public bool Equals(AngleValue other)
            => MathExtensions.Equals(Value, other.Value) && Unit == other.Unit;

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is AngleValue other && Equals(other);

        /// <inheritdoc />
        public int CompareTo(AngleValue other)
        {
            var valueComparison = Value.CompareTo(other.Value);
            if (valueComparison != 0)
                return valueComparison;

            return Unit.CompareTo(other.Unit);
        }

        /// <inheritdoc />
        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is AngleValue other)
                return CompareTo(other);

            throw new ArgumentException($"Object must be of type {nameof(AngleValue)}");
        }

        /// <inheritdoc />
        public override int GetHashCode()
            => HashCode.Combine(Value, (int)Unit);

        /// <inheritdoc />
        public override string ToString() => Unit switch
        {
            AngleUnit.Degree => $"{Value.ToString(CultureInfo.InvariantCulture)} degree",
            AngleUnit.Radian => $"{Value.ToString(CultureInfo.InvariantCulture)} radian",
            AngleUnit.Gradian => $"{Value.ToString(CultureInfo.InvariantCulture)} gradian",
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Determines whether two specified instances of <see cref="AngleValue"/> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(AngleValue left, AngleValue right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances of <see cref="AngleValue"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(AngleValue left, AngleValue right)
            => !left.Equals(right);

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left angle.</param>
        /// <param name="right">The right angle.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <(AngleValue left, AngleValue right)
            => left.CompareTo(right) < 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left angle.</param>
        /// <param name="right">The right angle.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >(AngleValue left, AngleValue right)
            => left.CompareTo(right) > 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left angle.</param>
        /// <param name="right">The right angle.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator <=(AngleValue left, AngleValue right)
            => left.CompareTo(right) <= 0;

        /// <summary>
        /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
        /// </summary>
        /// <param name="left">The left angle.</param>
        /// <param name="right">The right angle.</param>
        /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
        public static bool operator >=(AngleValue left, AngleValue right)
            => left.CompareTo(right) >= 0;

        /// <summary>
        /// Adds two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator +(AngleValue left, AngleValue right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new AngleValue(left.Value + right.Value, left.Unit);
        }

        /// <summary>
        /// Adds two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="number">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator +(double number, AngleValue right)
        {
            var left = new AngleValue(number, right.Unit);

            return left + right;
        }

        /// <summary>
        /// Adds two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="number">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static AngleValue operator +(AngleValue left, double number)
        {
            var right = new AngleValue(number, left.Unit);

            return left + right;
        }

        /// <summary>
        /// Subtracts two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator -(AngleValue left, AngleValue right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new AngleValue(left.Value - right.Value, left.Unit);
        }

        /// <summary>
        /// Subtracts two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="number">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator -(double number, AngleValue right)
        {
            var left = new AngleValue(number, right.Unit);

            return left - right;
        }

        /// <summary>
        /// Subtracts two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="number">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static AngleValue operator -(AngleValue left, double number)
        {
            var right = new AngleValue(number, left.Unit);

            return left - right;
        }

        /// <summary>
        /// Produces the negative of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The negative of <paramref name="angleValue"/>.</returns>
        public static AngleValue operator -(AngleValue angleValue)
            => new AngleValue(-angleValue.Value, angleValue.Unit);

        /// <summary>
        /// Multiplies two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator *(AngleValue left, AngleValue right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new AngleValue(left.Value * right.Value, left.Unit);
        }

        /// <summary>
        /// Multiplies two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="number">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator *(double number, AngleValue right)
        {
            var left = new AngleValue(number, right.Unit);

            return left * right;
        }

        /// <summary>
        /// Multiplies two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="number">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static AngleValue operator *(AngleValue left, double number)
        {
            var right = new AngleValue(number, left.Unit);

            return left * right;
        }

        /// <summary>
        /// Divides two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator /(AngleValue left, AngleValue right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new AngleValue(left.Value / right.Value, left.Unit);
        }

        /// <summary>
        /// Divides two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="number">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static AngleValue operator /(double number, AngleValue right)
        {
            var left = new AngleValue(number, right.Unit);

            return left / right;
        }

        /// <summary>
        /// Divides two objects of <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="number">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static AngleValue operator /(AngleValue left, double number)
        {
            var right = new AngleValue(number, left.Unit);

            return left / right;
        }

        private static (AngleValue Left, AngleValue Right) ToCommonUnits(AngleValue left, AngleValue right)
        {
            var commonUnit = GetCommonUnit(left.Unit, right.Unit);

            return (left.To(commonUnit), right.To(commonUnit));
        }

        private static AngleUnit GetCommonUnit(AngleUnit left, AngleUnit right)
            => (left, right) switch
            {
                _ when left == right => left,

                (AngleUnit.Radian, AngleUnit.Gradian) => AngleUnit.Radian,
                (AngleUnit.Gradian, AngleUnit.Radian) => AngleUnit.Radian,

                _ => AngleUnit.Degree,
            };

        /// <summary>
        /// Converts the current object to the specified <paramref name="unit"/>.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The angle which is converted to the specified <paramref name="unit"/>.</returns>
        public AngleValue To(AngleUnit unit) => unit switch
        {
            AngleUnit.Degree => ToDegree(),
            AngleUnit.Radian => ToRadian(),
            AngleUnit.Gradian => ToGradian(),
            _ => throw new ArgumentOutOfRangeException(nameof(unit)),
        };

        /// <summary>
        /// Converts the current object to degrees.
        /// </summary>
        /// <returns>The angle which is converted to degrees.</returns>
        public AngleValue ToDegree() => Unit switch
        {
            AngleUnit.Degree => this,
            AngleUnit.Radian => Degree(Value * 180 / Math.PI),
            AngleUnit.Gradian => Degree(Value * 0.9),
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Converts the current object to radians.
        /// </summary>
        /// <returns>The angle which is converted to radians.</returns>
        public AngleValue ToRadian() => Unit switch
        {
            AngleUnit.Degree => Radian(Value * Math.PI / 180),
            AngleUnit.Radian => this,
            AngleUnit.Gradian => Radian(Value * Math.PI / 200),
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Converts the current object to gradians.
        /// </summary>
        /// <returns>The angle which is converted to gradians.</returns>
        public AngleValue ToGradian() => Unit switch
        {
            AngleUnit.Degree => Gradian(Value / 0.9),
            AngleUnit.Radian => Gradian(Value * 200 / Math.PI),
            AngleUnit.Gradian => this,
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Normalizes the current angle between [0, 2pi).
        /// </summary>
        /// <returns>The normalized angle.</returns>
        public AngleValue Normalize()
        {
            const double degreeFullCircle = 360.0;
            const double radianFullCircle = 2 * Math.PI;
            const double gradianFullCircle = 400.0;

            static double NormalizeInternal(double value, double circle)
            {
                value %= circle;
                if (value < 0)
                    value += circle;

                return value;
            }

            return Unit switch
            {
                AngleUnit.Radian => Radian(NormalizeInternal(Value, radianFullCircle)),
                AngleUnit.Gradian => Gradian(NormalizeInternal(Value, gradianFullCircle)),
                _ => Degree(NormalizeInternal(Value, degreeFullCircle)),
            };
        }

        /// <summary>
        /// Returns the absolute value of a specified angle.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The angle, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
        public static AngleValue Abs(AngleValue angleValue)
            => new AngleValue(Math.Abs(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified angle number.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The smallest integral value.</returns>
        public static AngleValue Ceiling(AngleValue angleValue)
            => new AngleValue(Math.Ceiling(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the largest integral value less than or equal to the specified angle number.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The largest integral value.</returns>
        public static AngleValue Floor(AngleValue angleValue)
            => new AngleValue(Math.Floor(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Calculates the integral part of a specified angle number.
        /// </summary>
        /// <param name="angleValue">An angle to truncate.</param>
        /// <returns>The integral part of angle number.</returns>
        public static AngleValue Truncate(AngleValue angleValue)
            => new AngleValue(Math.Truncate(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the fractional part of the angle number.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The fractional part.</returns>
        public static AngleValue Frac(AngleValue angleValue)
            => new AngleValue(MathExtensions.Frac(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Converts <see cref="AngleValue"/> to <see cref="Angle"/>.
        /// </summary>
        /// <returns>The angle number.</returns>
        public Angle AsExpression()
            => new Angle(this);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets a unit.
        /// </summary>
        public AngleUnit Unit { get; }

        /// <summary>
        /// Gets an integer that indicates the sign of a double-precision floating-point number.
        /// </summary>
        public int Sign => Math.Sign(Value);
    }
}