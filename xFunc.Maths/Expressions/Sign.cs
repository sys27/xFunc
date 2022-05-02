// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Sign function.
/// </summary>
public class Sign : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sign"/> class.
    /// </summary>
    /// <param name="expression">The argument of the function.</param>
    /// <seealso cref="IExpression"/>
    public Sign(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sign"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Sign(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        var sign = result switch
        {
            NumberValue number => number.Sign,
            AngleValue angle => angle.Sign,
            PowerValue power => power.Sign,
            TemperatureValue temperature => temperature.Sign,
            MassValue mass => mass.Sign,
            LengthValue length => length.Sign,
            TimeValue time => time.Sign,
            AreaValue area => area.Sign,
            VolumeValue volume => volume.Sign,
            _ => throw new ResultIsNotSupportedException(this, result),
        };

        return new NumberValue(sign);
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
        => new Sign(argument ?? Argument);
}