using System;
using System.Collections.Generic;
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

        private void OKCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !string.IsNullOrWhiteSpace(this.nameBox.Text) && !string.IsNullOrWhiteSpace(this.funcBox.Text);
        }

    }

}
