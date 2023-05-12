// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Specialized;

namespace xFunc.Tests.Expressions.Parameters;

public class ExpressionParameterTest
{
    [Fact]
    public void NullCtorTest()
    {
        Assert.Throws<ArgumentNullException>(() => new ExpressionParameters(null as ExpressionParameters));
    }

    [Fact]
    public void InitializeWithConstantsTest()
    {
        var parameters = new ExpressionParameters(true);

        Assert.NotEmpty(parameters);
    }

    [Fact]
    public void InitializeWithoutConstantsTest()
    {
        var parameters = new ExpressionParameters(false);

        Assert.Empty(parameters);
    }

    [Fact]
    public void InitializeDuplicatesTest()
    {
        var array = new[]
        {
            new Parameter("x", 1.0),
            new Parameter("x", 2.0),
        };

        Assert.Throws<ArgumentException>(() => new ExpressionParameters(array));
    }

    [Fact]
    public void InitializeNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new ExpressionParameters(null));
    }

    [Fact]
    public void ChangedEventTest()
    {
        var isExecuted = false;

        var parameters = new ExpressionParameters(false);
        parameters.CollectionChanged += (sender, args) =>
        {
            isExecuted = true;

            Assert.Equal(NotifyCollectionChangedAction.Add, args.Action);
            Assert.Null(args.OldItems);
            Assert.Equal(1, args.NewItems.Count);
        };

        parameters.Add(new Parameter("xxx", 1.0));

        Assert.True(isExecuted);
    }

    [Fact]
    public void EnumeratorTest()
    {
        var parameters = new ExpressionParameters(true);

        Assert.True(parameters.Any());
    }

    [Fact]
    public void AddNullParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Add(null as Parameter));
    }

    [Fact]
    public void AddConstantParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Fact]
    public void AddParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0);

        parameters.Add(parameter);

        var result = parameters[parameter.Key];

        Assert.Equal(parameter.Value, result);
    }

    [Fact]
    public void RemoveNullParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(null as Parameter));
    }

    [Fact]
    public void RemoveConstantParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Fact]
    public void RemoveStringParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(string.Empty));
    }

    [Fact]
    public void GetItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 2.3)
        };

        Assert.Equal(new NumberValue(2.3), parameters["x"]);
    }

    [Fact]
    public void GetItemFromConstsTest()
    {
        var parameters = new ExpressionParameters();

        Assert.Equal(AngleValue.Radian(Math.PI), parameters["π"]);
    }

    [Fact]
    public void GetItemKeyNotFoundTest()
    {
        var parameters = new ExpressionParameters();

        Assert.Throws<KeyNotFoundException>(() => parameters["hello"]);
    }

    [Fact]
    public void SetItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            ["x"] = 2.3
        };

        Assert.True(parameters.ContainsKey("x"));
        Assert.True(parameters.Contains(new Parameter("x", 2.3)));
        Assert.Equal(new NumberValue(2.3), parameters["x"]);
    }

    [Fact]
    public void ContainsNullTest()
    {
        var parameters = new ExpressionParameters();

        Assert.Throws<ArgumentNullException>(() => parameters.Contains(null));
    }

    [Fact]
    public void SetExistItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 2.3)
        };
        parameters["x"] = new NumberValue(3.3);

        Assert.True(parameters.ContainsKey("x"));
        Assert.Equal(new NumberValue(3.3), parameters["x"]);
    }

    [Fact]
    public void SetReadOnlyItemTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("hello", 2.5, ParameterType.ReadOnly)
        };

        Assert.Throws<ParameterIsReadOnlyException>(() => parameters["hello"] = 5);
    }

    [Fact]
    public void OverrideConstsTest()
    {
        var parameters = new ExpressionParameters
        {
            ["π"] = 4
        };

        Assert.True(parameters.ContainsKey("π"));
        Assert.Equal(new NumberValue(4.0), parameters["π"]);
    }

    [Fact]
    public void OverrideRemoveTest()
    {
        var parameters = new ExpressionParameters(false)
        {
            new Parameter("a", 1)
        };
        parameters["a"] = 2;
        parameters.Remove("a");

        Assert.Empty(parameters);
    }

    [Fact]
    public void ClearTest()
    {
        var parameters = new ExpressionParameters(false)
        {
            new Parameter("a", 1)
        };

        parameters.Clear();

        Assert.Empty(parameters);
    }

    [Fact]
    public void ScopedGetEnumerator()
    {
        var parameters = new ExpressionParameters(false)
        {
            { "x", new ParameterValue(1) }
        };
        var scoped = parameters.CreateScope();
        scoped["y"] = new ParameterValue(2);

        var count = scoped.Count();

        Assert.Equal(2, count);
    }

    [Fact]
    public void ScopedGetParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = parameters.CreateScope();
        scoped["y"] = new ParameterValue(2);

        var result = scoped[parameter.Key];

        Assert.Equal(parameter.Value, result);
    }

    [Fact]
    public void ScopedContainsParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = parameters.CreateScope();
        scoped["y"] = new ParameterValue(2);

        var result = scoped.Contains(parameter);

        Assert.True(result);
    }

    [Fact]
    public void ScopedDoesntContainsParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = parameters.CreateScope();
        scoped["y"] = new ParameterValue(2);

        var result = scoped.Contains(new Parameter("z", new ParameterValue(1)));

        Assert.False(result);
    }

    [Fact]
    public void ScopedContainsValue()
    {
        var x = new Parameter("x", new ParameterValue(1));
        var y = new Parameter("y", new ParameterValue(2));

        var parameters = new ExpressionParameters(false)
        {
            x
        };
        var scoped = parameters.CreateScope();
        scoped.Add(y);

        var result = scoped.Contains(y);

        Assert.True(result);
    }

    [Fact]
    public void ScopedDoesntContainsValue()
    {
        var x = new Parameter("x", new ParameterValue(1));
        var y = new Parameter("y", new ParameterValue(2));

        var parameters = new ExpressionParameters(false)
        {
            x
        };
        var scoped = parameters.CreateScope();
        scoped.Add(y);

        var result = scoped.Contains(new Parameter("z", new ParameterValue(1)));

        Assert.False(result);
    }

    [Theory]
    [InlineData("add", typeof(Add))]
    [InlineData("sub", typeof(Sub))]
    [InlineData("mul", typeof(Mul))]
    [InlineData("div", typeof(Div))]
    [InlineData("pow", typeof(Pow))]
    [InlineData("exp", typeof(Exp))]
    [InlineData("abs", typeof(Abs))]
    [InlineData("sqrt", typeof(Sqrt))]
    [InlineData("root", typeof(Root))]
    [InlineData("fact", typeof(Fact))]
    [InlineData("factorial", typeof(Fact))]
    [InlineData("ln", typeof(Ln))]
    [InlineData("lg", typeof(Lg))]
    [InlineData("lb", typeof(Lb))]
    [InlineData("log2", typeof(Lb))]
    [InlineData("log", typeof(Log))]
    [InlineData("todeg", typeof(ToDegree))]
    [InlineData("todegree", typeof(ToDegree))]
    [InlineData("torad", typeof(ToRadian))]
    [InlineData("toradian", typeof(ToRadian))]
    [InlineData("tograd", typeof(ToGradian))]
    [InlineData("togradian", typeof(ToGradian))]
    [InlineData("sin", typeof(Sin))]
    [InlineData("cos", typeof(Cos))]
    [InlineData("tan", typeof(Tan))]
    [InlineData("tg", typeof(Tan))]
    [InlineData("cot", typeof(Cot))]
    [InlineData("ctg", typeof(Cot))]
    [InlineData("sec", typeof(Sec))]
    [InlineData("cosec", typeof(Csc))]
    [InlineData("csc", typeof(Csc))]
    [InlineData("arcsin", typeof(Arcsin))]
    [InlineData("arccos", typeof(Arccos))]
    [InlineData("arctan", typeof(Arctan))]
    [InlineData("arctg", typeof(Arctan))]
    [InlineData("arccot", typeof(Arccot))]
    [InlineData("arcctg", typeof(Arccot))]
    [InlineData("arcsec", typeof(Arcsec))]
    [InlineData("arccosec", typeof(Arccsc))]
    [InlineData("arccsc", typeof(Arccsc))]
    [InlineData("sh", typeof(Sinh))]
    [InlineData("sinh", typeof(Sinh))]
    [InlineData("ch", typeof(Cosh))]
    [InlineData("cosh", typeof(Cosh))]
    [InlineData("th", typeof(Tanh))]
    [InlineData("tanh", typeof(Tanh))]
    [InlineData("cth", typeof(Coth))]
    [InlineData("coth", typeof(Coth))]
    [InlineData("sech", typeof(Sech))]
    [InlineData("csch", typeof(Csch))]
    [InlineData("arsh", typeof(Arsinh))]
    [InlineData("arsinh", typeof(Arsinh))]
    [InlineData("arch", typeof(Arcosh))]
    [InlineData("arcosh", typeof(Arcosh))]
    [InlineData("arth", typeof(Artanh))]
    [InlineData("artanh", typeof(Artanh))]
    [InlineData("arcth", typeof(Arcoth))]
    [InlineData("arcoth", typeof(Arcoth))]
    [InlineData("arsch", typeof(Arsech))]
    [InlineData("arsech", typeof(Arsech))]
    [InlineData("arcsch", typeof(Arcsch))]
    [InlineData("round", typeof(Round))]
    [InlineData("floor", typeof(Floor))]
    [InlineData("ceil", typeof(Ceil))]
    [InlineData("trunc", typeof(Trunc))]
    [InlineData("truncate", typeof(Trunc))]
    [InlineData("frac", typeof(Frac))]
    [InlineData("transpose", typeof(Transpose))]
    [InlineData("det", typeof(Determinant))]
    [InlineData("determinant", typeof(Determinant))]
    [InlineData("inverse", typeof(Inverse))]
    [InlineData("dotproduct", typeof(DotProduct))]
    [InlineData("crossproduct", typeof(CrossProduct))]
    [InlineData("im", typeof(Im))]
    [InlineData("imaginary", typeof(Im))]
    [InlineData("re", typeof(Re))]
    [InlineData("real", typeof(Re))]
    [InlineData("phase", typeof(Phase))]
    [InlineData("conjugate", typeof(Conjugate))]
    [InlineData("reciprocal", typeof(Reciprocal))]
    [InlineData("tocomplex", typeof(ToComplex))]
    [InlineData("sign", typeof(Sign))]
    [InlineData("tobin", typeof(ToBin))]
    [InlineData("tooct", typeof(ToOct))]
    [InlineData("tohex", typeof(ToHex))]
    public void LambdaParametersTest(string function, Type type)
    {
        var parameters = new ExpressionParameters();
        var lambda = (Lambda)parameters[function].Value;

        Assert.IsType(type, lambda.Body);
    }
}