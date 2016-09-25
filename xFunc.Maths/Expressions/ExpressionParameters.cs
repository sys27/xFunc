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
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Combines all parameters of expressions.
    /// </summary>
    public class ExpressionParameters
    {

        private AngleMeasurement angleMeasuremnt;
        private ParameterCollection variables;
        private FunctionCollection functions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        public ExpressionParameters()
            : this(AngleMeasurement.Degree, new ParameterCollection(), new FunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt)
            : this(angleMeasuremnt, new ParameterCollection(), new FunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="parameters">The collection of variables' values.</param>
        public ExpressionParameters(ParameterCollection parameters)
            : this(AngleMeasurement.Degree, parameters, new FunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(FunctionCollection functions)
            : this(AngleMeasurement.Degree, new ParameterCollection(), functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="variables">The collection of variables' values.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, ParameterCollection variables)
            : this(angleMeasuremnt, variables, new FunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, FunctionCollection functions)
            : this(angleMeasuremnt, new ParameterCollection(), functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="variables">The collection of variables' values.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(ParameterCollection variables, FunctionCollection functions)
            : this(AngleMeasurement.Degree, variables, functions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParameters"/> class.
        /// </summary>
        /// <param name="angleMeasuremnt">The angle measuremnt.</param>
        /// <param name="variables">The collection of variables' values.</param>
        /// <param name="functions">The collection of user functions.</param>
        public ExpressionParameters(AngleMeasurement angleMeasuremnt, ParameterCollection variables, FunctionCollection functions)
        {
            this.angleMeasuremnt = angleMeasuremnt;
            this.variables = variables;
            this.functions = functions;
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="AngleMeasurement"/>.
        /// </summary>
        /// <param name="angleMeasurement">The angle measurement.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(AngleMeasurement angleMeasurement)
        {
            return new ExpressionParameters(angleMeasurement);
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="ParameterCollection"/>.
        /// </summary>
        /// <param name="parameters">The collection of variables' values.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(ParameterCollection parameters)
        {
            return new ExpressionParameters(parameters);
        }

        /// <summary>
        /// Creates a <see cref="ExpressionParameters"/> from the specified <see cref="FunctionCollection"/>.
        /// </summary>
        /// <param name="functions">The collection of user functions.</param>
        /// <returns>The created <see cref="ExpressionParameters"/>.</returns>
        public static implicit operator ExpressionParameters(FunctionCollection functions)
        {
            return new ExpressionParameters(functions);
        }

        /// <summary>
        /// Gets or sets the angle measurement.
        /// </summary>
        /// <value>
        /// The angle measurement.
        /// </value>
        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return angleMeasuremnt;
            }
            set
            {
                angleMeasuremnt = value;
            }
        }

        /// <summary>
        /// Gets the collection of variables' values.
        /// </summary>
        /// <value>
        /// The collection of variables' values.
        /// </value>
        public ParameterCollection Variables
        {
            get
            {
                return variables;
            }
        }

        /// <summary>
        /// Gets the collection of user functions.
        /// </summary>
        /// <value>
        /// The collection of user functions.
        /// </value>
        public FunctionCollection Functions
        {
            get
            {
                return functions;
            }
        }

    }

}
