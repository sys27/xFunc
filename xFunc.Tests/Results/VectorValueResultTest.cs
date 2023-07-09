// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VectorValueResultTest
{
    [Fact]
    public void ResultTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue);

        Assert.Equal(vectorValue, result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue) as IResult;

        Assert.Equal(vectorValue, result.Result);
    }

    [Fact]
    public void ToStringTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue);

        Assert.Equal("{1, 2}", result.ToString());
    }
}