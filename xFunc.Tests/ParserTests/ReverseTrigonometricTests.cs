// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ReverseTrigonometricTests : BaseParserTests
{
    [Fact]
    public void ArcsinTest()
        => ParseTest("arcsin(2)", new Arcsin(Number.Two));

    [Fact]
    public void ArccosTest()
        => ParseTest("arccos(2)", new Arccos(Number.Two));

    [Theory]
    [InlineData("arctan(2)")]
    [InlineData("arctg(2)")]
    public void ArctanTest(string function)
        => ParseTest(function, new Arctan(Number.Two));

    [Theory]
    [InlineData("arccot(2)")]
    [InlineData("arcctg(2)")]
    public void ArccotTest(string function)
        => ParseTest(function, new Arccot(Number.Two));

    [Fact]
    public void ArcsecTest()
        => ParseTest("arcsec(2)", new Arcsec(Number.Two));

    [Theory]
    [InlineData("arccsc(2)")]
    [InlineData("arccosec(2)")]
    public void ArccscTest(string function)
        => ParseTest(function, new Arccsc(Number.Two));
}