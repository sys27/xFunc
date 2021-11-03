// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

        public string FunctionName => this.nameBox.Text;

        public string Function => this.funcBox.Text;

    }

}