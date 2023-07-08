// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Contains LINQ-like extension methods for <see cref="VectorValue"/> and <see cref="MatrixValue"/>.
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Computes the average of a sequence of numeric values.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The average value.</returns>
    public static NumberValue Average(this VectorValue vector)
        => Sum(vector) / vector.Size;

    /// <summary>
    /// Computes the sum of a sequence of numeric values.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The sum of the values in the vector.</returns>
    public static NumberValue Sum(this VectorValue vector)
    {
        var sum = NumberValue.Zero;

        for (var i = 0; i < vector.Size; i++)
            sum += vector[i];

        return sum;
    }
}