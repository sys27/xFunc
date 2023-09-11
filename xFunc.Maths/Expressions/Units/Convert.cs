// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics;

namespace xFunc.Maths.Expressions.Units;

/// <summary>
/// Represents the convert function.
/// </summary>
public class Convert : IExpression
{
    private readonly IConverter converter;

    /// <summary>
    /// Initializes a new instance of the <see cref="Convert"/> class.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="unit">The target unit.</param>
    public Convert(IConverter converter, IExpression value, IExpression unit)
        : this(converter, ImmutableArray.Create(value, unit))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Convert"/> class.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <param name="arguments">The list of arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="converter"/> is <c>null</c>.</exception>
    internal Convert(IConverter converter, ImmutableArray<IExpression> arguments)
    {
        this.converter = converter ?? throw new ArgumentNullException(nameof(converter));

        Debug.Assert(arguments != null, $"{nameof(arguments)} shouldn't be null.");
        Debug.Assert(arguments.Length == 2, "{nameof(arguments)} should contain 2 parameters.");

        Value = arguments[0] ?? throw new ArgumentNullException(nameof(arguments));
        Unit = arguments[1] ?? throw new ArgumentNullException(nameof(arguments));
    }

    /// <summary>
    /// Deconstructs <see cref="Convert"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="unit">The target unit.</param>
    public void Deconstruct(out IExpression value, out IExpression unit)
    {
        value = Value;
        unit = Unit;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var (value, unit) = (Convert)obj;

        return Value.Equals(value) && Unit.Equals(unit);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    /// <exception cref="ExecutionException">The result of evaluation of arguments is not supported.</exception>
    public object Execute() => Execute(null);

    /// <inheritdoc />
    /// <exception cref="ExecutionException">The result of evaluation of arguments is not supported.</exception>
    public object Execute(ExpressionParameters? parameters)
    {
        var valueResult = Value.Execute(parameters);
        var unitResult = Unit.Execute(parameters);

        return unitResult switch
        {
            string unit => converter.Convert(valueResult, unit),
            _ => throw ExecutionException.For(this),
        };
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException"><paramref name="analyzer"/> is <c>null</c>.</exception>
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException"><paramref name="analyzer"/> is <c>null</c>.</exception>
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }

    /// <summary>
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="left">The left argument of new expression.</param>
    /// <param name="right">The right argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(IExpression? left = null, StringExpression? right = null)
        => new Convert(converter, left ?? Value, right ?? Unit);

    /// <summary>
    /// Gets the value.
    /// </summary>
    public IExpression Value { get; }

    /// <summary>
    /// Gets the target unit.
    /// </summary>
    public IExpression Unit { get; }
}