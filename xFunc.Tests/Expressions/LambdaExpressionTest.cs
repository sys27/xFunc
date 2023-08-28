namespace xFunc.Tests.Expressions;

public class LambdaExpressionTest
{
    [Test]
    public void EqualsNullTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda.Equals(null), Is.False);
    }

    [Test]
    public void EqualsSameTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda.Equals(lambda), Is.True);
    }

    [Test]
    public void EqualsTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda1.Equals(lambda2), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.Y)
            .AsExpression();

        Assert.That(lambda1.Equals(lambda2), Is.False);
    }

    [Test]
    public void EqualsObjectNullTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda.Equals((object)null), Is.False);
    }

    [Test]
    public void EqualsObjectSameTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda.Equals((object)lambda), Is.True);
    }

    [Test]
    public void EqualsObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.That(lambda1.Equals((object)lambda2), Is.True);
    }

    [Test]
    public void NotEqualsDifferentTypesTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = Variable.X;

        Assert.That(lambda1.Equals((object)lambda2), Is.False);
    }

    [Test]
    public void NotEqualsObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.Y)
            .AsExpression();

        Assert.That(lambda1.Equals((object)lambda2), Is.False);
    }

    [Test]
    public void NotEqualsParameterObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "y" }, Variable.X)
            .AsExpression();

        Assert.That(lambda1.Equals((object)lambda2), Is.False);
    }

    [Test]
    public void NotEqualsBodyObjectTest()
    {
        var lambda1 = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var lambda2 = new Lambda(new[] { "x" }, Variable.Y)
            .AsExpression();

        Assert.That(lambda1.Equals((object)lambda2), Is.False);
    }

    [Test]
    public void ExecuteTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        var result = exp.Execute();

        Assert.That(result, Is.EqualTo(exp.Lambda));
    }

    [Test]
    public void ExecuteWithParametersTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        var result = exp.Execute(new ExpressionParameters());

        Assert.That(result, Is.EqualTo(exp.Lambda));
    }

    [Test]
    public void NullAnalyzerTest1()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Test]
    public void NullAnalyzerTest2()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Test]
    public void CloneTest()
    {
        var exp = new Lambda(new[] { "x" }, Variable.X)
            .AsExpression();
        var clone = exp.Clone();

        Assert.That(clone, Is.EqualTo(exp));
    }
}