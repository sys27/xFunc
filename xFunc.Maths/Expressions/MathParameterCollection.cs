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

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Strongly typed dictionaty that contains value of variables.
    /// </summary>
    public class MathParameterCollection : IEnumerable<MathParameter>
    {

#if NET40_OR_GREATER || PORTABLE
        private HashSet<MathParameter> collection;
#elif NET20_OR_GREATER
        private List<MathParameter> collection;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterCollection"/> class.
        /// </summary>
        public MathParameterCollection()
        {
#if NET40_OR_GREATER
            collection = new HashSet<MathParameter>();
#elif NET20_OR_GREATER
            collection = new List<MathParameter>();
#endif

            collection.Add(new MathParameter("π", Math.PI, true));
            collection.Add(new MathParameter("e", Math.E, true));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathParameterCollection"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public MathParameterCollection(IEnumerable<MathParameter> parameters)
        {
#if NET40_OR_GREATER
            collection = new HashSet<MathParameter>(parameters);
#elif NET20_OR_GREATER
            collection = new List<MathParameter>(parameters);
#endif
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
            return collection.GetEnumerator();
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
                return collection.Where(p => p.Key == key).First().Value;
            }
            set
            {
                var param = collection.Where(p => p.Key == key).FirstOrDefault();
                if (param == null)
                    this.Add(key, value);
                else if (!param.IsReadOnly)
                    param.Value = value;
                else
                    // todo: error message
                    throw new MathParameterIsReadOnlyException();
            }
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the element is added to the object; <c>false</c> if the element is already present.</returns>
        public void Add(MathParameter param)
        {
            if (param == null)
                throw new ArgumentNullException("param");
            if (param.IsReadOnly)
                // todo: error message
                throw new MathParameterIsReadOnlyException();

            collection.Add(param);
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the element is added to the object; <c>false</c> if the element is already present.</returns>
        public void Add(string key)
        {
            this.Add(new MathParameter(key, 0));
        }

        /// <summary>
        /// Adds the specified element to a set.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <param name="value">The value of variable.</param>
        /// <returns>
        ///   <c>true</c> if the element is added to the object; <c>false</c> if the element is already present.
        /// </returns>
        public void Add(string key, double value)
        {
            this.Add(new MathParameter(key, value));
        }

        /// <summary>
        /// Removes the specified element from this object.
        /// </summary>
        /// <param name="param">The element.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns false if item is not found in the object.</returns>
        public void Remove(MathParameter param)
        {
            if (param == null)
                throw new ArgumentNullException("param");
            if (param.IsReadOnly)
                // todo: error message
                throw new MathParameterIsReadOnlyException();

            collection.Remove(param);
        }

        /// <summary>
        /// Removes the specified element from this object.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns false if item is not found in the object.</returns>
        public void Remove(string key)
        {
#if NET40_OR_GREATER || PORTABLE
            if (string.IsNullOrWhiteSpace(key))
#elif NET20_OR_GREATER
            if (StringExtention.IsNullOrWhiteSpace(key))
#endif
                throw new ArgumentNullException("key");

            var el = collection.Where(p => p.Key == key).FirstOrDefault();
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
            return collection.Contains(param);
        }

        /// <summary>
        /// Determines whether an onject contains the specified key.
        /// </summary>
        /// <param name="key">The name of variable.</param>
        /// <returns><c>true</c> if the object contains the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string key)
        {
            return collection.Where(p => p.Key == key).Count() != 0;
        }

    }

}
