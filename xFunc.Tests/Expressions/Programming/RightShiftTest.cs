// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Programming;

public class RightShiftTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new RightShift(new Number(512), new Number(9));
        var actual = exp.Execute();
        var expected = new NumberValue(1.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteDoubleLeftTest()
    {
        var exp = new RightShift(new Number(1.5), new Number(10));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteDoubleRightTest()
    {
        var exp = new RightShift(Number.One, new Number(10.1));

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteBoolTest()
        => TestNotSupported(new RightShift(Bool.False, Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new RightShift(Number.One, new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}