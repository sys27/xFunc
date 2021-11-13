// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class GCDTests : BaseParserTests
{
    [Theory]
    [InlineData("gcd(12, 16)")]
    [InlineData("gcf(12, 16)")]
    [InlineData("hcf(12, 16)")]
    public void GCDTest(string function)
        => ParseTest(function, new GCD(new Number(12), new Number(16)));

    [Fact]
    public void GCDOfThreeTest()
    {
        var expected = new GCD(new IExpression[] { new Number(12), new Number(16), new Number(8) });

        ParseTest("gcd(12, 16, 8)", expected);
    }

    [Fact]
    public void UnaryMinusAfterCommaTest()
    {
        var expected = new GCD(Number.Two, new UnaryMinus(Variable.X));

        ParseTest("gcd(2, -x)", expected);
    }

    [Theory]
    [InlineData("lcm(12, 16)")]
    [InlineData("scm(12, 16)")]
    public void LCMTest(string function)
        => ParseTest(function, new LCM(new Number(12), new Number(16)));
}