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

namespace xFunc.Logics.Expressions
{

    /// <summary>
    /// Represents the Assign operation.
    /// </summary>
    public class Assign : ILogicExpression
    {

        private Variable variable;
        private ILogicExpression value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assign"/> class.
        /// </summary>
        public Assign()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Assign"/> class.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="value">The value.</param>
        public Assign(Variable variable, ILogicExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} := {1}", variable, value);
        }

        /// <summary>
        /// Calculates this logical expression.
        /// </summary>
        /// <param name="parameters">A collection of variables that are used in the expression.</param>
        /// <returns>A result of the calculation.</returns>
        /// <seealso cref="LogicParameterCollection" />
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        public bool Calculate(LogicParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var localValue = value.Calculate(parameters);
            parameters.Add(variable.Name);
            parameters[variable.Name] = localValue;

            return false;
        }

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>The variable.</value>
        public Variable Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public ILogicExpression Value
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
