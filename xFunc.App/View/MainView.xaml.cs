using System.Windows;
using xFunc.App.ViewModel;
using xFunc.App.Properties;

namespace xFunc.App.View
{

    public partial class MainView : Window
    {

        public MainView()
        {
            InitializeComponent();

            if (Settings.Default.MainViewLeft == 0 && Settings.Default.MainViewTop == 0)
            {
                this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
                this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            }
            else
            {
                this.Top = Settings.Default.MainViewTop;
                this.Left = Settings.Default.MainViewLeft;
            }

            functionTextBox.SelectionChanged += SetSelectionPosition;
            functionTextBox.TextChanged += SetSelectionPosition;
            functionTextBox.Focus();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.MainViewTop = this.Top;
            Settings.Default.MainViewLeft = this.Left;
            Settings.Default.Save();
            base.OnClosing(e);
        }

        private void SetSelectionPosition(object o, RoutedEventArgs args)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.FunctionSelectionStart = functionTextBox.SelectionStart;
            viewModel.FunctionSelectionLength = functionTextBox.SelectionLength;
        }

    }

}
