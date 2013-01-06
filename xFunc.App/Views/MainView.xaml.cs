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
using xFunc.App.Presenters;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.App.Views
{

    public partial class MainView : Fluent.RibbonWindow, IMainView
    {

        private MainPresenter presenter;

        public static RoutedCommand DegreeCommand = new RoutedCommand();
        public static RoutedCommand RadianCommand = new RoutedCommand();
        public static RoutedCommand GradianCommand = new RoutedCommand();

        public MainView()
        {
            InitializeComponent();

            expressionBox.Focus();

            this.presenter = new MainPresenter(this);

            degreeButton.IsChecked = true;
        }

        private void DergeeButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            this.radianButton.IsChecked = false;
            this.gradianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Degree);
        }

        private void RadianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            this.degreeButton.IsChecked = false;
            this.gradianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Radian);
        }

        private void GradianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            this.degreeButton.IsChecked = false;
            this.radianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Gradian);
        }

        private void AndleButtons_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == logicTab)
                args.CanExecute = false;
            else
                args.CanExecute = true;
        }

        private void InsertChar_Click(object o, RoutedEventArgs args)
        {
            var prevSelectionStart = expressionBox.SelectionStart;
            expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, ((Button)o).Tag.ToString());
            expressionBox.SelectionStart = ++prevSelectionStart;
            expressionBox.Focus();
        }

        private void InsertFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            var prevSelectionStart = expressionBox.SelectionStart;

            if (expressionBox.SelectionLength > 0)
            {
                var prevSelectionLength = expressionBox.SelectionLength;

                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ")");
                expressionBox.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 2;
            }
            else
            {
                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func + "()");
                expressionBox.SelectionStart = prevSelectionStart + func.Length + 1;
            }

            expressionBox.Focus();
        }

        private void InsertInv_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            var prevSelectionStart = expressionBox.SelectionStart;

            if (expressionBox.SelectionLength > 0)
            {
                var prevSelectionLength = expressionBox.SelectionLength;

                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, "(").Insert(prevSelectionStart + prevSelectionLength + 1, ")" + func);
                expressionBox.SelectionStart = prevSelectionStart + prevSelectionLength + func.Length + 2;
            }
            else
            {
                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func);
                expressionBox.SelectionStart = prevSelectionStart + func.Length;
            }

            expressionBox.Focus();
        }

        private void InsertDoubleArgFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            var prevSelectionStart = expressionBox.SelectionStart;

            if (expressionBox.SelectionLength > 0)
            {
                var prevSelectionLength = expressionBox.SelectionLength;

                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ", )");
                expressionBox.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 3;
            }
            else
            {
                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func + "(, )");
                expressionBox.SelectionStart = prevSelectionStart + func.Length + 2;
            }

            expressionBox.Focus();
        }

        private void ExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
            {
                presenter.AddExpression(expressionBox.Text);
                expressionBox.Text = string.Empty;
            }
        }

        public IEnumerable<MathWorkspaceItem> MathExpressions
        {
            set
            {
                mathExpsListBox.ItemsSource = value;
            }
        }

    }

}
