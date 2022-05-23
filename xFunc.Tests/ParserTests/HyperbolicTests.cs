// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class HyperbolicTests : BaseParserTests
{
    [Theory]
    [InlineData("sinh(2)")]
    [InlineData("sh(2)")]
    public void SinhTest(string function)
        => ParseTest(function, new Sinh(Number.Two));

    [Theory]
    [InlineData("cosh(2)")]
    [InlineData("ch(2)")]
    public void CoshTest(string function)
        => ParseTest(function, new Cosh(Number.Two));

    [Theory]
    [InlineData("tanh(2)")]
    [InlineData("th(2)")]
    public void TanhTest(string function)
        => ParseTest(function, new Tanh(Number.Two));

    [Theory]
    [InlineData("coth(2)")]
    [InlineData("cth(2)")]
    public void CothTest(string function)
        => ParseTest(function, new Coth(Number.Two));

    [Fact]
    public void SechTest()
        => ParseTest("sech(2)", new Sech(Number.Two));

    [Fact]
    public void CschTest()
        => ParseTest("csch(2)", new Csch(Number.Two));
}