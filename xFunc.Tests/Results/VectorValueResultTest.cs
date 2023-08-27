// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class VectorValueResultTest
{
    [Test]
    public void ResultTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue);

        Assert.That(result.Result, Is.EqualTo(vectorValue));
    }

    [Test]
    public void IResultTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue) as IResult;

        Assert.That(result.Result, Is.EqualTo(vectorValue));
    }

    [Test]
    public void ToStringTest()
    {
        var vectorValue = VectorValue.Create(NumberValue.One, NumberValue.Two);
        var result = new VectorValueResult(vectorValue);

        Assert.That(result.ToString(), Is.EqualTo("{1, 2}"));
    }
}