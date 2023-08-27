// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.ParserTests;

public class AssignTests : BaseParserTests
{
    [Test]
    public void DefTest()
        => ParseTest("assign(x, 2)", new Assign(Variable.X, Number.Two));

    [Test]
    [TestCase("assign x, 2)")]
    [TestCase("assign(, 2)")]
    [TestCase("assign(x 2)")]
    [TestCase("assign(x,)")]
    [TestCase("assign(x, 2")]
    public void DefMissingOpenParen(string function)
        => ParseErrorTest(function);

    [Test]
    public void ParseDefine()
    {
        var expected = new Assign(Variable.X, new Number(3));

        ParseTest("x := 3", expected);
    }

    [Test]
    public void ParseDefineFirstParamIsNotVar()
        => ParseErrorTest("5 := 3");

    [Test]
    public void DefineComplexParserTest()
    {
        var expected = new Assign(
            new Variable("aaa"),
            new Add(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            ));

        ParseTest("aaa := 3+2*i", expected);
    }

    [Test]
    public void DefineUserFuncTest()
    {
        var expected = new Assign(
            new Variable("f"),
            new Lambda(new[] { Variable.X.Name }, Variable.X).AsExpression());

        ParseTest("f := (x) => x", expected);
    }

    [Test]
    public void UnaryMinusAssignTest()
    {
        var expected = new Assign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x := -sin(2)", expected);
    }

    [Test]
    public void AddAssign()
    {
        var expected = new AddAssign(Variable.X, Number.Two);

        ParseTest("x += 2", expected);
    }

    [Test]
    public void UnaryMinusAddAssignTest()
    {
        var expected = new AddAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x += -sin(2)", expected);
    }

    [Test]
    public void AddAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new AddAssign(Variable.X, Number.Two));

        ParseTest("1 + (x += 2)", expected);
    }

    [Test]
    public void SubAssign()
    {
        var expected = new SubAssign(Variable.X, Number.Two);

        ParseTest("x -= 2", expected);
    }

    [Test]
    public void UnaryMinusSubAssignTest()
    {
        var expected = new SubAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x -= -sin(2)", expected);
    }

    [Test]
    public void SubAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new SubAssign(Variable.X, Number.Two));

        ParseTest("1 + (x -= 2)", expected);
    }

    [Test]
    public void MulAssign()
    {
        var expected = new MulAssign(Variable.X, Number.Two);

        ParseTest("x *= 2", expected);
    }

    [Test]
    public void UnaryMinusMulAssignTest()
    {
        var expected = new MulAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x *= -sin(2)", expected);
    }

    [Test]
    public void MulAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new MulAssign(Variable.X, Number.Two));

        ParseTest("1 + (x *= 2)", expected);
    }

    [Test]
    public void DivAssign()
    {
        var expected = new DivAssign(Variable.X, Number.Two);

        ParseTest("x /= 2", expected);
    }

    [Test]
    public void UnaryMinusDivAssignTest()
    {
        var expected = new DivAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x /= -sin(2)", expected);
    }

    [Test]
    public void DivAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new DivAssign(Variable.X, Number.Two));

        ParseTest("1 + (x /= 2)", expected);
    }

    [Test]
    public void LeftShiftAssignTest()
    {
        var expected = new LeftShiftAssign(Variable.X, new Number(10));

        ParseTest("x <<= 10", expected);
    }

    [Test]
    public void LeftShiftAssignAsExpressionTest()
    {
        var expected = new Add(
            Number.One,
            new LeftShiftAssign(Variable.X, new Number(10)));

        ParseTest("1 + (x <<= 10)", expected);
    }

    [Test]
    public void RightShiftAssignTest()
    {
        var expected = new RightShiftAssign(Variable.X, new Number(10));

        ParseTest("x >>= 10", expected);
    }

    [Test]
    public void RightShiftAssignAsExpressionTest()
    {
        var expected = new Add(
            Number.One,
            new RightShiftAssign(Variable.X, new Number(10)));

        ParseTest("1 + (x >>= 10)", expected);
    }

    [Test]
    [TestCase("x :=")]
    [TestCase("x +=")]
    [TestCase("x -=")]
    [TestCase("x −=")]
    [TestCase("x *=")]
    [TestCase("x ×=")]
    [TestCase("x /=")]
    [TestCase("x <<=")]
    [TestCase("x >>=")]
    public void AssignMissingValue(string function)
        => ParseErrorTest(function);

    [Test]
    [TestCase("1 + x += 1")]
    [TestCase("1 + x -= 1")]
    [TestCase("1 + x *= 1")]
    [TestCase("1 + x /= 1")]
    [TestCase("1 + x <<= 1")]
    [TestCase("1 + x >>= 1")]
    public void AssignAsExpressionError(string function)
        => ParseErrorTest(function);

    [Test]
    public void IncTest()
        => ParseTest("x++", new Inc(Variable.X));

    [Test]
    public void IncAsExpression()
    {
        var expected = new Add(Number.One, new Inc(Variable.X));

        ParseTest("1 + x++", expected);
    }

    [Test]
    [TestCase("x--")]
    [TestCase("x−−")]
    public void DecTest(string function)
    {
        var expected = new Dec(Variable.X);

        ParseTest(function, expected);
    }

    [Test]
    public void DecAsExpression()
    {
        var expected = new Add(Number.One, new Dec(Variable.X));

        ParseTest("1 + x--", expected);
    }

    [Test]
    [TestCase("1 + (x := 2)")]
    [TestCase("1 + assign(x, 2)")]
    public void AssignAsExpression(string function)
    {
        var expected = new Add(
            Number.One,
            new Assign(Variable.X, Number.Two));

        ParseTest(function, expected);
    }

    [Test]
    public void UnassignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new Unassign(Variable.X));

        ParseTest("1 + unassign(x)", expected);
    }

    [Test]
    [TestCase("(f := (x) => x * x)(2)")]
    [TestCase("(assign(f, (x) => x * x))(2)")]
    public void AssignLambdaAsExpression(string function)
    {
        var expected = new CallExpression(
            new Assign(
                new Variable("f"),
                new Lambda(
                    new[] { Variable.X.Name },
                    new Mul(Variable.X, Variable.X)).AsExpression()),
            Number.Two);

        ParseTest(function, expected);
    }
}