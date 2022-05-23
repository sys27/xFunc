// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class NOrTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var nor = new NOr(Bool.False, Bool.True);

        Assert.False((bool) nor.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var nor = new NOr(Bool.False, Bool.False);

        Assert.True((bool) nor.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new NOr(Number.One, Number.Two));

    [Fact]
    public void CloneTest()
    {
        var exp = new NOr(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}