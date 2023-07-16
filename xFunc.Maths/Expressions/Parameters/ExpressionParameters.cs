// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Strongly typed collection that contains parameters.
/// </summary>
public partial class ExpressionParameters : IExpressionParameters, INotifyCollectionChanged
{
    private static readonly Dictionary<string, Parameter> Constants;
    private readonly Dictionary<string, Parameter> collection;
    private readonly bool withConstants;

    static ExpressionParameters()
    {
        Constants = new Dictionary<string, Parameter>();

        InitializeConstants();
    }

    /// <summary>
    /// Occurs when the collection changes.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    public ExpressionParameters()
        : this(true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters" /> class.
    /// </summary>
    /// <param name="initConstants">if set to <c>true</c> initialize constants.</param>
    public ExpressionParameters(bool initConstants)
        : this(Array.Empty<Parameter>(), initConstants)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public ExpressionParameters(IEnumerable<Parameter> parameters)
        : this(parameters, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    public ExpressionParameters(ExpressionParameters parameters)
        : this(parameters?.collection.Values, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionParameters" /> class.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="initConstants">if set to <c>true</c> initialize constants.</param>
    public ExpressionParameters(IEnumerable<Parameter>? parameters, bool initConstants)
    {
        if (parameters is null)
            throw new ArgumentNullException(nameof(parameters));

        collection = new Dictionary<string, Parameter>();
        foreach (var item in parameters)
            collection.Add(item.Key, item);

        withConstants = initConstants;
    }

    /// <summary>
    /// Raises the <see cref="CollectionChanged" /> event.
    /// </summary>
    /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        => CollectionChanged?.Invoke(this, args);

    private static void InitializeConstants()
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

        // functions
        AddConstant(Parameter.Constant("add", Expressions.Add.Lambda));
        AddConstant(Parameter.Constant("sub", Sub.Lambda));
        AddConstant(Parameter.Constant("mul", Mul.Lambda));
        AddConstant(Parameter.Constant("div", Div.Lambda));
        AddConstant(Parameter.Constant("pow", Pow.Lambda));
        AddConstant(Parameter.Constant("exp", Exp.Lambda));
        AddConstant(Parameter.Constant("abs", Abs.Lambda));
        AddConstant(Parameter.Constant("sqrt", Sqrt.Lambda));
        AddConstant(Parameter.Constant("root", Root.Lambda));

        AddConstant(Parameter.Constant("fact", Fact.Lambda));
        AddConstant(Parameter.Constant("factorial", Fact.Lambda));

        AddConstant(Parameter.Constant("ln", Ln.Lambda));
        AddConstant(Parameter.Constant("lg", Lg.Lambda));
        AddConstant(Parameter.Constant("lb", Lb.Lambda));
        AddConstant(Parameter.Constant("log2", Lb.Lambda));
        AddConstant(Parameter.Constant("log", Log.Lambda));

        AddConstant(Parameter.Constant("todeg", ToDegree.Lambda));
        AddConstant(Parameter.Constant("todegree", ToDegree.Lambda));
        AddConstant(Parameter.Constant("torad", ToRadian.Lambda));
        AddConstant(Parameter.Constant("toradian", ToRadian.Lambda));
        AddConstant(Parameter.Constant("tograd", ToGradian.Lambda));
        AddConstant(Parameter.Constant("togradian", ToGradian.Lambda));

        AddConstant(Parameter.Constant("sin", Sin.Lambda));
        AddConstant(Parameter.Constant("cos", Cos.Lambda));
        AddConstant(Parameter.Constant("tan", Tan.Lambda));
        AddConstant(Parameter.Constant("tg", Tan.Lambda));
        AddConstant(Parameter.Constant("cot", Cot.Lambda));
        AddConstant(Parameter.Constant("ctg", Cot.Lambda));
        AddConstant(Parameter.Constant("sec", Sec.Lambda));
        AddConstant(Parameter.Constant("cosec", Csc.Lambda));
        AddConstant(Parameter.Constant("csc", Csc.Lambda));

        AddConstant(Parameter.Constant("arcsin", Arcsin.Lambda));
        AddConstant(Parameter.Constant("arccos", Arccos.Lambda));
        AddConstant(Parameter.Constant("arctan", Arctan.Lambda));
        AddConstant(Parameter.Constant("arctg", Arctan.Lambda));
        AddConstant(Parameter.Constant("arccot", Arccot.Lambda));
        AddConstant(Parameter.Constant("arcctg", Arccot.Lambda));
        AddConstant(Parameter.Constant("arcsec", Arcsec.Lambda));
        AddConstant(Parameter.Constant("arccosec", Arccsc.Lambda));
        AddConstant(Parameter.Constant("arccsc", Arccsc.Lambda));

        AddConstant(Parameter.Constant("sh", Sinh.Lambda));
        AddConstant(Parameter.Constant("sinh", Sinh.Lambda));
        AddConstant(Parameter.Constant("ch", Cosh.Lambda));
        AddConstant(Parameter.Constant("cosh", Cosh.Lambda));
        AddConstant(Parameter.Constant("th", Tanh.Lambda));
        AddConstant(Parameter.Constant("tanh", Tanh.Lambda));
        AddConstant(Parameter.Constant("cth", Coth.Lambda));
        AddConstant(Parameter.Constant("coth", Coth.Lambda));
        AddConstant(Parameter.Constant("sech", Sech.Lambda));
        AddConstant(Parameter.Constant("csch", Csch.Lambda));

        AddConstant(Parameter.Constant("arsh", Arsinh.Lambda));
        AddConstant(Parameter.Constant("arsinh", Arsinh.Lambda));
        AddConstant(Parameter.Constant("arch", Arcosh.Lambda));
        AddConstant(Parameter.Constant("arcosh", Arcosh.Lambda));
        AddConstant(Parameter.Constant("arth", Artanh.Lambda));
        AddConstant(Parameter.Constant("artanh", Artanh.Lambda));
        AddConstant(Parameter.Constant("arcth", Arcoth.Lambda));
        AddConstant(Parameter.Constant("arcoth", Arcoth.Lambda));
        AddConstant(Parameter.Constant("arsch", Arsech.Lambda));
        AddConstant(Parameter.Constant("arsech", Arsech.Lambda));
        AddConstant(Parameter.Constant("arcsch", Arcsch.Lambda));

        AddConstant(Parameter.Constant("round", Round.Lambda));
        AddConstant(Parameter.Constant("floor", Floor.Lambda));
        AddConstant(Parameter.Constant("ceil", Ceil.Lambda));
        AddConstant(Parameter.Constant("trunc", Trunc.Lambda));
        AddConstant(Parameter.Constant("truncate", Trunc.Lambda));
        AddConstant(Parameter.Constant("frac", Frac.Lambda));

        AddConstant(Parameter.Constant("transpose", Transpose.Lambda));
        AddConstant(Parameter.Constant("det", Determinant.Lambda));
        AddConstant(Parameter.Constant("determinant", Determinant.Lambda));
        AddConstant(Parameter.Constant("inverse", Inverse.Lambda));
        AddConstant(Parameter.Constant("dotproduct", DotProduct.Lambda));
        AddConstant(Parameter.Constant("crossproduct", CrossProduct.Lambda));

        AddConstant(Parameter.Constant("im", Im.Lambda));
        AddConstant(Parameter.Constant("imaginary", Im.Lambda));
        AddConstant(Parameter.Constant("re", Re.Lambda));
        AddConstant(Parameter.Constant("real", Re.Lambda));
        AddConstant(Parameter.Constant("phase", Phase.Lambda));
        AddConstant(Parameter.Constant("conjugate", Conjugate.Lambda));
        AddConstant(Parameter.Constant("reciprocal", Reciprocal.Lambda));
        AddConstant(Parameter.Constant("tocomplex", ToComplex.Lambda));

        AddConstant(Parameter.Constant("sign", Sign.Lambda));
        AddConstant(Parameter.Constant("tobin", ToBin.Lambda));
        AddConstant(Parameter.Constant("tooct", ToOct.Lambda));
        AddConstant(Parameter.Constant("tohex", ToHex.Lambda));
    }

    private static void AddConstant(Parameter parameter)
        => Constants.Add(parameter.Key, parameter);

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public virtual IEnumerator<Parameter> GetEnumerator()
    {
        if (withConstants)
        {
            foreach (var (_, item) in Constants)
                yield return item;
        }

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
    public virtual ParameterValue this[string key]
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

    private Parameter GetParameterByKey(string key)
    {
        if (TryGetParameter(key, out var parameter))
            return parameter;

        throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Resource.VariableNotFoundExceptionError, key));
    }

    /// <summary>
    /// Gets the value of parameter.
    /// </summary>
    /// <param name="key">The name of parameter.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns><c>true</c> if the current collection contains specified parameter, otherwise <c>false</c>.</returns>
    public bool TryGetParameter(string key, [NotNullWhen(true)] out Parameter? parameter)
    {
        if (collection.TryGetValue(key, out var param))
        {
            parameter = param;
            return true;
        }

        if (withConstants && Constants.TryGetValue(key, out param))
        {
            parameter = param;
            return true;
        }

        parameter = null;
        return false;
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

    /// <inheritdoc />
    public bool Remove(Parameter param)
    {
        if (param is null)
            throw new ArgumentNullException(nameof(param));
        if (param.Type == ParameterType.Constant)
            throw new ArgumentException(Resource.ConstError);

        var result = collection.Remove(param.Key);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, param));

        return result;
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
    public virtual bool Contains(Parameter param)
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
    public virtual bool ContainsKey(string key)
        => collection.ContainsKey(key);

    /// <summary>
    /// Creates a new nested scope of parameters.
    /// </summary>
    /// <param name="parameters">The instance of the base parameters collection.</param>
    /// <returns>The expression parameters.</returns>
    public static IExpressionParameters CreateScoped(IExpressionParameters parameters)
        => new ScopedExpressionParameters(parameters);

    /// <summary>
    /// Creates a new <see cref="IExpressionParameters"/> instance based on two specified <see cref="IExpressionParameters"/>.
    /// </summary>
    /// <param name="actual">The instance with actual parameters.</param>
    /// <param name="parent">The instance with parent parameters.</param>
    /// <returns>The expression parameters.</returns>
    public static IExpressionParameters? CreateCombined(IExpressionParameters? actual, IExpressionParameters? parent)
    {
        if (parent is null)
            return actual;

        if (actual is null)
            return parent;

        return new ScopedWrapperExpressionParameters(actual, new WrapperExpressionParameters(parent));
    }
}