// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the "ceil" function.
/// </summary>
[FunctionName("ceil")]
public class Ceil : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Ceil" /> class.
    /// </summary>
    /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded up.</param>
    public Ceil(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ceil"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Ceil(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => NumberValue.Ceiling(number),
            AngleValue angle => AngleValue.Ceiling(angle),
            PowerValue power => PowerValue.Ceiling(power),
            TemperatureValue temperature => TemperatureValue.Ceiling(temperature),
            MassValue mass => MassValue.Ceiling(mass),
            LengthValue length => LengthValue.Ceiling(length),
            TimeValue time => TimeValue.Ceiling(time),
            AreaValue area => AreaValue.Ceiling(area),
            VolumeValue volume => VolumeValue.Ceiling(volume),
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
        => new Ceil(argument ?? Argument);
}