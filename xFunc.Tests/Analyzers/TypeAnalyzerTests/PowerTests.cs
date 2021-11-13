// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class PowerTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestPowComplexAndUndefined()
    {
        var exp = new Pow(new ComplexNumber(2, 2), Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestPowComplexAndNumber()
    {
        var exp = new Pow(new ComplexNumber(2, 4), new Number(4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestPowComplexAndComplex()
    {
        var exp = new Pow(new ComplexNumber(4, 2), new ComplexNumber(2, 4));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestPowUndefinedAndNumber()
    {
        var exp = new Pow(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestPowNumberAndUndefined()
    {
        var exp = new Pow(Number.Two, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestPowNumber()
    {
        var exp = new Pow(new Number(4), Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestPowException()
    {
        var exp = new Pow(Bool.False, Bool.False);

        TestBinaryException(exp);
    }
}