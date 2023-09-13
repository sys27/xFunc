// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the rational number.
/// </summary>
public readonly struct RationalValue : IEquatable<RationalValue>, IComparable<RationalValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RationalValue"/> struct.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The denominator.</param>
    public RationalValue(NumberValue numerator, NumberValue denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RationalValue"/> struct.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The denominator.</param>
    public RationalValue(double numerator, double denominator)
        : this(new NumberValue(numerator), new NumberValue(denominator))
    {
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="RationalValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(RationalValue left, RationalValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="RationalValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(RationalValue left, RationalValue right)
        => !left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(RationalValue left, RationalValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(RationalValue left, RationalValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(RationalValue left, RationalValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(RationalValue left, RationalValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Produces the negative of <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="rationalValue">The rational number.</param>
    /// <returns>The negative of <paramref name="rationalValue"/>.</returns>
    public static RationalValue operator -(RationalValue rationalValue)
        => new RationalValue(-rationalValue.Numerator, rationalValue.Denominator).ToCanonical();

    /// <summary>
    /// Adds two objects of <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator +(RationalValue left, RationalValue right)
    {
        if (left.Denominator == right.Denominator)
            return new RationalValue(left.Numerator + right.Numerator, left.Denominator).ToCanonical();

        var numerator = left.Numerator * right.Denominator + right.Numerator * left.Denominator;
        var denominator = left.Denominator * right.Denominator;

        return new RationalValue(numerator, denominator).ToCanonical();
    }

    /// <summary>
    /// Adds <see cref="double"/> and <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator +(NumberValue left, RationalValue right)
        => new RationalValue(left, NumberValue.One) + right;

    /// <summary>
    /// Adds <see cref="RationalValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator +(RationalValue left, NumberValue right)
        => left + new RationalValue(right, NumberValue.One);

    /// <summary>
    /// Subtracts two objects of <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator -(RationalValue left, RationalValue right)
    {
        if (left.Denominator == right.Denominator)
            return new RationalValue(left.Numerator - right.Numerator, left.Denominator).ToCanonical();

        var numerator = left.Numerator * right.Denominator - right.Numerator * left.Denominator;
        var denominator = left.Denominator * right.Denominator;

        return new RationalValue(numerator, denominator).ToCanonical();
    }

    /// <summary>
    /// Subtracts <see cref="double"/> and <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator -(NumberValue left, RationalValue right)
        => new RationalValue(left, NumberValue.One) - right;

    /// <summary>
    /// Subtracts <see cref="RationalValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator -(RationalValue left, NumberValue right)
        => left - new RationalValue(right, NumberValue.One);

    /// <summary>
    /// Multiplies two objects of <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator *(RationalValue left, RationalValue right)
    {
        var numerator = left.Numerator * right.Numerator;
        var denominator = left.Denominator * right.Denominator;

        return new RationalValue(numerator, denominator).ToCanonical();
    }

    /// <summary>
    /// Multiplies <see cref="double"/> and <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator *(NumberValue left, RationalValue right)
        => new RationalValue(left, NumberValue.One) * right;

    /// <summary>
    /// Multiplies <see cref="RationalValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator *(RationalValue left, NumberValue right)
        => left * new RationalValue(right, NumberValue.One);

    /// <summary>
    /// Divides two objects of <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator /(RationalValue left, RationalValue right)
    {
        var numerator = left.Numerator * right.Denominator;
        var denominator = left.Denominator * right.Numerator;

        return new RationalValue(numerator, denominator).ToCanonical();
    }

    /// <summary>
    /// Divides <see cref="double"/> and <see cref="RationalValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator /(NumberValue left, RationalValue right)
        => new RationalValue(left, NumberValue.One) / right;

    /// <summary>
    /// Divides <see cref="RationalValue"/> and <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static RationalValue operator /(RationalValue left, NumberValue right)
        => left / new RationalValue(right, NumberValue.One);

    /// <inheritdoc />
    public bool Equals(RationalValue other)
        => Numerator * other.Denominator == Denominator * other.Numerator;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is RationalValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(RationalValue other)
    {
        var left = ToCanonical();
        var right = other.ToCanonical();
        var result = left.Numerator * right.Denominator - left.Denominator * right.Numerator;

        if (result < 0)
            return -1;

        if (result > 0)
            return 1;

        return 0;
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return 1;

        return obj is RationalValue other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(RationalValue)}");
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Numerator, Denominator);

    /// <inheritdoc />
    public override string ToString()
        => $"{Numerator} // {Denominator}";

    /// <summary>
    /// Converts the current rational number to the canonical form.
    /// </summary>
    /// <returns>The rational number in the canonical form.</returns>
    public RationalValue ToCanonical()
    {
        var gcd = NumberValue.GCD(Numerator, Denominator);
        if (gcd == NumberValue.One)
            return this;

        var numerator = Numerator / gcd;
        var denominator = Denominator / gcd;

        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        return new RationalValue(numerator, denominator);
    }

    /// <summary>
    /// Converts the current rational number to irrational.
    /// </summary>
    /// <returns>The irrational number.</returns>
    public NumberValue ToIrrational()
        => Numerator / Denominator;

    /// <summary>
    /// Returns the absolute value of a rational number.
    /// </summary>
    /// <param name="rationalValue">The rational number.</param>
    /// <returns>The abs of rational number.</returns>
    public static RationalValue Abs(RationalValue rationalValue)
        => new RationalValue(NumberValue.Abs(rationalValue.Numerator), NumberValue.Abs(rationalValue.Denominator));

    /// <summary>
    /// Returns the rational number raised to the specified power.
    /// </summary>
    /// <param name="rationalValue">The rational number.</param>
    /// <param name="numberValue">The power.</param>
    /// <returns>The <paramref name="rationalValue"/> raised to the <paramref name="numberValue"/>.</returns>
    public static RationalValue Pow(RationalValue rationalValue, NumberValue numberValue)
    {
        double numerator;
        double denominator;

        var power = numberValue.Number;
        if (power >= 0)
        {
            numerator = rationalValue.Numerator.Number;
            denominator = rationalValue.Denominator.Number;
        }
        else
        {
            numerator = rationalValue.Denominator.Number;
            denominator = rationalValue.Numerator.Number;
            power = -power;
        }

        var numeratorPow = Math.Pow(numerator, power);
        var denominatorPow = Math.Pow(denominator, power);

        return new RationalValue(numeratorPow, denominatorPow).ToCanonical();
    }

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="rationalValue">The rational number whose logarithm is to be found.</param>
    /// <param name="base">The base of the logarithm.</param>
    /// <returns>The logarithm.</returns>
    public static NumberValue Log(RationalValue rationalValue, NumberValue @base)
        => NumberValue.Log(rationalValue.Numerator, @base) - NumberValue.Log(rationalValue.Denominator, @base);

    /// <summary>
    /// Returns the base 2 logarithm of a specified rational number.
    /// </summary>
    /// <param name="rationalValue">A rational number whose logarithm is to be found.</param>
    /// <returns>The binary logarithm.</returns>
    public static NumberValue Lb(RationalValue rationalValue)
        => Log(rationalValue, NumberValue.Two);

    /// <summary>
    /// Returns the base 10 logarithm of a specified rational number.
    /// </summary>
    /// <param name="rationalValue">A rational number whose logarithm is to be found.</param>
    /// <returns>The base 10 logarithm.</returns>
    public static NumberValue Lg(RationalValue rationalValue)
        => Log(rationalValue, new NumberValue(10));

    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a specified rational number.
    /// </summary>
    /// <param name="rationalValue">The rational number whose logarithm is to be found.</param>
    /// <returns>The natural (base <c>e</c>) logarithm.</returns>
    public static NumberValue Ln(RationalValue rationalValue)
        => Log(rationalValue, new NumberValue(Math.E));

    /// <summary>
    /// Gets the numerator.
    /// </summary>
    public NumberValue Numerator { get; }

    /// <summary>
    /// Gets the denominator.
    /// </summary>
    public NumberValue Denominator { get; }
}