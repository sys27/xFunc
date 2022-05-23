// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Analyzers.SimplifierTests;

public class AngleSimplifierTest : BaseSimplifierTest
{
    [Fact]
    public void ToDegreeNumber()
    {
        var exp = new ToDegree(new Number(10));
        var expected = AngleValue.Degree(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void DegreeToDegree()
    {
        var exp = new ToDegree(AngleValue.Degree(10).AsExpression());
        var expected = AngleValue.Degree(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void RadianToDegree()
    {
        var exp = new ToDegree(AngleValue.Radian(Math.PI).AsExpression());
        var expected = AngleValue.Degree(180).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToDegreeArgumentSimplified()
    {
        var exp = new ToDegree(
            new Arcsin(
                new Add(Number.One, Number.One)
            )
        );
        var expected = new ToDegree(new Arcsin(Number.Two));

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToDegreeNotSimplified()
    {
        var exp = new ToDegree(Variable.X);

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void ToRadianNumber()
    {
        var exp = new ToRadian(new Number(10));
        var expected = AngleValue.Radian(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void RadianToRadian()
    {
        var exp = new ToRadian(AngleValue.Radian(10).AsExpression());
        var expected = AngleValue.Radian(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void DegreeToRadian()
    {
        var exp = new ToRadian(AngleValue.Degree(180).AsExpression());
        var expected = AngleValue.Radian(Math.PI).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToRadianArgumentSimplified()
    {
        var exp = new ToRadian(
            new Arcsin(
                new Add(Number.One, Number.One)
            )
        );
        var expected = new ToRadian(new Arcsin(Number.Two));

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToRadianNotSimplified()
    {
        var exp = new ToRadian(Variable.X);

        SimplifyTest(exp, exp);
    }

    [Fact]
    public void ToGradianNumber()
    {
        var exp = new ToGradian(new Number(10));
        var expected = AngleValue.Gradian(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void GradianToGradian()
    {
        var exp = new ToGradian(AngleValue.Gradian(10).AsExpression());
        var expected = AngleValue.Gradian(10).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void DegreeToGradian()
    {
        var exp = new ToGradian(AngleValue.Degree(180).AsExpression());
        var expected = AngleValue.Gradian(200).AsExpression();

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToGradianArgumentSimplified()
    {
        var exp = new ToGradian(
            new Arcsin(
                new Add(Number.One, Number.One)
            )
        );
        var expected = new ToGradian(new Arcsin(Number.Two));

        SimplifyTest(exp, expected);
    }

    [Fact]
    public void ToGradianNotSimplified()
    {
        var exp = new ToGradian(Variable.X);

        SimplifyTest(exp, exp);
    }
}