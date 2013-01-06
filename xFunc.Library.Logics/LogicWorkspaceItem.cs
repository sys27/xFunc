using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Logics.Expressions;

namespace xFunc.Library.Logics
{

    public class LogicWorkspaceItem
    {

        private string strExp;
        private ILogicExpression exp;
        private string answer;

        public LogicWorkspaceItem()
        {

        }

        public LogicWorkspaceItem(string strExp, ILogicExpression exp, string answer)
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
