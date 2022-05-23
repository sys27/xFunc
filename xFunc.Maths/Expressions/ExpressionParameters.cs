// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Combines all parameters of expressions.
/// </summary>
public class ExpressionParameters
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    public ExpressionParameters()
        : this(new ParameterCollection(), new FunctionCollection())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="parameters">The collection of variables' values.</param>
    public ExpressionParameters(ParameterCollection parameters)
        : this(parameters, new FunctionCollection())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="functions">The collection of user functions.</param>
    public ExpressionParameters(FunctionCollection functions)
        : this(new ParameterCollection(), functions)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="parameters">Expression parameters.</param>
    public ExpressionParameters(ExpressionParameters parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        Variables = new ParameterCollection(parameters.Variables);
        Functions = parameters.Functions;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="variables">The collection of variables' values.</param>
    /// <param name="functions">The collection of user functions.</param>
    public ExpressionParameters(ParameterCollection variables, FunctionCollection functions)
    {
        Variables = variables;
        Functions = functions;
    }

    /// <summary>
    /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="ParameterCollection"/>.
    /// </summary>
    /// <param name="parameters">The collection of variables' values.</param>
    /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
    public static implicit operator ExpressionParameters(ParameterCollection parameters)
        => new ExpressionParameters(parameters);

    /// <summary>
    /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="FunctionCollection"/>.
    /// </summary>
    /// <param name="functions">The collection of user functions.</param>
    /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
    public static implicit operator ExpressionParameters(FunctionCollection functions)
        => new ExpressionParameters(functions);

    /// <summary>
    /// Gets the collection of variables' values.
    /// </summary>
    public ParameterCollection Variables { get; }

    /// <summary>
    /// Gets the collection of user functions.
    /// </summary>
    public FunctionCollection Functions { get; }
}