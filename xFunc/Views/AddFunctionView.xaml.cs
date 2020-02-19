// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Windows;
using System.Windows.Input;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class AddFunctionView : Window
    {

        public static RoutedCommand OKCommand = new RoutedCommand();

        public AddFunctionView()
        {
            InitializeComponent();

            this.nameBox.Focus();
        }

        public AddFunctionView(FunctionViewModel function)
        {
            InitializeComponent();

            this.nameBox.Text = function.Function.ToString();
            this.nameBox.IsEnabled = false;
            this.funcBox.Text = function.Value;
            this.nameBox.Focus();
        }

        private void OKCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !string.IsNullOrWhiteSpace(this.nameBox.Text) && !string.IsNullOrWhiteSpace(this.funcBox.Text);
        }

        public string FunctionName
        {
            get
            {
                return this.nameBox.Text;
            }
        }

        public string Function
        {
            get
            {
                return this.funcBox.Text;
            }
        }

    }

}