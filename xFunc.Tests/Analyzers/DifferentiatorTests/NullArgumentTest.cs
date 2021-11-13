// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Xunit.Sdk;

namespace xFunc.Tests.Analyzers.DifferentiatorTests;

public class NullArgumentTest : BaseTest
{
    private readonly Differentiator differentiator = new Differentiator();

    private void TestNullExp(Type type, object exp = null)
    {
        try
        {
            var context = typeof(DifferentiatorContext);
            var method = typeof(Differentiator)
                .GetMethod(nameof(Differentiator.Analyze), new[] { type, context });
            if (method is null)
                throw new InvalidOperationException("The 'Analyze' method not found.");

            method.Invoke(differentiator, new[] { exp, null });
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is ArgumentNullException)
                return;

            throw;
        }

        throw new XunitException("The exception is expected.");
    }

    [Theory]
    [InlineData(typeof(Abs))]
    [InlineData(typeof(Add))]
    [InlineData(typeof(Derivative))]
    [InlineData(typeof(Div))]
    [InlineData(typeof(Exp))]
    [InlineData(typeof(Lb))]
    [InlineData(typeof(Lg))]
    [InlineData(typeof(Ln))]
    [InlineData(typeof(Log))]
    [InlineData(typeof(Mul))]
    [InlineData(typeof(Number))]
    [InlineData(typeof(Angle))]
    [InlineData(typeof(Pow))]
    [InlineData(typeof(Root))]
    [InlineData(typeof(Simplify))]
    [InlineData(typeof(Sqrt))]
    [InlineData(typeof(Sub))]
    [InlineData(typeof(UnaryMinus))]
    [InlineData(typeof(UserFunction))]
    [InlineData(typeof(Variable))]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
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
    public void TestExpressionNullArgument(Type type)
        => TestNullExp(type);

    [Theory]
    [InlineData(typeof(Abs))]
    [InlineData(typeof(Exp))]
    [InlineData(typeof(Lb))]
    [InlineData(typeof(Lg))]
    [InlineData(typeof(Ln))]
    [InlineData(typeof(Sqrt))]
    [InlineData(typeof(UnaryMinus))]
    [InlineData(typeof(Arccos))]
    [InlineData(typeof(Arccot))]
    [InlineData(typeof(Arccsc))]
    [InlineData(typeof(Arcsec))]
    [InlineData(typeof(Arcsin))]
    [InlineData(typeof(Arctan))]
    [InlineData(typeof(Cos))]
    [InlineData(typeof(Cot))]
    [InlineData(typeof(Csc))]
    [InlineData(typeof(Sec))]
    [InlineData(typeof(Sin))]
    [InlineData(typeof(Tan))]
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
    public void TestUnaryContextNullArgument(Type type)
    {
        var exp = Create(type, Variable.X);

        Assert.Throws<ArgumentNullException>(() => { exp.Analyze(differentiator, null); });
    }

    [Theory]
    [InlineData(typeof(Add))]
    [InlineData(typeof(Div))]
    [InlineData(typeof(Log))]
    [InlineData(typeof(Mul))]
    [InlineData(typeof(Pow))]
    [InlineData(typeof(Root))]
    [InlineData(typeof(Sub))]
    public void TestBinaryContextNullArgument(Type type)
    {
        var exp = Create(type, Variable.X, Variable.X);

        Assert.Throws<ArgumentNullException>(() => { exp.Analyze(differentiator, null); });
    }

    [Fact]
    public void DerivContextArgumentTest()
    {
        var exp = new Derivative(differentiator, new Simplifier(), Variable.X);

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Fact]
    public void NumberContextArgumentTest()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Fact]
    public void AngleContextArgumentTest()
    {
        var exp = AngleValue.Degree(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Fact]
    public void SimplifyContextArgumentTest()
    {
        var exp = new Simplify(new Simplifier(), Variable.X);

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Fact]
    public void UserFunctionContextArgumentTest()
    {
        var exp = new UserFunction("func", new IExpression[] { Variable.X });

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Fact]
    public void VariableContextArgumentTest()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }
}