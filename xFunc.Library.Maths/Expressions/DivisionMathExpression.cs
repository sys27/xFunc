using System;

namespace xFunc.Library.Maths.Expressions
{

    public class DivisionMathExpression : BinaryMathExpression
    {

        public DivisionMathExpression() : base(null, null) { }

        public DivisionMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} / {1})");
            }

            return ToString("{0} / {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) / secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Clone());
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone().Derivative(variable));
                SubtractionMathExpression sub = new SubtractionMathExpression(mul1, mul2);
                ExponentiationMathExpression inv = new ExponentiationMathExpression(secondMathExpression.Clone(), new NumberMathExpression(2));
                DivisionMathExpression division = new DivisionMathExpression(sub, inv);

                return division;
            }
            else if (first)
            {
                return new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), secondMathExpression.Clone());
            }
            else if (second)
            {
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone().Derivative(variable));
                UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(mul2);
                ExponentiationMathExpression inv = new ExponentiationMathExpression(secondMathExpression.Clone(), new NumberMathExpression(2));
                DivisionMathExpression division = new DivisionMathExpression(unMinus, inv);

                return division;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new DivisionMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
