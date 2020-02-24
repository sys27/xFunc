// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Numerics;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Collections
{
    /// <summary>
    /// Strongly typed dictionary that contains value of variables.
    /// </summary>
    public class ParameterCollection : IEnumerable<Parameter>, INotifyCollectionChanged
    {
        private readonly HashSet<Parameter> constants;
        private readonly HashSet<Parameter> collection;

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

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
            : this(Enumerable.Empty<Parameter>(), initConstants)
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
        /// Initializes a new instance of the <see cref="ParameterCollection" /> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="initConstants">if set to <c>true</c> initialize constants.</param>
        public ParameterCollection(IEnumerable<Parameter> parameters, bool initConstants)
        {
            constants = new HashSet<Parameter>();
            collection = new HashSet<Parameter>(parameters);

            if (initConstants)
                InitializeConstants();
        }

        /// <summary>
        /// Raises the <see cref="CollectionChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        private void InitializeConstants()
        {
            constants.Add(Parameter.CreateConstant("π", Math.PI)); // Archimedes' constant
            constants.Add(Parameter.CreateConstant("pi", Math.PI)); // Archimedes' constant
            constants.Add(Parameter.CreateConstant("e", Math.E)); // Euler's number
            constants.Add(Parameter.CreateConstant("i", Complex.ImaginaryOne)); // Imaginary unit
            constants.Add(Parameter.CreateConstant("g", 9.80665)); // Gravity on Earth
            constants.Add(Parameter.CreateConstant("c", 299792458)); // Speed of Light (c0)
            constants.Add(Parameter.CreateConstant("h", 6.62607004E-34)); // Planck Constant
            constants.Add(Parameter.CreateConstant("F", 96485.33289)); // Faraday Constant
            constants.Add(Parameter.CreateConstant("ε", 8.854187817E-12)); // Electric Constant (ε0)
            constants.Add(Parameter.CreateConstant("µ", 1.2566370614E-6)); // Magnetic constant (µ0)
            constants.Add(Parameter.CreateConstant("G", 6.64078E-11)); // Gravitational constant
            constants.Add(Parameter.CreateConstant("α", 2.5029078750958928222839)); // Feigenbaum constant
            constants.Add(Parameter.CreateConstant("σ", 5.670367E-8)); // Stefan-Boltzmann constant
            constants.Add(Parameter.CreateConstant("γ", 0.57721566490153286060651)); // Euler–Mascheroni constant
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Parameter> GetEnumerator()
        {
            foreach (var item in constants)
                yield return item;

            foreach (var item in collection)
                yield return item;
        }

        /// <summary>
        /// Gets or sets the value of variable.
        /// </summary>
        /// <value>
        /// The value of parameter.
        /// </value>
        /// <param name="index">The index of variable.</param>
        /// <returns>The value of variable.</returns>
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= constants.Count + collection.Count)
                    throw new IndexOutOfRangeException();

                if (index < constants.Count)
                    return constants.ElementAt(index).Value;

                return collection.ElementAt(index - constants.Count).Value;
            }
            set
            {
                if (index < 0 || index >= constants.Count + collection.Count)
                    throw new IndexOutOfRangeException();

                if (index < constants.Count)
                    throw new ParameterIsReadOnlyException(Resource.ReadOnlyError, collection.ElementAt(index).Key);

                var element = collection.ElementAt(index - constants.Count);
                element.Value = value;

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Gets or sets the value of variable.
        /// </summary>
        /// <value>
        /// The value of parameter.
        /// </value>
        /// <param name="key">The name of variable.</param>
        /// <returns>The value of variable.</returns>
        public object this[string key]
        {
            get
            {
                return GetParameterByKey(key).Value;
            }
            set
            {
                var param = collection.FirstOrDefault(p => p.Key == key);
                if (param == null)
                {
                    this.Add(key, value);
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
            var item = collection.FirstOrDefault(p => p.Key == key);
            if (item != null)
                return item;

            var param = constants.FirstOrDefault(p => p.Key == key);
            if (param != null)
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
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            if (param.Type == ParameterType.Constant)
                throw new ArgumentException(Resource.ConstError);

            collection.Add(param);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, param));
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        public void Add(string key)
        {
            this.Add(new Parameter(key, 0));
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <param name="value">The value of variable.</param>
        public void Add(string key, object value)
        {
            this.Add(new Parameter(key, value));
        }

        /// <summary>
        /// Removes the specified element from this object.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
        /// <exception cref="ParameterIsReadOnlyException">The variable is read only.</exception>
        public void Remove(Parameter param)
        {
            if (param == null)
                throw new ArgumentNullException(nameof(param));
            if (param.Type == ParameterType.Constant)
                throw new ArgumentException(Resource.ConstError);

            collection.Remove(param);

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
            return collection.Contains(param) || constants.Contains(param);
        }

        /// <summary>
        /// Determines whether an object contains the specified element.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
        public bool ContainsInConstants(Parameter param)
        {
            return constants.Contains(param);
        }

        /// <summary>
        /// Determines whether an object contains the specified key.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return collection.FirstOrDefault(p => p.Key == key) != null || constants.FirstOrDefault(p => p.Key == key) != null;
        }

        /// <summary>
        /// Determines whether an object contains the specified key.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKeyInConstants(string key)
        {
            return constants.FirstOrDefault(p => p.Key == key) != null;
        }

        /// <summary>
        /// Gets the constants.
        /// </summary>
        /// <value>
        /// The constants.
        /// </value>
        public IEnumerable<Parameter> Constants => constants;

        /// <summary>
        /// Gets the collection of variables.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public IEnumerable<Parameter> Collection => collection;
    }
}