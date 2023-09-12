// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="AreaValue"/> to <see cref="AreaResult"/>.
    /// </summary>
    /// <param name="area">The area value.</param>
    /// <returns>The area result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(AreaValue area)
        => new AreaResult(area);

    /// <summary>
    /// Gets the area value.
    /// </summary>
    /// <param name="areaValue">The area value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="AreaValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetArea([NotNullWhen(true)] out AreaValue? areaValue)
    {
        if (this is AreaResult areaResult)
        {
            areaValue = areaResult.Area;
            return true;
        }

        areaValue = null;
        return false;
    }

    /// <summary>
    /// Gets the area value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="AreaResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual AreaValue Area
        => ((AreaResult)this).Area;

    /// <summary>
    /// Represents the area result.
    /// </summary>
    public sealed class AreaResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.AreaResult"/> class.
        /// </summary>
        /// <param name="area">The area value.</param>
        public AreaResult(AreaValue area)
            => Area = area;

        /// <summary>
        /// Converts <see cref="Result.AreaResult"/> to <see cref="AreaValue"/>.
        /// </summary>
        /// <param name="areaResult">The area result.</param>
        /// <returns>The area value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator AreaValue(AreaResult areaResult)
            => areaResult.Area;

        /// <inheritdoc />
        public override string ToString()
            => Area.ToString();

        /// <summary>
        /// Gets the area value.
        /// </summary>
        public override AreaValue Area { get; }
    }
}