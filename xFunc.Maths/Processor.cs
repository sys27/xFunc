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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Resources;
using xFunc.Maths.Results;

namespace xFunc.Maths
{

    /// <summary>
    /// The main point of this library. Bring together all features.
    /// </summary>
    public class Processor
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IDifferentiator differentiator;
        private IParser parser;

        private readonly ExpressionParameters parameters;
        private NumeralSystem numeralSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        public Processor()
        {
            lexer = new Lexer();
            simplifier = new Simplifier();
            differentiator = new Differentiator(simplifier);
            parser = new Parser(new ExpressionFactory(
                                    new DefaultDependencyResolver(new Type[] { typeof(ISimplifier), typeof(IDifferentiator) },
                                                                  new object[] { simplifier, differentiator })
                                ));

            parameters = new ExpressionParameters(AngleMeasurement.Degree, new ParameterCollection(), new FunctionCollection());
            numeralSystem = NumeralSystem.Decimal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor"/> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        public Processor(ILexer lexer, IParser parser, ISimplifier simplifier, IDifferentiator differentiator)
            : this(lexer, parser, simplifier, differentiator, new ExpressionParameters(AngleMeasurement.Degree, new ParameterCollection(), new FunctionCollection()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Processor" /> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="parameters">The collection of parameters.</param>
        public Processor(ILexer lexer, IParser parser, ISimplifier simplifier, IDifferentiator differentiator, ExpressionParameters parameters)
        {
            this.lexer = lexer;
            this.simplifier = simplifier;
            this.differentiator = differentiator;
            this.parser = parser;

            this.parameters = parameters;
            this.numeralSystem = NumeralSystem.Decimal;
        }

        /// <summary>
        /// Solves the specified expression.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The result of solving.</returns>
        public IResult Solve(string function)
        {
            var exp = Parse(function, true);

            if (exp is Define)
            {
                var assign = exp as Define;
                assign.Execute(parameters);

                if (assign.Key is Variable)
                    return new StringResult(string.Format(Resource.AssignVariable, assign.Key, assign.Value));

                return new StringResult(string.Format(Resource.AssignFunction, assign.Key, assign.Value));
            }
            if (exp is Undefine)
            {
                var undef = exp as Undefine;
                undef.Execute(parameters);

                if (undef.Key is Variable)
                    return new StringResult(string.Format(Resource.UndefineVariable, undef.Key));

                return new StringResult(string.Format(Resource.UndefineFunction, undef.Key));
            }

            var result = exp.Execute(parameters);
            if (result is double)
            {
                if (numeralSystem == NumeralSystem.Decimal)
                    return new NumberResult((double)result);

                return new StringResult(MathExtentions.ToNewBase((int)(double)result, numeralSystem));
            }
            if (result is int)
            {
                if (numeralSystem == NumeralSystem.Decimal)
                    return new NumberResult((int)result);

                return new StringResult(MathExtentions.ToNewBase((int)result, numeralSystem));
            }
            if (result is bool)
            {
                return new BooleanResult((bool)result);
            }
            if (result is IExpression)
            {
                return new ExpressionResult((IExpression)result);
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
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        public IExpression Simplify(IExpression expression)
        {
            return simplifier.Simplify(expression);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression)
        {
            return differentiator.Differentiate(expression);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IExpression Differentiate(IExpression expression, Variable variable)
        {
            return differentiator.Differentiate(expression, variable);
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
            return differentiator.Differentiate(expression, variable, parameters);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        public IExpression Parse(string function)
        {
            return Parse(function, true);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="simplify">if set to <c>true</c>, simplifies the expression.</param>
        /// <returns>The parsed expression.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="function"/> is null.</exception>
        /// <exception cref="ParserException">Error while parsing.</exception>
        public IExpression Parse(string function, bool simplify)
        {
            if (simplify)
                return simplifier.Simplify(parser.Parse(lexer.Tokenize(function)));

            return parser.Parse(lexer.Tokenize(function));
        }

        /// <summary>
        /// Gets or sets a implementation of <see cref="ILexer"/>.
        /// </summary>
        public ILexer Lexer
        {
            get
            {
                return lexer;
            }
            set
            {
                lexer = value;
            }
        }

        /// <summary>
        /// Gets or sets the parser.
        /// </summary>
        /// <value>
        /// The parser.
        /// </value>
        public IParser Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        /// <summary>
        /// Gets or sets the simplifier.
        /// </summary>
        /// <value>
        /// The simplifier.
        /// </value>
        public ISimplifier Simplifier
        {
            get
            {
                return simplifier;
            }
            set
            {
                simplifier = value;
            }
        }

        /// <summary>
        /// Gets or sets the differentiator.
        /// </summary>
        /// <value>
        /// The differentiator.
        /// </value>
        public IDifferentiator Differentiator
        {
            get
            {
                return differentiator;
            }
            set
            {
                differentiator = value;
            }
        }

        /// <summary>
        /// Gets expression parameters object.
        /// </summary>
        /// <value>
        /// The expression parameters object.
        /// </value>
        public ExpressionParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Gets or sets the numeral system.
        /// </summary>
        /// <value>
        /// The numeral system.
        /// </value>
        public NumeralSystem Base
        {
            get
            {
                return numeralSystem;
            }
            set
            {
                numeralSystem = value;
            }
        }

    }

}
