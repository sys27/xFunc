// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represent the expression to call function.
/// </summary>
/// <seealso cref="Function"/>
public class CallExpression : IExpression, IEquatable<CallExpression>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CallExpression"/> class.
    /// </summary>
    /// <param name="function">The expression that returns function.</param>
    /// <param name="argument">The parameter of the function.</param>
    public CallExpression(IExpression function, IExpression argument)
        : this(function, ImmutableArray.Create(argument))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CallExpression"/> class.
    /// </summary>
    /// <param name="function">The expression that returns function.</param>
    /// <param name="argument1">The first parameter of the function.</param>
    /// <param name="argument2">The second parameter of the function.</param>
    public CallExpression(IExpression function, IExpression argument1, IExpression argument2)
        : this(function, ImmutableArray.Create(argument1, argument2))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CallExpression"/> class.
    /// </summary>
    /// <param name="function">The expression that returns function.</param>
    /// <param name="parameters">The list of parameters of the function.</param>
    public CallExpression(IExpression function, ImmutableArray<IExpression> parameters)
    {
        Function = function;
        Parameters = parameters;
    }

    /// <inheritdoc />
    public bool Equals(CallExpression? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Function.Equals(other.Function) && Parameters.SequenceEqual(other.Parameters);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((CallExpression)obj);
    }

    /// <inheritdoc />
    public object Execute()
        => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        if (Function.Execute(parameters) is not Lambda function)
            throw new ResultIsNotSupportedException(this, Function);

        var result = function.Call(Parameters, parameters);

        return result;
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter)
        => formatter.Analyze(this);

    /// <inheritdoc />
    public override string ToString()
        => ToString(CommonFormatter.Instance);

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
    /// <param name="function">The expression that returns function.</param>
    /// <param name="parameters">The list of parameters of the function.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(IExpression? function = null, ImmutableArray<IExpression>? parameters = null)
        => new CallExpression(function ?? Function, parameters ?? Parameters);

    /// <summary>
    /// Gets the expression that returns the function.
    /// </summary>
    public IExpression Function { get; }

    /// <summary>
    /// Gets a list of parameters of the function.
    /// </summary>
    public ImmutableArray<IExpression> Parameters { get; }
}