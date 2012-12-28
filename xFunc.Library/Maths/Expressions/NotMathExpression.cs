using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
{

    public class NotMathExpression : UnaryMathExpression
    {

        public NotMathExpression()
            : base(null)
        {
        }

        public NotMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return ~(int)MathExtentions.Round(firstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
