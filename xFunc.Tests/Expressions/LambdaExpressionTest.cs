namespace xFunc.Tests.Expressions;

public class LambdaExpressionTest
{
    [Fact]
    public void EqualsNullTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.False(lambda.Equals(null));
    }

    [Fact]
    public void EqualsSameTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.True(lambda.Equals(lambda));
    }

    [Fact]
    public void EqualsTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.True(lambda1.Equals(lambda2));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.Y)
            .AsExpression();

        Assert.False(lambda1.Equals(lambda2));
    }

    [Fact]
    public void EqualsObjectNullTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.False(lambda.Equals((object)null));
    }

    [Fact]
    public void EqualsObjectSameTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.True(lambda.Equals((object)lambda));
    }

    [Fact]
    public void EqualsObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.True(lambda1.Equals((object)lambda2));
    }

    [Fact]
    public void NotEqualsDifferentTypesTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = Variable.X;

        Assert.False(lambda1.Equals((object)lambda2));
    }

    [Fact]
    public void NotEqualsObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.Y)
            .AsExpression();

        Assert.False(lambda1.Equals((object)lambda2));
    }

    [Fact]
    public void NotEqualsParameterObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.X)
            .AsExpression();

        Assert.False(lambda1.Equals((object)lambda2));
    }

    [Fact]
    public void NotEqualsBodyObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.Y)
            .AsExpression();

        Assert.False(lambda1.Equals((object)lambda2));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        var result = exp.Execute();

        Assert.Equal(exp.Lambda, result);
    }

    [Fact]
    public void ExecuteWithParametersTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        var result = exp.Execute(new ExpressionParameters());

        Assert.Equal(exp.Lambda, result);
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}