// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents a string.
/// </summary>
public class StringExpression : IExpression, IEquatable<StringExpression>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringExpression"/> class.
    /// </summary>
    /// <param name="value">A string.</param>
    public StringExpression(string value)
        => Value = value ?? throw new ArgumentNullException(nameof(value));

    /// <summary>
    /// Deconstructs <see cref="StringExpression"/> to <see cref="double"/>.
    /// </summary>
    /// <param name="value">The string.</param>
    public void Deconstruct(out string value) => value = Value;

    /// <inheritdoc />
    public bool Equals(StringExpression? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (typeof(StringExpression) != obj.GetType())
            return false;

        return Equals((StringExpression)obj);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter)
        => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString()
        => ToString(new CommonFormatter());

    /// <inheritdoc />
    public object Execute() => Value;

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters) => Value;

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }

    /// <summary>
    /// Gets a string.
    /// </summary>
    public string Value { get; }
}