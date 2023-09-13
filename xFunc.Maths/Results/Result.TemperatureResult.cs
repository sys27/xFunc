// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="TemperatureValue"/> to <see cref="TemperatureResult"/>.
    /// </summary>
    /// <param name="temperature">The temperature value.</param>
    /// <returns>The temperature result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(TemperatureValue temperature)
        => new TemperatureResult(temperature);

    /// <summary>
    /// Gets the temperature value.
    /// </summary>
    /// <param name="temperatureValue">The temperature value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="TemperatureValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetTemperature([NotNullWhen(true)] out TemperatureValue? temperatureValue)
    {
        if (this is TemperatureResult temperatureResult)
        {
            temperatureValue = temperatureResult.Temperature;
            return true;
        }

        temperatureValue = null;
        return false;
    }

    /// <summary>
    /// Gets the temperature value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="TemperatureResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual TemperatureValue Temperature
        => ((TemperatureResult)this).Temperature;

    /// <summary>
    /// Represents the temperature result.
    /// </summary>
    public sealed class TemperatureResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.TemperatureResult"/> class.
        /// </summary>
        /// <param name="temperature">The temperature value.</param>
        public TemperatureResult(TemperatureValue temperature)
            => Temperature = temperature;

        /// <summary>
        /// Converts <see cref="Result.TemperatureResult"/> to <see cref="TemperatureValue"/>.
        /// </summary>
        /// <param name="temperatureResult">The temperature result.</param>
        /// <returns>The temperature value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator TemperatureValue(TemperatureResult temperatureResult)
            => temperatureResult.Temperature;

        /// <inheritdoc />
        public override string ToString()
            => Temperature.ToString();

        /// <summary>
        /// Gets the temperature value.
        /// </summary>
        public override TemperatureValue Temperature { get; }
    }
}