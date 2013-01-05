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
using xFunc.Library.Maths.Expressions;

namespace xFunc.App.Views
{

    public partial class MainView : Fluent.RibbonWindow, IMainView
    {

        private MainPresenter presenter;

        public MainView()
        {
            this.presenter = new MainPresenter(this);

            InitializeComponent();

            expressionBox.Focus();
        }

        private void DergeeButton_Click(object o, RoutedEventArgs args)
        {
            this.radianButton.IsChecked = false;
            this.gradianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Degree);
        }

        private void RadianButton_Click(object o, RoutedEventArgs args)
        {
            this.degreeButton.IsChecked = false;
            this.gradianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Radian);
        }

        private void GradianButton_Click(object o, RoutedEventArgs args)
        {
            this.degreeButton.IsChecked = false;
            this.radianButton.IsChecked = false;
            presenter.SetAngleMeasurement(AngleMeasurement.Gradian);
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
                // todo: ...
            }
            else
            {
                expressionBox.Text = expressionBox.Text.Insert(prevSelectionStart, func + "()");
                expressionBox.SelectionStart = prevSelectionStart + func.Length + 2;
            }

            expressionBox.Focus();
        }

    }

}
