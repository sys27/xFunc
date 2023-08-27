// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class UnaryTest
{
    [Test]
    public void EqualsTest1()
    {
        var sine1 = new Sin(Number.Two);
        var sine2 = new Sin(Number.Two);

        Assert.That(sine2, Is.EqualTo(sine1));
    }

    [Test]
    public void EqualsTest2()
    {
        var sine = new Sin(Number.Two) as IExpression;
        var ln = new Ln(Number.Two) as IExpression;

        Assert.That(ln, Is.Not.EqualTo(sine));
    }

    [Test]
    public void ArgNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Sin(null));
    }
}