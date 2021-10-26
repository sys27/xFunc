// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using xFunc.Maths.Expressions.Collections;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class AddVariableView : Window
    {

        public static RoutedCommand OKCommand = new RoutedCommand();

        public AddVariableView()
        {
            InitializeComponent();

            nameBox.Focus();
        }

        public AddVariableView(VariableViewModel variable)
        {
            InitializeComponent();

            nameBox.Text = variable.Variable;
            nameBox.IsEnabled = false;
            valueBox.Text = Convert.ToString(variable.Value, CultureInfo.InvariantCulture);
            if (variable.Type != ParameterType.Normal)
            {
                valueBox.IsEnabled = false;
            }
            readOnlyBox.IsEnabled = false;

            nameBox.Focus();
        }

        private void OKCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !string.IsNullOrWhiteSpace(this.nameBox.Text) && double.TryParse(this.valueBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        }

        public string VariableName => this.nameBox.Text;

        public double Value => double.Parse(this.valueBox.Text, CultureInfo.InvariantCulture);

        public bool IsReadOnly => this.readOnlyBox.IsChecked.Value;
    }

}