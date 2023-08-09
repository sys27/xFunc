using System.Collections.Immutable;
using NSubstitute;

namespace xFunc.Tests.ParserTests;

public class LambdaTests : BaseParserTests
{
    [Theory]
    [InlineData("(x) -> x")]
    [InlineData("(x) −> x")]
    [InlineData("(x) => x")]
    [InlineData("((x) => x)")]
    public void ParseLambdaWithOneParameterTest(string function)
    {
        var expected = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        ParseTest(function, expected);
    }

    [Fact]
    public void ParseLambdaWithoutParametersTest()
    {
        var expected = new Lambda(Array.Empty<string>(), Number.One)
            .AsExpression();

        ParseTest("() => 1", expected);
    }

    [Fact]
    public void ParseLambdaWithTwoParametersTest()
    {
        var expected = new Lambda(
                new[] { "x", "y" },
                new Add(Variable.X, Variable.Y))
            .AsExpression();

        ParseTest("(x, y) => x + y", expected);
    }

    [Fact]
    public void ParseLambdaOfLambdaTest()
    {
        var expected = new Lambda(
                new[] { "x" },
                new Lambda(
                        new[] { "y" },
                        new Add(Variable.X, Variable.Y))
                    .AsExpression())
            .AsExpression();

        ParseTest("(x) => (y) => x + y", expected);
    }

    [Theory]
    [InlineData("(x, ")]
    [InlineData("(x, y")]
    [InlineData("(x, y)")]
    [InlineData("(x, y) =>")]
    [InlineData("(x, x) => x + x")]
    [InlineData("(x,) => x")]
    public void ParseLambdaErrorTests(string function)
        => ParseErrorTest(function);

    [Fact]
    public void ParseInlineTest()
    {
        var expected = new CallExpression(
            new Lambda(
                    new[] { "x" },
                    new Sin(Variable.X))
                .AsExpression(),
            new Number(90));

        ParseTest("((x) => sin(x))(90)", expected);
    }

    [Fact]
    public void ParseInlineWithoutParametersTest()
    {
        var expected = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        ParseTest("(() => 1)()", expected);
    }

    [Fact]
    public void ParseInlineTwoParametersTest()
    {
        var expected = new CallExpression(
            new Lambda(
                    new[] { "x", "y" },
                    new Add(Variable.X, Variable.Y))
                .AsExpression(),
            Number.One,
            Number.Two);

        ParseTest("((x, y) => x + y)(1, 2)", expected);
    }

    [Fact]
    public void ParseFunctionWithCallExpression()
    {
        var simplifier = Substitute.For<ISimplifier>();
        var expected = new CallExpression(
            new Simplify(
                simplifier,
                new Mul(Variable.X, Variable.X).ToLambdaExpression(Variable.X.Name)),
            Number.One);

        ParseTest("simplify((x) => x * x)(1)", expected);
    }

    [Fact]
    public void ParseNestedCallExpression()
    {
        var expected = new CallExpression(
            new CallExpression(
                new CallExpression(new Variable("f"), new Variable("g")),
                Number.One),
            Number.Two);

        ParseTest("f(g)(1)(2)", expected);
    }

    [Fact]
    public void ParseComplexCallExpression()
    {
        var expected = new Add(
            Number.One,
            new CallExpression(
                new CallExpression(new Variable("f"), new Variable("g")),
                Number.One));

        ParseTest("1 + f(g)(1)", expected);
    }
}