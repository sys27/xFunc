// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Tests.ParserTests;

public class AssignTests : BaseParserTests
{
    [Fact]
    public void DefTest()
        => ParseTest("assign(x, 2)", new Assign(Variable.X, Number.Two));

    [Theory]
    [InlineData("assign x, 2)")]
    [InlineData("assign(, 2)")]
    [InlineData("assign(x 2)")]
    [InlineData("assign(x,)")]
    [InlineData("assign(x, 2")]
    public void DefMissingOpenParen(string function)
        => ParseErrorTest(function);

    [Fact]
    public void ParseDefine()
    {
        var expected = new Assign(Variable.X, new Number(3));

        ParseTest("x := 3", expected);
    }

    [Fact]
    public void ParseDefineFirstParamIsNotVar()
        => ParseErrorTest("5 := 3");

    [Fact]
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

    [Fact]
    public void DefineUserFuncTest()
    {
        var expected = new Assign(
            new Variable("f"),
            new Lambda(new[] { Variable.X.Name }, Variable.X).AsExpression());

        ParseTest("f := (x) => x", expected);
    }

    [Fact]
    public void UnaryMinusAssignTest()
    {
        var expected = new Assign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x := -sin(2)", expected);
    }

    [Fact]
    public void AddAssign()
    {
        var expected = new AddAssign(Variable.X, Number.Two);

        ParseTest("x += 2", expected);
    }

    [Fact]
    public void UnaryMinusAddAssignTest()
    {
        var expected = new AddAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x += -sin(2)", expected);
    }

    [Fact]
    public void AddAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new AddAssign(Variable.X, Number.Two));

        ParseTest("1 + (x += 2)", expected);
    }

    [Fact]
    public void SubAssign()
    {
        var expected = new SubAssign(Variable.X, Number.Two);

        ParseTest("x -= 2", expected);
    }

    [Fact]
    public void UnaryMinusSubAssignTest()
    {
        var expected = new SubAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x -= -sin(2)", expected);
    }

    [Fact]
    public void SubAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new SubAssign(Variable.X, Number.Two));

        ParseTest("1 + (x -= 2)", expected);
    }

    [Fact]
    public void MulAssign()
    {
        var expected = new MulAssign(Variable.X, Number.Two);

        ParseTest("x *= 2", expected);
    }

    [Fact]
    public void UnaryMinusMulAssignTest()
    {
        var expected = new MulAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x *= -sin(2)", expected);
    }

    [Fact]
    public void MulAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new MulAssign(Variable.X, Number.Two));

        ParseTest("1 + (x *= 2)", expected);
    }

    [Fact]
    public void DivAssign()
    {
        var expected = new DivAssign(Variable.X, Number.Two);

        ParseTest("x /= 2", expected);
    }

    [Fact]
    public void UnaryMinusDivAssignTest()
    {
        var expected = new DivAssign(
            Variable.X,
            new UnaryMinus(new Sin(Number.Two))
        );

        ParseTest("x /= -sin(2)", expected);
    }

    [Fact]
    public void DivAssignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new DivAssign(Variable.X, Number.Two));

        ParseTest("1 + (x /= 2)", expected);
    }

    [Fact]
    public void LeftShiftAssignTest()
    {
        var expected = new LeftShiftAssign(Variable.X, new Number(10));

        ParseTest("x <<= 10", expected);
    }

    [Fact]
    public void LeftShiftAssignAsExpressionTest()
    {
        var expected = new Add(
            Number.One,
            new LeftShiftAssign(Variable.X, new Number(10)));

        ParseTest("1 + (x <<= 10)", expected);
    }

    [Fact]
    public void RightShiftAssignTest()
    {
        var expected = new RightShiftAssign(Variable.X, new Number(10));

        ParseTest("x >>= 10", expected);
    }

    [Fact]
    public void RightShiftAssignAsExpressionTest()
    {
        var expected = new Add(
            Number.One,
            new RightShiftAssign(Variable.X, new Number(10)));

        ParseTest("1 + (x >>= 10)", expected);
    }

    [Theory]
    [InlineData("x :=")]
    [InlineData("x +=")]
    [InlineData("x -=")]
    [InlineData("x −=")]
    [InlineData("x *=")]
    [InlineData("x ×=")]
    [InlineData("x /=")]
    [InlineData("x <<=")]
    [InlineData("x >>=")]
    public void AssignMissingValue(string function)
        => ParseErrorTest(function);

    [Theory]
    [InlineData("1 + x += 1")]
    [InlineData("1 + x -= 1")]
    [InlineData("1 + x *= 1")]
    [InlineData("1 + x /= 1")]
    [InlineData("1 + x <<= 1")]
    [InlineData("1 + x >>= 1")]
    public void AssignAsExpressionError(string function)
        => ParseErrorTest(function);

    [Fact]
    public void IncTest()
        => ParseTest("x++", new Inc(Variable.X));

    [Fact]
    public void IncAsExpression()
    {
        var expected = new Add(Number.One, new Inc(Variable.X));

        ParseTest("1 + x++", expected);
    }

    [Theory]
    [InlineData("x--")]
    [InlineData("x−−")]
    public void DecTest(string function)
    {
        var expected = new Dec(Variable.X);

        ParseTest(function, expected);
    }

    [Fact]
    public void DecAsExpression()
    {
        var expected = new Add(Number.One, new Dec(Variable.X));

        ParseTest("1 + x--", expected);
    }

    [Theory]
    [InlineData("1 + (x := 2)")]
    [InlineData("1 + assign(x, 2)")]
    public void AssignAsExpression(string function)
    {
        var expected = new Add(
            Number.One,
            new Assign(Variable.X, Number.Two));

        ParseTest(function, expected);
    }

    [Fact]
    public void UnassignAsExpression()
    {
        var expected = new Add(
            Number.One,
            new Unassign(Variable.X));

        ParseTest("1 + unassign(x)", expected);
    }

    [Theory]
    [InlineData("(f := (x) => x * x)(2)")]
    [InlineData("(assign(f, (x) => x * x))(2)")]
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