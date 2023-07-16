// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the unassign function.
/// </summary>
public class Unassign : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unassign"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public Unassign(Variable key)
        => Key = key ?? throw new ArgumentNullException(nameof(key));

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Unassign undef)
            return false;

        return Key.Equals(undef.Key);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Always.</exception>
    public object Execute()
        => throw new NotSupportedException();

    /// <inheritdoc />
    public object Execute(IExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        var parameter = parameters[Key.Name];

        parameters.Remove(Key.Name);

        return parameter.Value;
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
    /// <param name="key">The argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(Variable? key = null)
        => new Unassign(key ?? Key);

    /// <summary>
    /// Gets the key.
    /// </summary>
    public Variable Key { get; }
}