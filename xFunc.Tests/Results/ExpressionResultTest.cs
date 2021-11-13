// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class ExpressionResultTest
{
    [Fact]
    public void ResultTest()
    {
        var exp = new Sin(Variable.X);
        var result = new ExpressionResult(exp);

        Assert.Equal(exp, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var exp = new Sin(Variable.X);
        var result = new ExpressionResult(exp) as IResult;

        Assert.Equal(exp, result.Result);
    }

    [Fact]
    public void NullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new ExpressionResult(null));
    }

    [Fact]
    public void ToStringTest()
    {
        var exp = new Sin(Variable.X);
        var result = new ExpressionResult(exp);

        Assert.Equal("sin(x)", result.ToString());
    }
}