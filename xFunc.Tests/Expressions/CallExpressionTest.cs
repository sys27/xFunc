using System.Collections.Immutable;

namespace xFunc.Tests.Expressions;

public class CallExpressionTest
{
    [Fact]
    public void EqualsNullTest()
    {
        var lambda = new Lambda(Array.Empty<string>(), Number.One).AsExpression();
        var exp = new CallExpression(lambda, ImmutableArray<IExpression>.Empty);

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualsSameTest()
    {
        var lambda = new Lambda(Array.Empty<string>(), Number.One).AsExpression();
        var exp = new CallExpression(lambda, ImmutableArray<IExpression>.Empty);

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualsTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void NotEqualsParametersTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NotEqualsLambdaTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.Two).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.False(exp.Equals((object)null));
    }

    [Fact]
    public void EqualsObjectSameTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.True(exp.Equals((object)exp));
    }

    [Fact]
    public void EqualsObjectTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.True(exp1.Equals((object)exp2));
    }

    [Fact]
    public void NotEqualsDifferentTypesTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = Variable.X;

        Assert.False(exp1.Equals((object)exp2));
    }

    [Fact]
    public void NotEqualsObjectTest()
    {
        var exp1 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var exp2 = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        Assert.False(exp1.Equals((object)exp2));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<NotSupportedException>(() => exp.Execute());
    }

    [Fact]
    public void ExecuteWithNullParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ArgumentNullException>(() => exp.Execute(null));
    }

    [Fact]
    public void ExecuteWithNotFunctionTest()
    {
        var exp = new CallExpression(
            Number.One,
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute(new ExpressionParameters()));
    }

    [Fact]
    public void ExecuteWithoutParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        var result = exp.Execute(new ExpressionParameters());
        var expected = Number.One.Value;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteWithParametersTest()
    {
        var exp = new CallExpression(
            new Lambda(new[] { Variable.X.Name }, Variable.X).AsExpression(),
            new IExpression[] { Number.One }.ToImmutableArray());

        var result = exp.Execute(new ExpressionParameters());
        var expected = Number.One.Value;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new CallExpression(
            new Lambda(Array.Empty<string>(), Number.One).AsExpression(),
            ImmutableArray<IExpression>.Empty);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}