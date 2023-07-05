// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the ":=" operator and the assign function.
/// </summary>
public class Assign : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Assign"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
    public Assign(Variable key, IExpression value)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Assign def)
            return false;

        return Key.Equals(def.Key) && Value.Equals(def.Value);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute() => throw new NotSupportedException();

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        var value = Value.Execute(parameters);
        parameters[Key.Name] = new ParameterValue(value);

        return value;
    }

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
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="key">The left argument of new expression.</param>
    /// <param name="value">The right argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(Variable? key = null, IExpression? value = null)
        => new Assign(key ?? Key, value ?? Value);

    /// <summary>
    /// Gets the key.
    /// </summary>
    public Variable Key { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public IExpression Value { get; }
}