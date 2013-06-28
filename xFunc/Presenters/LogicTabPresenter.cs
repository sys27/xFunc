using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Logics;
using xFunc.Properties;
using xFunc.ViewModels;
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

            workspace = new LogicWorkspace(Settings.Default.MaxCountOfExpressions);
        }

        private void UpdateList()
        {
            var vm = new List<LogicWorkspaceItemViewModel>();
            for (int i = 0; i < workspace.Expressions.Count(); i++)
            {
                vm.Add(new LogicWorkspaceItemViewModel(i + 1, workspace.Expressions.ElementAt(i)));
            }

            view.LogicExpressions = vm;
        }

        public void Add(string strExp)
        {
            workspace.Add(strExp);

            UpdateList();
        }

        public void Clear()
        {
            workspace.Clear();

            UpdateList();
        }

        public void Remove(LogicWorkspaceItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            workspace.Remove(item.Item);

            UpdateList();
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
