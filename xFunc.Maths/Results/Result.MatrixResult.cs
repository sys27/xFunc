// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="MatrixValue"/> to <see cref="MatrixResult"/>.
    /// </summary>
    /// <param name="matrix">The matrix value.</param>
    /// <returns>The matrix result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(MatrixValue matrix)
        => new MatrixResult(matrix);

    /// <summary>
    /// Gets the matrix value.
    /// </summary>
    /// <param name="matrixValue">The matrix value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="MatrixValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetMatrix([NotNullWhen(true)] out MatrixValue? matrixValue)
    {
        if (this is MatrixResult matrixResult)
        {
            matrixValue = matrixResult.Matrix;
            return true;
        }

        matrixValue = null;
        return false;
    }

    /// <summary>
    /// Gets the matrix value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="MatrixResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual MatrixValue Matrix
        => ((MatrixResult)this).Matrix;

    /// <summary>
    /// Represents the matrix result.
    /// </summary>
    public sealed class MatrixResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.MatrixResult"/> class.
        /// </summary>
        /// <param name="matrix">The matrix value.</param>
        public MatrixResult(MatrixValue matrix)
            => Matrix = matrix;

        /// <summary>
        /// Converts <see cref="Result.MatrixResult"/> to <see cref="MatrixValue"/>.
        /// </summary>
        /// <param name="matrixResult">The matrix result.</param>
        /// <returns>The matrix value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator MatrixValue(MatrixResult matrixResult)
            => matrixResult.Matrix;

        /// <inheritdoc />
        public override string ToString()
            => Matrix.ToString();

        /// <summary>
        /// Gets the matrix value.
        /// </summary>
        public override MatrixValue Matrix { get; }
    }
}