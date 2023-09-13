// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Results;

public class ComplexNumberResultTest
{
    [Test]
    public void TryGetComplexTest()
    {
        var expected = Complex.ImaginaryOne;
        var areaResult = new Result.ComplexNumberResult(expected);
        var result = areaResult.TryGetComplexNumber(out var complex);

        Assert.That(result, Is.True);
        Assert.That(complex, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetComplexFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetComplexNumber(out var complex);

        Assert.That(result, Is.False);
        Assert.That(complex, Is.Null);
    }

    [Test]
    public void ZeroImToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(5.3, 0));

        Assert.That(token.ToString(), Is.EqualTo("5.3"));
    }

    [Test]
    public void PositiveImToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3+2.12i"));
    }

    [Test]
    public void NegativeImToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(5.3, -2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3-2.12i"));
    }

    [Test]
    public void ZeroReToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(0, 1.3));

        Assert.That(token.ToString(), Is.EqualTo("1.3i"));
    }

    [Test]
    public void PositiveReToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(5.3, 2.12));

        Assert.That(token.ToString(), Is.EqualTo("5.3+2.12i"));
    }

    [Test]
    public void NegativeReToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(-5.3, -2.12));

        Assert.That(token.ToString(), Is.EqualTo("-5.3-2.12i"));
    }

    [Test]
    public void ImOneToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(0, 1));

        Assert.That(token.ToString(), Is.EqualTo("i"));
    }

    [Test]
    public void ImNegOneToStringTest()
    {
        var token = new Result.ComplexNumberResult(new Complex(0, -1));

        Assert.That(token.ToString(), Is.EqualTo("-i"));
    }
}