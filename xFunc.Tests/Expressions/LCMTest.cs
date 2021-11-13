// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class LCMTest : BaseExpressionTests
{
    [Fact]
    public void NullArgTest()
        => Assert.Throws<ArgumentNullException>(() => new LCM(null));

    [Fact]
    public void ExecuteTest1()
    {
        var exp = new LCM(new Number(12), new Number(16));
        var expected = new NumberValue(48.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) });
        var expected = new NumberValue(16.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteNotSupportedTest()
        => TestNotSupported(new LCM(new IExpression[] { Bool.False, Bool.True }));

    [Fact]
    public void CloneTest()
    {
        var exp = new LCM(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}