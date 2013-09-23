using System;
using System.Globalization;
using xFunc.Maths.Expressions;
using xFunc.Maths.Resources;
using xFunc.Maths.Results;

namespace xFunc.Maths
{

    /// <summary>
    /// The main point of this library. Bring together all features.
    /// </summary>
    public class MathProcessor
    {

        private ILexer lexer;
        private ISimplifier simplifier;
        private IDifferentiator differentiator;
        private MathParser parser;

        private MathParameterCollection parameters;
        private MathFunctionCollection userFunctions;

        private NumeralSystem numberSystem;

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

        /// <summary>
        /// Solves the specified expression.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>The result of solving.</returns>
        public IResult Solve(string function)
        {
            var exp = this.Parse(function, true);

            if (exp is Derivative)
            {
                var deriv = exp as Derivative;
                return new ExpressionResult(Differentiate(deriv, deriv.Variable));
            }
            if (exp is Simplify)
            {
                return new ExpressionResult((exp as Simplify).Expression);
            }
            if (exp is Define)
            {
                Define assign = exp as Define;
                assign.Calculate(parameters, userFunctions);

                if (assign.Key is Variable)
                    return new StringResult(string.Format(Resource.AssignVariable, assign.Key, assign.Value));

                return new StringResult(string.Format(Resource.AssignFunction, assign.Key, assign.Value));
            }
            if (exp is Undefine)
            {
                Undefine undef = exp as Undefine;
                undef.Calculate(parameters, userFunctions);

                if (undef.Key is Variable)
                    return new StringResult(string.Format(Resource.UndefineVariable, undef.Key));

                return new StringResult(string.Format(Resource.UndefineFunction, undef.Key));
            }

            if (numberSystem == NumeralSystem.Decimal)
                return new NumberResult(exp.Calculate(parameters, userFunctions));

            return new StringResult(MathExtentions.ToNewBase((int)exp.Calculate(parameters, userFunctions), numberSystem));
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
        /// Gets or sets the numeral system.
        /// </summary>
        /// <value>
        /// The numeral system.
        /// </value>
        public NumeralSystem Base
        {
            get
            {
                return numberSystem;
            }
            set
            {
                numberSystem = value;
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
