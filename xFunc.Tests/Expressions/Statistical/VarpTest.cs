// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class VarpTest
{
    [Fact]
    public void OneNumberTest()
    {
        var exp = new Varp(new[] { new Number(4) });
        var result = exp.Execute();

        Assert.Equal(new NumberValue(0.0), result);
    }

    [Fact]
    public void TwoNumberTest()
    {
        var exp = new Varp(new[] { new Number(4), new Number(9) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(6.25);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ThreeNumberTest()
    {
        var exp = new Varp(new[] { new Number(9), Number.Two, new Number(4) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(8.66666666666667);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void VectorTest()
    {
        var exp = new Varp(new[] { new Vector(new[] { Number.Two, new Number(4), new Number(9) }) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(8.66666666666667);

        Assert.Equal(expected, result);
    }
}