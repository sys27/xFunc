using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{
    
    public class AndMathExpression : BinaryMathExpression
    {

        public AndMathExpression()
            : base(null, null)
        {
        }

        public AndMathExpression(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Round(firstMathExpression.Calculate(parameters)) & MathExtentions.Round(secondMathExpression.Calculate(parameters));
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
