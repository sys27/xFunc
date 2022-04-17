// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Convert = System.Convert;

namespace xFunc.Maths.Expressions;

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
    /// sqrt(2).
    /// </summary>
    public static readonly NumberValue Sqrt2 = new NumberValue(1.4142135623730951);

    /// <summary>
    /// sqrt(3).
    /// </summary>
    public static readonly NumberValue Sqrt3 = new NumberValue(1.7320508075688772);

    /// <summary>
    /// 0.
    /// </summary>
    public static readonly NumberValue Zero = new NumberValue(0.0);

    /// <summary>
    /// 1.
    /// </summary>
    public static readonly NumberValue One = new NumberValue(1.0);

    /// <summary>
    /// 2.
    /// </summary>
    public static readonly NumberValue Two = new NumberValue(2.0);

    /// <summary>
    /// 1 / 2 = 0.5.
    /// </summary>
    public static readonly NumberValue Half = new NumberValue(0.5);

    /// <summary>
    /// sqrt(2) / 2.
    /// </summary>
    public static readonly NumberValue Sqrt2By2 = Sqrt2 / 2.0;

    /// <summary>
    /// sqrt(3) / 2.
    /// </summary>
    public static readonly NumberValue Sqrt3By2 = Sqrt3 / 2.0;

    /// <summary>
    /// sqrt(3) / 3.
    /// </summary>
    public static readonly NumberValue Sqrt3By3 = Sqrt3 / 3.0;

    /// <summary>
    /// 2 * sqrt(3) / 3.
    /// </summary>
    public static readonly NumberValue Sqrt3By3By2 = 2 * Sqrt3By3;

    /// <summary>
    /// PositiveInfinity.
    /// </summary>
    public static readonly NumberValue PositiveInfinity = new NumberValue(double.PositiveInfinity);

    /// <summary>
    /// NegativeInfinity.
    /// </summary>
    public static readonly NumberValue NegativeInfinity = new NumberValue(double.PositiveInfinity);

    /// <summary>
    /// NaN.
    /// </summary>
    public static readonly NumberValue NaN = new NumberValue(double.NaN);

    /// <summary>
    /// Initializes a new instance of the <see cref="NumberValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    public NumberValue(double value)
        => Number = value;

    /// <summary>
    /// Deconstructs <see cref="NumberValue"/> to <see cref="double"/>.
    /// </summary>
    /// <param name="number">The value of current number value.</param>
    public void Deconstruct(out double number) => number = Number;

    /// <inheritdoc />
    public bool Equals(NumberValue other)
        => MathExtensions.Equals(Number, other.Number);

    /// <inheritdoc />
    public bool Equals(double other)
        => MathExtensions.Equals(Number, other);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is NumberValue other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Number);

    /// <inheritdoc />
    public int CompareTo(NumberValue other)
        => Number.CompareTo(other.Number);

    /// <inheritdoc />
    public int CompareTo(double other)
        => Number.CompareTo(other);

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
        => Number.ToString(CultureInfo.InvariantCulture);

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
    /// is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double left, NumberValue right)
        => right.CompareTo(left) > 0;

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
    /// is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double left, NumberValue right)
        => right.CompareTo(left) < 0;

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
    /// is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double left, NumberValue right)
        => right.CompareTo(left) >= 0;

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
    /// Indicates whether <paramref name="left"/> parameter
    /// is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double left, NumberValue right)
        => right.CompareTo(left) <= 0;

    /// <summary>
    /// Produces the negative of <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="numberValue">The number value.</param>
    /// <returns>The negative of <paramref name="numberValue"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator -(NumberValue numberValue)
        => new NumberValue(-numberValue.Number);

    /// <summary>
    /// Adds two objects of <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator +(NumberValue left, NumberValue right)
        => new NumberValue(left.Number + right.Number);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator +(NumberValue left, double right)
        => new NumberValue(left.Number + right);

    /// <summary>
    /// Adds <see cref="double"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator +(double left, NumberValue right)
        => new NumberValue(left + right.Number);

    /// <summary>
    /// Adds <see cref="AngleValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator +(AngleValue left, NumberValue right)
        => new AngleValue(left.Angle + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator +(NumberValue left, AngleValue right)
        => new AngleValue(left.Number + right.Angle, right.Unit);

    /// <summary>
    /// Adds <see cref="PowerValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator +(PowerValue left, NumberValue right)
        => new PowerValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator +(NumberValue left, PowerValue right)
        => new PowerValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="TemperatureValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator +(TemperatureValue left, NumberValue right)
        => new TemperatureValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator +(NumberValue left, TemperatureValue right)
        => new TemperatureValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="MassValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator +(MassValue left, NumberValue right)
        => new MassValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="MassValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator +(NumberValue left, MassValue right)
        => new MassValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="LengthValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator +(LengthValue left, NumberValue right)
        => new LengthValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator +(NumberValue left, LengthValue right)
        => new LengthValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="TimeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator +(TimeValue left, NumberValue right)
        => new TimeValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator +(NumberValue left, TimeValue right)
        => new TimeValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="AreaValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator +(AreaValue left, NumberValue right)
        => new AreaValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator +(NumberValue left, AreaValue right)
        => new AreaValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="VolumeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator +(VolumeValue left, NumberValue right)
        => new VolumeValue(left.Value + right.Number, left.Unit);

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator +(NumberValue left, VolumeValue right)
        => new VolumeValue(left.Number + right.Value, right.Unit);

    /// <summary>
    /// Adds <see cref="Complex"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator +(Complex left, NumberValue right)
        => left + right.Number;

    /// <summary>
    /// Adds <see cref="NumberValue"/> and <see cref="Complex"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator +(NumberValue left, Complex right)
        => left.Number + right;

    /// <summary>
    /// Subtracts two objects of <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator -(NumberValue left, NumberValue right)
        => new NumberValue(left.Number - right.Number);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator -(NumberValue left, double right)
        => new NumberValue(left.Number - right);

    /// <summary>
    /// Subtracts <see cref="double"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator -(double left, NumberValue right)
        => new NumberValue(left - right.Number);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator -(NumberValue left, AngleValue right)
        => new AngleValue(left.Number - right.Angle, right.Unit);

    /// <summary>
    /// Subtracts <see cref="AngleValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator -(AngleValue left, NumberValue right)
        => new AngleValue(left.Angle - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator -(NumberValue left, PowerValue right)
        => new PowerValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="PowerValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator -(PowerValue left, NumberValue right)
        => new PowerValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator -(NumberValue left, TemperatureValue right)
        => new TemperatureValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="TemperatureValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator -(TemperatureValue left, NumberValue right)
        => new TemperatureValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="MassValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator -(NumberValue left, MassValue right)
        => new MassValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="MassValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator -(MassValue left, NumberValue right)
        => new MassValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator -(NumberValue left, LengthValue right)
        => new LengthValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="LengthValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator -(LengthValue left, NumberValue right)
        => new LengthValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator -(NumberValue left, TimeValue right)
        => new TimeValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="TimeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator -(TimeValue left, NumberValue right)
        => new TimeValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator -(NumberValue left, AreaValue right)
        => new AreaValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="AreaValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator -(AreaValue left, NumberValue right)
        => new AreaValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator -(NumberValue left, VolumeValue right)
        => new VolumeValue(left.Number - right.Value, right.Unit);

    /// <summary>
    /// Subtracts <see cref="VolumeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator -(VolumeValue left, NumberValue right)
        => new VolumeValue(left.Value - right.Number, left.Unit);

    /// <summary>
    /// Subtracts <see cref="NumberValue"/> and <see cref="Complex"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator -(NumberValue left, Complex right)
        => left.Number - right;

    /// <summary>
    /// Subtracts <see cref="Complex"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator -(Complex left, NumberValue right)
        => left - right.Number;

    /// <summary>
    /// Multiplies two objects of <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator *(NumberValue left, NumberValue right)
        => new NumberValue(left.Number * right.Number);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator *(NumberValue left, double right)
        => new NumberValue(left.Number * right);

    /// <summary>
    /// Multiplies <see cref="double"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator *(double left, NumberValue right)
        => new NumberValue(left * right.Number);

    /// <summary>
    /// Multiplies <see cref="AngleValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator *(AngleValue left, NumberValue right)
        => new AngleValue(left.Angle * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator *(NumberValue left, AngleValue right)
        => new AngleValue(left.Number * right.Angle, right.Unit);

    /// <summary>
    /// Multiplies <see cref="PowerValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator *(PowerValue left, NumberValue right)
        => new PowerValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator *(NumberValue left, PowerValue right)
        => new PowerValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="TemperatureValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator *(TemperatureValue left, NumberValue right)
        => new TemperatureValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator *(NumberValue left, TemperatureValue right)
        => new TemperatureValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="MassValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator *(MassValue left, NumberValue right)
        => new MassValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="MassValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator *(NumberValue left, MassValue right)
        => new MassValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="LengthValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator *(LengthValue left, NumberValue right)
        => new LengthValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator *(NumberValue left, LengthValue right)
        => new LengthValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="TimeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator *(TimeValue left, NumberValue right)
        => new TimeValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator *(NumberValue left, TimeValue right)
        => new TimeValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="AreaValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator *(AreaValue left, NumberValue right)
        => new AreaValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator *(NumberValue left, AreaValue right)
        => new AreaValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="VolumeValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator *(VolumeValue left, NumberValue right)
        => new VolumeValue(left.Value * right.Number, left.Unit);

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator *(NumberValue left, VolumeValue right)
        => new VolumeValue(left.Number * right.Value, right.Unit);

    /// <summary>
    /// Multiplies <see cref="Complex"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator *(Complex left, NumberValue right)
        => left * right.Number;

    /// <summary>
    /// Multiplies <see cref="NumberValue"/> and <see cref="Complex"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator *(NumberValue left, Complex right)
        => left.Number * right;

    /// <summary>
    /// Divides two objects of <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator /(NumberValue left, NumberValue right)
        => new NumberValue(left.Number / right.Number);

    /// <summary>
    /// Divides <see cref="NumberValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator /(NumberValue left, double right)
        => new NumberValue(left.Number / right);

    /// <summary>
    /// Divides <see cref="double"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator /(double left, NumberValue right)
        => new NumberValue(left / right.Number);

    /// <summary>
    /// Divides <see cref="AngleValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AngleValue operator /(AngleValue left, NumberValue right)
        => new AngleValue(left.Angle / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="PowerValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PowerValue operator /(PowerValue left, NumberValue right)
        => new PowerValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="TemperatureValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TemperatureValue operator /(TemperatureValue left, NumberValue right)
        => new TemperatureValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="MassValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MassValue operator /(MassValue left, NumberValue right)
        => new MassValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="LengthValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LengthValue operator /(LengthValue left, NumberValue right)
        => new LengthValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="TimeValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeValue operator /(TimeValue left, NumberValue right)
        => new TimeValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="AreaValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AreaValue operator /(AreaValue left, NumberValue right)
        => new AreaValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="VolumeValue"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VolumeValue operator /(VolumeValue left, NumberValue right)
        => new VolumeValue(left.Value / right.Number, left.Unit);

    /// <summary>
    /// Divides <see cref="Complex"/> by <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator /(Complex left, NumberValue right)
        => left / right.Number;

    /// <summary>
    /// Divides <see cref="NumberValue"/> by <see cref="Complex"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex operator /(NumberValue left, Complex right)
        => left.Number / right;

    /// <summary>
    /// Returns the remainder of a division.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>The remainder of a division.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator %(NumberValue left, NumberValue right)
        => new NumberValue(left.Number % right.Number);

    /// <summary>
    /// Calculates NOT operation.
    /// </summary>
    /// <param name="number">The left operand.</param>
    /// <returns>The result of NOT operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="number"/> is not an integer.</exception>
    public static NumberValue operator ~(NumberValue number)
    {
        if (!IsInt(number))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(number));

        return new NumberValue(~(int)number.Number);
    }

    /// <summary>
    /// Calculates AND operation between two double values.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of AND operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator &(NumberValue left, NumberValue right)
    {
        if (!IsInt(left))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
        if (!IsInt(right))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

        return new NumberValue((int)left.Number & (int)right.Number);
    }

    /// <summary>
    /// Calculates OR operation between two double values.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of OR operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator |(NumberValue left, NumberValue right)
    {
        if (!IsInt(left))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
        if (!IsInt(right))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

        return new NumberValue((int)left.Number | (int)right.Number);
    }

    /// <summary>
    /// Calculates XOR operation between two double values.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of XOR operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue operator ^(NumberValue left, NumberValue right)
    {
        if (!IsInt(left))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
        if (!IsInt(right))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

        return new NumberValue((int)left.Number ^ (int)right.Number);
    }

    /// <summary>
    /// Shifts <paramref name="left"/> by number of bits from <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of '&lt;&lt;' operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
    public static NumberValue LeftShift(NumberValue left, NumberValue right)
    {
        if (!IsInt(left))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
        if (!IsInt(right))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

        return new NumberValue((int)left.Number << (int)right.Number);
    }

    /// <summary>
    /// Shifts <paramref name="left"/> by number of bits from <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>The result of '&gt;&gt;' operation.</returns>
    /// <exception cref="InvalidOperationException"><paramref name="left"/> or <paramref name="right"/> is not an integer.</exception>
    public static NumberValue RightShift(NumberValue left, NumberValue right)
    {
        if (!IsInt(left))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(left));
        if (!IsInt(right))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(right));

        return new NumberValue((int)left.Number >> (int)right.Number);
    }

    private static bool IsInt(NumberValue number)
        => Math.Abs(number.Number % 1) <= MathExtensions.Epsilon;

    /// <summary>
    /// Returns the absolute value of a double-precision floating-point number.
    /// </summary>
    /// <param name="numberValue">A number that is greater than or equal to <see cref="double.MinValue"/>, but less than or equal to <see cref="double.MaxValue"/>.</param>
    /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="double.MaxValue"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Abs(NumberValue numberValue)
        => new NumberValue(Math.Abs(numberValue.Number));

    /// <summary>
    /// Returns the smallest integral value that is greater than
    /// or equal to the specified double-precision floating-point number.
    /// </summary>
    /// <param name="numberValue">A double-precision floating-point number.</param>
    /// <returns>The smallest integral value that is greater than or equal to <paramref name="numberValue"/>. If <paramref name="numberValue"/> is equal to <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/>, or <see cref="double.PositiveInfinity"/>, that value is returned. Note that this method returns a Double instead of an integral type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Ceiling(NumberValue numberValue)
        => new NumberValue(Math.Ceiling(numberValue.Number));

    /// <summary>
    /// Returns <c>e</c> raised to the specified power.
    /// </summary>
    /// <param name="numberValue">A number specifying a power.</param>
    /// <returns>The number <c>e</c> raised to the power <paramref name="numberValue"/>. If <paramref name="numberValue"/> equals <see cref="double.NaN"/> or <see cref="double.PositiveInfinity"/>, that value is returned. If <paramref name="numberValue"/> equals <see cref="double.NegativeInfinity"/>, 0 is returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Exp(NumberValue numberValue)
        => new NumberValue(Math.Exp(numberValue.Number));

    /// <summary>
    /// Returns a factorial of specified number.
    /// </summary>
    /// <param name="numberValue">A specified number.</param>
    /// <returns>The factorial.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Factorial(NumberValue numberValue)
    {
        if (numberValue < 0)
            return NaN;

        var n = Math.Round(numberValue.Number);
        var result = 1.0;

        for (var i = n; i > 0; i--)
            result *= i;

        return new NumberValue(result);
    }

    /// <summary>
    /// Returns the largest integral value less than
    /// or equal to the specified double-precision floating-point number.
    /// </summary>
    /// <param name="numberValue">A double-precision floating-point number.</param>
    /// <returns>The largest integral value less than or equal to <paramref name="numberValue"/>. If <paramref name="numberValue"/> is equal to <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/>, or <see cref="double.PositiveInfinity"/>, that value is returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Floor(NumberValue numberValue)
        => new NumberValue(Math.Floor(numberValue.Number));

    /// <summary>
    /// Returns the fractional part of the number.
    /// </summary>
    /// <param name="numberValue">The number value.</param>
    /// <returns>The fractional part.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Frac(NumberValue numberValue)
    {
        if (numberValue >= 0)
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
        var a = left.Number;
        var b = right.Number;

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
        => new NumberValue(Math.Log(numberValue.Number, 2));

    /// <summary>
    /// Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <param name="numberValue">A number whose logarithm is to be found.</param>
    /// <returns>The base 10 logarithm.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Lg(NumberValue numberValue)
        => new NumberValue(Math.Log10(numberValue.Number));

    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a specified number.
    /// </summary>
    /// <param name="numberValue">The number whose logarithm is to be found.</param>
    /// <returns>The natural (base <c>e</c>) logarithm.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Ln(NumberValue numberValue)
        => new NumberValue(Math.Log(numberValue.Number));

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found.</param>
    /// <param name="base">The base of the logarithm.</param>
    /// <returns>The logarithm.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Log(NumberValue number, NumberValue @base)
        => new NumberValue(Math.Log(number.Number, @base.Number));

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="number">The number whose logarithm is to be found.</param>
    /// <param name="base">The base of the logarithm.</param>
    /// <returns>The logarithm.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex Log(Complex number, NumberValue @base)
        => Complex.Log(number, @base.Number);

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
            if ((BitConverter.DoubleToInt64Bits(power.Number) & 1) == 1)
                return new NumberValue(-Math.Pow(-number.Number, power.Number));

            if (power > 0 && power < 1)
                return Complex.Pow(number.Number, power.Number);

            if (power < 0 && power > -1)
                return new Complex(0, -Math.Pow(-number.Number, power.Number));
        }

        return new NumberValue(Math.Pow(number.Number, power.Number));
    }

    /// <summary>
    /// Returns a specified number raised to the specified power.
    /// </summary>
    /// <param name="number">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="power">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The <paramref name="number"/> raised to the <paramref name="power"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Complex Pow(Complex number, NumberValue power)
        => Complex.Pow(number, power.Number);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="number">A double-precision floating-point number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="number"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NumberValue Round(NumberValue number)
    {
        var rounded = Math.Round(number.Number, MidpointRounding.AwayFromZero);

        return new NumberValue(rounded);
    }

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
        if (!IsInt(digits))
            throw new InvalidOperationException(Resource.ValueIsNotInteger);

        var rounded = Math.Round(number.Number, (int)digits.Number, MidpointRounding.AwayFromZero);

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
            return Complex.Sqrt(numberValue.Number);

        return new NumberValue(Math.Sqrt(numberValue.Number));
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
        if (!IsInt(numberValue))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

        var result = Convert.ToString((int)numberValue.Number, 2);
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
        if (!IsInt(numberValue))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

        return $"0{Convert.ToString((int)numberValue.Number, 8)}";
    }

    /// <summary>
    /// Converts <paramref name="numberValue"/> to the hexadecimal number.
    /// </summary>
    /// <param name="numberValue">The number.</param>
    /// <returns>String that contains the number in the new numeral system.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHex(NumberValue numberValue)
    {
        if (!IsInt(numberValue))
            throw new ArgumentException(Resource.ValueIsNotInteger, nameof(numberValue));

        var result = Convert.ToString((int)numberValue.Number, 16).ToUpperInvariant();
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
        => new NumberValue(Math.Truncate(number.Number));

    /// <summary>
    /// Gets a value indicating whether the current value is not a number (NaN).
    /// </summary>
    public bool IsNaN => double.IsNaN(Number);

    /// <summary>
    /// Gets a value indicating whether the current number evaluates to infinity.
    /// </summary>
    public bool IsInfinity => double.IsInfinity(Number);

    /// <summary>
    /// Gets a value indicating whether the current number evaluates to positive infinity.
    /// </summary>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Number);

    /// <summary>
    /// Gets a value indicating whether the current number evaluates to negative infinity.
    /// </summary>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Number);

    /// <summary>
    /// Gets a sign of number.
    /// </summary>
    public double Sign => Math.Sign(Number);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public double Number { get; }
}