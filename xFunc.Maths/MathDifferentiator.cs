using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public class MathDifferentiator : IDifferentiator
    {

        private ISimplifier simplifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathDifferentiator"/> class.
        /// </summary>
        public MathDifferentiator()
            : this(new MathSimplifier())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathDifferentiator"/> class.
        /// </summary>
        /// <param name="simplifier">The simplifier.</param>
        public MathDifferentiator(ISimplifier simplifier)
        {
            this.simplifier = simplifier;
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression)
        {
            return Differentiate(expression, new Variable("x"));
        }

        /// <summary>
        /// Differentiates the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="variable">The variable.</param>
        /// <returns>Returns the derivative.</returns>
        public IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return simplifier.Simplify(expression.Differentiate(variable));
        }

        /// <summary>
        /// Gets or sets the simplifier.
        /// </summary>
        /// <value>The simplifier.</value>
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

    }

}
