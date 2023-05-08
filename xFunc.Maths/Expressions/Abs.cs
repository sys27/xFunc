// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Absolute operator.
/// </summary>
[FunctionName("abs")]
public class Abs : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Abs"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    public Abs(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Abs"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Abs(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => NumberValue.Abs(number),
            AngleValue angle => AngleValue.Abs(angle),
            PowerValue power => PowerValue.Abs(power),
            TemperatureValue temperature => TemperatureValue.Abs(temperature),
            MassValue mass => MassValue.Abs(mass),
            LengthValue length => LengthValue.Abs(length),
            TimeValue time => TimeValue.Abs(time),
            AreaValue area => AreaValue.Abs(area),
            VolumeValue volume => VolumeValue.Abs(volume),
            Complex complex => Complex.Abs(complex),
            Vector vector => vector.Abs(parameters),
            _ => throw new ResultIsNotSupportedException(this, result),
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
        => new Abs(argument ?? Argument);
}