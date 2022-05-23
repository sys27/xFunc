// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions.Collections;

/// <summary>
/// Strongly typed dictionary that contains user-defined functions.
/// </summary>
[Serializable]
public class FunctionCollection : Dictionary<UserFunction, IExpression>, INotifyCollectionChanged
{
    /// <summary>
    /// Occurs when the collection changes.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCollection"/> class.
    /// </summary>
    public FunctionCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCollection"/> class.
    /// </summary>
    /// <param name="info">The info.</param>
    /// <param name="context">The context.</param>
    [ExcludeFromCodeCoverage]
    protected FunctionCollection(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="IExpression"/> with the specified key.
    /// </summary>
    /// <value>
    /// The <see cref="IExpression"/>.
    /// </value>
    /// <param name="key">The key.</param>
    /// <returns>The saved user function.</returns>
    public new IExpression this[UserFunction key]
    {
        get
        {
            return base[key];
        }
        set
        {
            base[key] = value;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    /// <summary>
    /// Raises the <see cref="CollectionChanged" /> event.
    /// </summary>
    /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        => CollectionChanged?.Invoke(this, args);

    /// <summary>
    /// Adds new function.
    /// </summary>
    /// <param name="key">The signature of function.</param>
    /// <param name="value">The function.</param>
    public new void Add(UserFunction key, IExpression value)
    {
        base.Add(key, value);

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, key));
    }

    /// <summary>
    /// Removes the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    public new void Remove(UserFunction key)
    {
        if (base.Remove(key))
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, key));
    }

    /// <summary>
    /// Gets an user function.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>An user function.</returns>
    /// <exception cref="KeyNotFoundException">The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</exception>
    public UserFunction GetKey(UserFunction function)
    {
        var func = Keys.FirstOrDefault(uf => uf.Equals(function));
        if (func is null)
            throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Resource.FunctionNotFoundExceptionError, function));

        return func;
    }
}