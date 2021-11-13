// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class NumericConvertionTests : TypeAnalyzerBaseTests
{
    [Theory]
    [InlineData(typeof(ToBin))]
    [InlineData(typeof(ToOct))]
    [InlineData(typeof(ToHex))]
    public void UndefinedTest(Type type)
    {
        var exp = Create(type, Variable.X);

        Test(exp, ResultTypes.String);
    }

    [Theory]
    [InlineData(typeof(ToBin))]
    [InlineData(typeof(ToOct))]
    [InlineData(typeof(ToHex))]
    public void NumberTest(Type type)
    {
        var exp = Create(type, new Number(10));

        Test(exp, ResultTypes.String);
    }

    [Theory]
    [InlineData(typeof(ToBin))]
    [InlineData(typeof(ToOct))]
    [InlineData(typeof(ToHex))]
    public void BoolTest(Type type)
    {
        var exp = Create(type, Bool.False);

        TestException(exp);
    }
}