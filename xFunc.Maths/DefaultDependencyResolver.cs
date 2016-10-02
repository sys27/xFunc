// Copyright 2012-2016 Dmitry Kischenko
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
using System.Reflection;

namespace xFunc.Maths
{

    /// <summary>
    /// The default implementation of methods to resolve object for post-parse process.
    /// </summary>
    public class DefaultDependencyResolver : IDependencyResolver
    {

        private Dictionary<Type, object> container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver"/> class.
        /// </summary>
        public DefaultDependencyResolver()
        {
            container = new Dictionary<Type, object>();
            container.Add(typeof(ISimplifier), new Simplifier());
            container.Add(typeof(IDifferentiator), new Differentiator());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver" /> class.
        /// </summary>
        /// <param name="types">The array of types to register in DI-container.</param>
        /// <param name="objects">The array of objects to register in DI-container (as singletons).</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="types"/> or <paramref name="objects"/> is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">The length of <paramref name="types"/> is not equal to length of <paramref name="objects"/></exception>
        public DefaultDependencyResolver(Type[] types, object[] objects)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));
            if (objects == null)
                throw new ArgumentNullException(nameof(objects));
            if (types.Length != objects.Length)
                throw new ArgumentException();

            container = new Dictionary<Type, object>(types.Length);

            for (int i = 0; i < objects.Length; i++)
                container.Add(types[i], objects[i]);
        }

        /// <summary>
        /// Resolves the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Resolve(object obj)
        {
            var type = obj.GetType();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (prop.CanWrite && container.ContainsKey(prop.PropertyType))
                    prop.SetValue(obj, container[prop.PropertyType], null);
        }

        /// <summary>
        /// Resolves the specified object.
        /// </summary>
        /// <typeparam name="T">The type of specified object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The object with injected properties.
        /// </returns>
        public T Resolve<T>(T obj)
        {
            Resolve((object)obj);

            return obj;
        }

    }

}
