// Copyright 2012-2013 Dmitry Kischenko
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
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Strongly typed dictionaty that contains value of variables.
    /// </summary>
    public class MathParameterCollection : IEnumerable<MathParameter>
    {

#if NET40_OR_GREATER || PORTABLE
        private HashSet<MathParameter> consts;
        private HashSet<MathParameter> collection;
#elif NET20_OR_GREATER
        private List<MathParameter> consts;
        private List<MathParameter> collection;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterCollection"/> class.
        /// </summary>
        public MathParameterCollection()
        {
#if NET40_OR_GREATER || PORTABLE
            consts = new HashSet<MathParameter>();
            collection = new HashSet<MathParameter>();
#elif NET20_OR_GREATER
            consts = new List<MathParameter>();
            collection = new List<MathParameter>();
#endif

            InitializeDefaults();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterCollection"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public MathParameterCollection(IEnumerable<MathParameter> parameters)
        {
#if NET40_OR_GREATER || PORTABLE
            consts = new HashSet<MathParameter>();
            collection = new HashSet<MathParameter>(parameters);
#elif NET20_OR_GREATER
            consts = new List<MathParameter>();
            collection = new List<MathParameter>(parameters);
#endif

            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            consts.Add(MathParameter.CreateConstant("π", Math.PI));
            consts.Add(MathParameter.CreateConstant("e", Math.E));
            consts.Add(MathParameter.CreateConstant("g", 9.80665));
            consts.Add(MathParameter.CreateConstant("c", 299792458));
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
        public IEnumerator<MathParameter> GetEnumerator()
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
        /// The <see cref="Double"/>.
        /// </value>
        /// <param name="key">The name of variable.</param>
        /// <returns>The value of variable.</returns>
        public double this[string key]
        {
            get
            {
                var item = collection.FirstOrDefault(p => p.Key == key);

                if (item != null)
                    return item.Value;

                var param = collection.FirstOrDefault(p => p.Key == key);
                if (param == null)
                    throw new KeyNotFoundException(string.Format(Resource.VariableNotFoundExceptionError, key));

                return param.Value;
            }
            set
            {
                var param = collection.FirstOrDefault(p => p.Key == key);
                if (param == null)
                    this.Add(key, value);
                else if (param.Type == MathParameterType.Normal)
                    param.Value = value;
                else
                    throw new MathParameterIsReadOnlyException(string.Format(Resource.ReadOnlyError, param.Key));
            }
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
        /// <exception cref="MathParameterIsReadOnlyException">The variable is read only.</exception>
        public void Add(MathParameter param)
        {
            if (param == null)
                throw new ArgumentNullException("param");
            if (param.Type == MathParameterType.Constant)
                throw new ArgumentException(Resource.ConstError);

            collection.Add(param);
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        public void Add(string key)
        {
            this.Add(new MathParameter(key, 0));
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <param name="value">The value of variable.</param>
        public void Add(string key, double value)
        {
            this.Add(new MathParameter(key, value));
        }

        /// <summary>
        /// Removes the specified element from this object.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <exception cref="ArgumentNullException"><paramref name="param"/> is null.</exception>
        /// <exception cref="MathParameterIsReadOnlyException">The variable is read only.</exception>
        public void Remove(MathParameter param)
        {
            if (param == null)
                throw new ArgumentNullException("param");

            collection.Remove(param);
        }

        /// <summary>
        /// Removes the specified element from this object.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
        /// <exception cref="MathParameterIsReadOnlyException">The variable is read only.</exception>
        public void Remove(string key)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(key))
#elif NET20_OR_GREATER
            if (StringExtention.IsNullOrWhiteSpace(key))
#endif
                throw new ArgumentNullException("key");

            var el = collection.FirstOrDefault(p => p.Key == key);
            if (el == null)
                return;

            Remove(el);
        }

        /// <summary>
        /// Determines whether an onject contains the specified element.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
        public bool Contains(MathParameter param)
        {
            return collection.Contains(param) || consts.Contains(param);
        }

        /// <summary>
        /// Determines whether an onject contains the specified element.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the object contains the specified element; otherwise, <c>false</c>.</returns>
        public bool ContainsInConstants(MathParameter param)
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
        public IEnumerable<MathParameter> Constants
        {
            get
            {
                return consts;
            }
        }

        /// <summary>
        /// Gets the collection of variables.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public IEnumerable<MathParameter> Collection
        {
            get
            {
                return collection;
            }
        }

    }

}
