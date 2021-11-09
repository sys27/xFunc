// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the user-defined function.
/// </summary>
public class UserFunction : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserFunction"/> class.
    /// </summary>
    /// <param name="function">The name of function.</param>
    /// <param name="arguments">Arguments.</param>
    public UserFunction(string function, IEnumerable<IExpression> arguments)
        : this(function, arguments.ToImmutableArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserFunction"/> class.
    /// </summary>
    /// <param name="function">The name of function.</param>
    /// <param name="arguments">Arguments.</param>
    public UserFunction(string function, ImmutableArray<IExpression> arguments)
        : base(arguments)
        => Function = function;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is UserFunction exp &&
           Function == exp.Function &&
           ParametersCount == exp.ParametersCount;

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Function, ParametersCount);

    /// <inheritdoc />
    public override string ToString() => ToString(new CommonFormatter());

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        var func = parameters.Functions.GetKey(this);

        var newParams = new ExpressionParameters(parameters);
        for (var i = 0; i < ParametersCount; i++)
            if (func.Arguments[i] is Variable arg)
                newParams.Variables[arg.Name] = new ParameterValue(Arguments[i].Execute(parameters));

        return parameters.Functions[this].Execute(newParams);
    }

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);

    /// <inheritdoc />
    public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
        => new UserFunction(Function, arguments ?? Arguments);

    /// <summary>
    /// Gets the name of function.
    /// </summary>
    public string Function { get; }

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => null;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => null;
}