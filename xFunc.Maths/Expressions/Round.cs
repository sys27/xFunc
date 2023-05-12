// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the "round" function.
/// </summary>
public class Round : DifferentParametersExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(
        new[] { Variable.X.Name, Variable.Y.Name },
        new Round(Variable.X, Variable.Y));

    /// <summary>
    /// Initializes a new instance of the <see cref="Round"/> class.
    /// </summary>
    /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
    public Round(IExpression argument)
        : this(ImmutableArray.Create(argument))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Round"/> class.
    /// </summary>
    /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded.</param>
    /// <param name="digits">The expression that represents the number of fractional digits in the return value.</param>
    public Round(IExpression argument, IExpression digits)
        : this(ImmutableArray.Create(argument, digits))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Round"/> class.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    internal Round(ImmutableArray<IExpression> args)
        : base(args)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);
        var digits = Digits?.Execute(parameters) ?? new NumberValue(0.0);

        return (result, digits) switch
        {
            (NumberValue left, NumberValue right) => NumberValue.Round(left, right),
            (AngleValue left, NumberValue right) => AngleValue.Round(left, right),
            (PowerValue left, NumberValue right) => PowerValue.Round(left, right),
            (TemperatureValue left, NumberValue right) => TemperatureValue.Round(left, right),
            (MassValue left, NumberValue right) => MassValue.Round(left, right),
            (LengthValue left, NumberValue right) => LengthValue.Round(left, right),
            (TimeValue left, NumberValue right) => TimeValue.Round(left, right),
            (AreaValue left, NumberValue right) => AreaValue.Round(left, right),
            (VolumeValue left, NumberValue right) => VolumeValue.Round(left, right),
            _ => throw new ResultIsNotSupportedException(this, result, digits),
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
    public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
        => new Round(arguments ?? Arguments);

    /// <summary>
    /// Gets the expression that represents a double-precision floating-point number to be rounded.
    /// </summary>
    public IExpression Argument => this[0];

    /// <summary>
    /// Gets the expression that represents the number of fractional digits in the return value.
    /// </summary>
    public IExpression? Digits => ParametersCount == 2 ? this[1] : null;

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 1;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => 2;
}