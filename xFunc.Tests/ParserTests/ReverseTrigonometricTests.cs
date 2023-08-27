// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class ReverseTrigonometricTests : BaseParserTests
{
    [Test]
    public void ArcsinTest()
        => ParseTest("arcsin(2)", new Arcsin(Number.Two));

    [Test]
    public void ArccosTest()
        => ParseTest("arccos(2)", new Arccos(Number.Two));

    [Test]
    [TestCase("arctan(2)")]
    [TestCase("arctg(2)")]
    public void ArctanTest(string function)
        => ParseTest(function, new Arctan(Number.Two));

    [Test]
    [TestCase("arccot(2)")]
    [TestCase("arcctg(2)")]
    public void ArccotTest(string function)
        => ParseTest(function, new Arccot(Number.Two));

    [Test]
    public void ArcsecTest()
        => ParseTest("arcsec(2)", new Arcsec(Number.Two));

    [Test]
    [TestCase("arccsc(2)")]
    [TestCase("arccosec(2)")]
    public void ArccscTest(string function)
        => ParseTest(function, new Arccsc(Number.Two));
}