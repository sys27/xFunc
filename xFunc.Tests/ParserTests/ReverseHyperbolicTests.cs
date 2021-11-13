// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ReverseHyperbolicTests : BaseParserTests
{
    [Theory]
    [InlineData("arsinh(2)")]
    [InlineData("arsh(2)")]
    public void ArsinhTest(string function)
        => ParseTest(function, new Arsinh(Number.Two));

    [Theory]
    [InlineData("arcosh(2)")]
    [InlineData("arch(2)")]
    public void ArcoshTest(string function)
        => ParseTest(function, new Arcosh(Number.Two));

    [Theory]
    [InlineData("artanh(2)")]
    [InlineData("arth(2)")]
    public void ArtanhTest(string function)
        => ParseTest(function, new Artanh(Number.Two));

    [Theory]
    [InlineData("arcoth(2)")]
    [InlineData("arcth(2)")]
    public void ArcothTest(string function)
        => ParseTest(function, new Arcoth(Number.Two));

    [Theory]
    [InlineData("arsech(2)")]
    [InlineData("arsch(2)")]
    public void ArsechTest(string function)
        => ParseTest(function, new Arsech(Number.Two));

    [Fact]
    public void ArcschTest()
        => ParseTest("arcsch(2)", new Arcsch(Number.Two));
}