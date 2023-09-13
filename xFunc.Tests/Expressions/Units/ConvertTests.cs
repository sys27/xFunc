// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Convert = xFunc.Maths.Expressions.Units.Convert;

namespace xFunc.Tests.Expressions.Units;

public class ConvertTests
{
    [Test]
    public void NullConverterTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Convert(null, null, null));
    }

    [Test]
    public void NullValueTest()
    {
        var converter = new Converter();

        Assert.Throws<ArgumentNullException>(() => new Convert(converter, null, null));
    }

    [Test]
    public void NullUnitTest()
    {
        var converter = new Converter();
        var value = Number.One;

        Assert.Throws<ArgumentNullException>(() => new Convert(converter, value, null));
    }

    [Test]
    public void EqualSameObjects()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.That(convert.Equals(convert), Is.True);
    }

    [Test]
    public void EqualNull()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.That(convert.Equals(null), Is.False);
    }

    [Test]
    public void EqualDifferentObject()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));
        var number = Number.One;

        Assert.That(convert.Equals(number), Is.False);
    }

    [Test]
    public void EqualDifferentValues()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.Two, new StringExpression("deg"));

        Assert.That(convert1.Equals(convert2), Is.False);
    }

    [Test]
    public void EqualDifferentUnits()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.One, new StringExpression("rad"));

        Assert.That(convert1.Equals(convert2), Is.False);
    }

    [Test]
    public void EqualObjects()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.That(convert1.Equals(convert2), Is.True);
    }

    [Test]
    public void Execute()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));
        var expected = AngleValue.Degree(1);

        var result = convert.Execute();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ExecuteNotSupported()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, Number.Two);

        Assert.Throws<ExecutionException>(() => convert.Execute());
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }

    [Test]
    public void MatrixAnalyzeNull()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void MatrixAnalyzeNull2()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void AnalyzeNotSupported()
    {
        var diff = new Differentiator();
        var context = new DifferentiatorContext();

        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<NotSupportedException>(() => exp.Analyze(diff, context));
    }
}