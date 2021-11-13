// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Undefine operator.
/// </summary>
public class Undefine : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Undefine"/> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public Undefine(IExpression key)
    {
        if (key is null)
            throw new ArgumentNullException(nameof(key));

        if (!(key is Variable || key is UserFunction))
            throw new NotSupportedException();

        Key = key;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        var undef = obj as Undefine;
        if (undef is null)
            return false;

        return Key.Equals(undef.Key);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(new CommonFormatter());

    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Always.</exception>
    public object Execute() => throw new NotSupportedException();

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        if (Key is Variable variable)
        {
            parameters.Variables.Remove(variable.Name);

            return string.Format(CultureInfo.InvariantCulture, Resource.UndefineVariable, Key);
        }

        parameters.Functions.Remove((UserFunction)Key);

        return string.Format(CultureInfo.InvariantCulture, Resource.UndefineFunction, Key);
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
    public IExpression Clone(IExpression? key = null)
        => new Undefine(key ?? Key);

    /// <summary>
    /// Gets the key.
    /// </summary>
    public IExpression Key { get; }
}