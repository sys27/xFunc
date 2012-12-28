using System;

namespace xFunc.Library.Maths.Expressions
{
    
    public class MultiplicationMathExpression : BinaryMathExpression
    {

        public MultiplicationMathExpression() : base(null, null) { }

        public MultiplicationMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} * {1})");
            }

            return ToString("{0} * {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) * secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            var first = MathParser.HaveVar(firstMathExpression, variable);
            var second = MathParser.HaveVar(secondMathExpression, variable);

            if (first && second)
            {
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(firstMathExpression.Derivative(variable), secondMathExpression);
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression, secondMathExpression.Derivative(variable));
                AdditionMathExpression add = new AdditionMathExpression(mul1, mul2);

                return add;
            }
            else if (first)
            {
                return new MultiplicationMathExpression(firstMathExpression.Derivative(variable), secondMathExpression);
            }
            else if (second)
            {
                return new MultiplicationMathExpression(firstMathExpression, secondMathExpression.Derivative(variable));
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

    }

}
