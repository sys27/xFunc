// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Maths;

/// <summary>
/// Trigonometric functions for Complex numbers.
/// </summary>
internal static class ComplexExtensions
{
    /// <summary>
    /// Returns the cotangent of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Cotangent of complex number.</returns>
    public static Complex Cot(Complex number)
        => Complex.Cos(number) / Complex.Sin(number);

    /// <summary>
    /// Returns the secant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Secant of complex number.</returns>
    public static Complex Sec(Complex number)
        => Complex.One / Complex.Cos(number);

    /// <summary>
    /// Returns the cosecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Cosecant of complex number.</returns>
    public static Complex Csc(Complex number)
        => Complex.One / Complex.Sin(number);

    /// <summary>
    /// Returns the arccotangent of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Arccotangent of complex number.</returns>
    public static Complex Acot(Complex number)
    {
        return new Complex(0.0, 0.5) * (Complex.Log(1 - Complex.ImaginaryOne / number) - Complex.Log(1 + Complex.ImaginaryOne / number));
    }

    /// <summary>
    /// Returns the arcosecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Arcosecant of complex number.</returns>
    public static Complex Asec(Complex number)
    {
        return Complex.ImaginaryOne * Complex.Log(Complex.Sqrt(1 / Complex.Pow(number, 2) - 1) + 1 / number);
    }

    /// <summary>
    /// Returns the arcosecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Arcosecant of complex number.</returns>
    public static Complex Acsc(Complex number)
    {
        return -Complex.ImaginaryOne * Complex.Log(Complex.Sqrt(1 - 1 / Complex.Pow(number, 2)) + Complex.ImaginaryOne / number);
    }

    /// <summary>
    /// Returns the hyperbolic cotangent of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic cotangent of complex number.</returns>
    public static Complex Coth(Complex number)
        => 1 / Complex.Tanh(number);

    /// <summary>
    /// Returns the hyperbolic secant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic secant of complex number.</returns>
    public static Complex Sech(Complex number)
        => Complex.One / Complex.Cosh(number);

    /// <summary>
    /// Returns the hyperbolic cosecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic cosecant of complex number.</returns>
    public static Complex Csch(Complex number)
        => Complex.One / Complex.Sinh(number);

    /// <summary>
    /// Returns the hyperbolic arsine of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arsine of complex number.</returns>
    public static Complex Asinh(Complex number)
        => Complex.Log(number + Complex.Sqrt(Complex.Pow(number, 2) + 1));

    /// <summary>
    /// Returns the hyperbolic arcosine of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arcosine of complex number.</returns>
    public static Complex Acosh(Complex number)
    {
        return Complex.Log(number + Complex.Sqrt(number + Complex.One) * Complex.Sqrt(number - Complex.One));
    }

    /// <summary>
    /// Returns the hyperbolic artangent of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arctangent of complex number.</returns>
    public static Complex Atanh(Complex number)
        => 0.5 * Complex.Log((1 + number) / (1 - number));

    /// <summary>
    /// Returns the hyperbolic arcotangent of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arcoctangent of complex number.</returns>
    public static Complex Acoth(Complex number)
        => 0.5 * Complex.Log((number + 1) / (number - 1));

    /// <summary>
    /// Returns the hyperbolic arsecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arsecant of complex number.</returns>
    public static Complex Asech(Complex number)
        => Complex.Log(1 / number + Complex.Sqrt(1 / number + 1) * Complex.Sqrt(1 / number - 1));

    /// <summary>
    /// Returns the hyperbolic arcosecant of the specified complex number.
    /// </summary>
    /// <param name="number">Complex number.</param>
    /// <returns>Hyperbolic arcosecant of complex number.</returns>
    public static Complex Acsch(Complex number)
        => Complex.Log(1 / number + Complex.Sqrt(1 / Complex.Pow(number, 2) + 1));
}