// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.LengthUnits;

/// <summary>
/// Represents a number with unit.
/// </summary>
public readonly struct LengthValue : IEquatable<LengthValue>, IComparable<LengthValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LengthValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public LengthValue(NumberValue value, LengthUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Meter(double value)
        => Meter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Meter(NumberValue value)
        => new LengthValue(value, LengthUnit.Meter);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Nanometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Nanometer(double value)
        => Nanometer(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Nanometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Nanometer(NumberValue value)
        => new LengthValue(value, LengthUnit.Nanometer);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Micrometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Micrometer(double value)
        => Micrometer(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Micrometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Micrometer(NumberValue value)
        => new LengthValue(value, LengthUnit.Micrometer);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Millimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Millimeter(double value)
        => Millimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Millimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Millimeter(NumberValue value)
        => new LengthValue(value, LengthUnit.Millimeter);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Centimeter(double value)
        => Centimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Centimeter(NumberValue value)
        => new LengthValue(value, LengthUnit.Centimeter);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Decimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Decimeter(double value)
        => Decimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Decimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Decimeter(NumberValue value)
        => new LengthValue(value, LengthUnit.Decimeter);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Kilometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Kilometer(double value)
        => Kilometer(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Kilometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Kilometer(NumberValue value)
        => new LengthValue(value, LengthUnit.Kilometer);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Inch(double value)
        => Inch(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Inch(NumberValue value)
        => new LengthValue(value, LengthUnit.Inch);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Foot(double value)
        => Foot(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Foot(NumberValue value)
        => new LengthValue(value, LengthUnit.Foot);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Yard(double value)
        => Yard(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Yard(NumberValue value)
        => new LengthValue(value, LengthUnit.Yard);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Mile(double value)
        => Mile(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Mile(NumberValue value)
        => new LengthValue(value, LengthUnit.Mile);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Nautical Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue NauticalMile(double value)
        => NauticalMile(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Nautical Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue NauticalMile(NumberValue value)
        => new LengthValue(value, LengthUnit.NauticalMile);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Chain</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Chain(double value)
        => Chain(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Chain</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Chain(NumberValue value)
        => new LengthValue(value, LengthUnit.Chain);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Rod</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Rod(double value)
        => Rod(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Rod</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Rod(NumberValue value)
        => new LengthValue(value, LengthUnit.Rod);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Astronomical  Unit</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue AstronomicalUnit(double value)
        => AstronomicalUnit(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Astronomical Unit</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue AstronomicalUnit(NumberValue value)
        => new LengthValue(value, LengthUnit.AstronomicalUnit);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>LightYear</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue LightYear(double value)
        => LightYear(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>LightYear</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue LightYear(NumberValue value)
        => new LengthValue(value, LengthUnit.LightYear);

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Parsec</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Parsec(double value)
        => Parsec(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="LengthValue"/> struct with <c>Parsec</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The length value.</returns>
    public static LengthValue Parsec(NumberValue value)
        => new LengthValue(value, LengthUnit.Parsec);

    /// <inheritdoc />
    public bool Equals(LengthValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is LengthValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(LengthValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.CompareTo(otherInBase.Value);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            LengthValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(LengthValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} {Unit.UnitName}";

    /// <summary>
    /// Determines whether two specified instances of <see cref="LengthValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(LengthValue left, LengthValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="LengthValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(LengthValue left, LengthValue right)
        => !(left == right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(LengthValue left, LengthValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(LengthValue left, LengthValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(LengthValue left, LengthValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(LengthValue left, LengthValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static LengthValue operator +(LengthValue left, LengthValue right)
    {
        right = right.To(left.Unit);

        return new LengthValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static LengthValue operator -(LengthValue left, LengthValue right)
    {
        right = right.To(left.Unit);

        return new LengthValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static LengthValue operator -(LengthValue value)
        => new LengthValue(-value.Value, value.Unit);

    /// <summary>
    /// Multiplies two objects of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static AreaValue operator *(LengthValue left, LengthValue right)
    {
        right = right.To(left.Unit);

        var areaUnit = left.Unit.ToAreaUnit();

        return new AreaValue(left.Value * right.Value, areaUnit);
    }

    /// <summary>
    /// Multiplies two objects of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VolumeValue operator *(AreaValue left, LengthValue right)
    {
        var areaUnit = right.Unit.ToAreaUnit();
        var rightArea = new AreaValue(right.Value, areaUnit);
        var volumeUnit = left.Unit.ToVolumeUnit();

        return new VolumeValue(left.Value * rightArea.Value, volumeUnit);
    }

    /// <summary>
    /// Multiplies two objects of <see cref="LengthValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VolumeValue operator *(LengthValue left, AreaValue right)
    {
        var areaUnit = left.Unit.ToAreaUnit();
        var leftArea = new AreaValue(left.Value, areaUnit);
        var volumeUnit = right.Unit.ToVolumeUnit();

        return new VolumeValue(leftArea.Value * right.Value, volumeUnit);
    }

    private LengthValue ToBase()
        => new LengthValue(Value * Unit.Factor, LengthUnit.Meter);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public LengthValue To(LengthUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new LengthValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the value to the meter unit.
    /// </summary>
    /// <returns>The value in meter unit.</returns>
    public LengthValue ToMeter()
        => To(LengthUnit.Meter);

    /// <summary>
    /// Converts the value to the nanometer unit.
    /// </summary>
    /// <returns>The value in nanometer unit.</returns>
    public LengthValue ToNanometer()
        => To(LengthUnit.Nanometer);

    /// <summary>
    /// Converts the value to the micrometer unit.
    /// </summary>
    /// <returns>The value in micrometer unit.</returns>
    public LengthValue ToMicrometer()
        => To(LengthUnit.Micrometer);

    /// <summary>
    /// Converts the value to the millimeter unit.
    /// </summary>
    /// <returns>The value in millimeter unit.</returns>
    public LengthValue ToMillimeter()
        => To(LengthUnit.Millimeter);

    /// <summary>
    /// Converts the value to the centimeter unit.
    /// </summary>
    /// <returns>The value in centimeter unit.</returns>
    public LengthValue ToCentimeter()
        => To(LengthUnit.Centimeter);

    /// <summary>
    /// Converts the value to the decimeter unit.
    /// </summary>
    /// <returns>The value in decimeter unit.</returns>
    public LengthValue ToDecimeter()
        => To(LengthUnit.Decimeter);

    /// <summary>
    /// Converts the value to the kilometer unit.
    /// </summary>
    /// <returns>The value in kilometer unit.</returns>
    public LengthValue ToKilometer()
        => To(LengthUnit.Kilometer);

    /// <summary>
    /// Converts the value to the inch unit.
    /// </summary>
    /// <returns>The value in inch unit.</returns>
    public LengthValue ToInch()
        => To(LengthUnit.Inch);

    /// <summary>
    /// Converts the value to the foot unit.
    /// </summary>
    /// <returns>The value in foot unit.</returns>
    public LengthValue ToFoot()
        => To(LengthUnit.Foot);

    /// <summary>
    /// Converts the value to the yard unit.
    /// </summary>
    /// <returns>The value in yard unit.</returns>
    public LengthValue ToYard()
        => To(LengthUnit.Yard);

    /// <summary>
    /// Converts the value to the mile unit.
    /// </summary>
    /// <returns>The value in mile unit.</returns>
    public LengthValue ToMile()
        => To(LengthUnit.Mile);

    /// <summary>
    /// Converts the value to the nautical mile unit.
    /// </summary>
    /// <returns>The value in nautical mile unit.</returns>
    public LengthValue ToNauticalMile()
        => To(LengthUnit.NauticalMile);

    /// <summary>
    /// Converts the value to the chain unit.
    /// </summary>
    /// <returns>The value in chain unit.</returns>
    public LengthValue ToChain()
        => To(LengthUnit.Chain);

    /// <summary>
    /// Converts the value to the rod unit.
    /// </summary>
    /// <returns>The value in rod unit.</returns>
    public LengthValue ToRod()
        => To(LengthUnit.Rod);

    /// <summary>
    /// Converts the value to the astronomical unit.
    /// </summary>
    /// <returns>The value in astronomical unit.</returns>
    public LengthValue ToAstronomicalUnit()
        => To(LengthUnit.AstronomicalUnit);

    /// <summary>
    /// Converts the value to the light year unit.
    /// </summary>
    /// <returns>The value in light year unit.</returns>
    public LengthValue ToLightYear()
        => To(LengthUnit.LightYear);

    /// <summary>
    /// Converts the value to the parsec unit.
    /// </summary>
    /// <returns>The value in parsec unit.</returns>
    public LengthValue ToParsec()
        => To(LengthUnit.Parsec);

    /// <summary>
    /// Returns the absolute value of a specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static LengthValue Abs(LengthValue value)
        => new LengthValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The smallest integral value.</returns>
    public static LengthValue Ceiling(LengthValue value)
        => new LengthValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified value number.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The largest integral value.</returns>
    public static LengthValue Floor(LengthValue value)
        => new LengthValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified value number.
    /// </summary>
    /// <param name="value">An value to truncate.</param>
    /// <returns>The integral part of value number.</returns>
    public static LengthValue Truncate(LengthValue value)
        => new LengthValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the value number.
    /// </summary>
    /// <param name="value">The value number.</param>
    /// <returns>The fractional part.</returns>
    public static LengthValue Frac(LengthValue value)
        => new LengthValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="lengthValue">The number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="lengthValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="lengthValue"/> is returned unchanged.</returns>
    public static LengthValue Round(LengthValue lengthValue, NumberValue digits)
        => new LengthValue(NumberValue.Round(lengthValue.Value, digits), lengthValue.Unit);

    /// <summary>
    /// Converts <see cref="LengthValue"/> to <see cref="Length"/>.
    /// </summary>
    /// <returns>The length number.</returns>
    public Length AsExpression()
        => new Length(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public LengthUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}