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
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Deriv function.
    /// </summary>
    public class Derivative : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        public Derivative(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression)
            : this(differentiator, simplifier, new[] { expression }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression, Variable variable)
            : this(differentiator, simplifier, new[] { expression, variable }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="point">The point of derivation.</param>
        public Derivative(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression, Variable variable, Number point)
            : this(differentiator, simplifier, new[] { expression, variable, point }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="args"/> is null.</exception>
        public Derivative(IDifferentiator differentiator, ISimplifier simplifier, IExpression[] args)
            : base(args)
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
            return base.GetHashCode(587, 1249);
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            if (Differentiator == null)
                throw new ArgumentNullException(nameof(Differentiator));

            var variable = this.Variable;

            Differentiator.Variable = variable;
            Differentiator.Parameters = parameters;

            var diff = this.Analyze(Differentiator);

            var point = this.DerivativePoint;
            if (variable != null && point != null)
            {
                if (parameters == null)
                    parameters = new ExpressionParameters();

                parameters.Variables[variable.Name] = point.Value;

                return diff.Execute(parameters);
            }

            if (Simplifier != null)
                return diff.Analyze(Simplifier);

            return diff;
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
        /// <returns>Returns the new instance of <see cref="IExpression"/> that is a clone of this instance.</returns>
        public override IExpression Clone()
        {
            return new Derivative(this.Differentiator, this.Simplifier, CloneArguments());
        }

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public IExpression Expression
        {
            get
            {
                return m_arguments[0];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                m_arguments[0] = value;
                m_arguments[0].Parent = this;
            }
        }

        /// <summary>
        /// Gets or sets the variable.
        /// </summary>
        /// <value>
        /// The variable.
        /// </value>
        public Variable Variable => ParametersCount >= 2 ? (Variable)m_arguments[1] : Variable.X;

        /// <summary>
        /// Gets the derivative point.
        /// </summary>
        /// <value>
        /// The derivative point.
        /// </value>
        public Number DerivativePoint => ParametersCount >= 3 ? (Number)m_arguments[2] : null;

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

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int? MinParametersCount => 1;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int? MaxParametersCount => 3;

    }

}