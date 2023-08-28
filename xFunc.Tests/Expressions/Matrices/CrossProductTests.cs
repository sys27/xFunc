// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Matrices;

public class CrossProductTests : BaseExpressionTests
{
    [Test]
    public void ExecuteTest()
    {
        var exp = new CrossProduct(
            new Vector(new[] { Number.One, Number.Two, new Number(3) }),
            new Vector(new[] { new Number(4), new Number(5), new Number(6) })
        );
        var result = exp.Execute();
        var expected = VectorValue.Create(new NumberValue(-3), new NumberValue(6), new NumberValue(-3));

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteLeftLessThenThreeTest()
    {
        var exp = new CrossProduct(
            new Vector(new[] { Number.One, Number.Two, }),
            new Vector(new[] { new Number(4), new Number(5), new Number(6) })
        );

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteRightLessThenThreeTest()
    {
        var exp = new CrossProduct(
            new Vector(new[] { Number.One, Number.Two, new Number(3) }),
            new Vector(new[] { new Number(4), new Number(5), })
        );

        Assert.Throws<ArgumentException>(() => exp.Execute());
    }

    [Test]
    public void ExecuteTypeExceptionTest()
        => TestNotSupported(new CrossProduct(Number.One, Number.Two));

    [Test]
    public void ExecuteLeftTypeExceptionTest()
    {
        var exp = new CrossProduct(
            Number.One,
            new Vector(new[] { Number.One, Number.Two, new Number(3) }));

        TestNotSupported(exp);
    }

    [Test]
    public void ExecuteRightTypeExceptionTest()
    {
        var exp = new CrossProduct(
            new Vector(new[] { Number.One, Number.Two, new Number(3) }),
            Number.Two);

        TestNotSupported(exp);
    }

    [Test]
    public void CloneTest()
    {
        var exp = new CrossProduct(
            new Vector(new[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new[] { new Number(4), Number.Zero, new Number(6) })
        );
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}