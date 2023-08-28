// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class TrigonometricTests : BaseParserTests
{
    [Test]
    public void SinTest()
        => ParseTest("sin(2)", new Sin(Number.Two));

    [Test]
    public void CosTest()
        => ParseTest("cos(2)", new Cos(Number.Two));

    [Test]
    [TestCase("tan(2)")]
    [TestCase("tg(2)")]
    public void TanTest(string function)
        => ParseTest(function, new Tan(Number.Two));

    [Test]
    [TestCase("cot(2)")]
    [TestCase("ctg(2)")]
    public void CotTest(string function)
        => ParseTest(function, new Cot(Number.Two));

    [Test]
    public void SecTest()
        => ParseTest("sec(2)", new Sec(Number.Two));

    [Test]
    [TestCase("csc(2)")]
    [TestCase("cosec(2)")]
    public void CscTest(string function)
        => ParseTest(function, new Csc(Number.Two));
}