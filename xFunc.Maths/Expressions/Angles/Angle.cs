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
    public readonly struct Angle : IEquatable<Angle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit of number.</param>
        public Angle(double value, AngleUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Creates the <see cref="Angle"/> struct with <c>Degree</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static Angle Degree(double value)
            => new Angle(value, AngleUnit.Degree);

        /// <summary>
        /// Creates the <see cref="Angle"/> struct with <c>Radian</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static Angle Radian(double value)
            => new Angle(value, AngleUnit.Radian);

        /// <summary>
        /// Creates the <see cref="Angle"/> struct with <c>Gradian</c> unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The angle.</returns>
        public static Angle Gradian(double value)
            => new Angle(value, AngleUnit.Gradian);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public bool Equals(Angle other)
            => MathExtensions.Equals(Value, other.Value) && Unit == other.Unit;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
            => obj is Angle other && Equals(other);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
            => HashCode.Combine(Value, (int)Unit);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => Unit switch
        {
            AngleUnit.Degree => $"{Value.ToString(CultureInfo.InvariantCulture)} degree",
            AngleUnit.Radian => $"{Value.ToString(CultureInfo.InvariantCulture)} radian",
            AngleUnit.Gradian => $"{Value.ToString(CultureInfo.InvariantCulture)} gradian",
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Determines whether two specified instances of <see cref="Angle"/> are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Angle left, Angle right)
            => left.Equals(right);

        /// <summary>
        /// Determines whether two specified instances of <see cref="Angle"/> are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Angle left, Angle right)
            => !left.Equals(right);

        /// <summary>
        /// Adds two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Angle operator +(Angle left, Angle right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new Angle(left.Value + right.Value, left.Unit);
        }

        /// <summary>
        /// Adds two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="number">The first object to add.</param>
        /// <param name="right">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static Angle operator +(double number, Angle right)
        {
            var left = new Angle(number, right.Unit);

            return left + right;
        }

        /// <summary>
        /// Adds two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to add.</param>
        /// <param name="number">The second object to add.</param>
        /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static Angle operator +(Angle left, double number)
        {
            var right = new Angle(number, left.Unit);

            return left + right;
        }

        /// <summary>
        /// Subtracts two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Angle operator -(Angle left, Angle right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new Angle(left.Value - right.Value, left.Unit);
        }

        /// <summary>
        /// Subtracts two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="number">The first object to sub.</param>
        /// <param name="right">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static Angle operator -(double number, Angle right)
        {
            var left = new Angle(number, right.Unit);

            return left - right;
        }

        /// <summary>
        /// Subtracts two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to sub.</param>
        /// <param name="number">The second object to sub.</param>
        /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static Angle operator -(Angle left, double number)
        {
            var right = new Angle(number, left.Unit);

            return left - right;
        }

        /// <summary>
        /// Produces the negative of <see cref="Angle"/>.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The negative of <paramref name="angle"/>.</returns>
        public static Angle operator -(Angle angle)
            => new Angle(-angle.Value, angle.Unit);

        /// <summary>
        /// Multiplies two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Angle operator *(Angle left, Angle right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new Angle(left.Value * right.Value, left.Unit);
        }

        /// <summary>
        /// Multiplies two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="number">The first object to multiply.</param>
        /// <param name="right">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static Angle operator *(double number, Angle right)
        {
            var left = new Angle(number, right.Unit);

            return left * right;
        }

        /// <summary>
        /// Multiplies two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to multiply.</param>
        /// <param name="number">The second object to multiply.</param>
        /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static Angle operator *(Angle left, double number)
        {
            var right = new Angle(number, left.Unit);

            return left * right;
        }

        /// <summary>
        /// Divides two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static Angle operator /(Angle left, Angle right)
        {
            (left, right) = ToCommonUnits(left, right);

            return new Angle(left.Value / right.Value, left.Unit);
        }

        /// <summary>
        /// Divides two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="number">The first object to divide.</param>
        /// <param name="right">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="number"/> and <paramref name="right"/>.</returns>
        public static Angle operator /(double number, Angle right)
        {
            var left = new Angle(number, right.Unit);

            return left / right;
        }

        /// <summary>
        /// Divides two objects of <see cref="Angle"/>.
        /// </summary>
        /// <param name="left">The first object to divide.</param>
        /// <param name="number">The second object to divide.</param>
        /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="number"/>.</returns>
        public static Angle operator /(Angle left, double number)
        {
            var right = new Angle(number, left.Unit);

            return left / right;
        }

        private static (Angle Left, Angle Right) ToCommonUnits(Angle left, Angle right)
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
        public Angle To(AngleUnit unit) => unit switch
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
        public Angle ToDegree() => Unit switch
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
        public Angle ToRadian() => Unit switch
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
        public Angle ToGradian() => Unit switch
        {
            AngleUnit.Degree => Gradian(Value / 0.9),
            AngleUnit.Radian => Gradian(Value * 200 / Math.PI),
            AngleUnit.Gradian => this,
            _ => throw new InvalidOperationException(),
        };

        /// <summary>
        /// Converts <see cref="Angle"/> to <see cref="AngleNumber"/>.
        /// </summary>
        /// <returns>The angle number.</returns>
        public AngleNumber AsExpression()
            => new AngleNumber(this);

        /// <summary>
        /// Returns the absolute value of a specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The angle, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
        public static Angle Abs(Angle angle)
            => new Angle(Math.Abs(angle.Value), angle.Unit);

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified angle number.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The smallest integral value.</returns>
        public static Angle Ceiling(Angle angle)
            => new Angle(Math.Ceiling(angle.Value), angle.Unit);

        /// <summary>
        /// Returns the largest integral value less than or equal to the specified angle number.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The largest integral value.</returns>
        public static Angle Floor(Angle angle)
            => new Angle(Math.Floor(angle.Value), angle.Unit);

        /// <summary>
        /// Calculates the integral part of a specified angle number.
        /// </summary>
        /// <param name="angle">An angle to truncate.</param>
        /// <returns>The integral part of angle number.</returns>
        public static Angle Truncate(Angle angle)
            => new Angle(Math.Truncate(angle.Value), angle.Unit);

        /// <summary>
        /// Returns the fractional part of the angle number.
        /// </summary>
        /// <param name="angle">The angle number.</param>
        /// <returns>The fractional part.</returns>
        public static Angle Frac(Angle angle)
            => new Angle(MathExtensions.Frac(angle.Value), angle.Unit);

        /// <summary>
        /// Gets a value.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets a unit.
        /// </summary>
        public AngleUnit Unit { get; }
    }
}