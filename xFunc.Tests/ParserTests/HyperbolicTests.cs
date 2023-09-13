// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class HyperbolicTests : BaseParserTests
{
    [Test]
    [TestCase("sinh(2)")]
    [TestCase("sh(2)")]
    public void SinhTest(string function)
        => ParseTest(function, new Sinh(Number.Two));

    [Test]
    [TestCase("cosh(2)")]
    [TestCase("ch(2)")]
    public void CoshTest(string function)
        => ParseTest(function, new Cosh(Number.Two));

    [Test]
    [TestCase("tanh(2)")]
    [TestCase("th(2)")]
    public void TanhTest(string function)
        => ParseTest(function, new Tanh(Number.Two));

    [Test]
    [TestCase("coth(2)")]
    [TestCase("cth(2)")]
    public void CothTest(string function)
        => ParseTest(function, new Coth(Number.Two));

    [Test]
    public void SechTest()
        => ParseTest("sech(2)", new Sech(Number.Two));

    [Test]
    public void CschTest()
        => ParseTest("csch(2)", new Csch(Number.Two));
}