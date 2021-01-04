// Copyright 2012-2021 Dmytro Kyshchenko
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Collections
{
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
}