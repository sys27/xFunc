// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class BinaryTest
{
    [Test]
    public void EqualsTest1()
    {
        var add1 = new Add(Number.Two, new Number(3));
        var add2 = new Add(Number.Two, new Number(3));

        Assert.That(add2, Is.EqualTo(add1));
    }

    [Test]
    public void EqualsTest2()
    {
        var add = new Add(Number.Two, new Number(3)) as IExpression;
        var sub = new Sub(Number.Two, new Number(3)) as IExpression;

        Assert.That(sub, Is.Not.EqualTo(add));
    }

    [Test]
    public void EqualsSameTest()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.That(exp.Equals(exp), Is.True);
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void LeftNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Add(null, Number.One));
    }

    [Test]
    public void RightNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Add(Number.One, null));
    }
}