// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Define operator.
/// </summary>
public class Define : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Define"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
    public Define(IExpression key, IExpression value)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));

        if (key is not Variable and not UserFunction)
            throw new NotSupportedException();

        if (value is null)
            throw new ArgumentNullException(nameof(value));

        Key = key;
        Value = value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Define def)
            return false;

        return Key.Equals(def.Key) && Value.Equals(def.Value);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(new CommonFormatter());

    /// <inheritdoc />
    public object Execute() => throw new NotSupportedException();

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        if (Key is Variable variable)
        {
            parameters.Variables[variable.Name] = new ParameterValue(Value.Execute(parameters));

            return string.Format(CultureInfo.InvariantCulture, Resource.AssignVariable, Key, Value);
        }

        parameters.Functions[(UserFunction)Key] = Value;

        return string.Format(CultureInfo.InvariantCulture, Resource.AssignFunction, Key, Value);
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
    public IExpression Clone(IExpression? key = null, IExpression? value = null)
        => new Define(key ?? Key, value ?? Value);

    /// <summary>
    /// Gets the key.
    /// </summary>
    public IExpression Key { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public IExpression Value { get; }
}