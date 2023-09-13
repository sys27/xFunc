// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the 'tohex' function.
/// </summary>
public class ToHex : UnaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new ToHex(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="ToHex"/> class.
    /// </summary>
    /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded down.</param>
    public ToHex(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToHex"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal ToHex(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => NumberValue.ToHex(number),
            _ => throw ExecutionException.For(this),
        };
    }

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);

    /// <inheritdoc />
    public override IExpression Clone(IExpression? argument = null)
        => new ToHex(argument ?? Argument);
}