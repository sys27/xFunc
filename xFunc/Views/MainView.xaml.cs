// Copyright 2012-2013 Dmitry Kischenko
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using xFunc.Logics.Exceptions;
using xFunc.Logics.Expressions;
using xFunc.Maths.Exceptions;
using xFunc.Maths.Expressions;
using xFunc.Presenters;
using xFunc.Properties;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class MainView : Fluent.RibbonWindow, IMainView
    {

        private MathTabPresenter mathPresenter;
        private LogicTabPresenter logicPresenter;
        private GraphsTabPresenter graphsPresenter;
        private TruthTableTabPresenter truthTablePresenter;

        public static RoutedCommand DegreeCommand = new RoutedCommand();
        public static RoutedCommand RadianCommand = new RoutedCommand();
        public static RoutedCommand GradianCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();
        public static RoutedCommand ClearCommand = new RoutedCommand();

        public static RoutedCommand AboutCommand = new RoutedCommand();

        public MainView()
        {
            InitializeComponent();

            mathExpressionBox.Focus();

            mathPresenter = new MathTabPresenter(this);
            logicPresenter = new LogicTabPresenter(this);
            graphsPresenter = new GraphsTabPresenter(this);
            truthTablePresenter = new TruthTableTabPresenter(this);

            LoadSettings();

            switch (mathPresenter.AngleMeasurement)
            {
                case AngleMeasurement.Degree:
                    degreeButton.IsChecked = true;
                    break;
                case AngleMeasurement.Radian:
                    radianButton.IsChecked = true;
                    break;
                case AngleMeasurement.Gradian:
                    gradianButton.IsChecked = true;
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveSettings();

            base.OnClosing(e);
        }

        private void LoadSettings()
        {
            if (Settings.Default.WindowState != WindowState.Minimized)
            {
                WindowState = Settings.Default.WindowState;

                if (Settings.Default.WindowTop == 0 || Settings.Default.WindowLeft == 0)
                {
                    Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
                    Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                }
                else
                {
                    Top = Settings.Default.WindowTop;
                    Left = Settings.Default.WindowLeft;
                }
            }
            Width = Settings.Default.WindowWidth;
            Height = Settings.Default.WindowHeight;

            mathPresenter.AngleMeasurement = Settings.Default.AngleMeasurement;

            tabControl.SelectedIndex = Settings.Default.SelectedTabIndex;

            numberToolBar.IsExpanded = Settings.Default.NumbersExpanded;
            standartMathToolBar.IsExpanded = Settings.Default.StandartMathExpanded;
            trigonometricToolBar.IsExpanded = Settings.Default.TrigonometricExpanded;
            hyperbolicToolBar.IsExpanded = Settings.Default.HyperbolicExpanded;
            bitwiseToolBar.IsExpanded = Settings.Default.BitwiseExpanded;
            constantsMathToolBar.IsExpanded = Settings.Default.ConstantsMathExpanded;
            additionalToolBar.IsExpanded = Settings.Default.AdditionalExpanded;

            standartLogicToolBar.IsExpanded = Settings.Default.StandartLogicExpanded;
            constantsLogicToolBar.IsExpanded = Settings.Default.ConstantsLogicExpanded;
        }

        private void SaveSettings()
        {
            if (WindowState != WindowState.Minimized)
                Settings.Default.WindowState = WindowState;

            Settings.Default.WindowTop = Top;
            Settings.Default.WindowLeft = Left;

            Settings.Default.WindowWidth = Width;
            Settings.Default.WindowHeight = Height;

            Settings.Default.AngleMeasurement = mathPresenter.AngleMeasurement;

            Settings.Default.SelectedTabIndex = tabControl.SelectedIndex;

            Settings.Default.NumbersExpanded = numberToolBar.IsExpanded;
            Settings.Default.StandartMathExpanded = standartMathToolBar.IsExpanded;
            Settings.Default.TrigonometricExpanded = trigonometricToolBar.IsExpanded;
            Settings.Default.HyperbolicExpanded = hyperbolicToolBar.IsExpanded;
            Settings.Default.BitwiseExpanded = bitwiseToolBar.IsExpanded;
            Settings.Default.ConstantsMathExpanded = constantsMathToolBar.IsExpanded;
            Settings.Default.AdditionalExpanded = additionalToolBar.IsExpanded;

            Settings.Default.StandartLogicExpanded = standartLogicToolBar.IsExpanded;
            Settings.Default.ConstantsLogicExpanded = constantsLogicToolBar.IsExpanded;

            Settings.Default.Save();
        }

        private void DergeeButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            radianButton.IsChecked = false;
            gradianButton.IsChecked = false;
            mathPresenter.AngleMeasurement = AngleMeasurement.Degree;
            degreeButton.IsChecked = true;
        }

        private void RadianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            degreeButton.IsChecked = false;
            gradianButton.IsChecked = false;
            mathPresenter.AngleMeasurement = AngleMeasurement.Radian;
            radianButton.IsChecked = true;
        }

        private void GradianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            degreeButton.IsChecked = false;
            radianButton.IsChecked = false;
            mathPresenter.AngleMeasurement = AngleMeasurement.Gradian;
            gradianButton.IsChecked = true;
        }

        private void AndleButtons_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void DeleteExp_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
            {
                var item = (MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem;

                mathPresenter.Remove(item);
            }
            else if (tabControl.SelectedItem == logicTab)
            {
                var item = (LogicWorkspaceItemViewModel)logicExpsListBox.SelectedItem;

                logicPresenter.Remove(item);
            }
        }

        private void DeleteExp_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = (tabControl.SelectedItem == mathTab && mathExpsListBox.SelectedItem != null) ||
                              (tabControl.SelectedItem == logicTab && logicExpsListBox.SelectedItem != null);
        }

        private void Clear_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
                mathPresenter.Clear();
            else if (tabControl.SelectedItem == logicTab)
                logicPresenter.Clear();
        }

        private void Clear_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab || tabControl.SelectedItem == logicTab;
        }

        private void AboutCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            AboutView aboutView = new AboutView { Owner = this };
            aboutView.ShowDialog();
        }

        private void InsertChar_Click(object o, RoutedEventArgs args)
        {
            var tag = ((Button)o).Tag.ToString();
            TextBox tb = null;
            if (tabControl.SelectedItem == mathTab)
                tb = mathExpressionBox;
            else if (tabControl.SelectedItem == logicTab)
                tb = logicExpressionBox;
            else if (tabControl.SelectedItem == graphsTab)
                tb = graphExpressionBox;

            var prevSelectionStart = tb.SelectionStart;
            tb.Text = tb.Text.Insert(prevSelectionStart, tag);
            tb.SelectionStart = prevSelectionStart + tag.Length;
            tb.Focus();
        }

        private void InsertFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = null;
            if (tabControl.SelectedItem == mathTab)
                tb = mathExpressionBox;
            else if (tabControl.SelectedItem == logicTab)
                tb = logicExpressionBox;
            else if (tabControl.SelectedItem == graphsTab)
                tb = graphExpressionBox;

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ")");
                tb.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 2;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func + "()");
                tb.SelectionStart = prevSelectionStart + func.Length + 1;
            }

            tb.Focus();
        }

        private void InsertInv_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = null;
            if (tabControl.SelectedItem == mathTab)
                tb = mathExpressionBox;
            else if (tabControl.SelectedItem == logicTab)
                tb = logicExpressionBox;
            else if (tabControl.SelectedItem == graphsTab)
                tb = graphExpressionBox;

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, "(").Insert(prevSelectionStart + prevSelectionLength + 1, ")" + func);
                tb.SelectionStart = prevSelectionStart + prevSelectionLength + func.Length + 2;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func);
                tb.SelectionStart = prevSelectionStart + func.Length;
            }

            tb.Focus();
        }

        private void InsertDoubleArgFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = null;
            if (tabControl.SelectedItem == mathTab)
                tb = mathExpressionBox;
            else if (tabControl.SelectedItem == logicTab)
                tb = logicExpressionBox;
            else if (tabControl.SelectedItem == graphsTab)
                tb = graphExpressionBox;

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ", )");
                tb.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 3;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(, )");
                tb.SelectionStart = prevSelectionStart + func.Length + 1;
            }

            tb.Focus();
        }

        private void EnterButton_Click(object o, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(mathExpressionBox.Text))
                MathExpEnter();
        }

        private void MathExpEnter()
        {
            try
            {
                mathPresenter.Add(mathExpressionBox.Text);
                var count = mathExpsListBox.Items.Count;
                if (count > 0)
                    mathExpsListBox.ScrollIntoView(mathExpsListBox.Items[count - 1]);
                statusBox.Text = string.Empty;
            }
            catch (MathLexerException mle)
            {
                statusBox.Text = mle.Message;
            }
            catch (MathParserException mpe)
            {
                statusBox.Text = mpe.Message;
            }
            catch (DivideByZeroException dbze)
            {
                statusBox.Text = dbze.Message;
            }
            catch (ArgumentNullException ane)
            {
                statusBox.Text = ane.Message;
            }
            catch (ArgumentException ae)
            {
                statusBox.Text = ae.Message;
            }
            catch (FormatException fe)
            {
                statusBox.Text = fe.Message;
            }
            catch (OverflowException oe)
            {
                statusBox.Text = oe.Message;
            }
            catch (KeyNotFoundException)
            {
                statusBox.Text = Resource.VariableNotFoundExceptionError;
            }
            catch (IndexOutOfRangeException)
            {
                statusBox.Text = Resource.IndexOutOfRangeExceptionError;
            }
            catch (InvalidOperationException ioe)
            {
                statusBox.Text = ioe.Message;
            }
            catch (NotSupportedException)
            {
                statusBox.Text = Resource.NotSupportedOperationError;
            }

            mathExpressionBox.Text = string.Empty;
        }

        private void GenerateTruthTable(IEnumerable<ILogicExpression> exps, LogicParameterCollection parameters)
        {
            truthTableGridView.Columns.Clear();

            truthTableGridView.Columns.Add(new GridViewColumn
            {
                Header = "#",
                DisplayMemberBinding = new Binding("Index")
            });
            for (int i = 0; i < parameters.Count; i++)
            {
                truthTableGridView.Columns.Add(new GridViewColumn
                {
                    Header = parameters[i],
                    DisplayMemberBinding = new Binding(string.Format("VarsValues[{0}]", i))
                });
            }
            for (int i = 0; i < exps.Count() - 1; i++)
            {
                truthTableGridView.Columns.Add(new GridViewColumn
                {
                    Header = exps.ElementAt(i),
                    DisplayMemberBinding = new Binding(string.Format("Values[{0}]", i))
                });
            }
            if (exps.Count() != 0)
                truthTableGridView.Columns.Add(new GridViewColumn
                {
                    Header = exps.ElementAt(exps.Count() - 1),
                    DisplayMemberBinding = new Binding("Result")
                });
        }

        private void mathExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(mathExpressionBox.Text))
            {
                MathExpEnter();
            }
        }

        private void logicExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(logicExpressionBox.Text))
            {
                try
                {
                    logicPresenter.Add(logicExpressionBox.Text);
                    var count = logicExpsListBox.Items.Count;
                    if (count > 0)
                        logicExpsListBox.ScrollIntoView(logicExpsListBox.Items[count - 1]);
                    statusBox.Text = string.Empty;
                }
                catch (LogicLexerException lle)
                {
                    statusBox.Text = lle.Message;
                }
                catch (LogicParserException lpe)
                {
                    statusBox.Text = lpe.Message;
                }
                catch (DivideByZeroException dbze)
                {
                    statusBox.Text = dbze.Message;
                }
                catch (ArgumentNullException ane)
                {
                    statusBox.Text = ane.Message;
                }
                catch (ArgumentException ae)
                {
                    statusBox.Text = ae.Message;
                }
                catch (FormatException fe)
                {
                    statusBox.Text = fe.Message;
                }
                catch (OverflowException oe)
                {
                    statusBox.Text = oe.Message;
                }
                catch (KeyNotFoundException)
                {
                    statusBox.Text = Resource.VariableNotFoundExceptionError;
                }
                catch (IndexOutOfRangeException)
                {
                    statusBox.Text = Resource.IndexOutOfRangeExceptionError;
                }
                catch (InvalidOperationException ioe)
                {
                    statusBox.Text = ioe.Message;
                }
                catch (NotSupportedException)
                {
                    statusBox.Text = Resource.NotSupportedOperationError;
                }

                logicExpressionBox.Text = string.Empty;
            }
        }

        private void graphExpBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(graphExpressionBox.Text))
            {
                try
                {
                    graphsPresenter.Add(graphExpressionBox.Text);
                    statusBox.Text = string.Empty;
                }
                catch (MathLexerException mle)
                {
                    statusBox.Text = mle.Message;
                }
                catch (MathParserException mpe)
                {
                    statusBox.Text = mpe.Message;
                }
                catch (DivideByZeroException dbze)
                {
                    statusBox.Text = dbze.Message;
                }
                catch (ArgumentNullException ane)
                {
                    statusBox.Text = ane.Message;
                }
                catch (ArgumentException ae)
                {
                    statusBox.Text = ae.Message;
                }
                catch (FormatException fe)
                {
                    statusBox.Text = fe.Message;
                }
                catch (OverflowException oe)
                {
                    statusBox.Text = oe.Message;
                }
                catch (KeyNotFoundException)
                {
                    statusBox.Text = Resource.VariableNotFoundExceptionError;
                }
                catch (IndexOutOfRangeException)
                {
                    statusBox.Text = Resource.IndexOutOfRangeExceptionError;
                }
                catch (InvalidOperationException ioe)
                {
                    statusBox.Text = ioe.Message;
                }
                catch (NotSupportedException)
                {
                    statusBox.Text = Resource.NotSupportedOperationError;
                }

                graphExpressionBox.Text = string.Empty;
            }
        }

        private void truthTableExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(truthTableExpressionBox.Text))
            {
                try
                {
                    truthTablePresenter.Generate(truthTableExpressionBox.Text);
                    GenerateTruthTable(truthTablePresenter.Expressions, truthTablePresenter.Parameters);
                    truthTableList.ItemsSource = truthTablePresenter.Table;

                    statusBox.Text = string.Empty;
                }
                catch (LogicLexerException lle)
                {
                    statusBox.Text = lle.Message;
                }
                catch (LogicParserException lpe)
                {
                    statusBox.Text = lpe.Message;
                }
                catch (DivideByZeroException dbze)
                {
                    statusBox.Text = dbze.Message;
                }
                catch (ArgumentNullException ane)
                {
                    statusBox.Text = ane.Message;
                }
                catch (ArgumentException ae)
                {
                    statusBox.Text = ae.Message;
                }
                catch (FormatException fe)
                {
                    statusBox.Text = fe.Message;
                }
                catch (OverflowException oe)
                {
                    statusBox.Text = oe.Message;
                }
                catch (KeyNotFoundException)
                {
                    statusBox.Text = Resource.VariableNotFoundExceptionError;
                }
                catch (IndexOutOfRangeException)
                {
                    statusBox.Text = Resource.IndexOutOfRangeExceptionError;
                }
                catch (InvalidOperationException ioe)
                {
                    statusBox.Text = ioe.Message;
                }
                catch (NotSupportedException)
                {
                    statusBox.Text = Resource.NotSupportedOperationError;
                }
            }
        }

        private void graphsList_SelectionChanged(object o, SelectionChangedEventArgs args)
        {
            if (graphsList.SelectedIndex >= 0)
                plot.Expression = (graphsList.Items[graphsList.SelectedIndex] as GraphItemViewModel).Expression;
            else
                plot.Expression = null;
        }

        private void tabControl_SelectionChanged(object o, SelectionChangedEventArgs args)
        {
            if (tabControl.SelectedItem == logicTab || tabControl.SelectedItem == truthTableTab)
            {
                numberToolBar.Visibility = Visibility.Collapsed;
                standartMathToolBar.Visibility = Visibility.Collapsed;
                trigonometricToolBar.Visibility = Visibility.Collapsed;
                hyperbolicToolBar.Visibility = Visibility.Collapsed;
                bitwiseToolBar.Visibility = Visibility.Collapsed;
                constantsMathToolBar.Visibility = Visibility.Collapsed;
                additionalToolBar.Visibility = Visibility.Collapsed;

                standartLogicToolBar.Visibility = Visibility.Visible;
                constantsLogicToolBar.Visibility = Visibility.Visible;
            }
            else
            {
                numberToolBar.Visibility = Visibility.Visible;
                standartMathToolBar.Visibility = Visibility.Visible;
                trigonometricToolBar.Visibility = Visibility.Visible;
                hyperbolicToolBar.Visibility = Visibility.Visible;
                bitwiseToolBar.Visibility = Visibility.Visible;
                constantsMathToolBar.Visibility = Visibility.Visible;
                additionalToolBar.Visibility = Visibility.Visible;

                standartLogicToolBar.Visibility = Visibility.Collapsed;
                constantsLogicToolBar.Visibility = Visibility.Collapsed;
            }
        }

        private void removeMath_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as MathWorkspaceItemViewModel;

            mathPresenter.Remove(item);
        }

        private void removeLogic_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as LogicWorkspaceItemViewModel;

            logicPresenter.Remove(item);
        }

        private void removeGraph_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as GraphItemViewModel;

            graphsPresenter.Remove(item);
        }

        public IEnumerable<MathWorkspaceItemViewModel> MathExpressions
        {
            set
            {
                mathExpsListBox.ItemsSource = value;
            }
        }

        public IEnumerable<GraphItemViewModel> Graphs
        {
            set
            {
                graphsList.ItemsSource = value;
                graphsList.SelectedIndex = value.Count() - 1;
            }
        }

        public IEnumerable<LogicWorkspaceItemViewModel> LogicExpressions
        {
            set
            {
                logicExpsListBox.ItemsSource = value;
            }
        }

    }

}
