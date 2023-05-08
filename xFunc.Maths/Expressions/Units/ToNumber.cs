// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units;

/// <summary>
/// Represents the 'tonumber' function.
/// </summary>
[FunctionName("tonumber")]
public class ToNumber : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToNumber"/> class.
    /// </summary>
    /// <param name="argument">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    public ToNumber(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToNumber"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal ToNumber(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            AngleValue angleValue => angleValue.Angle,
            PowerValue powerValue => powerValue.Value,
            TemperatureValue temperatureValue => temperatureValue.Value,
            MassValue massValue => massValue.Value,
            LengthValue lengthValue => lengthValue.Value,
            TimeValue timeValue => timeValue.Value,
            AreaValue areaValue => areaValue.Value,
            VolumeValue volumeValue => volumeValue.Value,
            _ => throw new ResultIsNotSupportedException(this, result),
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
        => new ToNumber(argument ?? Argument);
}