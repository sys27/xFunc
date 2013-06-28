using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths
{

    public class MathDifferentiator : IDifferentiator
    {

        private ISimplifier simplifier;

        public MathDifferentiator()
            : this(new MathSimplifier())
        {

        }

        public MathDifferentiator(ISimplifier simplifier)
        {
            this.simplifier = simplifier;
        }

        public IMathExpression Differentiate(IMathExpression expression)
        {
            return Differentiate(expression, new Variable("x"));
        }

        public IMathExpression Differentiate(IMathExpression expression, Variable variable)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return simplifier.Simplify(expression.Differentiate(variable));
        }

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
