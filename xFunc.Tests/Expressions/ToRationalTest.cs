// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions;

public class ToRationalTest : BaseExpressionTests
{
    [Test]
    public void ExecuteTest()
    {
        var exp = new ToRational(Number.Two);
        var expected = new RationalValue(2, 1);
        var actual = exp.Execute();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteExceptionTest()
        => TestNotSupported(new ToRational(Bool.True));

    [Test]
    public void CloneTest()
    {
        var exp = new ToRational(Number.Two);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}