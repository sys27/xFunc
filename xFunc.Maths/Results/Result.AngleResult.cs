// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="AngleValue"/> to <see cref="AngleResult"/>.
    /// </summary>
    /// <param name="angle">The angle value.</param>
    /// <returns>The angle result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(AngleValue angle)
        => new AngleResult(angle);

    /// <summary>
    /// Gets the angle value.
    /// </summary>
    /// <param name="angleValue">The angle value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="AngleValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetAngle([NotNullWhen(true)] out AngleValue? angleValue)
    {
        if (this is AngleResult angleResult)
        {
            angleValue = angleResult.Angle;
            return true;
        }

        angleValue = null;
        return false;
    }

    /// <summary>
    /// Gets the angle value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="AngleResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual AngleValue Angle
        => ((AngleResult)this).Angle;

    /// <summary>
    /// Represents the angle result.
    /// </summary>
    public sealed class AngleResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.AngleResult"/> class.
        /// </summary>
        /// <param name="angle">The angle value.</param>
        public AngleResult(AngleValue angle)
            => Angle = angle;

        /// <summary>
        /// Converts <see cref="Result.AngleResult"/> to <see cref="AngleValue"/>.
        /// </summary>
        /// <param name="angleResult">The angle result.</param>
        /// <returns>The angle value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator AngleValue(AngleResult angleResult)
            => angleResult.Angle;

        /// <inheritdoc />
        public override string ToString()
            => Angle.ToString();

        /// <summary>
        /// Gets the angle value.
        /// </summary>
        public override AngleValue Angle { get; }
    }
}