using System;

namespace xFunc.Library.Expressions.Maths
{

    public class LogMathExpression : BinaryMathExpression
    {

        public LogMathExpression() : base(null, null) { }

        public LogMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("log({0}, {1})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(firstMathExpression.Calculate(parameters), secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HaveVar(firstMathExpression, variable))
            {
                if (!(secondMathExpression is NumberMathExpression))
                    throw new NotSupportedException();

                LnMathExpression ln = new LnMathExpression(secondMathExpression);
                MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression, ln);
                DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), mul);

                return div;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

    }

}
