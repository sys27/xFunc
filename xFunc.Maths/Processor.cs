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
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Results;

namespace xFunc.Maths
{
    /// <summary>
    /// The main point of this library. Brings together all features.
    /// </summary>
    public class Processor
    {
        private readonly ITypeAnalyzer typeAnalyzer;
        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;
        private readonly IParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        public Processor()
        {
            simplifier = new Simplifier();
            differentiator = new Differentiator();
            parser = new Parser(differentiator, simplifier);
            typeAnalyzer = new TypeAnalyzer();

            Parameters = new ExpressionParameters();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        public Processor(
            ISimplifier simplifier,
            IDifferentiator differentiator)
            : this(
                simplifier,
                differentiator,
                new TypeAnalyzer(),
                new ExpressionParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="typeAnalyzer">The type analyzer.</param>
        /// <param name="parameters">The collection of parameters.</param>
        public Processor(
            ISimplifier simplifier,
            IDifferentiator differentiator,
            ITypeAnalyzer typeAnalyzer,
            ExpressionParameters parameters)
        {
            parser = new Parser();

            this.simplifier = simplifier ??
                              throw new ArgumentNullException(nameof(simplifier));
            this.differentiator = differentiator ??
                                  throw new ArgumentNullException(nameof(differentiator));
            this.typeAnalyzer = typeAnalyzer ??
                                throw new ArgumentNullException(nameof(typeAnalyzer));

            Parameters = parameters;
        }

        /// <summary>
        /// Solves the specified expression.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The result of solving.</returns>
        public IResult Solve(string function) => Solve(function, true);

        /// <summary>
        /// Solves the specified expression.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="simplify">if set to <c>true</c> parser will simplify expression.</param>
        /// <returns>The result of solving.</returns>
        public IResult Solve(string function, bool simplify)
        {
            var exp = Parse(function);
            exp.Analyze(typeAnalyzer);

            var result = exp.Execute(Parameters);
            if (result is double number)
            {
                return new NumberResult(number);
            }

            if (result is AngleValue angle)
            {
                return new AngleNumberResult(angle);
            }

            if (result is Complex complex)
            {
                return new ComplexNumberResult(complex);
            }

            if (result is bool boolean)
            {
                return new BooleanResult(boolean);
            }

            if (result is string str)
            {
                return new StringResult(str);
            }

            if (result is IExpression expression)
            {
                if (simplify)
                    return new ExpressionResult(Simplify(expression));

                return new ExpressionResult(expression);
            }

            throw new InvalidResultException();
        }

        /// <summary>
        /// Solves the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns>The result of solving.</returns>
        public TResult Solve<TResult>(string function) where TResult : IResult
            => (TResult)Solve(function);

        /// <summary>
        /// Solves the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="simplify">if set to <c>true</c> parser will simplify expression.</param>
        /// <returns>The result of solving.</returns>
        public TResult Solve<TResult>(string function, bool simplify) where TResult : IResult
            => (TResult)Solve(function, simplify);

        /// <summary>
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        public IExpression Simplify(IExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return expression.Analyze(simplifier);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression)
            => Differentiate(expression, Variable.X);

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression, Variable variable)
            => Differentiate(expression, variable, new ExpressionParameters());

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns the derivative.
        /// </returns>
        public IExpression Differentiate(
            IExpression expression,
            Variable variable,
            ExpressionParameters parameters)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var context = new DifferentiatorContext(parameters, variable);

            return expression.Analyze(differentiator, context);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
        /// <exception cref="ParseException">Error while parsing.</exception>
        public IExpression Parse(string function)
            => parser.Parse(function);

        /// <summary>
        /// Gets expression parameters object.
        /// </summary>
        /// <value>
        /// The expression parameters object.
        /// </value>
        public ExpressionParameters Parameters { get; }
    }
}