// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="MassValue"/> to <see cref="MassResult"/>.
    /// </summary>
    /// <param name="mass">The mass value.</param>
    /// <returns>The mass result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(MassValue mass)
        => new MassResult(mass);

    /// <summary>
    /// Gets the mass value.
    /// </summary>
    /// <param name="massValue">The mass value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="MassValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetMass([NotNullWhen(true)] out MassValue? massValue)
    {
        if (this is MassResult massResult)
        {
            massValue = massResult.Mass;
            return true;
        }

        massValue = null;
        return false;
    }

    /// <summary>
    /// Gets the mass value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="MassResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual MassValue Mass
        => ((MassResult)this).Mass;

    /// <summary>
    /// Represents the mass result.
    /// </summary>
    public sealed class MassResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.MassResult"/> class.
        /// </summary>
        /// <param name="mass">The mass value.</param>
        public MassResult(MassValue mass)
            => Mass = mass;

        /// <summary>
        /// Converts <see cref="Result.MassResult"/> to <see cref="MassValue"/>.
        /// </summary>
        /// <param name="massResult">The mass result.</param>
        /// <returns>The mass value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator MassValue(MassResult massResult)
            => massResult.Mass;

        /// <inheritdoc />
        public override string ToString()
            => Mass.ToString();

        /// <summary>
        /// Gets the mass value.
        /// </summary>
        public override MassValue Mass { get; }
    }
}