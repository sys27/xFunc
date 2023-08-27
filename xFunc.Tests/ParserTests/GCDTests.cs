// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.ParserTests;

public class GCDTests : BaseParserTests
{
    [Test]
    public void CtorNullTest()
        => Assert.Throws<ArgumentNullException>(() => new GCD(new ImmutableArray<IExpression>()));

    [Test]
    [TestCase("gcd(12, 16)")]
    [TestCase("gcf(12, 16)")]
    [TestCase("hcf(12, 16)")]
    public void GCDTest(string function)
        => ParseTest(function, new GCD(new Number(12), new Number(16)));

    [Test]
    public void GCDOfThreeTest()
    {
        var expected = new GCD(new IExpression[] { new Number(12), new Number(16), new Number(8) });

        ParseTest("gcd(12, 16, 8)", expected);
    }

    [Test]
    public void UnaryMinusAfterCommaTest()
    {
        var expected = new GCD(Number.Two, new UnaryMinus(Variable.X));

        ParseTest("gcd(2, -x)", expected);
    }

    [Test]
    [TestCase("lcm(12, 16)")]
    [TestCase("scm(12, 16)")]
    public void LCMTest(string function)
        => ParseTest(function, new LCM(new Number(12), new Number(16)));
}