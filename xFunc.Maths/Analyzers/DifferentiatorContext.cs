// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers;

/// <summary>
/// The context for differentiator.
/// </summary>
public class DifferentiatorContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
    /// </summary>
    public DifferentiatorContext()
        : this(Variable.X)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
    /// </summary>
    /// <param name="variable">The variable.</param>
    public DifferentiatorContext(Variable variable)
        => Variable = variable ?? throw new ArgumentNullException(nameof(variable));

    /// <summary>
    /// Gets the variable.
    /// </summary>
    public Variable Variable { get; }
}