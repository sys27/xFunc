using System;

namespace xFunc.Library.Expressions.Maths
{
    
    public class SubtractionMathExpression : BinaryMathExpression
    {

        public SubtractionMathExpression() : base(null, null) { }

        public SubtractionMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} - {1})");
            }

            return ToString("{0} - {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) - secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            var first = MathParser.HaveVar(firstMathExpression, variable);
            var second = MathParser.HaveVar(secondMathExpression, variable);

            if (first && second)
            {
                return new SubtractionMathExpression(firstMathExpression.Derivative(variable), secondMathExpression.Derivative(variable));
            }
            else if (first)
            {
                return firstMathExpression.Derivative(variable);
            }
            else if (second)
            {
                return new UnaryMinusMathExpression(secondMathExpression.Derivative(variable));
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

    }

}
