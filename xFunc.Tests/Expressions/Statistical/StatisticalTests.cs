// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Expressions.Statistical;

public class StatisticalTests : BaseExpressionTests
{
    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void NotSupportedException(Type type)
    {
        var exp = Create(type, new IExpression[] { Bool.False, Bool.False });

        TestNotSupported(exp);
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void CloneTest(Type type)
    {
        var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    [TestCase(typeof(Avg))]
    [TestCase(typeof(Count))]
    [TestCase(typeof(Max))]
    [TestCase(typeof(Min))]
    [TestCase(typeof(Product))]
    [TestCase(typeof(Stdev))]
    [TestCase(typeof(Stdevp))]
    [TestCase(typeof(Sum))]
    [TestCase(typeof(Var))]
    [TestCase(typeof(Varp))]
    public void CloneWithReplaceTest(Type type)
    {
        var exp = Create<StatisticalExpression>(type, new IExpression[] { Number.One, Number.Two });
        var arg = ImmutableArray.Create<IExpression>(Number.One);
        var clone = exp.Clone(arg);
        var expected = Create(type, arg);

        Assert.That(clone, Is.EqualTo(expected));
    }
}