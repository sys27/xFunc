using System;

namespace xFunc.Library.Expressions.Maths
{

    public class ExponentiationMathExpression : BinaryMathExpression
    {

        public ExponentiationMathExpression() : base(null, null) { }

        public ExponentiationMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            if (parentMathExpression != null && parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} ^ {1})");
            }

            return ToString("{0} ^ {1}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HaveVar(firstMathExpression, variable))
            {
                SubtractionMathExpression sub = new SubtractionMathExpression(secondMathExpression, new NumberMathExpression(1));
                ExponentiationMathExpression inv = new ExponentiationMathExpression(firstMathExpression, sub);
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(secondMathExpression, inv);
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Derivative(variable), mul1);

                return mul2;
            }
            else if (MathParser.HaveVar(secondMathExpression, variable))
            {
                LnMathExpression ln = new LnMathExpression(firstMathExpression);
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(ln, this);
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(mul1, secondMathExpression.Derivative(variable));

                return mul2;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

    }

}
