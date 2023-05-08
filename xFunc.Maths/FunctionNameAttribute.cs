// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths;

/// <summary>
/// Specifies the name of the function to be generated in the factory method.
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
internal sealed class FunctionNameAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionNameAttribute"/> class.
    /// </summary>
    /// <param name="name">The name of the function.</param>
    public FunctionNameAttribute(string name)
        => Name = name ?? throw new ArgumentNullException(nameof(name));

    /// <summary>
    /// Gets the name of the function.
    /// </summary>
    public string Name { get; }
}