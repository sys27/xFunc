// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Statistical;

public class StdevTest
{
    [Fact]
    public void OneNumberTest()
    {
        var exp = new Stdev(new[] { new Number(4) });
        var result = (NumberValue)exp.Execute();

        Assert.True(result.IsNaN);
    }

    [Fact]
    public void TwoNumberTest()
    {
        var exp = new Stdev(new[] { new Number(4), new Number(9) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.53553390593274);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ThreeNumberTest()
    {
        var exp = new Stdev(new[] { new Number(9), Number.Two, new Number(4) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.60555127546399);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void VectorTest()
    {
        var exp = new Stdev(new[] { new Vector(new[] { Number.Two, new Number(4), new Number(9) }) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.60555127546399);

        Assert.Equal(expected, result);
    }
}