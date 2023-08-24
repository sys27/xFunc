// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToRationalTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest()
    {
        var exp = new ToRational(Number.Two);
        var expected = new RationalValue(2, 1);
        var actual = exp.Execute();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteExceptionTest()
        => TestNotSupported(new ToRational(Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new ToRational(Number.Two);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}