// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// The abstract base class that represents the unary operation.
/// </summary>
public abstract class UnaryExpression : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
    /// </summary>
    /// <param name="argument">The expression.</param>
    protected UnaryExpression(IExpression argument)
        => Argument = argument ?? throw new ArgumentNullException(nameof(argument));

    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryExpression"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    protected UnaryExpression(ImmutableArray<IExpression> arguments)
    {
        if (arguments == null)
            throw new ArgumentNullException(nameof(arguments));

        if (arguments.Length < 1)
            throw new ParseException(Resource.LessParams);

        if (arguments.Length > 1)
            throw new ParseException(Resource.MoreParams);

        Argument = arguments[0];
    }

    /// <summary>
    /// Deconstructs <see cref="UnaryExpression"/>.
    /// </summary>
    /// <param name="argument">The argument.</param>
    public void Deconstruct(out IExpression argument) => argument = Argument;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        return Argument.Equals(((UnaryExpression)obj).Argument);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(new CommonFormatter());

    /// <inheritdoc />
    public object Execute() => Execute(null);

    /// <inheritdoc />
    public abstract object Execute(ExpressionParameters? parameters);

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
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="argument">The argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public abstract IExpression Clone(IExpression? argument = null);

    /// <summary>
    /// Gets the expression.
    /// </summary>
    public IExpression Argument { get; }
}