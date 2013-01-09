using System;

namespace xFunc.Library.Maths.Expressions
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
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                return new SubtractionMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Clone().Derivative(variable));
            }
            else if (first)
            {
                return firstMathExpression.Clone().Derivative(variable);
            }
            else if (second)
            {
                return new UnaryMinusMathExpression(secondMathExpression.Clone().Derivative(variable));
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new SubtractionMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
