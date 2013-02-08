using System;
using xFunc.Logics;
using xFunc.Logics.Expressions;

namespace xFunc.ViewModels
{

    public class LogicWorkspaceItemViewModel
    {

        private int index;
        private LogicWorkspaceItem item;

        public LogicWorkspaceItemViewModel(int index, LogicWorkspaceItem item)
        {
            this.index = index;
            this.item = item;
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        public LogicWorkspaceItem Item
        {
            get
            {
                return item;
            }
        }

        public string StringExpression
        {
            get
            {
                return item.StringExpression;
            }
        }

        public ILogicExpression Expression
        {
            get
            {
                return item.Expression;
            }
        }

        public string Answer
        {
            get
            {
                return item.Answer;
            }
        }

    }

}
