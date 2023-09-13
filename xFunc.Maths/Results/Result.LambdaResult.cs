// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="Lambda"/> to <see cref="LambdaResult"/>.
    /// </summary>
    /// <param name="lambda">The lambda.</param>
    /// <returns>The lambda result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(Lambda lambda)
        => new LambdaResult(lambda);

    /// <summary>
    /// Gets the lambda.
    /// </summary>
    /// <param name="lambda">The lambda.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="Lambda"/>, otherwise <c>false</c>.</returns>
    public bool TryGetLambda([NotNullWhen(true)] out Lambda? lambda)
    {
        if (this is LambdaResult lambdaResult)
        {
            lambda = lambdaResult.Lambda;
            return true;
        }

        lambda = null;
        return false;
    }

    /// <summary>
    /// Gets the lambda value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="LambdaResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual Lambda Lambda
        => ((LambdaResult)this).Lambda;

    /// <summary>
    /// Represents the lambda result.
    /// </summary>
    public sealed class LambdaResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.LambdaResult"/> class.
        /// </summary>
        /// <param name="lambda">The lambda value.</param>
        public LambdaResult(Lambda lambda)
            => Lambda = lambda;

        /// <summary>
        /// Converts <see cref="Result.LambdaResult"/> to <see cref="Lambda"/>.
        /// </summary>
        /// <param name="lambdaResult">The lambda result.</param>
        /// <returns>The lambda.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator Lambda(LambdaResult lambdaResult)
            => lambdaResult.Lambda;

        /// <inheritdoc />
        public override string ToString()
            => Lambda.ToString();

        /// <summary>
        /// Gets the lambda value.
        /// </summary>
        public override Lambda Lambda { get; }
    }
}