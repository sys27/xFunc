namespace xFunc.Tests.Expressions;

public class LambdaTests
{
    [Test]
    public void EqualsTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2), Is.True);
    }

    [Test]
    public void NotEqualsTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2), Is.False);
    }

    [Test]
    public void NotEqualsTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1.Equals(f2), Is.False);
    }

    [Test]
    public void EqualsObjectTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2 as object), Is.True);
    }

    [Test]
    public void NotEqualsObjectTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void NotEqualsObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void NotEqualsDiffTypesObjectTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = Variable.X;

        Assert.That(f1.Equals(f2 as object), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Sin(Variable.X));

        Assert.That(f1 == f2, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest1()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x", "y" }, new Sin(Variable.X));

        Assert.That(f1 != f2, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest2()
    {
        var f1 = new Lambda(new[] { "x" }, new Sin(Variable.X));
        var f2 = new Lambda(new[] { "x" }, new Add(Variable.X, Variable.Y));

        Assert.That(f1 != f2, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var function = new Lambda(
            new[] { "x", "y" },
            new Add(Variable.X, Variable.Y));

        Assert.That(function.ToString(), Is.EqualTo("(x, y) => x + y"));
    }

    [Test]
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

        Assert.That(result, Is.EqualTo(expected));
    }
}