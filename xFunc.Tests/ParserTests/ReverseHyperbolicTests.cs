// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ReverseHyperbolicTests : BaseParserTests
{
    [Test]
    [TestCase("arsinh(2)")]
    [TestCase("arsh(2)")]
    public void ArsinhTest(string function)
        => ParseTest(function, new Arsinh(Number.Two));

    [Test]
    [TestCase("arcosh(2)")]
    [TestCase("arch(2)")]
    public void ArcoshTest(string function)
        => ParseTest(function, new Arcosh(Number.Two));

    [Test]
    [TestCase("artanh(2)")]
    [TestCase("arth(2)")]
    public void ArtanhTest(string function)
        => ParseTest(function, new Artanh(Number.Two));

    [Test]
    [TestCase("arcoth(2)")]
    [TestCase("arcth(2)")]
    public void ArcothTest(string function)
        => ParseTest(function, new Arcoth(Number.Two));

    [Test]
    [TestCase("arsech(2)")]
    [TestCase("arsch(2)")]
    public void ArsechTest(string function)
        => ParseTest(function, new Arsech(Number.Two));

    [Test]
    public void ArcschTest()
        => ParseTest("arcsch(2)", new Arcsch(Number.Two));
}