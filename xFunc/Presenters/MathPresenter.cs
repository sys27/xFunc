// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Results;
using xFunc.Properties;
using xFunc.ViewModels;
using xFunc.Views;

namespace xFunc.Presenters
{

    public class MathPresenter : INotifyPropertyChanged
    {

        private readonly IMathView view;

        private readonly Processor processor;
        private OutputFormats outputFormat;

        public event PropertyChangedEventHandler PropertyChanged;

        public MathPresenter(IMathView view, Processor processor)
        {
            this.view = view;

            this.processor = processor;
            Workspace = new MathWorkspace(Settings.Default.MaxCountOfExpressions);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateList()
        {
            var vm = Workspace.Select((t, i) => new MathWorkspaceItemViewModel(i + 1, t));

            view.MathExpressions = vm;
        }

        public void Add(string strExp)
        {
            if (string.IsNullOrWhiteSpace(strExp))
                throw new ArgumentNullException(nameof(strExp));

            var exps = strExp.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(str => str.Trim());

            foreach (var s in exps)
            {
                var result = processor.Solve(s);
                if (result is NumberResult num)
                {
                    if (outputFormat == OutputFormats.Normal)
                    {
                        Workspace.Add(new MathWorkspaceItem(s, result, num.Result.ToString("F", CultureInfo.InvariantCulture)));
                        continue;
                    }
                    if (outputFormat == OutputFormats.Exponential)
                    {
                        Workspace.Add(new MathWorkspaceItem(s, result, num.Result.ToString("E", CultureInfo.InvariantCulture)));
                        continue;
                    }
                }

                Workspace.Add(new MathWorkspaceItem(s, result, result.ToString()));
            }

            UpdateList();
        }

        public void Clear()
        {
            Workspace.Clear();

            UpdateList();
        }

        public void Remove(MathWorkspaceItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Workspace.Remove(item.Item);

            UpdateList();
        }

        public MathWorkspace Workspace { get; }

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