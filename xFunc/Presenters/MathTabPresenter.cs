using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Properties;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class MathTabPresenter
    {

        private IMainView view;

        private MathWorkspace workspace;

        public MathTabPresenter(IMainView view)
        {
            this.view = view;

            workspace = new MathWorkspace(Settings.Default.MaxCountOfExpressions);
        }

        private void UpdateList()
        {
            var vm = new List<MathWorkspaceItemViewModel>();
            for (int i = 0; i < workspace.Expressions.Count(); i++)
            {
                vm.Add(new MathWorkspaceItemViewModel(i + 1, workspace.Expressions.ElementAt(i)));
            }

            view.MathExpressions = vm;
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

        public void Remove(MathWorkspaceItemViewModel item)
        {
            workspace.Remove(item.Item);

            UpdateList();
        }

        public MathWorkspace Workspace
        {
            get
            {
                return workspace;
            }
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return workspace.Parser.AngleMeasurement;
            }
            set
            {
                workspace.Parser.AngleMeasurement = value;
            }
        }

    }

}
