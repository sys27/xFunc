using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library
{

    public class MathExpressionItem
    {

        private IMathExpression exp;
        private string answer;

        public MathExpressionItem()
        {

        }

        public MathExpressionItem(IMathExpression exp, string answer)
        {
            this.exp = exp;
            this.answer = answer;
        }

        public IMathExpression Expression
        {
            get
            {
                return exp;
            }
            set
            {
                exp = value;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
            }
        }

    }

}
