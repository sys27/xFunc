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
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the "Del" operator.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Expressions.UnaryExpression" />
    public class Del : UnaryExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Del"/> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        public Del(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression)
            : base(expression)
        {
            this.Differentiator = differentiator;
            this.Simplifier = simplifier;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return GetHashCode(26321);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The differentiator is null.</exception>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            if (Differentiator == null)
                throw new ArgumentNullException(nameof(Differentiator));

            var variables = Helpers.GetAllVariables(m_argument).ToList();
            var vector = new Vector(variables.Count);

            Differentiator.Parameters = parameters;

            for (var i = 0; i < variables.Count; i++)
            {
                Differentiator.Variable = variables[i];

                vector[i] = m_argument.Analyze(Differentiator).Analyze(Simplifier);
            }

            return vector;
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Del(this.Differentiator, this.Simplifier, m_argument.Clone());
        }

        /// <summary>
        /// Gets or sets the simplifier.
        /// </summary>
        /// <value>
        /// The simplifier.
        /// </value>
        public ISimplifier Simplifier { get; private set; }

        /// <summary>
        /// Gets or sets the differentiator.
        /// </summary>
        /// <value>
        /// The differentiator.
        /// </value>
        public IDifferentiator Differentiator { get; private set; }

    }

}