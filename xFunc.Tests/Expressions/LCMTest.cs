// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class LCMTest : BaseExpressionTests
{
    [Test]
    public void NullArgTest()
        => Assert.Throws<ArgumentNullException>(() => new LCM(null));

    [Test]
    public void ExecuteTest1()
    {
        var exp = new LCM(new Number(12), new Number(16));
        var expected = new NumberValue(48.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteTest2()
    {
        var exp = new LCM(new IExpression[] { new Number(4), new Number(16), new Number(8) });
        var expected = new NumberValue(16.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNotSupportedTest()
        => TestNotSupported(new LCM(new IExpression[] { Bool.False, Bool.True }));

    [Test]
    public void CloneTest()
    {
        var exp = new LCM(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void CloneWithArgsTest()
    {
        var exp = new LCM(Variable.X, Number.Zero);
        var args = new IExpression[] { Variable.X, Variable.Y, }.ToImmutableArray();
        var clone = exp.Clone(args);
        var expected = new LCM(args);

        Assert.That(clone, Is.EqualTo(expected));
    }
}