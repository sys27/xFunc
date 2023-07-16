// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Represents a strongly typed collection that contains parameters.
/// </summary>
public interface IExpressionParameters : IEnumerable<Parameter>
{
    /// <summary>
    /// Gets or sets the value of parameter.
    /// </summary>
    /// <param name="key">The name of parameter.</param>
    ParameterValue this[string key] { get; set; }

    /// <summary>
    /// Gets the value of parameter.
    /// </summary>
    /// <param name="key">The name of parameter.</param>
    /// <param name="parameter">The parameter.</param>
    /// <returns><c>true</c> if the current collection contains specified parameter, otherwise <c>false</c>.</returns>
    bool TryGetParameter(string key, [NotNullWhen(true)] out Parameter? parameter);

    /// <summary>
    /// Adds the specified element to a set.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    void Add(Parameter param);

    /// <summary>
    /// Adds the specified element to a set.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <param name="value">The value of variable.</param>
    void Add(string key, ParameterValue value);

    /// <summary>
    /// Removes the specified element from this object.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    /// <returns><c>true</c> if item was successfully removed from the collection; otherwise, <c>false</c>.</returns>
    bool Remove(Parameter param);

    /// <summary>
    /// Removes the specified element from this object.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
    /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
    void Remove(string key);

    /// <summary>
    /// Determines whether an object contains the specified key.
    /// </summary>
    /// <param name="key">The name of variable.</param>
    /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
    bool ContainsKey(string key);

    /// <summary>
    /// Determines whether an object contains the specified element.
    /// </summary>
    /// <param name="param">The element.</param>
    /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
    bool Contains(Parameter param);

    /// <summary>
    /// Clears this collection.
    /// </summary>
    void Clear();
}