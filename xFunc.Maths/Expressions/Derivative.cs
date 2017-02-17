// Copyright 2012-2017 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions
{

    /// <summary>
    /// Represents the Deriv function.
    /// </summary>
    public class Derivative : DifferentParametersExpression
    {

        [ExcludeFromCodeCoverage]
        internal Derivative() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="countOfParams">The count of parameters.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="args"/> is null.</exception>
        public Derivative(IExpression[] args, int countOfParams)
            : base(args, countOfParams)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if (args.Length != countOfParams)
                throw new ArgumentException();
            if (countOfParams == 2 && !(args[1] is Variable))
                throw new ArgumentException(Resource.InvalidExpression);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public Derivative(IExpression expression) : base(new[] { expression }, 1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        public Derivative(IExpression expression, Variable variable) : base(new[] { expression, variable }, 2) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Derivative" /> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="point">The point of derivation.</param>
        public Derivative(IExpression expression, Variable variable, Number point) : base(new[] { expression, variable, point }, 3) { }

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

            return diff.Analyze(Simplifier);
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
            return new Derivative(CloneArguments(), ParametersCount);
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
        public Variable Variable => ParametersCount >= 2 ? (Variable)m_arguments[1] : new Variable("x");

        /// <summary>
        /// Gets the derivative point.
        /// </summary>
        /// <value>
        /// The derivative point.
        /// </value>
        public Number DerivativePoint => ParametersCount >= 3 ? (Number)m_arguments[2] : null;

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public override IExpression[] Arguments
        {
            get
            {
                return base.Arguments;
            }
            set
            {
                if (value != null &&
                    ((value.Length >= 2 && !(value[1] is Variable)) ||
                     (value.Length >= 3 && !(value[2] is Number))))
                    throw new ArgumentException(Resource.InvalidExpression);

                base.Arguments = value;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters => 1;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters => 3;

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        /// <remarks>
        /// Usage of this property can affect performance. Don't use this property each time if you need to check result type of current expression. Just store/cache value only once and use it everywhere.
        /// </remarks>
        public override ExpressionResultType ResultType => ParametersCount == 3 ? ExpressionResultType.Number : ExpressionResultType.Expression;

        /// <summary>
        /// Gets or sets the simplifier.
        /// </summary>
        /// <value>
        /// The simplifier.
        /// </value>
        public ISimplifier Simplifier { get; set; }

        /// <summary>
        /// Gets or sets the differentiator.
        /// </summary>
        /// <value>
        /// The differentiator.
        /// </value>
        public IDifferentiator Differentiator { get; set; }

    }

}
