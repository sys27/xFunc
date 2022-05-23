// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class TrigonometricSimplifierTest : BaseSimplifierTest
{
    [Theory]
    [InlineData(typeof(Arcsin), typeof(Sin))]
    [InlineData(typeof(Arccos), typeof(Cos))]
    [InlineData(typeof(Arctan), typeof(Tan))]
    [InlineData(typeof(Arccot), typeof(Cot))]
    [InlineData(typeof(Arcsec), typeof(Sec))]
    [InlineData(typeof(Arccsc), typeof(Csc))]
    [InlineData(typeof(Sin), typeof(Arcsin))]
    [InlineData(typeof(Cos), typeof(Arccos))]
    [InlineData(typeof(Tan), typeof(Arctan))]
    [InlineData(typeof(Cot), typeof(Arccot))]
    [InlineData(typeof(Sec), typeof(Arcsec))]
    [InlineData(typeof(Csc), typeof(Arccsc))]
    [InlineData(typeof(Arsinh), typeof(Sinh))]
    [InlineData(typeof(Arcosh), typeof(Cosh))]
    [InlineData(typeof(Artanh), typeof(Tanh))]
    [InlineData(typeof(Arcoth), typeof(Coth))]
    [InlineData(typeof(Arsech), typeof(Sech))]
    [InlineData(typeof(Arcsch), typeof(Csch))]
    [InlineData(typeof(Sinh), typeof(Arsinh))]
    [InlineData(typeof(Cosh), typeof(Arcosh))]
    [InlineData(typeof(Tanh), typeof(Artanh))]
    [InlineData(typeof(Coth), typeof(Arcoth))]
    [InlineData(typeof(Sech), typeof(Arsech))]
    [InlineData(typeof(Csch), typeof(Arcsch))]
    public void ReserseFunctionsTest(Type outer, Type inner)
    {
        var innerFunction = Create(inner, Variable.X);
        var outerFunction = Create(outer, innerFunction);
        var expected = Variable.X;

        SimplifyTest(outerFunction, expected);
    }

    [Theory]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Tan))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Csc))]
    public void SimplifyArgumentTest(Type type)
    {
        var exp = Create(type, new Add(Number.One, Number.Two));
        var expected = Create(type, new Number(3));

        SimplifyTest(exp, expected);
    }
}