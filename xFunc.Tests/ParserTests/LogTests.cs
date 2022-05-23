// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class LogTests : BaseParserTests
{
    [Fact]
    public void ParseLog()
        => ParseTest("log(9, 3)", new Log(new Number(9), new Number(3)));

    [Fact]
    public void ParseLogWithOneParam()
        => ParseErrorTest("log(9)");

    [Theory]
    [InlineData("lb(2)")]
    [InlineData("log2(2)")]
    public void LbTest(string function)
        => ParseTest(function, new Lb(Number.Two));

    [Fact]
    public void LgTest()
        => ParseTest("lg(2)", new Lg(Number.Two));

    [Fact]
    public void LnTest()
        => ParseTest("ln(2)", new Ln(Number.Two));
}