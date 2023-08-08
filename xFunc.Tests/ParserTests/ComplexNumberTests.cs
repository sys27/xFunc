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
        var expected = new Add(
            new Number(3),
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );

        ParseTest(exp, expected);
    }

    [Fact]
    public void ComplexNumberNegativeTest()
    {
        var expected = new Sub(
            new Number(3),
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );

        ParseTest("3-2i", expected);
    }

    [Fact]
    public void ComplexNumberNegativeAllPartsTest()
    {
        var expected = new Sub(
            new UnaryMinus(new Number(3)),
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );

        ParseTest("-3-2i", expected);
    }

    [Fact]
    public void ComplexOnlyRePartTest()
    {
        var expected = new Add(
            new Number(3),
            new Mul(
                Number.Zero,
                new Variable("i")
            )
        );

        ParseTest("3+0i", expected);
    }

    [Fact]
    public void ComplexOnlyImPartTest()
    {
        var expected = new Add(
            Number.Zero,
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );

        ParseTest("0+2i", expected);
    }

    [Fact]
    public void ComplexOnlyImPartNegativeTest()
    {
        var expected = new Sub(
            Number.Zero,
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );

        ParseTest("0-2i", expected);
    }

    [Fact]
    public void ComplexWithVarTest1()
    {
        var expected = new Sub(
            Variable.X,
            new Add(
                Number.Zero,
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest("x - (0+2i)", expected);
    }

    [Fact]
    public void ComplexWithVarTest2()
    {
        var expected = new Add(
            Variable.X,
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
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
        var expected = new Im(
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest(function, expected);
    }

    [Theory]
    [InlineData("re(3-2i)")]
    [InlineData("real(3-2i)")]
    public void ReTest(string function)
    {
        var expected = new Re(
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest(function, expected);
    }

    [Fact]
    public void PhaseTest()
    {
        var expected = new Phase(
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest("phase(3-2i)", expected);
    }

    [Fact]
    public void ConjugateTest()
    {
        var expected = new Conjugate(
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest("conjugate(3-2i)", expected);
    }

    [Fact]
    public void ReciprocalTest()
    {
        var expected = new Reciprocal(
            new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            )
        );

        ParseTest("reciprocal(3-2i)", expected);
    }

    [Fact]
    public void ToComplexTest()
        => ParseTest("tocomplex(2)", new ToComplex(Number.Two));
}