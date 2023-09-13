// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class EqualityOperatorTests : BaseParserTests
{
    [Test]
    public void EqualTest()
        => ParseTest("x == 0", new Equal(Variable.X, Number.Zero));

    [Test]
    public void NotEqualTest()
        => ParseTest("x != 0", new NotEqual(Variable.X, Number.Zero));

    [Test]
    public void PrecedenceTest()
    {
        var expected = new And(
            new Variable("a"),
            new NotEqual(new Variable("b"), new Variable("c"))
        );

        ParseTest("a & b != c", expected);
    }
}