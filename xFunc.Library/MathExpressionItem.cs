using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library
{

    public class MathExpressionItem
    {

        private string strExp;
        private IMathExpression exp;
        private string answer;

        public MathExpressionItem()
        {

        }

        public MathExpressionItem(string strExp, IMathExpression exp, string answer)
        {
            this.strExp = strExp;
            this.exp = exp;
            this.answer = answer;
        }

        public string StringExpression
        {
            get
            {
                return strExp;
            }
        }

        public IMathExpression Expression
        {
            get
            {
                return exp;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }
        }

    }

}
