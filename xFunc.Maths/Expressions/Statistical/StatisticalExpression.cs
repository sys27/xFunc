// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represent the Avg function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
public abstract class StatisticalExpression : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticalExpression"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    protected StatisticalExpression(IEnumerable<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticalExpression"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    protected StatisticalExpression(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="numbers">The array of expressions.</param>
    /// <returns>
    /// A result of the execution.
    /// </returns>
    protected abstract double ExecuteInternal(double[] numbers);

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var (data, size) = (Arguments, ParametersCount);

        if (ParametersCount == 1)
        {
            var result = this[0].Execute(parameters);
            if (result is Vector vector)
                (data, size) = (vector.Arguments, vector.ParametersCount);
        }

        var calculated = new double[size];
        for (var i = 0; i < data.Length; i++)
        {
            var result = data[i].Execute(parameters);
            if (!(result is NumberValue number))
                throw new ResultIsNotSupportedException(this, result);

            calculated[i] = number.Number;
        }

        return new NumberValue(ExecuteInternal(calculated));
    }

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    /// <value>
    /// The minimum count of parameters.
    /// </value>
    public override int? MinParametersCount => 1;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    /// <value>
    /// The maximum count of parameters.
    /// </value>
    public override int? MaxParametersCount => null;
}