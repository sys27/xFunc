namespace xFunc.Tests.Expressions;

public class LambdaTests
{
    [Fact]
    public void EqualsTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.True(f1.Equals(f2));
    }

    [Fact]
    public void NotEqualsTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.False(f1.Equals(f2));
    }

    [Fact]
    public void NotEqualsTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.False(f1.Equals(f2));
    }

    [Fact]
    public void EqualsObjectTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.True(f1.Equals(f2 as object));
    }

    [Fact]
    public void NotEqualsObjectTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.False(f1.Equals(f2 as object));
    }

    [Fact]
    public void NotEqualsObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.False(f1.Equals(f2 as object));
    }

    [Fact]
    public void NotEqualsDiffTypesObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = Variable.X;

        Assert.False(f1.Equals(f2 as object));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.True(f1 == f2);
    }

    [Fact]
    public void NotEqualsOperatorTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.True(f1 != f2);
    }

    [Fact]
    public void NotEqualsOperatorTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.True(f1 != f2);
    }

    [Fact]
    public void ToStringTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));

        Assert.Equal("(x, y) => x + y", function.ToString());
    }

    [Fact]
    public void CallTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));
        var parameters = new ExpressionParameters
        {
            { "x", new ParameterValue(1.0) },
            { "y", new ParameterValue(2.0) }
        };

        var result = function.Call(parameters);
        var expected = new NumberValue(3.0);

        Assert.Equal(expected, result);
    }
}