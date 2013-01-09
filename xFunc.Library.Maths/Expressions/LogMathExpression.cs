using System;

namespace xFunc.Library.Maths.Expressions
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
            if (MathParser.HasVar(firstMathExpression, variable))
            {
                if (!(secondMathExpression is NumberMathExpression))
                    throw new NotSupportedException();

                LnMathExpression ln = new LnMathExpression(secondMathExpression.Clone());
                MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression.Clone(), ln);
                DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), mul);

                return div;
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

        public override IMathExpression Clone()
        {
            return new LogMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}
