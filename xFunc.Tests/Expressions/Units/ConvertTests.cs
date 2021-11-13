// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Convert = xFunc.Maths.Expressions.Units.Convert;

namespace xFunc.Tests.Expressions.Units;

public class ConvertTests
{
    [Fact]
    public void NullConverterTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Convert(null, null, null));
    }

    [Fact]
    public void NullValueTest()
    {
        var converter = new Converter();

        Assert.Throws<ArgumentNullException>(() => new Convert(converter, null, null));
    }

    [Fact]
    public void NullUnitTest()
    {
        var converter = new Converter();
        var value = Number.One;

        Assert.Throws<ArgumentNullException>(() => new Convert(converter, value, null));
    }

    [Fact]
    public void EqualSameObjects()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.True(convert.Equals(convert));
    }

    [Fact]
    public void EqualNull()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.False(convert.Equals(null));
    }

    [Fact]
    public void EqualDifferentObject()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));
        var number = Number.One;

        Assert.False(convert.Equals(number));
    }

    [Fact]
    public void EqualDifferentValues()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.Two, new StringExpression("deg"));

        Assert.False(convert1.Equals(convert2));
    }

    [Fact]
    public void EqualDifferentUnits()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.One, new StringExpression("rad"));

        Assert.False(convert1.Equals(convert2));
    }

    [Fact]
    public void EqualObjects()
    {
        var converter = new Converter();
        var convert1 = new Convert(converter, Number.One, new StringExpression("deg"));
        var convert2 = new Convert(converter, Number.One, new StringExpression("deg"));

        Assert.True(convert1.Equals(convert2));
    }

    [Fact]
    public void Execute()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, new StringExpression("deg"));
        var expected = AngleValue.Degree(1);

        var result = convert.Execute();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteNotSupported()
    {
        var converter = new Converter();
        var convert = new Convert(converter, Number.One, Number.Two);

        Assert.Throws<ResultIsNotSupportedException>(() => convert.Execute());
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }

    [Fact]
    public void MatrixAnalyzeNull()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void MatrixAnalyzeNull2()
    {
        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void AnalyzeNotSupported()
    {
        var diff = new Differentiator();
        var context = new DifferentiatorContext(new ExpressionParameters());

        var exp = new Convert(new Converter(), Number.One, new StringExpression("deg"));

        Assert.Throws<NotSupportedException>(() => exp.Analyze(diff, context));
    }
}