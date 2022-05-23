// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class EqualityTest
{
    [Fact]
    public void ExecuteTest1()
    {
        var eq = new Equality(Bool.True, Bool.True);

        Assert.True((bool) eq.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var eq = new Equality(Bool.True, Bool.False);

        Assert.False((bool) eq.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
    {
        var eq = new Equality(Number.One, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => eq.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Equality(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}