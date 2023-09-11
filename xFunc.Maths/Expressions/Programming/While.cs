// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "while" loop.
/// </summary>
public class While : IExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="While"/> class.
    /// </summary>
    /// <param name="body">The body of while loop.</param>
    /// <param name="condition">The condition of loop.</param>
    public While(IExpression body, IExpression condition)
    {
        Body = body;
        Condition = condition;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var other = (While)obj;

        return Body.Equals(other.Body) && Condition.Equals(other.Condition);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute() => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        var nested = ExpressionParameters.CreateScoped(parameters);

        while (true)
        {
            var condition = Condition.Execute(nested);
            if (condition is not bool conditionResult)
                throw ExecutionException.For(this);

            if (!conditionResult)
                break;

            Body.Execute(nested);
        }

        return null!; // TODO:
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
    /// <param name="body">The body of the loop.</param>
    /// <param name="condition">The condition of the loop.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(IExpression? body = null, IExpression? condition = null)
        => new While(body ?? Body, condition ?? Condition);

    /// <summary>
    /// Gets the body of the loop.
    /// </summary>
    public IExpression Body { get; }

    /// <summary>
    /// Gets the condition of the loop.
    /// </summary>
    public IExpression Condition { get; }
}