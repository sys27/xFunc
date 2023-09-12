// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the 'frac' function.
/// </summary>
public class Frac : UnaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Frac(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Frac"/> class.
    /// </summary>
    /// <param name="argument">The expression that represents a double-precision floating-point number to be rounded down.</param>
    public Frac(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Frac"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Frac(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => NumberValue.Frac(number),
            AngleValue angle => AngleValue.Frac(angle),
            PowerValue power => PowerValue.Frac(power),
            TemperatureValue temperature => TemperatureValue.Frac(temperature),
            MassValue mass => MassValue.Frac(mass),
            LengthValue length => LengthValue.Frac(length),
            TimeValue time => TimeValue.Frac(time),
            AreaValue area => AreaValue.Frac(area),
            VolumeValue volume => VolumeValue.Frac(volume),
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
        => new Frac(argument ?? Argument);
}