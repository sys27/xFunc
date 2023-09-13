// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Represents the item of <see cref="ExpressionParameters"/>.
/// </summary>
public class Parameter : IComparable<Parameter>, IEquatable<Parameter>
{
    private ParameterValue value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parameter" /> class.
    /// </summary>
    /// <param name="key">The name of parameter.</param>
    /// <param name="value">The value of parameter.</param>
    /// <param name="type">The type of parameter.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="key"/> is <c>null</c> or empty.</exception>
    public Parameter(string key, ParameterValue value, ParameterType type = ParameterType.Normal)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));

        Key = key;
        Value = value;
        Type = type;
    }

    /// <summary>
    /// Creates a constant.
    /// </summary>
    /// <param name="key">The name of parameter.</param>
    /// <param name="value">The value of parameter.</param>
    /// <returns>A constant.</returns>
    public static Parameter Constant(string key, ParameterValue value)
        => new Parameter(key, value, ParameterType.Constant);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is Parameter param)
            return Equals(param);

        return false;
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Key);

    /// <inheritdoc />
    public override string ToString()
        => string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2})", Key, value.Value, Type);

    /// <inheritdoc />
    public int CompareTo(Parameter? other)
        => string.Compare(Key, other?.Key, StringComparison.Ordinal);

    /// <inheritdoc />
    public bool Equals(Parameter? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Key == other.Key && value.Equals(other.value);
    }

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Parameter? left, Parameter? right)
        => Equals(left, right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Parameter? left, Parameter? right)
        => !Equals(left, right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(Parameter? left, Parameter? right)
    {
        if (left is null || right is null)
            return false;

        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(Parameter? left, Parameter? right)
    {
        if (left is null || right is null)
            return false;

        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(Parameter? left, Parameter? right)
        => !(left < right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left parameter.</param>
    /// <param name="right">The right parameter.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(Parameter? left, Parameter? right)
        => !(left > right);

    /// <summary>
    /// Gets the name of parameter.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets or sets the value of parameter.
    /// </summary>
    /// <exception cref="ParameterIsReadOnlyException">Thrown when the user tries to update read-only or constant parameter.</exception>
    public ParameterValue Value
    {
        get => value;
        set
        {
            if (Type != ParameterType.Normal)
                throw new ParameterIsReadOnlyException(Resource.ReadOnlyError, Key);

            this.value = value;
        }
    }

    /// <summary>
    /// Gets the type of parameter.
    /// </summary>
    public ParameterType Type { get; }
}