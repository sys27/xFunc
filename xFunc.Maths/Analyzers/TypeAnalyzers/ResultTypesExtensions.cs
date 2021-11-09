// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Extensions method for the <see cref="ResultTypes"/> enum.
/// </summary>
internal static class ResultTypesExtensions
{
    /// <summary>
    /// Helper method for throw the <see cref="ParameterTypeMismatchException"/> exception.
    /// </summary>
    /// <param name="expected">The expected result type.</param>
    /// <param name="actual">The actual result type.</param>
    /// <returns>Always throws <see cref="ParameterTypeMismatchException"/>.</returns>
    [DoesNotReturn]
    internal static ResultTypes ThrowFor(this ResultTypes expected, ResultTypes actual) =>
        throw new ParameterTypeMismatchException(expected, actual);

    /// <summary>
    /// Helper method for throw the <see cref="BinaryParameterTypeMismatchException"/> exception.
    /// </summary>
    /// <param name="expected">The expected result type.</param>
    /// <param name="actual">The actual result type.</param>
    /// <returns>Always throws <see cref="BinaryParameterTypeMismatchException"/>.</returns>
    [DoesNotReturn]
    internal static ResultTypes ThrowForLeft(this ResultTypes expected, ResultTypes actual) =>
        throw new BinaryParameterTypeMismatchException(expected, actual, BinaryParameterType.Left);

    /// <summary>
    /// Helper method for throw the <see cref="BinaryParameterTypeMismatchException"/> exception.
    /// </summary>
    /// <param name="expected">The expected result type.</param>
    /// <param name="actual">The actual result type.</param>
    /// <returns>Always throws <see cref="BinaryParameterTypeMismatchException"/>.</returns>
    [DoesNotReturn]
    internal static ResultTypes ThrowForRight(this ResultTypes expected, ResultTypes actual) =>
        throw new BinaryParameterTypeMismatchException(expected, actual, BinaryParameterType.Right);
}