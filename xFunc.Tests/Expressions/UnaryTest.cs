// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UnaryTest
{
    [Fact]
    public void EqualsTest1()
    {
        var sine1 = new Sin(Number.Two);
        var sine2 = new Sin(Number.Two);

        Assert.Equal(sine1, sine2);
    }

    [Fact]
    public void EqualsTest2()
    {
        var sine = new Sin(Number.Two);
        var ln = new Ln(Number.Two);

        Assert.NotEqual<IExpression>(sine, ln);
    }

    [Fact]
    public void ArgNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Sin(null));
    }
}