// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.LogicalAndBitwise;

public class ImplicationTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var impl = new Implication(Bool.True, Bool.False);

        Assert.False((bool) impl.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var impl = new Implication(Bool.True, Bool.True);

        Assert.True((bool) impl.Execute());
    }

    [Fact]
    public void ExecuteResultIsNotSupported()
        => TestNotSupported(new Implication(Number.One, Number.Two));

    [Fact]
    public void CloneTest()
    {
        var exp = new Implication(Bool.True, Bool.False);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}