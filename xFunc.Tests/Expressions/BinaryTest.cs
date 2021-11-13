// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class BinaryTest
{
    [Fact]
    public void EqualsTest1()
    {
        var add1 = new Add(Number.Two, new Number(3));
        var add2 = new Add(Number.Two, new Number(3));

        Assert.Equal(add1, add2);
    }

    [Fact]
    public void EqualsTest2()
    {
        var add = new Add(Number.Two, new Number(3));
        var sub = new Sub(Number.Two, new Number(3));

        Assert.NotEqual<IExpression>(add, sub);
    }

    [Fact]
    public void EqualsSameTest()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsNullTest()
    {
        var exp = new Add(Number.One, Number.One);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void LeftNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Add(null, Number.One));
    }

    [Fact]
    public void RightNullExceptionTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Add(Number.One, null));
    }
}