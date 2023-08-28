// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Results;

public class ComplexNumberResultTest
{
    [Test]
    public void ResultTest()
    {
        var result = new ComplexNumberResult(new Complex(2.1, 4.7));

        Assert.That(result.Result, Is.EqualTo(new Complex(2.1, 4.7)));
    }

    [Test]
    public void IResultTest()
    {
        var result = new ComplexNumberResult(new Complex(2.1, 4.7)) as IResult;

        Assert.That(result.Result, Is.EqualTo(new Complex(2.1, 4.7)));
    }

    [Test]
    public void ZeroImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 0));

        Assert.That(token.ToString(), Is.EqualTo("5.3"));
    }

    [Test]
    public void PositiveImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3+2.12i"));
    }

    [Test]
    public void NegativeImToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, -2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3-2.12i"));
    }

    [Test]
    public void ZeroReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, 1.3));

        Assert.That(token.ToString(), Is.EqualTo("1.3i"));
    }

    [Test]
    public void PositiveReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3+2.12i"));
    }

    [Test]
    public void NegativeReToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(-5.3, -2.12));

        Assert.That(token.ToString(), Is.EqualTo("-5.3-2.12i"));
    }

    [Test]
    public void ImOneToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, 1));

        Assert.That(token.ToString(), Is.EqualTo("i"));
    }

    [Test]
    public void ImNegOneToStringTest()
    {
        var token = new ComplexNumberResult(new Complex(0, -1));

        Assert.That(token.ToString(), Is.EqualTo("-i"));
    }
}