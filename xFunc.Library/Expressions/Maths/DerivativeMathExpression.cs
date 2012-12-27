using System;

namespace xFunc.Library.Expressions.Maths
{

    public class DerivativeMathExpression : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private IMathExpression firstMathExpression;
        private VariableMathExpression variable;

        public DerivativeMathExpression() { }

        public DerivativeMathExpression(IMathExpression firstMathExpression, VariableMathExpression variable) { }

        public override string ToString()
        {
            return string.Format("deriv({0}, {1})", firstMathExpression.ToString(), variable.ToString());
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return Derivative().Calculate(parameters);
        }

        public IMathExpression Derivative()
        {
            if (firstMathExpression is DerivativeMathExpression)
                return MathParser.SimplifyExpressions(firstMathExpression.Derivative(variable).Derivative(variable));

            return MathParser.SimplifyExpressions(firstMathExpression.Derivative(variable));
        }

        // The local "variable" is ignored.
        public IMathExpression Derivative(VariableMathExpression variable)
        {
            if (firstMathExpression is DerivativeMathExpression)
                return MathParser.SimplifyExpressions(firstMathExpression.Derivative(this.variable).Derivative(this.variable));

            return MathParser.SimplifyExpressions(firstMathExpression.Derivative(this.variable));
        }

        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
                if (firstMathExpression != null)
                    firstMathExpression.Parent = this;
            }
        }

        public VariableMathExpression Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
                if (variable != null)
                    variable.Parent = this;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
