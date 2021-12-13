// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class DivTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestDivNumberNumberTest()
    {
        var exp = new Div(Number.One, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestDivComplexNumberComplexNumberTest()
    {
        var exp = new Div(new ComplexNumber(3, 2), new ComplexNumber(2, 4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestDivNumberComplexNumberTest()
    {
        var exp = new Div(new Number(3), new ComplexNumber(2, 4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestDivComplexNumberNumberTest()
    {
        var exp = new Div(new ComplexNumber(3, 2), Number.Two);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestDivComplexNumberBoolException()
    {
        var exp = new Div(new ComplexNumber(3, 2), Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestDivBoolComplexNumberException()
    {
        var exp = new Div(Bool.True, new ComplexNumber(3, 2));

        TestBinaryException(exp);
    }

    [Fact]
    public void TestDivNumberBoolException()
    {
        var exp = new Div(new Number(3), Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestDivBoolNumberException()
    {
        var exp = new Div(Bool.True, new Number(3));

        TestBinaryException(exp);
    }

    [Fact]
    public void TestDivNumberSqrtComplexTest()
    {
        var exp = new Div(new Sqrt(new Number(-16)), Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestDivTwoVarTest()
    {
        var exp = new Div(Variable.X, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestDivNumberAndVarTest()
    {
        var exp = new Div(Number.One, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestDivThreeVarTest()
    {
        var exp = new Div(new Add(Variable.X, Variable.X), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestDivException()
    {
        var exp = new Div(Bool.False, Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestDivAngleNumber()
    {
        var exp = new Div(
            AngleValue.Degree(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestDivPowerNumber()
    {
        var exp = new Div(
            PowerValue.Watt(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestDivTemperatureNumber()
    {
        var exp = new Div(
            TemperatureValue.Celsius(10).AsExpression(),
            new Number(10)
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }
}