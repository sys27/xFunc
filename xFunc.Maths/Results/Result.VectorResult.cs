// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="VectorValue"/> to <see cref="VectorResult"/>.
    /// </summary>
    /// <param name="vector">The vector value.</param>
    /// <returns>The vector result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(VectorValue vector)
        => new VectorResult(vector);

    /// <summary>
    /// Gets the vector value.
    /// </summary>
    /// <param name="vectorValue">The vector value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="VectorValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetVector([NotNullWhen(true)] out VectorValue? vectorValue)
    {
        if (this is VectorResult vectorResult)
        {
            vectorValue = vectorResult.Vector;
            return true;
        }

        vectorValue = null;
        return false;
    }

    /// <summary>
    /// Gets the vector value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="VectorResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual VectorValue Vector
        => ((VectorResult)this).Vector;

    /// <summary>
    /// Represents the vector result.
    /// </summary>
    public sealed class VectorResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.VectorResult"/> class.
        /// </summary>
        /// <param name="vector">The vector value.</param>
        public VectorResult(VectorValue vector)
            => Vector = vector;

        /// <summary>
        /// Converts <see cref="Result.VectorResult"/> to <see cref="VectorValue"/>.
        /// </summary>
        /// <param name="vectorResult">The vector result.</param>
        /// <returns>The vector value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator VectorValue(VectorResult vectorResult)
            => vectorResult.Vector;

        /// <inheritdoc />
        public override string ToString()
            => Vector.ToString();

        /// <summary>
        /// Gets the vector value.
        /// </summary>
        public override VectorValue Vector { get; }
    }
}