// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class LogSimplifierTest : BaseSimplifierTest
{
    [Test]
    public void Log()
    {
        var log = new Log(Variable.X, Variable.X);
        var expected = Number.One;

        SimplifyTest(log, expected);
    }

    [Test]
    public void LogNotSimplified()
    {
        var log = new Log(new Number(3), new Number(11));

        SimplifyTest(log, log);
    }

    [Test]
    public void LogArgumentSimplified()
    {
        var log = new Log(Variable.X, new Add(Number.One, Number.One));
        var expected = new Log(Variable.X, Number.Two);

        SimplifyTest(log, expected);
    }

    [Test]
    public void Ln()
    {
        var ln = new Ln(new Variable("e"));
        var expected = Number.One;

        SimplifyTest(ln, expected);
    }

    [Test]
    public void LnArgumentSimplified()
    {
        var log = new Ln(new Add(Number.Two, Number.Two));
        var expected = new Ln(new Number(4));

        SimplifyTest(log, expected);
    }

    [Test]
    public void LnNotSimplified()
    {
        var ln = new Ln(new Variable("z"));

        SimplifyTest(ln, ln);
    }

    [Test]
    public void Lg()
    {
        var log = new Lg(new Number(10));
        var expected = Number.One;

        SimplifyTest(log, expected);
    }

    [Test]
    public void LgArgumentSimplified()
    {
        var log = new Lg(new Add(Number.Two, Number.Two));
        var expected = new Lg(new Number(4));

        SimplifyTest(log, expected);
    }

    [Test]
    public void LgNotSimplified()
    {
        var log = new Lg(new Number(101));

        SimplifyTest(log, log);
    }

    [Test]
    public void Lb()
    {
        var log = new Lb(Number.Two);
        var expected = Number.One;

        SimplifyTest(log, expected);
    }

    [Test]
    public void LbArgumentSimplified()
    {
        var log = new Lb(new Add(Number.Two, Number.Two));
        var expected = new Lb(new Number(4));

        SimplifyTest(log, expected);
    }

    [Test]
    public void LbNotSimplified()
    {
        var log = new Lb(new Number(3));

        SimplifyTest(log, log);
    }
}