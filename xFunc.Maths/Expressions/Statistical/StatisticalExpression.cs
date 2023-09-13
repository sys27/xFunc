// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Runtime.CompilerServices;

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
    /// <param name="vector">The array of numbers.</param>
    /// <returns>A result of the execution.</returns>
    protected abstract object ExecuteInternal(VectorValue vector);

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var vector = default(VectorValue);

        if (ParametersCount == 1)
        {
            var result = this[0].Execute(parameters);
            if (result is VectorValue vectorValue)
                vector = vectorValue;
        }

        if (vector == default)
        {
            var array = new NumberValue[ParametersCount];
            for (var i = 0; i < array.Length; i++)
            {
                var result = this[i].Execute(parameters);
                if (result is not NumberValue number)
                    throw ExecutionException.For(this);

                array[i] = number;
            }

            vector = Unsafe.As<NumberValue[], VectorValue>(ref array);
        }

        return ExecuteInternal(vector);
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