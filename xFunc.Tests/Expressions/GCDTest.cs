// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class GCDTest : BaseExpressionTests
{
    [Test]
    public void CalculateTest1()
    {
        var exp = new GCD(new Number(12), new Number(16));
        var expected = new NumberValue(4.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void CalculateTest2()
    {
        var exp = new GCD(new IExpression[] { new Number(64), new Number(16), new Number(8) });
        var expected = new NumberValue(8.0);

        Assert.That(exp.Execute(), Is.EqualTo(expected));
    }

    [Test]
    public void CalculateWrongArgumentTypeTest()
    {
        var exp = new GCD(new IExpression[] { Bool.True, new Number(16), new Number(8) });

        TestNotSupported(exp);
    }

    [Test]
    public void NullArgTest()
        => Assert.Throws<ArgumentNullException>(() => new GCD(null));

    [Test]
    public void CloneTest()
    {
        var exp = new GCD(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void CloneWithArgsTest()
    {
        var exp = new GCD(Variable.X, Number.Zero);
        var args = new IExpression[] { Variable.X, Variable.Y, }.ToImmutableArray();
        var clone = exp.Clone(args);
        var expected = new GCD(args);

        Assert.That(clone, Is.EqualTo(expected));
    }

    [Test]
    public void EqualsSameTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.That(exp.Equals(exp));
    }

    [Test]
    public void EqualsNullTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.That(exp.Equals(null), Is.False);
    }

    [Test]
    public void EqualsDiffTypesTest()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });
        var number = Number.Two;

        Assert.That(exp.Equals(number), Is.False);
    }

    [Test]
    public void EqualsDiffCountTest()
    {
        var exp1 = new GCD(new IExpression[] { new Number(16), new Number(8) });
        var exp2 = new GCD(new IExpression[] { new Number(16), new Number(8), Number.Two });

        Assert.That(exp1.Equals(exp2), Is.False);
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new GCD(new IExpression[] { new Number(16), new Number(8) });

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}