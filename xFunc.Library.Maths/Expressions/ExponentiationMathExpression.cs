using System;

namespace xFunc.Library.Maths.Expressions
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
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                SubtractionMathExpression sub = new SubtractionMathExpression(secondMathExpression.Clone(), new NumberMathExpression(1));
                ExponentiationMathExpression inv = new ExponentiationMathExpression(firstMathExpression.Clone(), sub);
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(secondMathExpression.Clone(), inv);
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);

                return mul2;
            }
            else if (MathParser.HasVar(secondMathExpression, variable))
            {
                LnMathExpression ln = new LnMathExpression(firstMathExpression.Clone());
                MultiplicationMathExpression mul1 = new MultiplicationMathExpression(ln, this.Clone());
                MultiplicationMathExpression mul2 = new MultiplicationMathExpression(mul1, secondMathExpression.Clone().Derivative(variable));

                return mul2;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new ExponentiationMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
