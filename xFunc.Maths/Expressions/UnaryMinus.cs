// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the unary minus.
/// </summary>
public class UnaryMinus : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryMinus"/> class.
    /// </summary>
    /// <param name="expression">The expression.</param>
    public UnaryMinus(IExpression expression)
        : base(expression)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => -number,
            AngleValue angle => -angle,
            PowerValue power => -power,
            TemperatureValue temperature => -temperature,
            MassValue mass => -mass,
            LengthValue length => -length,
            TimeValue time => -time,
            AreaValue area => -area,
            VolumeValue volume => -volume,
            Complex complex => Complex.Negate(complex),
            RationalValue rationalValue => -rationalValue,
            _ => throw ExecutionException.For(this),
        };
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
    public override IExpression Clone(IExpression? argument = null)
        => new UnaryMinus(argument ?? Argument);
}