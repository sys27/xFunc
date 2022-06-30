// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class SimplifierTest : BaseSimplifierTest
{
    [Test]
    public void DoubleUnary()
    {
        var un = new UnaryMinus(new UnaryMinus(Variable.X));
        var expected = Variable.X;

        SimplifyTest(un, expected);
    }

    [Test]
    public void UnaryNumber()
    {
        var un = new UnaryMinus(Number.One);
        var expected = new Number(-1);

        SimplifyTest(un, expected);
    }

    [Test]
    public void UnaryNumberArgumentSimplified()
    {
        var un = new UnaryMinus(
            new Sin(
                new Add(Number.One, Number.One)
            )
        );
        var expected = new UnaryMinus(new Sin(Number.Two));

        SimplifyTest(un, expected);
    }

    [Test]
    public void UnaryNumberNotSimplified()
    {
        var un = new UnaryMinus(Variable.X);

        SimplifyTest(un, un);
    }

    [Test]
    public void Define()
    {
        var define = new Assign(Variable.X, new Add(Number.Two, Number.Two));
        var expected = new Assign(Variable.X, new Number(4));

        SimplifyTest(define, expected);
    }

    [Test]
    public void DefineNotSimplifierTest()
    {
        var define = new Assign(Variable.X, Number.Two);

        SimplifyTest(define, define);
    }

    [Test]
    public void Simplify()
    {
        // TODO: fix
        // var simp = new Simplify(simplifier, new Pow(Variable.X, Number.Zero));
        // var expected = Number.One;
        //
        // SimplifyTest(simp, expected);
    }

    [Test]
    public void Deriv()
    {
        var diff = new Differentiator();
        var simpl = new Simplifier();
        var simp = new Derivative(diff, simpl, new Add(Number.Two, new Number(3)));
        var expected = new Derivative(diff, simpl, new Number(5));

        SimplifyTest(simp, expected);
    }

    [Test]
    public void DerivNotSimplifiedTest()
    {
        var differentiator = new Differentiator();
        var simplifier = new Simplifier();
        var exp = new Derivative(differentiator, simplifier, Number.Two);

        SimplifyTest(exp, exp);
    }

    [Test]
    public void DiffTest()
    {
        var exp = new Count(new IExpression[] { new Add(Number.Two, Number.Two) });
        var expected = new Count(new IExpression[] { new Number(4) });

        SimplifyTest(exp, expected);
    }

    [Test]
    public void UnaryMinusNumberTest()
    {
        var exp = new Abs(new UnaryMinus(Variable.X));
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Test]
    public void AbsAbsTest()
    {
        var exp = new Abs(new Abs(Variable.X));
        var expected = new Abs(Variable.X);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void AbsAbsAbsTest()
    {
        var exp = new Abs(new Abs(new Abs(Variable.X)));
        var expected = new Abs(Variable.X);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void AbsArgumentSimplifiedTest()
    {
        var exp = new Abs(new Add(Number.One, Number.One));
        var expected = new Abs(Number.Two);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void MatrixTest()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[]
            {
                new Add(Number.One, Number.Two),
            }),
        });
        var expected = new Matrix(new[]
        {
            new Vector(new IExpression[]
            {
                new Number(3),
            }),
        });

        SimplifyTest(exp, expected);
    }

    [Test]
    public void MatrixNotSimplifiedTest()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One }),
        });

        SimplifyTest(exp, exp);
    }

    [Test]
    public void AddAssignTest()
    {
        var exp = new AddAssign(Variable.X, new Add(Number.One, Number.Two));
        var expected = new AddAssign(Variable.X, new Number(3));

        SimplifyTest(exp, expected);
    }

    [Test]
    public void AddAssignNotSimplifiedTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        SimplifyTest(exp, exp);
    }

    [Test]
    public void CeilArgumentSimplifiedTest()
    {
        var exp = new Ceil(new Add(Number.One, Number.One));
        var expected = new Ceil(Number.Two);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void CeilNotSimplifiedTest()
    {
        var exp = new Ceil(Number.One);

        SimplifyTest(exp, exp);
    }

    [Test]
    public void ModLeftSimplifiedTest()
    {
        var exp = new Mod(new Add(Number.One, Number.One), Number.One);
        var expected = new Mod(Number.Two, Number.One);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void ModRightSimplifiedTest()
    {
        var exp = new Mod(Number.One, new Add(Number.One, Number.One));
        var expected = new Mod(Number.One, Number.Two);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void ModNotSimplifiedTest()
    {
        var exp = new Mod(Number.One, Number.One);

        SimplifyTest(exp, exp);
    }

    [Test]
    public void CallExpressionSimplifiedTest()
    {
        var exp = new CallExpression(
            new Lambda(new[] { "x" }, new Add(Variable.X, Number.Zero)).AsExpression(),
            new IExpression[] { new Add(Number.One, Number.One) }.ToImmutableArray());
        var expected = new CallExpression(
            new Lambda(new[] { "x" }, Variable.X).AsExpression(),
            new IExpression[] { Number.Two }.ToImmutableArray());

        SimplifyTest(exp, expected);
    }

    [Test]
    public void CallExpressionNotSimplifiedTest()
    {
        var exp = new CallExpression(
            new Lambda(new[] { "x" }, Variable.X).AsExpression(),
            new IExpression[] { Variable.X }.ToImmutableArray());

        SimplifyTest(exp, exp);
    }

    [Test]
    public void LambdaExpressionSimplifiedTest()
    {
        var exp = new Lambda(new[] { "x" }, new Add(Variable.X, Number.Zero)).AsExpression();
        var expected = new Lambda(new[] { "x" }, Variable.X).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Test]
    public void LambdaExpressionNotSimplifiedTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X).AsExpression();

        SimplifyTest(exp, exp);
    }

    [Test]
    public void WhileIsNotSimplifiedTest()
    {
        var exp = new While(Variable.X, new Equal(Variable.X, Number.One));

        SimplifyTest(exp, exp);
    }

    [Test]
    public void WhileBodyIsSimplifiedTest()
    {
        var exp = new While(
            new AddAssign(Variable.X, new Add(Number.One, Number.One)),
            new Equal(Variable.X, Number.One));
        var expected = new While(
            new AddAssign(Variable.X, Number.Two),
            new Equal(Variable.X, Number.One));

        SimplifyTest(exp, expected);
    }

    [Test]
    public void WhileConditionIsSimplifiedTest()
    {
        var exp = new While(
            Variable.X,
            new Equal(Variable.X, new Add(Number.One, Number.One)));
        var expected = new While(
            Variable.X,
            new Equal(Variable.X, Number.Two));

        SimplifyTest(exp, expected);
    }

    [Test]
    [TestCaseSource(typeof(AllExpressionsData))]
    public void TestNullException(Type type) => TestNullExp(type);
}