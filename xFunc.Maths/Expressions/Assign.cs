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

namespace xFunc.Maths.Expressions
{

    public class Assign : IMathExpression
    {

        private IMathExpression key;
        private IMathExpression value;

        /// <summary>
        /// Initializes a new instance of <see cref="Assign"/>.
        /// </summary>
        public Assign()
            : this(null, null)
        {

        }

        public Assign(IMathExpression key, IMathExpression value)
        {
            this.Key = key;
            this.value = value;
        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return string.Format("{0} := {1}", key, value);
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            if (key is Variable)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                var e = key as Variable;

                parameters[e.Name] = value.Calculate(parameters);
            }
            else
            {
                throw new NotSupportedException();
            }

            return double.NaN;
        }

        public double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            if (key is Variable)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                var e = key as Variable;

                parameters[e.Name] = value.Calculate(parameters);
            }
            else if (key is UserFunction)
            {
                if (functions == null)
                    throw new ArgumentNullException("functions");

                var e = key as UserFunction;

                functions[e] = value;
            }

            return double.NaN;
        }

        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instanse of the <see cref="Assign"/>.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public IMathExpression Clone()
        {
            return new Assign(key.Clone(), value.Clone());
        }

        /// <summary>
        /// Get or Set the parent expression.
        /// </summary>
        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public IMathExpression Key
        {
            get
            {
                return key;
            }
            set
            {
                if (value != null && !(value is Variable || value is UserFunction))
                {
                    throw new NotSupportedException();
                }

                key = value;
            }
        }

        public IMathExpression Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

    }

}
