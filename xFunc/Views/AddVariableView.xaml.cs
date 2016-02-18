// Copyright 2012-2016 Dmitry Kischenko
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
using System.Globalization;
using System.Windows;
using System.Windows.Input;
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

            nameBox.Focus();
        }

        private void OKCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            double value;

            args.CanExecute = !string.IsNullOrWhiteSpace(this.nameBox.Text) && double.TryParse(this.valueBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        public string VariableName
        {
            get
            {
                return this.nameBox.Text;
            }
        }

        public double Value
        {
            get
            {
                return double.Parse(this.valueBox.Text, CultureInfo.InvariantCulture);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.readOnlyBox.IsChecked.Value;
            }
        }

    }

}
