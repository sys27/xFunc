using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xFunc.Maths.Expressions;
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
            valueBox.Text = variable.Value.ToString(CultureInfo.InvariantCulture);

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
