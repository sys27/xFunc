// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Specialized;

namespace xFunc.Tests.Expressions.Parameters;

public class ExpressionParameterTest
{
    [Test]
    public void NullCtorTest()
    {
        Assert.Throws<ArgumentNullException>(() => new ExpressionParameters(null as ExpressionParameters));
    }

    [Test]
    public void InitializeWithConstantsTest()
    {
        var parameters = new ExpressionParameters(true);

        Assert.That(parameters, Is.Not.Empty);
    }

    [Test]
    public void InitializeWithoutConstantsTest()
    {
        var parameters = new ExpressionParameters(false);

        Assert.That(parameters, Is.Empty);
    }

    [Test]
    public void InitializeDuplicatesTest()
    {
        var array = new[]
        {
            new Parameter("x", 1.0),
            new Parameter("x", 2.0),
        };

        Assert.Throws<ArgumentException>(() => new ExpressionParameters(array));
    }

    [Test]
    public void InitializeNullTest()
        => Assert.Throws<ArgumentNullException>(() => new ExpressionParameters(null));

    [Test]
    public void ChangedEventTest()
    {
        var isExecuted = false;

        var parameters = new ExpressionParameters(false);
        parameters.CollectionChanged += (_, args) =>
        {
            isExecuted = true;

            Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(args.OldItems, Is.Null);
            Assert.That(args.NewItems, Has.Count.EqualTo(1));
        };

        parameters.Add(new Parameter("xxx", 1.0));

        Assert.That(isExecuted);
    }

    [Test]
    public void EnumeratorTest()
    {
        var parameters = new ExpressionParameters(true);

        Assert.That(parameters.Any(), Is.True);
    }

    [Test]
    public void AddNullParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Add(null as Parameter));
    }

    [Test]
    public void AddConstantParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Test]
    public void AddParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0);

        parameters.Add(parameter);

        var result = parameters[parameter.Key];

        Assert.That(result, Is.EqualTo(parameter.Value));
    }

    [Test]
    public void RemoveNullParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(null as Parameter));
    }

    [Test]
    public void RemoveConstantParameter()
    {
        var parameters = new ExpressionParameters(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Test]
    public void RemoveStringParameter()
    {
        var parameters = new ExpressionParameters(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(string.Empty));
    }

    [Test]
    public void GetItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 2.3)
        };

        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(2.3)));
    }

    [Test]
    public void GetItemFromConstsTest()
    {
        var parameters = new ExpressionParameters();

        Assert.That(parameters["π"].Value, Is.EqualTo(AngleValue.Radian(Math.PI)));
    }

    [Test]
    public void GetItemKeyNotFoundTest()
    {
        var parameters = new ExpressionParameters();

        Assert.Throws<KeyNotFoundException>(() =>
        {
            var value = parameters["hello"].Value;
        });
    }

    [Test]
    public void SetItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            ["x"] = 2.3
        };

        Assert.That(parameters.ContainsKey("x"), Is.True);
        Assert.That(parameters, Contains.Item(new Parameter("x", 2.3)));
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(2.3)));
    }

    [Test]
    public void ContainsNullTest()
    {
        var parameters = new ExpressionParameters();

        Assert.Throws<ArgumentNullException>(() => parameters.Contains(null));
    }

    [Test]
    public void SetExistItemFromCollectionTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("x", 2.3)
        };
        parameters["x"] = new NumberValue(3.3);

        Assert.That(parameters.ContainsKey("x"), Is.True);
        Assert.That(parameters["x"].Value, Is.EqualTo(new NumberValue(3.3)));
    }

    [Test]
    public void SetReadOnlyItemTest()
    {
        var parameters = new ExpressionParameters
        {
            new Parameter("hello", 2.5, ParameterType.ReadOnly)
        };

        Assert.Throws<ParameterIsReadOnlyException>(() => parameters["hello"] = 5);
    }

    [Test]
    public void OverrideConstsTest()
    {
        var parameters = new ExpressionParameters
        {
            ["π"] = 4
        };

        Assert.That(parameters.ContainsKey("π"), Is.True);
        Assert.That(parameters["π"].Value, Is.EqualTo(new NumberValue(4.0)));
    }

    [Test]
    public void OverrideRemoveTest()
    {
        var parameters = new ExpressionParameters(false)
        {
            new Parameter("a", 1)
        };
        parameters["a"] = 2;
        parameters.Remove("a");

        Assert.That(parameters, Is.Empty);
    }

    [Test]
    public void ClearTest()
    {
        var parameters = new ExpressionParameters(false)
        {
            new Parameter("a", 1)
        };

        parameters.Clear();

        Assert.That(parameters, Is.Empty);
    }

    [Test]
    public void ScopedGetEnumerator()
    {
        var parameters = new ExpressionParameters(false)
        {
            { "x", new ParameterValue(1) }
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped["y"] = new ParameterValue(2);

        Assert.That(scoped.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ScopedGetMissingParameter()
    {
        var parameters = new ExpressionParameters(false);
        var scoped = ExpressionParameters.CreateScoped(parameters);

        Assert.Throws<KeyNotFoundException>(() =>
        {
            var x = scoped["x"];
        });
    }

    [Test]
    public void ScopedGetMissingWithParentNullParameter()
    {
        var scoped = ExpressionParameters.CreateScoped(null);

        Assert.Throws<KeyNotFoundException>(() =>
        {
            var x = scoped["x"];
        });
    }

    [Test]
    public void ScopedGetParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped["y"] = new ParameterValue(2);

        var result = scoped[parameter.Key];

        Assert.That(result, Is.EqualTo(parameter.Value));
    }

    [Test]
    public void ScopedGetCurrentValue()
    {
        var parameters = new ExpressionParameters(false);
        var scoped = ExpressionParameters.CreateScoped(parameters);
        var parameter = new Parameter("x", new ParameterValue(1));
        scoped[parameter.Key] = parameter.Value;

        var result = scoped[parameter.Key];

        Assert.That(result, Is.EqualTo(parameter.Value));
    }

    [Test]
    public void ScopedSetActualValue()
    {
        const string key = "x";
        var parent = new ExpressionParameters(false)
        {
            [key] = new ParameterValue(NumberValue.One)
        };

        var scoped = ExpressionParameters.CreateScoped(parent);
        scoped.Add(key, new ParameterValue(NumberValue.One));
        scoped[key] = new ParameterValue(NumberValue.Two);

        var result = scoped[key];
        var parentResult = parent[key];

        Assert.That(result, Is.EqualTo(new ParameterValue(NumberValue.Two)));
        Assert.That(parentResult, Is.EqualTo(new ParameterValue(NumberValue.One)));
    }

    [Test]
    public void ScopedSetParentValue()
    {
        const string key = "x";

        var parent = new ExpressionParameters(false)
        {
            [key] = new ParameterValue(NumberValue.One)
        };

        var scoped = ExpressionParameters.CreateScoped(parent);
        scoped[key] = new ParameterValue(NumberValue.Two);

        var result = scoped[key];
        var parentResult = parent[key];

        Assert.That(result, Is.EqualTo(new ParameterValue(NumberValue.Two)));
        Assert.That(parentResult, Is.EqualTo(new ParameterValue(NumberValue.Two)));
    }

    [Test]
    public void ScopedContainsParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped["y"] = new ParameterValue(2);

        var result = scoped.Contains(parameter);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ScopedContainsParentNullValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var scoped = ExpressionParameters.CreateScoped(null);

        var result = scoped.Contains(parameter);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ScopedDoesntContainsParentValue()
    {
        var parameter = new Parameter("x", new ParameterValue(1));
        var parameters = new ExpressionParameters(false)
        {
            parameter
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped["y"] = new ParameterValue(2);

        var result = scoped.Contains(new Parameter("z", new ParameterValue(1)));

        Assert.That(result, Is.False);
    }

    [Test]
    public void ScopedContainsValue()
    {
        var x = new Parameter("x", new ParameterValue(1));
        var y = new Parameter("y", new ParameterValue(2));

        var parameters = new ExpressionParameters(false)
        {
            x
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped.Add(y);

        var result = scoped.Contains(y);

        Assert.That(result);
    }

    [Test]
    public void ScopedDoesntContainsValue()
    {
        var x = new Parameter("x", new ParameterValue(1));
        var y = new Parameter("y", new ParameterValue(2));

        var parameters = new ExpressionParameters(false)
        {
            x
        };
        var scoped = ExpressionParameters.CreateScoped(parameters);
        scoped.Add(y);

        var result = scoped.Contains(new Parameter("z", new ParameterValue(1)));

        Assert.That(result, Is.False);
    }

    [Test]
    public void ScopedContainsKeyParentNullValue()
    {
        var scoped = ExpressionParameters.CreateScoped(null);

        var result = scoped.ContainsKey("x");

        Assert.That(result, Is.False);
    }

    [Test]
    [TestCase("add", typeof(Add))]
    [TestCase("sub", typeof(Sub))]
    [TestCase("mul", typeof(Mul))]
    [TestCase("div", typeof(Div))]
    [TestCase("pow", typeof(Pow))]
    [TestCase("exp", typeof(Exp))]
    [TestCase("abs", typeof(Abs))]
    [TestCase("sqrt", typeof(Sqrt))]
    [TestCase("root", typeof(Root))]
    [TestCase("fact", typeof(Fact))]
    [TestCase("factorial", typeof(Fact))]
    [TestCase("ln", typeof(Ln))]
    [TestCase("lg", typeof(Lg))]
    [TestCase("lb", typeof(Lb))]
    [TestCase("log2", typeof(Lb))]
    [TestCase("log", typeof(Log))]
    [TestCase("todeg", typeof(ToDegree))]
    [TestCase("todegree", typeof(ToDegree))]
    [TestCase("torad", typeof(ToRadian))]
    [TestCase("toradian", typeof(ToRadian))]
    [TestCase("tograd", typeof(ToGradian))]
    [TestCase("togradian", typeof(ToGradian))]
    [TestCase("sin", typeof(Sin))]
    [TestCase("cos", typeof(Cos))]
    [TestCase("tan", typeof(Tan))]
    [TestCase("tg", typeof(Tan))]
    [TestCase("cot", typeof(Cot))]
    [TestCase("ctg", typeof(Cot))]
    [TestCase("sec", typeof(Sec))]
    [TestCase("cosec", typeof(Csc))]
    [TestCase("csc", typeof(Csc))]
    [TestCase("arcsin", typeof(Arcsin))]
    [TestCase("arccos", typeof(Arccos))]
    [TestCase("arctan", typeof(Arctan))]
    [TestCase("arctg", typeof(Arctan))]
    [TestCase("arccot", typeof(Arccot))]
    [TestCase("arcctg", typeof(Arccot))]
    [TestCase("arcsec", typeof(Arcsec))]
    [TestCase("arccosec", typeof(Arccsc))]
    [TestCase("arccsc", typeof(Arccsc))]
    [TestCase("sh", typeof(Sinh))]
    [TestCase("sinh", typeof(Sinh))]
    [TestCase("ch", typeof(Cosh))]
    [TestCase("cosh", typeof(Cosh))]
    [TestCase("th", typeof(Tanh))]
    [TestCase("tanh", typeof(Tanh))]
    [TestCase("cth", typeof(Coth))]
    [TestCase("coth", typeof(Coth))]
    [TestCase("sech", typeof(Sech))]
    [TestCase("csch", typeof(Csch))]
    [TestCase("arsh", typeof(Arsinh))]
    [TestCase("arsinh", typeof(Arsinh))]
    [TestCase("arch", typeof(Arcosh))]
    [TestCase("arcosh", typeof(Arcosh))]
    [TestCase("arth", typeof(Artanh))]
    [TestCase("artanh", typeof(Artanh))]
    [TestCase("arcth", typeof(Arcoth))]
    [TestCase("arcoth", typeof(Arcoth))]
    [TestCase("arsch", typeof(Arsech))]
    [TestCase("arsech", typeof(Arsech))]
    [TestCase("arcsch", typeof(Arcsch))]
    [TestCase("round", typeof(Round))]
    [TestCase("floor", typeof(Floor))]
    [TestCase("ceil", typeof(Ceil))]
    [TestCase("trunc", typeof(Trunc))]
    [TestCase("truncate", typeof(Trunc))]
    [TestCase("frac", typeof(Frac))]
    [TestCase("transpose", typeof(Transpose))]
    [TestCase("det", typeof(Determinant))]
    [TestCase("determinant", typeof(Determinant))]
    [TestCase("inverse", typeof(Inverse))]
    [TestCase("dotproduct", typeof(DotProduct))]
    [TestCase("crossproduct", typeof(CrossProduct))]
    [TestCase("im", typeof(Im))]
    [TestCase("imaginary", typeof(Im))]
    [TestCase("re", typeof(Re))]
    [TestCase("real", typeof(Re))]
    [TestCase("phase", typeof(Phase))]
    [TestCase("conjugate", typeof(Conjugate))]
    [TestCase("reciprocal", typeof(Reciprocal))]
    [TestCase("tocomplex", typeof(ToComplex))]
    [TestCase("sign", typeof(Sign))]
    [TestCase("tobin", typeof(ToBin))]
    [TestCase("tooct", typeof(ToOct))]
    [TestCase("tohex", typeof(ToHex))]
    [TestCase("tonumber", typeof(ToNumber))]
    public void LambdaParametersTest(string function, Type type)
    {
        var parameters = new ExpressionParameters();
        var lambda = (Lambda)parameters[function].Value;

        Assert.That(lambda.Body, Is.InstanceOf(type));
    }
}