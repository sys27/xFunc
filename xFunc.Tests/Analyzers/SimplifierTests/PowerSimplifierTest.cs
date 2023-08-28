// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class PowerSimplifierTest : BaseSimplifierTest
{
    [Test]
    public void PowerXZero()
    {
        var pow = new Pow(Variable.X, Number.Zero);
        var expected = Number.One;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowerZeroX()
    {
        var pow = new Pow(Number.Zero, Variable.X);
        var expected = Number.Zero;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowerOne()
    {
        var pow = new Pow(Variable.X, Number.One);
        var expected = Variable.X;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowerArgumentSimplified()
    {
        var pow = new Pow(Variable.X, new Add(Number.One, Number.One));
        var expected = new Pow(Variable.X, Number.Two);

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowerNotSimplified()
    {
        var pow = new Pow(Variable.X, Variable.X);

        SimplifyTest(pow, pow);
    }

    [Test]
    public void PowLog()
    {
        var pow = new Pow(
            new Number(30),
            new Log(new Number(30), Variable.X));
        var expected = Variable.X;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowLg()
    {
        var pow = new Pow(
            new Number(10),
            new Lg(Variable.X));
        var expected = Variable.X;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowLn()
    {
        var pow = new Pow(
            new Variable("e"),
            new Ln(Variable.X));
        var expected = Variable.X;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void PowLb()
    {
        var pow = new Pow(
            Number.Two,
            new Lb(Variable.X));
        var expected = Variable.X;

        SimplifyTest(pow, expected);
    }

    [Test]
    public void RootOne()
    {
        var root = new Root(Variable.X, Number.One);
        var expected = Variable.X;

        SimplifyTest(root, expected);
    }

    [Test]
    public void RootArgumentSimplified()
    {
        var root = new Root(Variable.X, new Add(Number.One, Number.One));
        var expected = new Root(Variable.X, Number.Two);

        SimplifyTest(root, expected);
    }

    [Test]
    public void RootNotSimplified()
    {
        var root = new Root(Variable.X, new Number(5));

        SimplifyTest(root, root);
    }

    [Test]
    public void Exp()
    {
        var exp = new Exp(new Number(30));

        SimplifyTest(exp, exp);
    }

    [Test]
    public void ExpLn()
    {
        var exp = new Exp(new Ln(new Number(30)));
        var expected = new Number(30);

        SimplifyTest(exp, expected);
    }

    [Test]
    public void ExpArgumentSimplified()
    {
        var exp = new Exp(new Add(Number.One, Number.One));
        var expected = new Exp(Number.Two);

        SimplifyTest(exp, expected);
    }
}