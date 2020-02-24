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
using System.Numerics;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Results;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths
{
    /// <summary>
    /// The main point of this library. Bring together all features.
    /// </summary>
    public class Processor
    {
        private readonly ITypeAnalyzer typeAnalyzer;
        private readonly IDifferentiator differentiator;
        private readonly ISimplifier simplifier;
        private readonly IParser parser;
        private readonly ILexer lexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        public Processor()
        {
            lexer = new Lexer();
            simplifier = new Simplifier();
            differentiator = new Differentiator();
            parser = new Parser(differentiator, simplifier);
            typeAnalyzer = new TypeAnalyzer();

            Parameters = new ExpressionParameters(AngleMeasurement.Degree, new ParameterCollection(), new FunctionCollection());
            NumeralSystem = NumeralSystem.Decimal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        public Processor(
            ILexer lexer,
            IParser parser,
            ISimplifier simplifier,
            IDifferentiator differentiator)
            : this(
                lexer,
                parser,
                simplifier,
                differentiator,
                new TypeAnalyzer(),
                new ExpressionParameters(AngleMeasurement.Degree, new ParameterCollection(), new FunctionCollection()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="typeAnalyzer">The type analyzer.</param>
        /// <param name="parameters">The collection of parameters.</param>
        public Processor(
            ILexer lexer,
            IParser parser,
            ISimplifier simplifier,
            IDifferentiator differentiator,
            ITypeAnalyzer typeAnalyzer,
            ExpressionParameters parameters)
        {
            this.lexer = lexer ??
                         throw new ArgumentNullException(nameof(lexer));
            this.simplifier = simplifier ??
                              throw new ArgumentNullException(nameof(simplifier));
            this.differentiator = differentiator ??
                                  throw new ArgumentNullException(nameof(differentiator));
            this.parser = parser ??
                          throw new ArgumentNullException(nameof(parser));
            this.typeAnalyzer = typeAnalyzer ??
                                throw new ArgumentNullException(nameof(typeAnalyzer));

            Parameters = parameters;
            NumeralSystem = NumeralSystem.Decimal;
        }

        /// <summary>
        /// Solves the specified expression.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The result of solving.</returns>
        public IResult Solve(string function)
        {
            return Solve(function, true);
        }

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
                if (NumeralSystem == NumeralSystem.Decimal)
                    return new NumberResult(number);

                return new StringResult(MathExtensions.ToNewBase((int)number, NumeralSystem));
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
        {
            return (TResult)Solve(function);
        }

        /// <summary>
        /// Solves the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="simplify">if set to <c>true</c> parser will simplify expression.</param>
        /// <returns>The result of solving.</returns>
        public TResult Solve<TResult>(string function, bool simplify) where TResult : IResult
        {
            return (TResult)Solve(function, simplify);
        }

        /// <summary>
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        public IExpression Simplify(IExpression expression)
        {
            return expression.Analyze(simplifier);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression)
        {
            return Differentiate(expression, Variable.X);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression, Variable variable)
        {
            return Differentiate(expression, variable, new ExpressionParameters());
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Returns the derivative.
        /// </returns>
        public IExpression Differentiate(IExpression expression, Variable variable, ExpressionParameters parameters)
        {
            differentiator.Variable = variable;
            differentiator.Parameters = parameters;

            return expression.Analyze(differentiator);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
        /// <exception cref="ParseException">Error while parsing.</exception>
        public IExpression Parse(string function)
        {
            return parser.Parse(lexer.Tokenize(function));
        }

        /// <summary>
        /// Converts the string into a sequence of tokens.
        /// </summary>
        /// <param name="function">The string that contains the functions and operators.</param>
        /// <returns>The sequence of tokens.</returns>
        public IEnumerable<IToken> Tokenize(string function)
        {
            return lexer.Tokenize(function);
        }

        /// <summary>
        /// Gets expression parameters object.
        /// </summary>
        /// <value>
        /// The expression parameters object.
        /// </value>
        public ExpressionParameters Parameters { get; }

        /// <summary>
        /// Gets or sets the numeral system.
        /// </summary>
        /// <value>
        /// The numeral system.
        /// </value>
        public NumeralSystem NumeralSystem { get; set; }
    }
}