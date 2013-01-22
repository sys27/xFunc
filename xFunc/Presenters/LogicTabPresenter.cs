using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Logics;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class LogicTabPresenter
    {

        private IMainView view;

        private LogicWorkspace workspace;

        public LogicTabPresenter(IMainView view)
        {
            this.view = view;

            // TODO: add settings...
            this.workspace = new LogicWorkspace();
        }

        public void Add(string strExp)
        {
            workspace.Add(strExp);

            view.LogicExpressions = workspace.Expressions;
        }

        public void Remove(LogicWorkspaceItem item)
        {
            workspace.Remove(item);

            view.LogicExpressions = workspace.Expressions;
        }

        public LogicWorkspace Workspace
        {
            get
            {
                return workspace;
            }
        }

    }

}
