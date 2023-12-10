// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Tests.Expressions.Statistical;

public class StdevTest
{
    [Test]
    public void OneNumberTest()
    {
        var exp = new Stdev(new[] { new Number(4) });
        var result = (Complex)exp.Execute();

        Assert.That(Complex.IsNaN(result));
    }

    [Test]
    public void TwoNumberTest()
    {
        var exp = new Stdev(new[] { new Number(4), new Number(9) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.53553390593274);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ThreeNumberTest()
    {
        var exp = new Stdev(new[] { new Number(9), Number.Two, new Number(4) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.60555127546399);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void VectorTest()
    {
        var exp = new Stdev(new[] { new Vector(new IExpression[] { Number.Two, new Number(4), new Number(9) }) });
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(3.60555127546399);

        Assert.That(result, Is.EqualTo(expected));
    }
}