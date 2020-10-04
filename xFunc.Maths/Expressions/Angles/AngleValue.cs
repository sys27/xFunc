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
            : this(new NumberValue(value), unit)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleValue"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit of number.</param>
        public AngleValue(NumberValue value, AngleUnit unit)
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
            AngleUnit.Degree => $"{Value.ToString()} degree",
            AngleUnit.Radian => $"{Value.ToString()} radian",
            AngleUnit.Gradian => $"{Value.ToString()} gradian",
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
                AngleUnit.Radian => Radian(NormalizeInternal(Value.Value, radianFullCircle)),
                AngleUnit.Gradian => Gradian(NormalizeInternal(Value.Value, gradianFullCircle)),
                _ => Degree(NormalizeInternal(Value.Value, degreeFullCircle)),
            };
        }

        /// <summary>
        /// Returns the absolute value of a specified angle.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The angle, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
        public static AngleValue Abs(AngleValue angleValue)
            => new AngleValue(NumberValue.Abs(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified angle number.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The smallest integral value.</returns>
        public static AngleValue Ceiling(AngleValue angleValue)
            => new AngleValue(NumberValue.Ceiling(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the largest integral value less than or equal to the specified angle number.
        /// </summary>
        /// <param name="angleValue">The angle.</param>
        /// <returns>The largest integral value.</returns>
        public static AngleValue Floor(AngleValue angleValue)
            => new AngleValue(NumberValue.Floor(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Calculates the integral part of a specified angle number.
        /// </summary>
        /// <param name="angleValue">An angle to truncate.</param>
        /// <returns>The integral part of angle number.</returns>
        public static AngleValue Truncate(AngleValue angleValue)
            => new AngleValue(NumberValue.Truncate(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// Returns the fractional part of the angle number.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The fractional part.</returns>
        public static AngleValue Frac(AngleValue angleValue)
            => new AngleValue(NumberValue.Frac(angleValue.Value), angleValue.Unit);

        /// <summary>
        /// The 'sin' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of sine function.</returns>
        public static NumberValue Sin(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.Zero;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Half;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.Sqrt2By2;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Sqrt3By2;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.One;

            // 120
            if (value == 2 * Math.PI / 3)
                return NumberValue.Sqrt3By2;

            // 135
            if (value == 3 * Math.PI / 4)
                return NumberValue.Sqrt2By2;

            // 150
            if (value == 5 * Math.PI / 6)
                return NumberValue.Half;

            // 180
            if (value == Math.PI)
                return NumberValue.Zero;

            // 210
            if (value == 7 * Math.PI / 6)
                return -NumberValue.Half;

            // 225
            if (value == 5 * Math.PI / 4)
                return -NumberValue.Sqrt2By2;

            // 240
            if (value == 4 * Math.PI / 3)
                return -NumberValue.Sqrt3By2;

            // 270
            if (value == 3 * Math.PI / 2)
                return -NumberValue.One;

            // 300
            if (value == 5 * Math.PI / 3)
                return -NumberValue.Sqrt3By2;

            // 315
            if (value == 7 * Math.PI / 4)
                return -NumberValue.Sqrt2By2;

            // 330
            if (value == 11 * Math.PI / 6)
                return -NumberValue.Half;

            return new NumberValue(Math.Sin(angleValue.Value.Value));
        }

        /// <summary>
        /// The 'cos' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of cosine function.</returns>
        public static NumberValue Cos(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.One;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Sqrt3By2;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.Sqrt2By2;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Half;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.Zero;

            // 120
            if (value == 2 * Math.PI / 3)
                return -NumberValue.Half;

            // 135
            if (value == 3 * Math.PI / 4)
                return -NumberValue.Sqrt2By2;

            // 150
            if (value == 5 * Math.PI / 6)
                return -NumberValue.Sqrt3By2;

            // 180
            if (value == Math.PI)
                return -NumberValue.One;

            // 210
            if (value == 7 * Math.PI / 6)
                return -NumberValue.Sqrt3By2;

            // 225
            if (value == 5 * Math.PI / 4)
                return -NumberValue.Sqrt2By2;

            // 240
            if (value == 4 * Math.PI / 3)
                return -NumberValue.Half;

            // 270
            if (value == 3 * Math.PI / 2)
                return NumberValue.Zero;

            // 300
            if (value == 5 * Math.PI / 3)
                return NumberValue.Half;

            // 315
            if (value == 7 * Math.PI / 4)
                return NumberValue.Sqrt2By2;

            // 330
            if (value == 11 * Math.PI / 6)
                return NumberValue.Sqrt3By2;

            return new NumberValue(Math.Cos(angleValue.Value.Value));
        }

        /// <summary>
        /// The 'tan' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of tangent function.</returns>
        public static NumberValue Tan(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.Zero;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Sqrt3By3;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.One;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Sqrt3;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.PositiveInfinity;

            // 120
            if (value == 2 * Math.PI / 3)
                return -NumberValue.Sqrt3;

            // 135
            if (value == 3 * Math.PI / 4)
                return -NumberValue.One;

            // 150
            if (value == 5 * Math.PI / 6)
                return -NumberValue.Sqrt3By3;

            // 180
            if (value == Math.PI)
                return NumberValue.Zero;

            // 210
            if (value == 7 * Math.PI / 6)
                return NumberValue.Sqrt3By3;

            // 225
            if (value == 5 * Math.PI / 4)
                return NumberValue.One;

            // 240
            if (value == 4 * Math.PI / 3)
                return NumberValue.Sqrt3;

            // 270
            if (value == 3 * Math.PI / 2)
                return NumberValue.PositiveInfinity;

            // 300
            if (value == 5 * Math.PI / 3)
                return -NumberValue.Sqrt3;

            // 315
            if (value == 7 * Math.PI / 4)
                return -NumberValue.One;

            // 330
            if (value == 11 * Math.PI / 6)
                return -NumberValue.Sqrt3By3;

            return new NumberValue(Math.Tan(angleValue.Value.Value));
        }

        /// <summary>
        /// The 'cot' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of cotangent function.</returns>
        public static NumberValue Cot(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.PositiveInfinity;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Sqrt3;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.One;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Sqrt3By3;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.Zero;

            // 120
            if (value == 2 * Math.PI / 3)
                return -NumberValue.Sqrt3By3;

            // 135
            if (value == 3 * Math.PI / 4)
                return -NumberValue.One;

            // 150
            if (value == 5 * Math.PI / 6)
                return -NumberValue.Sqrt3;

            // 180
            if (value == Math.PI)
                return NumberValue.PositiveInfinity;

            // 210
            if (value == 7 * Math.PI / 6)
                return NumberValue.Sqrt3;

            // 225
            if (value == 5 * Math.PI / 4)
                return NumberValue.One;

            // 240
            if (value == 4 * Math.PI / 3)
                return NumberValue.Sqrt3By3;

            // 270
            if (value == 3 * Math.PI / 2)
                return NumberValue.Zero;

            // 300
            if (value == 5 * Math.PI / 3)
                return -NumberValue.Sqrt3;

            // 315
            if (value == 7 * Math.PI / 4)
                return -NumberValue.One;

            // 330
            if (value == 11 * Math.PI / 6)
                return -NumberValue.Sqrt3By3;

            return new NumberValue(Math.Cos(angleValue.Value.Value) / Math.Sin(angleValue.Value.Value));
        }

        /// <summary>
        /// The 'cot' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of secant function.</returns>
        public static NumberValue Sec(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.One;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Sqrt3By3By2;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.Sqrt2;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Two;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.PositiveInfinity;

            // 120
            if (value == 2 * Math.PI / 3)
                return -NumberValue.Two;

            // 135
            if (value == 3 * Math.PI / 4)
                return -NumberValue.Sqrt2;

            // 150
            if (value == 5 * Math.PI / 6)
                return -NumberValue.Sqrt3By3By2;

            // 180
            if (value == Math.PI)
                return -NumberValue.One;

            // 210
            if (value == 7 * Math.PI / 6)
                return -NumberValue.Sqrt3By3By2;

            // 225
            if (value == 5 * Math.PI / 4)
                return -NumberValue.Sqrt2;

            // 240
            if (value == 4 * Math.PI / 3)
                return -NumberValue.Two;

            // 270
            if (value == 3 * Math.PI / 2)
                return NumberValue.PositiveInfinity;

            // 300
            if (value == 5 * Math.PI / 3)
                return -NumberValue.Two;

            // 315
            if (value == 7 * Math.PI / 4)
                return NumberValue.Sqrt2;

            // 330
            if (value == 11 * Math.PI / 6)
                return NumberValue.Sqrt3By3By2;

            return new NumberValue(1 / Math.Cos(angleValue.Value.Value));
        }

        /// <summary>
        /// The 'csc' function.
        /// </summary>
        /// <param name="angleValue">The angle number.</param>
        /// <returns>The result of cosecant function.</returns>
        public static NumberValue Csc(AngleValue angleValue)
        {
            var value = angleValue.Normalize().Value;

            // 0
            if (value == 0)
                return NumberValue.PositiveInfinity;

            // 30
            if (value == Math.PI / 6)
                return NumberValue.Two;

            // 45
            if (value == Math.PI / 4)
                return NumberValue.Sqrt2;

            // 60
            if (value == Math.PI / 3)
                return NumberValue.Sqrt3By3By2;

            // 90
            if (value == Math.PI / 2)
                return NumberValue.One;

            // 120
            if (value == 2 * Math.PI / 3)
                return NumberValue.Sqrt3By3By2;

            // 135
            if (value == 3 * Math.PI / 4)
                return NumberValue.Sqrt2;

            // 150
            if (value == 5 * Math.PI / 6)
                return NumberValue.Two;

            // 180
            if (value == Math.PI)
                return NumberValue.PositiveInfinity;

            // 210
            if (value == 7 * Math.PI / 6)
                return -NumberValue.Two;

            // 225
            if (value == 5 * Math.PI / 4)
                return -NumberValue.Sqrt2;

            // 240
            if (value == 4 * Math.PI / 3)
                return -NumberValue.Sqrt3By3By2;

            // 270
            if (value == 3 * Math.PI / 2)
                return -NumberValue.One;

            // 300
            if (value == 5 * Math.PI / 3)
                return NumberValue.Sqrt3By3By2;

            // 315
            if (value == 7 * Math.PI / 4)
                return NumberValue.Sqrt2;

            // 330
            if (value == 11 * Math.PI / 6)
                return NumberValue.Two;

            return new NumberValue(1 / Math.Sin(angleValue.Value.Value));
        }

        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="number">A number representing a sine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
        /// </returns>
        public static AngleValue Asin(NumberValue number)
            => Radian(Math.Asin(number.Value));

        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="number">A number representing a cosine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ π.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
        /// </returns>
        public static AngleValue Acos(NumberValue number)
            => Radian(Math.Acos(number.Value));

        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="number">A number representing a tangent.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> equals <see cref="double.NaN"/>,
        /// -π/2 rounded to double precision (-1.5707963267949) if <paramref name="number"/> equals <see cref="double.NegativeInfinity"/>,
        /// or π/2 rounded to double precision (1.5707963267949) if <paramref name="number"/> equals <see cref="double.PositiveInfinity"/>.
        /// </returns>
        public static AngleValue Atan(NumberValue number)
            => Radian(Math.Atan(number.Value));

        /// <summary>
        /// Returns the angle whose cotangent is the specified number.
        /// </summary>
        /// <param name="number">A number representing a cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Acot(NumberValue number)
            => Radian(Math.PI / 2 - Math.Atan(number.Value));

        /// <summary>
        /// Returns the angle whose secant is the specified number.
        /// </summary>
        /// <param name="number">A number representing a secant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Asec(NumberValue number)
            => Radian(Math.Acos(1 / number.Value));

        /// <summary>
        /// Returns the angle whose cosecant is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic cosecant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Acsc(NumberValue number)
            => Radian(Math.Asin(1 / number.Value));

        /// <summary>
        /// Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="angle">An angle.</param>
        /// <returns>The hyperbolic sine of <paramref name="angle"/>. If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/>, <see cref="double.PositiveInfinity"/>, or <see cref="double.NaN"/>.</returns>
        public static NumberValue Sinh(AngleValue angle)
            => new NumberValue(Math.Sinh(angle.ToRadian().Value.Value));

        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="angle">An angle.</param>
        /// <returns>
        /// The hyperbolic cosine of <paramref name="angle"/>.
        /// If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/> or <see cref="double.PositiveInfinity"/>, <see cref="double.PositiveInfinity"/> is returned.
        /// If <paramref name="angle"/> is equal to <see cref="double.NaN"/>, <see cref="double.NaN"/> is returned.
        /// </returns>
        public static NumberValue Cosh(AngleValue angle)
            => new NumberValue(Math.Cosh(angle.ToRadian().Value.Value));

        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="angle">An angle.</param>
        /// <returns>
        /// The hyperbolic tangent of <paramref name="angle"/>.
        /// If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/>, this method returns -1.
        /// If <paramref name="angle"/> is equal to <see cref="double.PositiveInfinity"/>, this method returns 1.
        /// If <paramref name="angle"/> is equal to <see cref="double.NaN"/>, this method returns <see cref="double.NaN"/>.
        /// </returns>
        public static NumberValue Tanh(AngleValue angle)
            => new NumberValue(Math.Tanh(angle.ToRadian().Value.Value));

        /// <summary>
        /// Returns the hyperbolic cotangent of the specified angle.
        /// </summary>
        /// <param name="angle">An angle, measured in radians.</param>
        /// <returns>The hyperbolic cotangent of value.</returns>
        public static NumberValue Coth(AngleValue angle)
        {
            var d = angle.ToRadian().Value.Value;

            return new NumberValue((Math.Exp(d) + Math.Exp(-d)) / (Math.Exp(d) - Math.Exp(-d)));
        }

        /// <summary>
        /// Returns the hyperbolic secant of the specified angle.
        /// </summary>
        /// <param name="angle">An angle.</param>
        /// <returns>The hyperbolic secant of value.</returns>
        public static NumberValue Sech(AngleValue angle)
        {
            var d = angle.ToRadian().Value.Value;

            return new NumberValue(2 / (Math.Exp(d) + Math.Exp(-d)));
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of the specified angle.
        /// </summary>
        /// <param name="angle">An angle.</param>
        /// <returns>The hyperbolic cosecant of value.</returns>
        public static NumberValue Csch(AngleValue angle)
        {
            var d = angle.ToRadian().Value.Value;

            return new NumberValue(2 / (Math.Exp(d) - Math.Exp(-d)));
        }

        /// <summary>
        /// Returns the angle whose hyperbolic sine is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic sine, where <paramref name="number"/> must be greater than or equal to <see cref="double.NegativeInfinity"/>, but less than or equal to <see cref="double.PositiveInfinity"/>.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -∞ &lt; θ ≤ -1, or 1 ≤ θ &lt; ∞.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> equals <see cref="double.NaN"/>.
        /// </returns>
        public static AngleValue Asinh(NumberValue number)
            => Radian(Math.Asinh(number.Value));

        /// <summary>
        /// Returns the angle whose hyperbolic cosine is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic cosine, where <paramref name="number"/> must be greater than or equal to 1, but less than or equal to <see cref="double.PositiveInfinity"/>.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ ∞.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
        /// </returns>
        public static AngleValue Acosh(NumberValue number)
            => Radian(Math.Acosh(number.Value));

        /// <summary>
        /// Returns the angle whose hyperbolic tangent is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic tangent, where <paramref name="number"/> must be greater than or equal to -1, but less than or equal to 1.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -∞ &lt; θ &lt; -1, or 1 &lt; θ &lt; ∞.
        /// -or-
        /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
        /// </returns>
        public static AngleValue Atanh(NumberValue number)
            => Radian(Math.Atanh(number.Value));

        /// <summary>
        /// Returns the angle whose hyperbolic cotangent is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic cotangent.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Acoth(NumberValue number)
            => Radian(Math.Log((number.Value + 1) / (number.Value - 1)) / 2);

        /// <summary>
        /// Returns the angle whose hyperbolic secant is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic secant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Asech(NumberValue number)
        {
            var z = 1 / number.Value;

            return Radian(Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1)));
        }

        /// <summary>
        /// Returns the angle whose hyperbolic cosecant is the specified number.
        /// </summary>
        /// <param name="number">A number representing a hyperbolic cosecant.</param>
        /// <returns>An angle, measured in radians.</returns>
        public static AngleValue Acsch(NumberValue number)
            => Radian(Math.Log(1 / number.Value + Math.Sqrt(1 / number.Value * number.Value + 1)));

        /// <summary>
        /// Converts <see cref="AngleValue"/> to <see cref="Angle"/>.
        /// </summary>
        /// <returns>The angle number.</returns>
        public Angle AsExpression()
            => new Angle(this);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public NumberValue Value { get; }

        /// <summary>
        /// Gets a unit.
        /// </summary>
        public AngleUnit Unit { get; }

        /// <summary>
        /// Gets an integer that indicates the sign of a double-precision floating-point number.
        /// </summary>
        public double Sign => Value.Sign;
    }
}