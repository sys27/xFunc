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
using SimpleInjector;
using System;

namespace xFunc.Maths
{

    /// <summary>
    /// The default implementation of methods to resolve object for post-parse process.
    /// </summary>
    public class DefaultDependencyResolver : IDependencyResolver
    {

        private Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver"/> class.
        /// </summary>
        public DefaultDependencyResolver()
        {
            container = new Container();
            container.RegisterSingleton<ISimplifier, Simplifier>();
            container.RegisterSingleton<IDifferentiator>(() => new Differentiator(container.GetInstance<ISimplifier>()));
            container.Verify();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver"/> class.
        /// </summary>
        /// <param name="objects">The array of object to register in DI-container (as singletons).</param>
        public DefaultDependencyResolver(object[] objects)
        {
            container = new Container();

            if (objects != null)
                foreach (var obj in objects)
                    container.RegisterSingleton(obj.GetType(), obj);

            container.Verify();
        }

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type">The type for resolving.</param>
        /// <returns>
        /// The object of specified type.
        /// </returns>
        public object Resolve(Type type)
        {
            return container.GetInstance(type);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T">The type for resolving.</typeparam>
        /// <returns>
        /// The object of specified type.
        /// </returns>
        public T Resolve<T>() where T : class
        {
            return container.GetInstance<T>();
        }

    }

}
