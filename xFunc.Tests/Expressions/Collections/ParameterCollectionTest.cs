// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Specialized;

namespace xFunc.Tests.Expressions.Collections;

public class ParameterCollectionTest
{
    [Fact]
    public void InitializeWithConstantsTest()
    {
        var parameters = new ParameterCollection(true);

        Assert.NotEmpty(parameters);
    }

    [Fact]
    public void InitializeWithoutConstantsTest()
    {
        var parameters = new ParameterCollection(false);

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

        Assert.Throws<ArgumentException>(() => new ParameterCollection(array));
    }

    [Fact]
    public void InitializeNullTest()
    {
        Assert.Throws<ArgumentNullException>(() => new ParameterCollection(null));
    }

    [Fact]
    public void ChangedEventTest()
    {
        var isExecuted = false;

        var parameters = new ParameterCollection(false);
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
        var parameters = new ParameterCollection(true);

        Assert.True(parameters.Any());
    }

    [Fact]
    public void AddNullParameter()
    {
        var parameters = new ParameterCollection(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Add(null as Parameter));
    }

    [Fact]
    public void AddConstantParameter()
    {
        var parameters = new ParameterCollection(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Fact]
    public void AddParameter()
    {
        var parameters = new ParameterCollection(true);
        var parameter = new Parameter("xxx", 1.0);

        parameters.Add(parameter);

        var result = parameters[parameter.Key];

        Assert.Equal(parameter.Value, result);
    }

    [Fact]
    public void RemoveNullParameter()
    {
        var parameters = new ParameterCollection(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(null as Parameter));
    }

    [Fact]
    public void RemoveConstantParameter()
    {
        var parameters = new ParameterCollection(true);
        var parameter = new Parameter("xxx", 1.0, ParameterType.Constant);

        Assert.Throws<ArgumentException>(() => parameters.Add(parameter));
    }

    [Fact]
    public void RemoveStringParameter()
    {
        var parameters = new ParameterCollection(true);

        Assert.Throws<ArgumentNullException>(() => parameters.Remove(string.Empty));
    }

    [Fact]
    public void GetItemFromCollectionTest()
    {
        var parameters = new ParameterCollection
        {
            new Parameter("x", 2.3)
        };

        Assert.Equal(new NumberValue(2.3), parameters["x"]);
    }

    [Fact]
    public void GetItemFromConstsTest()
    {
        var parameters = new ParameterCollection();

        Assert.Equal(AngleValue.Radian(Math.PI), parameters["π"]);
    }

    [Fact]
    public void GetItemKeyNotFoundTest()
    {
        var parameters = new ParameterCollection();

        Assert.Throws<KeyNotFoundException>(() => parameters["hello"]);
    }

    [Fact]
    public void SetItemFromCollectionTest()
    {
        var parameters = new ParameterCollection
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
        var parameters = new ParameterCollection();

        Assert.Throws<ArgumentNullException>(() => parameters.Contains(null));
    }

    [Fact]
    public void SetExistItemFromCollectionTest()
    {
        var parameters = new ParameterCollection
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
        var parameters = new ParameterCollection
        {
            new Parameter("hello", 2.5, ParameterType.ReadOnly)
        };

        Assert.Throws<ParameterIsReadOnlyException>(() => parameters["hello"] = 5);
    }

    [Fact]
    public void OverrideConstsTest()
    {
        var parameters = new ParameterCollection
        {
            ["π"] = 4
        };

        Assert.True(parameters.ContainsKey("π"));
        Assert.Equal(new NumberValue(4.0), parameters["π"]);
    }

    [Fact]
    public void OverrideRemoveTest()
    {
        var parameters = new ParameterCollection(false)
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
        var parameters = new ParameterCollection(false)
        {
            new Parameter("a", 1)
        };

        parameters.Clear();

        Assert.Empty(parameters);
    }
}