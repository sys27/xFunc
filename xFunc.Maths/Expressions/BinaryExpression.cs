// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// The base class for binary operations.
/// </summary>
public abstract class BinaryExpression : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
    /// </summary>
    /// <param name="left">The left (first) operand.</param>
    /// <param name="right">The right (second) operand.</param>
    protected BinaryExpression(IExpression left, IExpression right)
    {
        Left = left ?? throw new ArgumentNullException(nameof(left));
        Right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryExpression"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    protected BinaryExpression(ImmutableArray<IExpression> arguments)
    {
        if (arguments == null)
            throw new ArgumentNullException(nameof(arguments));

        if (arguments.Length < 2)
            throw new ParseException(Resource.LessParams);

        if (arguments.Length > 2)
            throw new ParseException(Resource.MoreParams);

        Left = arguments[0];
        Right = arguments[1];
    }

    /// <summary>
    /// Deconstructs <see cref="BinaryExpression"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public void Deconstruct(out IExpression left, out IExpression right)
    {
        left = Left;
        right = Right;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var (left, right) = (BinaryExpression)obj;

        return Left.Equals(left) && Right.Equals(right);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

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
    /// <param name="left">The left argument of new expression.</param>
    /// <param name="right">The right argument of new expression.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public abstract IExpression Clone(IExpression? left = null, IExpression? right = null);

    /// <summary>
    /// Gets the left (first) operand.
    /// </summary>
    public IExpression Left { get; }

    /// <summary>
    /// Gets the right (second) operand.
    /// </summary>
    public IExpression Right { get; }
}