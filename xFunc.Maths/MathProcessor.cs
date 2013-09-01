using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public class MathProcessor
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IDifferentiator differentiator;
        private MathParser parser;

        private MathParameterCollection parameters;
        private MathFunctionCollection userFunctions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathProcessor"/> class.
        /// </summary>
        public MathProcessor()
        {
            lexer = new MathLexer();
            simplifier = new MathSimplifier();
            differentiator = new MathDifferentiator(simplifier);
            parser = new MathParser(lexer, simplifier);

            parameters = new MathParameterCollection();
            userFunctions = new MathFunctionCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathProcessor"/> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        public MathProcessor(ILexer lexer, ISimplifier simplifier, IDifferentiator differentiator)
            : this(lexer, simplifier, differentiator, new MathParameterCollection(), new MathFunctionCollection())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathProcessor" /> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        /// <param name="parameters">The collection of parameters.</param>
        /// <param name="userFunctions">The collection of functions.</param>
        public MathProcessor(ILexer lexer, ISimplifier simplifier, IDifferentiator differentiator, MathParameterCollection parameters, MathFunctionCollection userFunctions)
        {
            this.lexer = lexer;
            this.simplifier = simplifier;
            this.differentiator = differentiator;
            this.parser = new MathParser(lexer, simplifier);

            this.parameters = parameters;
            this.userFunctions = userFunctions;
        }

        public void Solve(string function)
        {
            var func = this.Parse(function, true);
        }

        /// <summary>
        /// Simplifies the <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">A expression to simplify.</param>
        /// <returns>A simplified expression.</returns>
        public IMathExpression Simplify(IMathExpression expression)
        {
            return simplifier.Simplify(expression);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression)
        {
            return differentiator.Differentiate(expression);
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            return differentiator.Differentiate(expression, variable);
        }

        /// <summary>
        /// Parses the specified function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The parsed expression.</returns>
        public IMathExpression Parse(string function)
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
        /// <exception cref="MathParserException">Error while parsing.</exception>
        public IMathExpression Parse(string function, bool simplify)
        {
            if (simplify)
                return simplifier.Simplify(parser.Parse(function));

            return parser.Parse(function);
        }

        /// <summary>
        /// Gets or Sets a measurement of angles.
        /// </summary>
        /// <seealso cref="AngleMeasurement"/>
        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return parser.AngleMeasurement;
            }
            set
            {
                parser.AngleMeasurement = value;
            }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public MathParameterCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        public MathFunctionCollection UserFunctions
        {
            get
            {
                return userFunctions;
            }
        }

    }

}
