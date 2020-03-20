// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Maths.Expressions
{
    /// <summary>
    /// Represents user-defined functions.
    /// </summary>
    public class UserFunction : IExpression
    {
        private readonly IList<IExpression> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFunction"/> class.
        /// </summary>
        /// <param name="function">The name of function.</param>
        /// <param name="arguments">Arguments.</param>
        public UserFunction(string function, IList<IExpression> arguments)
        {
            Function = function;

            this.arguments = arguments ??
                             throw new ArgumentNullException(nameof(arguments));

            foreach (var item in arguments)
                if (item != null)
                    item.Parent = this;
        }

        /// <summary>
        /// Gets or sets the <see cref="IExpression"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IExpression"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The argument.</returns>
        public IExpression this[int index]
        {
            get
            {
                return arguments[index];
            }
            set
            {
                arguments[index] = value ?? throw new ArgumentNullException(nameof(value));
                value.Parent = this;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is UserFunction exp &&
                Function == exp.Function &&
                ParametersCount == exp.ParametersCount;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() =>
            HashCode.Combine(Function, ParametersCount);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute() => Execute(null);

        /// <summary>
        /// Executes the user function.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="parameters"/> is null.</exception>
        /// <seealso cref="ExpressionParameters" />
        public object Execute(ExpressionParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var func = parameters.Functions.GetKeyByKey(this);

            var newParameters = new ParameterCollection(parameters.Variables.Collection);
            for (var i = 0; i < ParametersCount; i++)
            {
                var arg = func.arguments[i] as Variable;
                newParameters[arg.Name] = (double)arguments[i].Execute(parameters);
            }

            var expParam = new ExpressionParameters(parameters.AngleMeasurement, newParameters, parameters.Functions);
            return parameters.Functions[this].Execute(expParam);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public IExpression Clone()
        {
            var args = new IExpression[ParametersCount];
            for (var i = 0; i < ParametersCount; i++)
                args[i] = arguments[i].Clone();

            return new UserFunction(Function, args);
        }

        /// <summary>
        /// Gets or sets the parent expression.
        /// </summary>
        public IExpression Parent { get; set; }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public IEnumerable<IExpression> Arguments => arguments;

        /// <summary>
        /// Gets the name of function.
        /// </summary>
        /// <value>The name of function.</value>
        public string Function { get; }

        /// <summary>
        /// Gets the count of parameters.
        /// </summary>
        /// <value>
        /// The count of parameters.
        /// </value>
        public int ParametersCount => arguments.Count;
    }
}