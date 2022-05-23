// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class ComplexNumberTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestComplexNumber()
    {
        var exp = new ComplexNumber(2, 2);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestConjugateUndefined()
    {
        var exp = new Conjugate(Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestConjugateComplexNumber()
    {
        var exp = new Conjugate(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestConjugateException()
    {
        var exp = new Conjugate(Number.Two);

        TestException(exp);
    }

    [Fact]
    public void TestImUndefined()
    {
        var exp = new Im(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestImComplexNumber()
    {
        var exp = new Im(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestImException()
    {
        var exp = new Im(Number.Two);

        TestException(exp);
    }

    [Fact]
    public void TestPhaseUndefined()
    {
        var exp = new Phase(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestPhaseComplexNumber()
    {
        var exp = new Phase(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestPhaseException()
    {
        var exp = new Phase(Number.Two);

        TestException(exp);
    }

    [Fact]
    public void TestReUndefined()
    {
        var exp = new Re(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestReComplexNumber()
    {
        var exp = new Re(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestReException()
    {
        var exp = new Re(Number.Two);

        TestException(exp);
    }

    [Fact]
    public void TestReciprocalUndefined()
    {
        var exp = new Reciprocal(Variable.X);

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestReciprocalComplexNumber()
    {
        var exp = new Reciprocal(new ComplexNumber(2, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestReciprocalException()
    {
        var exp = new Reciprocal(Number.Two);

        TestException(exp);
    }

    [Fact]
    public void ToComplexUndefined()
        => Test(new ToComplex(Variable.X), ResultTypes.ComplexNumber);

    [Fact]
    public void ToComplexNubmer()
        => Test(new ToComplex(Number.One), ResultTypes.ComplexNumber);

    [Fact]
    public void ToComplexException()
        => TestException(new ToComplex(Bool.False));
}