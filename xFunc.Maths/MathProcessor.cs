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

        /// <summary>
        /// Initializes a new instance of the <see cref="MathProcessor"/> class.
        /// </summary>
        public MathProcessor()
        {
            lexer = new MathLexer();
            simplifier = new MathSimplifier();
            differentiator = new MathDifferentiator(simplifier);
            parser = new MathParser(lexer, simplifier);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathProcessor"/> class.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="simplifier">The simplifier.</param>
        /// <param name="differentiator">The differentiator.</param>
        public MathProcessor(ILexer lexer, ISimplifier simplifier, IDifferentiator differentiator)
        {
            this.lexer = lexer;
            this.simplifier = simplifier;
            this.differentiator = differentiator;
            parser = new MathParser(lexer, simplifier);
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

    }

}
