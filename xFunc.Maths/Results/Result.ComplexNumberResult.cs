// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="Complex"/> to <see cref="ComplexNumberResult"/>.
    /// </summary>
    /// <param name="complexNumber">The complex number.</param>
    /// <returns>The complex number result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(Complex complexNumber)
        => new ComplexNumberResult(complexNumber);

    /// <summary>
    /// Gets the complex number.
    /// </summary>
    /// <param name="complexNumberValue">The complex number.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="Complex"/>, otherwise <c>false</c>.</returns>
    public bool TryGetComplexNumber([NotNullWhen(true)] out Complex? complexNumberValue)
    {
        if (this is ComplexNumberResult complexNumberResult)
        {
            complexNumberValue = complexNumberResult.ComplexNumber;
            return true;
        }

        complexNumberValue = null;
        return false;
    }

    /// <summary>
    /// Gets the complex number value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="ComplexNumberResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual Complex ComplexNumber
        => ((ComplexNumberResult)this).ComplexNumber;

    /// <summary>
    /// Represents the complex number value.
    /// </summary>
    public sealed class ComplexNumberResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.ComplexNumberResult"/> class.
        /// </summary>
        /// <param name="complexNumber">The complex number value.</param>
        public ComplexNumberResult(Complex complexNumber)
            => ComplexNumber = complexNumber;

        /// <summary>
        /// Converts <see cref="Result.ComplexNumberResult"/> to <see cref="Complex"/>.
        /// </summary>
        /// <param name="complexNumberResult">The complex number result.</param>
        /// <returns>The complex number.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator Complex(ComplexNumberResult complexNumberResult)
            => complexNumberResult.ComplexNumber;

        /// <inheritdoc />
        public override string ToString()
            => ComplexNumber.Format();

        /// <summary>
        /// Gets complex number value.
        /// </summary>
        public override Complex ComplexNumber { get; }
    }
}