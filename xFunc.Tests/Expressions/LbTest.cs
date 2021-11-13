// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class LbTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new Lb(Number.Two);
        var expected = new NumberValue(Math.Log(2, 2));

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Lb(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Lb(new Number(5));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}