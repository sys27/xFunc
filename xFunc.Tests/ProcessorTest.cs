// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests;

public class ProcessorTest
{
    [Fact]
    public void SimplifierNull()
        => Assert.Throws<ArgumentNullException>(() => new Processor(null, null, null, null, null));

    [Fact]
    public void DifferentiatorNull()
    {
        var simplifier = new Simplifier();

        Assert.Throws<ArgumentNullException>(() => new Processor(simplifier, null));
    }

    [Fact]
    public void ConverterNull()
    {
        var simplifier = new Simplifier();
        var differentiator = new Differentiator();

        Assert.Throws<ArgumentNullException>(() => new Processor(simplifier, differentiator, null, null, null));
    }

    [Fact]
    public void TypeAnalyzerNull()
    {
        var simplifier = new Simplifier();
        var differentiator = new Differentiator();
        var converter = new Converter();

        Assert.Throws<ArgumentNullException>(() => new Processor(simplifier, differentiator, converter, null, null));
    }

    [Fact]
    public void CtorTest()
    {
        var simplifier = new Simplifier();
        var differentiator = new Differentiator();

        var processor = new Processor(simplifier, differentiator);
    }

    [Fact]
    public void CtorTest2()
    {
        var simplifier = new Simplifier();
        var differentiator = new Differentiator();
        var converter = new Converter();
        var typeAnalyzer = new TypeAnalyzer();
        var parameters = new ExpressionParameters();

        var processor = new Processor(simplifier, differentiator, converter, typeAnalyzer, parameters);
    }

    [Fact]
    public void SolveDoubleTest()
    {
        var processor = new Processor();
        var result = processor.Solve<NumberResult>("1 + 1.1");

        Assert.Equal(2.1, result.Result);
    }

    [Fact]
    public void SolveComplexTest()
    {
        var processor = new Processor();

        var result = processor.Solve<ComplexNumberResult>("conjugate(2.3 + 1.4i)");
        var expected = Complex.Conjugate(new Complex(2.3, 1.4));

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveBoolTest()
    {
        var processor = new Processor();
        var result = processor.Solve<BooleanResult>("true & false");

        Assert.False(result.Result);
    }

    [Fact]
    public void SolveStringTest()
    {
        var processor = new Processor();
        var result = processor.Solve<StringResult>("x := 1");

        Assert.Equal("The value '1' was assigned to 'x'.", result.Result);
    }

    [Fact]
    public void SolveExpTest()
    {
        var processor = new Processor();
        var result = processor.Solve<ExpressionResult>("deriv(x)");

        Assert.Equal(Number.One, result.Result);
    }

    [Fact]
    public void SolveExpDoNotSimplifyTest()
    {
        var processor = new Processor();
        var result = processor.Solve<ExpressionResult>("deriv(x + 1)", false);

        Assert.Equal(Number.One, result.Result);
    }

    [Fact]
    public void SolveAngleTest()
    {
        var processor = new Processor();

        var result = processor.Solve<AngleNumberResult>("90 degree");
        var expected = AngleValue.Degree(90);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolvePowerTest()
    {
        var processor = new Processor();

        var result = processor.Solve<PowerNumberResult>("10 W");
        var expected = PowerValue.Watt(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveTemperatureTest()
    {
        var processor = new Processor();

        var result = processor.Solve<TemperatureNumberResult>("10 °C");
        var expected = TemperatureValue.Celsius(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveMassTest()
    {
        var processor = new Processor();

        var result = processor.Solve<MassNumberResult>("10 g");
        var expected = MassValue.Gram(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveLengthTest()
    {
        var processor = new Processor();

        var result = processor.Solve<LengthNumberResult>("10 m");
        var expected = LengthValue.Meter(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveTimeTest()
    {
        var processor = new Processor();

        var result = processor.Solve<TimeNumberResult>("10 s");
        var expected = TimeValue.Second(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveAreaTest()
    {
        var processor = new Processor();

        var result = processor.Solve<AreaNumberResult>("10 m^2");
        var expected = AreaValue.Meter(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveVolumeTest()
    {
        var processor = new Processor();

        var result = processor.Solve<VolumeNumberResult>("10 m^3");
        var expected = VolumeValue.Meter(10);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void SolveLambdaTest()
    {
        var processor = new Processor();

        var result = processor.Solve<LambdaResult>("(x) => x");
        var expected = new Lambda(new[] { "x" }, Variable.X);

        Assert.Equal(expected, result.Result);
    }

    [Fact]
    public void ParseTest()
    {
        var processor = new Processor();

        var result = processor.Parse("x + 1");
        var expected = new Add(Variable.X, Number.One);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SimplifyTest()
    {
        var processor = new Processor();

        var exp = new Add(Number.One, Number.One);
        var result = processor.Simplify(exp);

        Assert.Equal(Number.Two, result);
    }

    [Fact]
    public void SimplifyFunctionTest()
    {
        var processor = new Processor();
        var result = processor.Simplify("1 + 1");

        Assert.Equal(Number.Two, result);
    }

    [Fact]
    public void SimplifyNullTest()
    {
        var processor = new Processor();

        Assert.Throws<ArgumentNullException>(() => processor.Simplify(null as IExpression));
    }

    [Fact]
    public void DifferentiateExpTest()
    {
        var processor = new Processor();
        var result = processor.Differentiate(new Add(Variable.X, Number.One));

        Assert.Equal(Number.One, result);
    }

    [Fact]
    public void DifferentiateFunctionTest()
    {
        var processor = new Processor();
        var result = processor.Differentiate("x + 1");

        Assert.Equal(Number.One, result);
    }

    [Fact]
    public void DifferentiateNullExpTest()
    {
        var processor = new Processor();

        Assert.Throws<ArgumentNullException>(() => processor.Differentiate(null as IExpression));
    }

    [Fact]
    public void DifferentiateVarTest()
    {
        var processor = new Processor();

        var y = Variable.Y;
        var result = processor.Differentiate(new Add(y, Number.One), y);

        Assert.Equal(Number.One, result);
    }

    [Fact]
    public void DifferentiateParamsTest()
    {
        var processor = new Processor();

        var y = Variable.Y;
        var result = processor.Differentiate(new Add(y, Number.One), y, new ExpressionParameters());

        Assert.Equal(Number.One, result);
    }
}