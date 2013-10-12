// Copyright 2012-2013 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Logics;
using xFunc.Properties;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class LogicPresenter
    {

        private ILogicView view;

        private LogicWorkspace workspace;

        public LogicPresenter(ILogicView view)
        {
            this.view = view;

            workspace = new LogicWorkspace(Settings.Default.MaxCountOfExpressions);
        }

        private void UpdateList()
        {
            var vm = new List<LogicWorkspaceItemViewModel>();
            for (int i = 0; i < workspace.Count; i++)
                vm.Add(new LogicWorkspaceItemViewModel(i + 1, workspace[i]));

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
