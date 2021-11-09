// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units;

/// <summary>
/// Represents an expression that contains unit number.
/// </summary>
/// <typeparam name="T">The type of unit.</typeparam>
public abstract class Unit<T> : IExpression, IEquatable<Unit<T>>
    where T : struct, IEquatable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unit{T}"/> class.
    /// </summary>
    /// <param name="value">A unit number.</param>
    protected Unit(T value) => Value = value;

    /// <summary>
    /// Deconstructs <see cref="Unit{T}"/> to <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">The angle.</param>
    public void Deconstruct(out T value) => value = Value;

    /// <inheritdoc />
    public bool Equals(Unit<T>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        return Equals((Unit<T>)obj);
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

        return AnalyzeInternal(analyzer);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return AnalyzeInternal(analyzer, context);
    }

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <returns>
    /// The analysis result.
    /// </returns>
    protected abstract TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer);

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <param name="context">The context.</param>
    /// <returns>The analysis result.</returns>
    protected abstract TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public T Value { get; }
}