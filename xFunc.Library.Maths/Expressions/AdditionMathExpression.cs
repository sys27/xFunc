using System;

namespace xFunc.Library.Maths.Expressions
{

    public class AdditionMathExpression : BinaryMathExpression
    {

        public AdditionMathExpression() : base(null, null) { }

        public AdditionMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} + {1})");
            }

            return ToString("{0} + {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) + secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                return new AdditionMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Derivative(variable).Clone());
            }
            else if (first)
            {
                return firstMathExpression.Clone().Derivative(variable);
            }
            else if (second)
            {
                return secondMathExpression.Derivative(variable).Clone();
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new AdditionMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
