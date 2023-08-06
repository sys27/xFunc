// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.ParserTests;

public class ComplexNumberTests : BaseParserTests
{
    [Theory]
    [InlineData("3+2i")]
    [InlineData("+3+2i")]
    public void ComplexNumberTest(string exp)
    {
        var expected = new ComplexNumber(new Complex(3, 2));

        ParseTest(exp, expected);
    }

    [Fact]
    public void ComplexNumberNegativeTest()
    {
        var expected = new ComplexNumber(new Complex(3, -2));

        ParseTest("3-2i", expected);
    }

    [Fact]
    public void ComplexNumberNegativeAllPartsTest()
    {
        var expected = new ComplexNumber(new Complex(-3, -2));

        ParseTest("-3-2i", expected);
    }

    [Fact]
    public void ComplexOnlyRePartTest()
    {
        var expected = new ComplexNumber(new Complex(3, 0));

        ParseTest("3+0i", expected);
    }

    [Fact]
    public void ComplexOnlyImPartTest()
    {
        var expected = new ComplexNumber(new Complex(0, 2));

        ParseTest("0+2i", expected);
    }

    [Fact]
    public void ComplexOnlyImPartNegativeTest()
    {
        var expected = new ComplexNumber(new Complex(0, -2));

        ParseTest("0-2i", expected);
    }

    [Fact]
    public void ComplexWithVarTest1()
    {
        var expected = new Sub(
            Variable.X,
            new ComplexNumber(new Complex(0, 2))
        );

        ParseTest("x - (0+2i)", expected);
    }

    [Fact]
    public void ComplexWithVarTest2()
    {
        var expected = new Add(
            Variable.X,
            new ComplexNumber(new Complex(3, -2))
        );

        ParseTest("x + (3-2i)", expected);
    }

    [Theory]
    [InlineData("10∠0.78539816339744828°")]
    [InlineData("10∠+0.78539816339744828°")]
    public void ComplexFromPolarTest(string exp)
    {
        var complex = Complex.FromPolarCoordinates(10, 0.78539816339744828 * Math.PI / 180);
        var expected = new ComplexNumber(complex);

        ParseTest(exp, expected);
    }

    [Theory]
    [InlineData("10∠-7.1°")]
    [InlineData("+10∠-7.1°")]
    public void ComplexFromPolarNegativePhaseTest(string exp)
    {
        var complex = Complex.FromPolarCoordinates(10, -7.1 * Math.PI / 180);
        var expected = new ComplexNumber(complex);

        ParseTest(exp, expected);
    }

    [Fact]
    public void ComplexFromPolarNegativeMagnitudeTest()
    {
        var complex = Complex.FromPolarCoordinates(10, 7.1 * Math.PI / 180);
        var expected = new UnaryMinus(new ComplexNumber(complex));

        ParseTest("-10∠7.1°", expected);
    }

    [Theory]
    [InlineData("10∠°")]
    [InlineData("10∠0.78539816339744828")]
    [InlineData("x°")]
    public void ComplexFromPolarMissingPartsTest(string exp)
        => ParseErrorTest(exp);

    [Theory]
    [InlineData("im(3-2i)")]
    [InlineData("imaginary(3-2i)")]
    public void ImTest(string function)
    {
        var expected = new Im(new ComplexNumber(new Complex(3, -2)));

        ParseTest(function, expected);
    }

    [Theory]
    [InlineData("re(3-2i)")]
    [InlineData("real(3-2i)")]
    public void ReTest(string function)
    {
        var expected = new Re(new ComplexNumber(new Complex(3, -2)));

        ParseTest(function, expected);
    }

    [Fact]
    public void PhaseTest()
    {
        var expected = new Phase(new ComplexNumber(new Complex(3, -2)));

        ParseTest("phase(3-2i)", expected);
    }

    [Fact]
    public void ConjugateTest()
    {
        var expected = new Conjugate(new ComplexNumber(new Complex(3, -2)));

        ParseTest("conjugate(3-2i)", expected);
    }

    [Fact]
    public void ReciprocalTest()
    {
        var expected = new Reciprocal(new ComplexNumber(new Complex(3, -2)));

        ParseTest("reciprocal(3-2i)", expected);
    }

    [Fact]
    public void ToComplexTest()
        => ParseTest("tocomplex(2)", new ToComplex(Number.Two));
}