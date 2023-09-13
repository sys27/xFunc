// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Results;

public class LambdaResultTest
{
    [Test]
    public void TryGetLambdaTest()
    {
        var expected = Variable.X.ToLambda();
        var areaResult = new Result.LambdaResult(expected);
        var result = areaResult.TryGetLambda(out var lambdaValue);

        Assert.That(result, Is.True);
        Assert.That(lambdaValue, Is.EqualTo(expected));
    }

    [Test]
    public void TryGetLambdaFalseTest()
    {
        var areaResult = new Result.NumberResult(NumberValue.One);
        var result = areaResult.TryGetLambda(out var lambdaValue);

        Assert.That(result, Is.False);
        Assert.That(lambdaValue, Is.Null);
    }

    [Test]
    public void ToStringTest()
    {
        var lambda = new Lambda(new[] { "x" }, Variable.X);
        var result = new Result.LambdaResult(lambda);

        Assert.That(result.ToString(), Is.EqualTo("(x) => x"));
    }
}