// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Reflection;
using NUnit.Framework.Internal;

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

        throw new NUnitException("The exception is expected.");
    }

    [Test]
    [TestCase(typeof(Abs))]
    [TestCase(typeof(Add))]
    [TestCase(typeof(Derivative))]
    [TestCase(typeof(Div))]
    [TestCase(typeof(Exp))]
    [TestCase(typeof(Lb))]
    [TestCase(typeof(Lg))]
    [TestCase(typeof(Ln))]
    [TestCase(typeof(Log))]
    [TestCase(typeof(Mul))]
    [TestCase(typeof(Number))]
    [TestCase(typeof(Angle))]
    [TestCase(typeof(Pow))]
    [TestCase(typeof(Root))]
    [TestCase(typeof(Simplify))]
    [TestCase(typeof(Sqrt))]
    [TestCase(typeof(Sub))]
    [TestCase(typeof(UnaryMinus))]
    [TestCase(typeof(Variable))]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestExpressionNullArgument(Type type)
        => TestNullExp(type);

    [Test]
    [TestCase(typeof(Abs))]
    [TestCase(typeof(Exp))]
    [TestCase(typeof(Lb))]
    [TestCase(typeof(Lg))]
    [TestCase(typeof(Ln))]
    [TestCase(typeof(Sqrt))]
    [TestCase(typeof(UnaryMinus))]
    [TestCase(typeof(Arccos))]
    [TestCase(typeof(Arccot))]
    [TestCase(typeof(Arccsc))]
    [TestCase(typeof(Arcsec))]
    [TestCase(typeof(Arcsin))]
    [TestCase(typeof(Arctan))]
    [TestCase(typeof(Cos))]
    [TestCase(typeof(Cot))]
    [TestCase(typeof(Csc))]
    [TestCase(typeof(Sec))]
    [TestCase(typeof(Sin))]
    [TestCase(typeof(Tan))]
    [TestCase(typeof(Arcosh))]
    [TestCase(typeof(Arcoth))]
    [TestCase(typeof(Arcsch))]
    [TestCase(typeof(Arsech))]
    [TestCase(typeof(Arsinh))]
    [TestCase(typeof(Artanh))]
    [TestCase(typeof(Cosh))]
    [TestCase(typeof(Coth))]
    [TestCase(typeof(Csch))]
    [TestCase(typeof(Sech))]
    [TestCase(typeof(Sinh))]
    [TestCase(typeof(Tanh))]
    public void TestUnaryContextNullArgument(Type type)
    {
        var exp = Create(type, Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze(differentiator, null));
    }

    [Test]
    [TestCase(typeof(Add))]
    [TestCase(typeof(Div))]
    [TestCase(typeof(Log))]
    [TestCase(typeof(Mul))]
    [TestCase(typeof(Pow))]
    [TestCase(typeof(Root))]
    [TestCase(typeof(Sub))]
    public void TestBinaryContextNullArgument(Type type)
    {
        var exp = Create(type, Variable.X, Variable.X);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze(differentiator, null));
    }

    [Test]
    public void DerivContextArgumentTest()
    {
        var exp = new Derivative(differentiator, new Simplifier(), Variable.X);

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Test]
    public void NumberContextArgumentTest()
    {
        var exp = Number.One;

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Test]
    public void AngleContextArgumentTest()
    {
        var exp = AngleValue.Degree(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Test]
    public void SimplifyContextArgumentTest()
    {
        var exp = new Simplify(new Simplifier(), Variable.X);

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Test]
    public void VariableContextArgumentTest()
    {
        var exp = Variable.X;

        Assert.Throws<ArgumentNullException>(() => differentiator.Analyze(exp, null));
    }

    [Test]
    public void CallExpressionContextNullArgument()
    {
        var exp = new CallExpression(
            Variable.X.ToLambdaExpression(Variable.X.Name),
            new IExpression[] { Variable.X }.ToImmutableArray());

        Assert.Throws<NotSupportedException>(() => exp.Analyze(differentiator, null));
    }

    [Test]
    public void LambdaExpressionContextNullArgument()
    {
        var exp = Variable.X.ToLambdaExpression(Variable.X.Name);

        Assert.Throws<NotSupportedException>(() => exp.Analyze(differentiator, null));
    }
}