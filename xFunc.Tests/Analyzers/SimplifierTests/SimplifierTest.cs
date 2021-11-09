// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class SimplifierTest : BaseSimplifierTest
{
    [Fact]
    public void DoubleUnary()
    {
        var un = new UnaryMinus(new UnaryMinus(Variable.X));
        var expected = Variable.X;

        SimplifyTest(un, expected);
    }

    [Fact]
    public void UnaryNumber()
    {
        var un = new UnaryMinus(Number.One);
        var expected = new Number(-1);

        SimplifyTest(un, expected);
    }

    [Fact]
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

    [Fact]
    public void UnaryNumberNotSimplified()
    {
        var un = new UnaryMinus(Variable.X);

        SimplifyTest(un, un);
    }

    [Fact]
    public void Define()
    {
        var define = new Define(Variable.X, new Add(Number.Two, Number.Two));
        var expected = new Define(Variable.X, new Number(4));

        SimplifyTest(define, expected);
    }

    [Fact]
    public void DefineNotSimplifierTest()
    {
        var define = new Define(Variable.X, Number.Two);

        SimplifyTest(define, define);
    }

    [Fact]
    public void Simplify()
    {
        var simp = new Simplify(simplifier, new Pow(Variable.X, Number.Zero));
        var expected = Number.One;

        SimplifyTest(simp, expected);
    }

    [Fact]
    public void Deriv()
    {
        var diff = new Differentiator();
        var simpl = new Simplifier();
        var simp = new Derivative(diff, simpl, new Add(Number.Two, new Number(3)));
        var expected = new Derivative(diff, simpl, new Number(5));

        SimplifyTest(simp, expected);
    }

    [Fact]
    public void DerivNotSimplifiedTest()
    {
        var differentiator = new Differentiator();
        var simplifier = new Simplifier();
        var exp = new Derivative(differentiator, simplifier, Number.Two);

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void UserFunc()
    {
        var exp = new UserFunction("f", new IExpression[] { new Mul(Number.Two, Number.Two) });
        var expected = new UserFunction("f", new IExpression[] { new Number(4) });

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void UserFuncNotSimplified()
    {
        var exp = new UserFunction("f", new IExpression[] { Number.One });

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void DiffTest()
    {
        var exp = new Count(new IExpression[] { new Add(Number.Two, Number.Two) });
        var expected = new Count(new IExpression[] { new Number(4) });

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void UnaryMinusNumberTest()
    {
        var exp = new Abs(new UnaryMinus(Variable.X));
        var expected = Variable.X;

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AbsAbsTest()
    {
        var exp = new Abs(new Abs(Variable.X));
        var expected = new Abs(Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AbsAbsAbsTest()
    {
        var exp = new Abs(new Abs(new Abs(Variable.X)));
        var expected = new Abs(Variable.X);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AbsArgumentSimplifiedTest()
    {
        var exp = new Abs(new Add(Number.One, Number.One));
        var expected = new Abs(Number.Two);

        SimplifyTest(exp, expected);
    }

    [Fact]
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

    [Fact]
    public void MatrixNotSimplifiedTest()
    {
        var exp = new Matrix(new[]
        {
            new Vector(new IExpression[] { Number.One }),
        });

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void AddAssignTest()
    {
        var exp = new AddAssign(Variable.X, new Add(Number.One, Number.Two));
        var expected = new AddAssign(Variable.X, new Number(3));

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void AddAssignNotSimplifiedTest()
    {
        var exp = new AddAssign(Variable.X, Number.One);

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void CeilArgumentSimplifiedTest()
    {
        var exp = new Ceil(new Add(Number.One, Number.One));
        var expected = new Ceil(Number.Two);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void CeilNotSimplifiedTest()
    {
        var exp = new Ceil(Number.One);

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void ModLeftSimplifiedTest()
    {
        var exp = new Mod(new Add(Number.One, Number.One), Number.One);
        var expected = new Mod(Number.Two, Number.One);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ModRightSimplifiedTest()
    {
        var exp = new Mod(Number.One, new Add(Number.One, Number.One));
        var expected = new Mod(Number.One, Number.Two);

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ModNotSimplifiedTest()
    {
        var exp = new Mod(Number.One, Number.One);

        SimplifyTest(exp, exp);
    }

    [Theory]
    [ClassData(typeof(AllExpressionsData))]
    public void TestNullException(Type type) => TestNullExp(type);
}