// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class ToGradianTest
{
    [Test]
    public void ExecuteNumberTest()
    {
        var exp = new ToGradian(new Number(10));
        var actual = exp.Execute();
        var expected = AngleValue.Gradian(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteAngleTest()
    {
        var exp = new ToGradian(AngleValue.Gradian(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Gradian(10);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteBoolTest()
    {
        Assert.Throws<ResultIsNotSupportedException>(() => new ToGradian(Bool.False).Execute());
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new ToGradian(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new ToGradian(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new ToGradian(new Number(10));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}