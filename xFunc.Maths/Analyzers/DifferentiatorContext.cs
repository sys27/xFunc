// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The context for differentiator.
/// </summary>
public class DifferentiatorContext
{
    private Variable variable = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public DifferentiatorContext(IExpressionParameters? parameters)
        : this(parameters, Variable.X)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="variable">The variable.</param>
    public DifferentiatorContext(IExpressionParameters? parameters, Variable variable)
    {
        Parameters = parameters;
        Variable = variable;
    }

    /// <summary>
    /// Creates an empty object.
    /// </summary>
    /// <returns>The differentiator context.</returns>
    public static DifferentiatorContext Default()
        => new DifferentiatorContext(new ExpressionParameters());

    /// <summary>
    /// Gets the parameters.
    /// </summary>
    /// <value>
    /// The parameters.
    /// </value>
    public IExpressionParameters? Parameters { get; }

    /// <summary>
    /// Gets or sets the variable.
    /// </summary>
    /// <value>
    /// The variable.
    /// </value>
    public Variable Variable
    {
        get => variable;
        set => variable = value ?? throw new ArgumentNullException(nameof(value));
    }
}