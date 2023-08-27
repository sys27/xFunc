// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class NumberTests : BaseParserTests
{
    [Test]
    public void NumberFormatTest()
        => ParseErrorTest<TokenizeException>("0.");

    [Test]
    [TestCase("1.2345E-10", 0.00000000012345)]
    [TestCase("1.2345E10", 12345000000)]
    [TestCase("1.2345E+10", 12345000000)]
    [TestCase("1.2e2", 120)]
    public void ExpNegativeNumber(string exp, double number)
        => ParseTest(exp, new Number(number));

    [Test]
    [TestCase("0b01100110")]
    [TestCase("0B01100110")]
    public void BinTest(string function)
        => ParseTest(function, new Number(0b01100110));

    [Test]
    [TestCase("0XFF00")]
    [TestCase("0xff00")]
    public void HexTest(string function)
        => ParseTest(function, new Number(0xFF00));

    [Test]
    public void OctTest()
        => ParseTest("0436", new Number(286));
}