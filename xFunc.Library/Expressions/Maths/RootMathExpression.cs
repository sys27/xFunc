using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Expressions.Maths
{

    public class RootMathExpression : BinaryMathExpression
    {

        public RootMathExpression() : base(null, null) { }

        public RootMathExpression(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        public override string ToString()
        {
            return ToString("root({0}, {1})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Pow(firstMathExpression.Calculate(parameters), 1 / secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            if (MathParser.HaveVar(firstMathExpression, variable) || MathParser.HaveVar(secondMathExpression, variable))
            {
                DivisionMathExpression div = new DivisionMathExpression(new NumberMathExpression(1), secondMathExpression);
                ExponentiationMathExpression inv = new ExponentiationMathExpression(firstMathExpression, div);

                return inv.Derivative(variable);
            }
            else
            {
                return new NumberMathExpression(0);
            }
        }

    }

}
