using System;
using xFunc.Library.Logics.Expressions;

namespace xFunc.Library
{

    public class LogicExpressionItem
    {

        private ILogicExpression exp;
        private string answer;

        public LogicExpressionItem()
        {

        }

        public LogicExpressionItem(ILogicExpression exp, string answer)
        {
            this.exp = exp;
            this.answer = answer;
        }

        public ILogicExpression Expression
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
