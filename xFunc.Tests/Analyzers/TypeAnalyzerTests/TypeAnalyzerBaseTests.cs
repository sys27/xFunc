// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public abstract class TypeAnalyzerBaseTests : BaseTest
{
    protected readonly ITypeAnalyzer analyzer;

    protected TypeAnalyzerBaseTests()
    {
        analyzer = new TypeAnalyzer();
    }

    protected void Test(IExpression exp, ResultTypes expected)
    {
        var simple = exp.Analyze(analyzer);

        Assert.Equal(expected, simple);
    }

    protected void TestException(IExpression exp)
    {
        Assert.Throws<ParameterTypeMismatchException>(() => exp.Analyze(analyzer));
    }

    protected void TestBinaryException(BinaryExpression exp)
    {
        Assert.Throws<BinaryParameterTypeMismatchException>(() => exp.Analyze(analyzer));
    }

    protected void TestDiffParamException(DifferentParametersExpression exp)
    {
        Assert.Throws<DifferentParameterTypeMismatchException>(() => exp.Analyze(analyzer));
    }

    protected void TestNullExp(Type type)
    {
        try
        {
            var method = typeof(TypeAnalyzer)
                .GetMethod(nameof(TypeAnalyzer.Analyze), new[] { type });
            method.Invoke(analyzer, new object[] { null });
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is ArgumentNullException)
                return;

            throw;
        }

        throw new XunitException("The exception is expected.");
    }
}