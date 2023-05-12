using System.Collections.Immutable;

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

    [Fact]
    public void ParseInlineTest()
    {
        var expected = new CallExpression(
            new Lambda(
                    new[] { "x" },
                    new Sin(Variable.X))
                .AsExpression(),
            new IExpression[] { new Number(90) }.ToImmutableArray());

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
            new IExpression[] { Number.One, Number.Two }.ToImmutableArray());

        ParseTest("((x, y) => x + y)(1, 2)", expected);
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
}