// Copyright 2012-2018 Dmitry Kischenko
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
using System.Linq;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Collections
{

    /// <summary>
    /// Strongly typed dictionaty that contains value of variables.
    /// </summary>
    public class ParameterCollection : IEnumerable<Parameter>, INotifyCollectionChanged
    {

        private readonly HashSet<Parameter> consts;
        private HashSet<Parameter> collection;

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
        /// <param name="initConsts">if set to <c>true</c> initialize constants.</param>
        public ParameterCollection(bool initConsts)
        {
            consts = new HashSet<Parameter>();
            collection = new HashSet<Parameter>();

            if (initConsts)
                InitializeDefaults();
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
        /// <param name="initConsts">if set to <c>true</c> initialize constants.</param>
        public ParameterCollection(IEnumerable<Parameter> parameters, bool initConsts)
        {
            consts = new HashSet<Parameter>();
            collection = new HashSet<Parameter>(parameters);

            if (initConsts)
                InitializeDefaults();
        }

        /// <summary>
        /// Raises the <see cref="E:CollectionChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        private void InitializeDefaults()
        {
            consts.Add(Parameter.CreateConstant("π", Math.PI)); // Archimedes' constant
            consts.Add(Parameter.CreateConstant("pi", Math.PI)); // Archimedes' constant
            consts.Add(Parameter.CreateConstant("e", Math.E));  // Euler's number
            consts.Add(Parameter.CreateConstant("g", 9.80665)); // Gravity on Earth
            consts.Add(Parameter.CreateConstant("c", 299792458));   // Speed of Light (c0)
            consts.Add(Parameter.CreateConstant("h", 6.62607004E-34));  // Planck Constant
            consts.Add(Parameter.CreateConstant("F", 96485.33289)); // Faraday Constant
            consts.Add(Parameter.CreateConstant("ε", 8.854187817E-12)); // Electric Constant (ε0)
            consts.Add(Parameter.CreateConstant("µ", 1.2566370614E-6)); // Magnetic constant (µ0)
            consts.Add(Parameter.CreateConstant("G", 6.64078E-11)); // Gravitational constant
            consts.Add(Parameter.CreateConstant("α", 2.5029078750958928222839)); // Feigenbaum constant
            consts.Add(Parameter.CreateConstant("σ", 5.670367E-8)); // Stefan-Boltzmann constant
            consts.Add(Parameter.CreateConstant("γ", 0.57721566490153286060651)); // Euler–Mascheroni constant
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Parameter> GetEnumerator()
        {
            foreach (var item in consts)
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
                if (collection.Count + consts.Count - 1 < index)
                    throw new IndexOutOfRangeException();

                if (collection.Count > index)
                    return collection.ElementAt(index).Value;

                return consts.ElementAt(index - collection.Count).Value;
            }
            set
            {
                if (collection.Count + consts.Count - 1 < index)
                    throw new IndexOutOfRangeException();

                if (collection.Count <= index)
                    throw new ParameterIsReadOnlyException(string.Format(Resource.ReadOnlyError, collection.ElementAt(index).Key));

                var element = collection.ElementAt(index);
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
                else if (param.Type == ParameterType.Normal)
                {
                    param.Value = value;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
                else
                {
                    throw new ParameterIsReadOnlyException(string.Format(Resource.ReadOnlyError, param.Key));
                }
            }
        }

        private Parameter GetParameterByKey(string key)
        {
            var item = collection.FirstOrDefault(p => p.Key == key);

            if (item != null)
                return item;

            var param = consts.FirstOrDefault(p => p.Key == key);
            if (param != null)
                return param;

            throw new KeyNotFoundException(string.Format(Resource.VariableNotFoundExceptionError, key));
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
        /// Determines whether an onject contains the specified element.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
        public bool Contains(Parameter param)
        {
            return collection.Contains(param) || consts.Contains(param);
        }

        /// <summary>
        /// Determines whether an onject contains the specified element.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
        public bool ContainsInConstants(Parameter param)
        {
            return consts.Contains(param);
        }

        /// <summary>
        /// Determines whether an onject contains the specified key.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return collection.FirstOrDefault(p => p.Key == key) != null || consts.FirstOrDefault(p => p.Key == key) != null;
        }

        /// <summary>
        /// Determines whether an onject contains the specified key.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKeyInConstants(string key)
        {
            return consts.FirstOrDefault(p => p.Key == key) != null;
        }

        /// <summary>
        /// Gets the constants.
        /// </summary>
        /// <value>
        /// The constants.
        /// </value>
        public IEnumerable<Parameter> Constants => consts;

        /// <summary>
        /// Gets the collection of variables.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public IEnumerable<Parameter> Collection => collection;

    }

}
