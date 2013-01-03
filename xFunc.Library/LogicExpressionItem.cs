using System;
using xFunc.Library.Logics.Expressions;

namespace xFunc.Library
{

    public class LogicExpressionItem
    {

        private string strExp;
        private ILogicExpression exp;
        private string answer;

        public LogicExpressionItem()
        {

        }

        public LogicExpressionItem(string strExp, ILogicExpression exp, string answer)
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

        public ILogicExpression Expression
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
