// Copyright 2012-2015 Dmitry Kischenko
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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Results;
using xFunc.Properties;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class MathPresenter : INotifyPropertyChanged
    {

        private IMathView view;

        private Processor processor;
        private MathWorkspace workspace;

        private OutputFormats outputFormat;

        public event PropertyChangedEventHandler PropertyChanged;

        public MathPresenter(IMathView view, Processor processor)
        {
            this.view = view;

            this.processor = processor;
            workspace = new MathWorkspace(Settings.Default.MaxCountOfExpressions);
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateList()
        {
            var vm = new List<MathWorkspaceItemViewModel>();
            for (int i = 0; i < workspace.Count; i++)
                vm.Add(new MathWorkspaceItemViewModel(i + 1, workspace[i]));

            view.MathExpressions = vm;
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException(nameof(strExp));

            var exps = strExp.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var s in exps)
            {
                var result = processor.Solve(s);
                var num = result as NumberResult;
                if (num != null)
                {
                    if (outputFormat == OutputFormats.Normal)
                    {
                        workspace.Add(new MathWorkspaceItem(s, result, num.Result.ToString("F", CultureInfo.InvariantCulture)));
                        continue;
                    }
                    else if (outputFormat == OutputFormats.Exponential)
                    {
                        workspace.Add(new MathWorkspaceItem(s, result, num.Result.ToString("E", CultureInfo.InvariantCulture)));
                        continue;
                    }
                }

                workspace.Add(new MathWorkspaceItem(s, result, result.ToString()));
            }

            UpdateList();
        }

        public void Clear()
        {
            workspace.Clear();

            UpdateList();
        }

        public void Remove(MathWorkspaceItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

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
                return processor.AngleMeasurement;
            }
            set
            {
                processor.AngleMeasurement = value;
                OnPropertyChanged(nameof(AngleMeasurement));
            }
        }

        public NumeralSystem Base
        {
            get
            {
                return processor.Base;
            }
            set
            {
                processor.Base = value;
                OnPropertyChanged(nameof(Base));
            }
        }

        public OutputFormats OutputFormat
        {
            get
            {
                return outputFormat;
            }
            set
            {
                outputFormat = value;
                OnPropertyChanged(nameof(OutputFormat));
            }
        }

    }

}
