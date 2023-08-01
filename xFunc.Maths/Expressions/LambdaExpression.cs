// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents an expression that returns a function.
/// </summary>
/// <seealso cref="Lambda"/>
public class LambdaExpression : IExpression, IEquatable<LambdaExpression>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaExpression"/> class.
    /// </summary>
    /// <param name="lambda">The function.</param>
    public LambdaExpression(Lambda lambda)
        => Lambda = lambda;

    /// <inheritdoc />
    public bool Equals(LambdaExpression? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return Lambda.Equals(other.Lambda);
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

        return Equals((LambdaExpression)obj);
    }

    /// <inheritdoc />
    public object Execute()
        => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
        => Lambda.Capture(parameters);

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
    /// <param name="lambda">The function.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(Lambda? lambda = null)
        => new LambdaExpression(lambda ?? Lambda);

    /// <summary>
    /// Gets the function.
    /// </summary>
    public Lambda Lambda { get; }
}