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
using System.Collections.Generic;
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
#if !PORTABLE
using System.Runtime.Serialization;
#endif
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Strongly typed dictionaty that contains user-defined functions.
    /// </summary>
#if !PORTABLE
    [Serializable]
#endif
    public class MathFunctionCollection : Dictionary<UserFunction, IExpression>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MathFunctionCollection"/> class.
        /// </summary>
        public MathFunctionCollection() { }

#if !PORTABLE
        /// <summary>
        /// Initializes a new instance of the <see cref="MathFunctionCollection"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected MathFunctionCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif

        /// <summary>
        /// Gets an user function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>An user function</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</exception>
        public UserFunction GetKeyByKey(UserFunction function)
        {
            var func = Keys.FirstOrDefault(uf => uf.Equals(function));
            if (func == null)
                throw new KeyNotFoundException(string.Format(Resource.FunctionNotFoundExceptionError, function));

            return func;
        }

    }

}
