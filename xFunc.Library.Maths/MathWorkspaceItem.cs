using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Library.Maths
{

    public class MathWorkspaceItem
    {

        private string strExp;
        private IMathExpression exp;
        private string answer;

        public MathWorkspaceItem()
        {

        }

        public MathWorkspaceItem(string strExp, IMathExpression exp)
            : this(strExp, exp, null)
        {

        }

        public MathWorkspaceItem(string strExp, IMathExpression exp, string answer)
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
            internal set
            {
                answer = value;
            }
        }

    }

}
