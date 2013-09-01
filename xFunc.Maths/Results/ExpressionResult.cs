using System;
using xFunc.Maths.Expressions;

namespace xFunc.Maths.Results
{

    public class ExpressionResult : IResult
    {

        private IMathExpression exp;

        public ExpressionResult(IMathExpression exp)
        {
            this.exp = exp;
        }

        public override string ToString()
        {
            return exp.ToString();
        }

        public IMathExpression Result
        {
            get
            {
                return exp;
            }
        }

    }

}
