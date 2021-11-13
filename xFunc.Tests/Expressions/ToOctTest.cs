// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToOctTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new ToOct(Number.Two);

        Assert.Equal("02", exp.Execute());
    }

    [Fact]
    public void ExecuteNumberExceptionTest()
    {
        var exp = new ToOct(new Number(2.5));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteLongMaxNumberTest()
    {
        var exp = new ToOct(new Number(int.MaxValue));

        Assert.Equal("017777777777", exp.Execute());
    }

    [Fact]
    public void ExecuteNegativeNumberTest()
    {
        var exp = new ToOct(new Number(-2));

        Assert.Equal("037777777776", exp.Execute());
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new ToOct(Bool.False));

    [Fact]
    public void CloseTest()
    {
        var exp = new ToOct(new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}