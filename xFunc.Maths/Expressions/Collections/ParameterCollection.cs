// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Numerics;

namespace xFunc.Maths.Expressions.Collections;

/// <summary>
/// Strongly typed dictionary that contains value of variables.
/// </summary>
public class ParameterCollection : IEnumerable<Parameter>, INotifyCollectionChanged
{
    private readonly Dictionary<string, Parameter> constants;
    private readonly Dictionary<string, Parameter> collection;

    /// <summary>
    /// Occurs when the collection changes.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollection"/> class.
    /// </summary>
    public ParameterCollection()
        : this(true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollection" /> class.
    /// </summary>
    /// <param name="initConstants">if set to <c>true</c> initialize constants.</param>
    public ParameterCollection(bool initConstants)
        : this(Array.Empty<Parameter>(), initConstants)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollection"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public ParameterCollection(IEnumerable<Parameter> parameters)
        : this(parameters, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollection"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public ParameterCollection(ParameterCollection parameters)
        : this(parameters?.collection.Values, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCollection" /> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="initConstants">if set to <c>true</c> initialize constants.</param>
    public ParameterCollection(IEnumerable<Parameter>? parameters, bool initConstants)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        constants = new Dictionary<string, Parameter>();
        collection = new Dictionary<string, Parameter>();
        foreach (var item in parameters)
            collection.Add(item.Key, item);

        if (initConstants)
            InitializeConstants();
    }

    /// <summary>
    /// Raises the <see cref="CollectionChanged" /> event.
    /// </summary>
    /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        => CollectionChanged?.Invoke(this, args);

    private void InitializeConstants()
    {
        // Archimedes' constant
        AddConstant(Parameter.Constant("π", AngleValue.Radian(Math.PI)));

        // Archimedes' constant
        AddConstant(Parameter.Constant("pi", AngleValue.Radian(Math.PI)));

        // Euler's number
        AddConstant(Parameter.Constant("e", Math.E));

        // Imaginary unit
        AddConstant(Parameter.Constant("i", Complex.ImaginaryOne));

        // Gravity on Earth
        AddConstant(Parameter.Constant("g", 9.80665));

        // Speed of Light (c0)
        AddConstant(Parameter.Constant("c", 299792458));

        // Planck Constant
        AddConstant(Parameter.Constant("h", 6.62607004E-34));

        // Faraday Constant
        AddConstant(Parameter.Constant("F", 96485.33289));

        // Electric Constant (ε0)
        AddConstant(Parameter.Constant("ε", 8.854187817E-12));

        // Magnetic constant (µ0)
        AddConstant(Parameter.Constant("µ", 1.2566370614E-6));

        // Gravitational constant
        AddConstant(Parameter.Constant("G", 6.64078E-11));

        // Feigenbaum constant
        AddConstant(Parameter.Constant("α", 2.5029078750958928222839));

        // Stefan-Boltzmann constant
        AddConstant(Parameter.Constant("σ", 5.670367E-8));

        // Euler–Mascheroni constant
        AddConstant(Parameter.Constant("γ", 0.57721566490153286060651));
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public IEnumerator<Parameter> GetEnumerator()
    {
        foreach (var (_, item) in constants)
            yield return item;

        foreach (var (_, item) in collection)
            yield return item;
    }

    /// <summary>
    /// Gets or sets the value of variable.
    /// </summary>
    /// <value>
    /// The value of parameter.
    /// </value>
    /// <param name="key">The name of variable.</param>
    /// <returns>The value of variable.</returns>
    public ParameterValue this[string key]
    {
        get => GetParameterByKey(key).Value;
        set
        {
            if (!collection.TryGetValue(key, out var param))
            {
                Add(key, value);
            }
            else
            {
                param.Value = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }

    private void AddConstant(Parameter parameter)
        => constants.Add(parameter.Key, parameter);

    private Parameter GetParameterByKey(string key)
    {
        if (collection.TryGetValue(key, out var param))
            return param;

        if (constants.TryGetValue(key, out param))
            return param;

        throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Resource.VariableNotFoundExceptionError, key));
    }

    /// <summary>
    /// Adds the specified element to a set.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    public void Add(Parameter param)
    {
        if (param is null)
            throw new ArgumentNullException(nameof(param));
        if (param.Type == ParameterType.Constant)
            throw new ArgumentException(Resource.ConstError);

        collection.Add(param.Key, param);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, param));
    }

    /// <summary>
    /// Adds the specified element to a set.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <param name="value">The value of variable.</param>
    public void Add(string key, ParameterValue value)
        => Add(new Parameter(key, value));

    /// <summary>
    /// Removes the specified element from this object.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    public void Remove(Parameter param)
    {
        if (param is null)
            throw new ArgumentNullException(nameof(param));
        if (param.Type == ParameterType.Constant)
            throw new ArgumentException(Resource.ConstError);

        collection.Remove(param.Key);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, param));
    }

    /// <summary>
    /// Removes the specified element from this object.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    public void Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));

        Remove(GetParameterByKey(key));
    }

    /// <summary>
    /// Clears this collection.
    /// </summary>
    public void Clear()
    {
        collection.Clear();
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    /// <summary>
    /// Determines whether an object contains the specified element.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
    public bool Contains(Parameter param)
    {
        if (param is null)
            throw new ArgumentNullException(nameof(param));

        return ContainsKey(param.Key);
    }

    /// <summary>
    /// Determines whether an object contains the specified key.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
    public bool ContainsKey(string key)
        => collection.ContainsKey(key) || constants.ContainsKey(key);
}