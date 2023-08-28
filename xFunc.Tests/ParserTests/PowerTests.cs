// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class PowerTests : BaseParserTests
{
    [Test]
    [TestCase("pow(1, 2)")]
    [TestCase("1 ^ 2")]
    public void PowFuncTest(string function)
        => ParseTest(function, new Pow(Number.One, Number.Two));

    [Test]
    public void PowRightAssociativityTest()
    {
        var expected = new Pow(Number.One, new Pow(Number.Two, new Number(3)));

        ParseTest("1 ^ 2 ^ 3", expected);
    }

    [Test]
    public void PowUnaryMinusTest()
    {
        var expected = new UnaryMinus(new Pow(Number.One, Number.Two));

        ParseTest("-1 ^ 2", expected);
    }

    [Test]
    public void PowerWithUnaryMinus()
    {
        var expected = new Pow(Number.Two, new UnaryMinus(Number.Two));

        ParseTest("2 ^ -2", expected);
    }

    [Test]
    public void ImplicitMulAndPowerFunction()
    {
        var expected = new Mul(
            Number.Two,
            new Pow(new Sin(Variable.X), Number.Two)
        );

        ParseTest("2sin(x) ^ 2", expected);
    }

    [Test]
    public void ImplicitMulAndPowerVariable()
    {
        var expected = new Mul(
            Number.Two,
            new Pow(Variable.X, Number.Two)
        );

        ParseTest("2x^2", expected);
    }

    [Test]
    public void ImplicitNegativeNumberMulAndPowerVariable()
    {
        var expected = new Mul(
            new UnaryMinus(Number.Two),
            new Pow(Variable.X, Number.Two)
        );

        ParseTest("-2x^2", expected);
    }

    [Test]
    [TestCase("2 ^")]
    [TestCase("2x ^")]
    public void PowErrorTest(string exp)
        => ParseErrorTest(exp);
}