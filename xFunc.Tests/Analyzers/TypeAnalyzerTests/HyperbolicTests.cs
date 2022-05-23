// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class HyperbolicTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(Arcosh))]
    [InlineData(typeof(Arcoth))]
    [InlineData(typeof(Arcsch))]
    [InlineData(typeof(Arsech))]
    [InlineData(typeof(Arsinh))]
    [InlineData(typeof(Artanh))]
    [InlineData(typeof(Cosh))]
    [InlineData(typeof(Coth))]
    [InlineData(typeof(Csch))]
    [InlineData(typeof(Sech))]
    [InlineData(typeof(Sinh))]
    [InlineData(typeof(Tanh))]
    public void TestUndefined(Type type)
    {
        var exp = Create(type, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Theory]
    [InlineData(typeof(Arcosh))]
    [InlineData(typeof(Arcoth))]
    [InlineData(typeof(Arcsch))]
    [InlineData(typeof(Arsech))]
    [InlineData(typeof(Arsinh))]
    [InlineData(typeof(Artanh))]
    public void TestAngleNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.AngleNumber);
    }

    [Theory]
    [InlineData(typeof(Cosh))]
    [InlineData(typeof(Coth))]
    [InlineData(typeof(Csch))]
    [InlineData(typeof(Sech))]
    [InlineData(typeof(Sinh))]
    [InlineData(typeof(Tanh))]
    public void TestNumber(Type type)
    {
        var exp = Create(type, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Theory]
    [InlineData(typeof(Arcosh))]
    [InlineData(typeof(Arcoth))]
    [InlineData(typeof(Arcsch))]
    [InlineData(typeof(Arsech))]
    [InlineData(typeof(Arsinh))]
    [InlineData(typeof(Artanh))]
    [InlineData(typeof(Cosh))]
    [InlineData(typeof(Coth))]
    [InlineData(typeof(Csch))]
    [InlineData(typeof(Sech))]
    [InlineData(typeof(Sinh))]
    [InlineData(typeof(Tanh))]
    public void TestComplexNumber(Type type)
    {
        var exp = Create(type, new ComplexNumber(2, 2));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Theory]
    [InlineData(typeof(Arcosh))]
    [InlineData(typeof(Arcoth))]
    [InlineData(typeof(Arcsch))]
    [InlineData(typeof(Arsech))]
    [InlineData(typeof(Arsinh))]
    [InlineData(typeof(Artanh))]
    [InlineData(typeof(Cosh))]
    [InlineData(typeof(Coth))]
    [InlineData(typeof(Csch))]
    [InlineData(typeof(Sech))]
    [InlineData(typeof(Sinh))]
    [InlineData(typeof(Tanh))]
    public void TestParameterException(Type type)
    {
        var exp = Create(type, Bool.False);

        TestException(exp);
    }
}