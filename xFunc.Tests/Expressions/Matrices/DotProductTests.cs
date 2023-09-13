// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Matrices;

public class DotProductTests : BaseExpressionTests
{
    [Test]
    public void CtorNullTest()
        => Assert.Throws<ArgumentNullException>(() => new DotProduct(new ImmutableArray<IExpression>()));

    [Test]
    public void ExecuteTest()
    {
        var exp = new DotProduct(
            new Vector(new[] { Number.One, Number.Two, new Number(3) }),
            new Vector(new[] { new Number(4), new Number(5), new Number(6) })
        );
        var result = exp.Execute();
        var expected = new NumberValue(32.0);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTypeExceptionTest()
        => TestNotSupported(new DotProduct(Number.One, Number.Two));

    [Test]
    public void ExecuteLeftTypeExceptionTest()
    {
        var exp = new DotProduct(
            Number.One,
            new Vector(new[] { Number.One, Number.Two, new Number(3) }));

        TestNotSupported(exp);
    }

    [Test]
    public void ExecuteRightTypeExceptionTest()
    {
        var exp = new DotProduct(
            new Vector(new[] { Number.One, Number.Two, new Number(3) }),
            Number.Two);

        TestNotSupported(exp);
    }

    [Test]
    public void CloneTest()
    {
        var exp = new DotProduct(
            new Vector(new[] { Number.One, new Number(-2), new Number(3) }),
            new Vector(new[] { new Number(4), Number.Zero, new Number(6) })
        );
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}