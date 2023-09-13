// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArctanTest : BaseExpressionTests
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new Arctan(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.7853981633974483);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arctan(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.That(result.Real, Is.EqualTo(1.3389725222944935).Within(15));
        Assert.That(result.Imaginary, Is.EqualTo(0.14694666622552977).Within(15));
    }

    [Test]
    public void ExecuteTestException()
        => TestNotSupported(new Arctan(Bool.False));

    [Test]
    public void CloneTest()
    {
        var exp = new Arctan(Number.One);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}