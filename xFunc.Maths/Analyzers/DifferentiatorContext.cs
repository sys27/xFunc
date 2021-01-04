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
using xFunc.Maths.Expressions;

namespace xFunc.Maths.Analyzers
{
    /// <summary>
    /// The context for differentiator.
    /// </summary>
    public class DifferentiatorContext
    {
        private Variable variable = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public DifferentiatorContext(ExpressionParameters? parameters)
            : this(parameters, Variable.X)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentiatorContext"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="variable">The variable.</param>
        public DifferentiatorContext(ExpressionParameters? parameters, Variable variable)
        {
            Parameters = parameters;
            Variable = variable;
        }

        /// <summary>
        /// Creates an empty object.
        /// </summary>
        /// <returns>The differentiator context.</returns>
        public static DifferentiatorContext Default()
            => new DifferentiatorContext(new ExpressionParameters());

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ExpressionParameters? Parameters { get; }

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>
        /// The variable.
        /// </value>
        public Variable Variable
        {
            get => variable;
            set => variable = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}