// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Parameters;

public class ParameterValueTest
{
    [Fact]
    public void EqualNullTest()
    {
        var value = new ParameterValue(1);

        Assert.False(value.Equals(null as object));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(1) as object;

        Assert.True(x.Equals(y));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(1);

        Assert.True(x == y);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var x = new ParameterValue(1);
        var y = new ParameterValue(2);

        Assert.True(x != y);
    }
}