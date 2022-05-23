// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Results;

public class ComplexNumberResultTest
{
    [Fact]
    public void ResultTest()
    {
        var result = new ComplexNumberResult(new Complex(2.1, 4.7));

        Assert.Equal(new Complex(2.1, 4.7), result.Result);
    }

    [Fact]
    public void IResultTest()
    {
        var result = new ComplexNumberResult(new Complex(2.1, 4.7)) as IResult;

        Assert.Equal(new Complex(2.1, 4.7), result.Result);
    }

    [Fact]
    public void ZeroImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 0));

        Assert.Equal("5.3", token.ToString());
    }

    [Fact]
    public void PositiveImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.Equal("5.3+2.12i", token.ToString());
    }

    [Fact]
    public void NegativeImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, -2.12));

        Assert.Equal("5.3-2.12i", token.ToString());
    }

    [Fact]
    public void ZeroReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, 1.3));

        Assert.Equal("1.3i", token.ToString());
    }

    [Fact]
    public void PositiveReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.Equal("5.3+2.12i", token.ToString());
    }

    [Fact]
    public void NegativeReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(-5.3, -2.12));

        Assert.Equal("-5.3-2.12i", token.ToString());
    }

    [Fact]
    public void ImOneToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, 1));

        Assert.Equal("i", token.ToString());
    }

    [Fact]
    public void ImNegOneToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, -1));

        Assert.Equal("-i", token.ToString());
    }
}