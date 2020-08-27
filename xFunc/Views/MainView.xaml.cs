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

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;
using xFunc.Presenters;
using xFunc.Properties;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class MainView : Fluent.RibbonWindow
    {

        private readonly Processor processor;

        private readonly MathPresenter mathPresenter;
        private readonly GraphsPresenter graphsPresenter;
        private readonly TruthTablePresenter truthTablePresenter;
        private string fileName;

        #region Commands

        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand OpenCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand SaveAsCommand = new RoutedCommand();

        public static RoutedCommand AutoFormatCommand = new RoutedCommand();
        public static RoutedCommand NormalFormatCommand = new RoutedCommand();
        public static RoutedCommand ExponentialFormatCommand = new RoutedCommand();

        public static RoutedCommand BinCommand = new RoutedCommand();
        public static RoutedCommand OctCommand = new RoutedCommand();
        public static RoutedCommand DecCommand = new RoutedCommand();
        public static RoutedCommand HexCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();
        public static RoutedCommand ClearCommand = new RoutedCommand();

        public static RoutedCommand VariablesCommand = new RoutedCommand();
        public static RoutedCommand FunctionsCommand = new RoutedCommand();
        public static RoutedCommand ConverterCommand = new RoutedCommand();

        public static RoutedCommand AboutCommand = new RoutedCommand();
        public static RoutedCommand SettingsCommand = new RoutedCommand();
        public static RoutedCommand ExitCommand = new RoutedCommand();

        #endregion Commands

        private VariableView variableView;
        private FunctionView functionView;
        private Converter converterView;

        public MainView()
        {
            InitializeComponent();

            processor = new Processor();

            mathPresenter = new MathPresenter(this.mathControl, processor);
            mathPresenter.PropertyChanged += mathPresenter_PropertyChanged;
            this.mathControl.Presenter = mathPresenter;
            graphsPresenter = new GraphsPresenter(this.graphsControl, processor);
            this.graphsControl.Presenter = graphsPresenter;
            truthTablePresenter = new TruthTablePresenter();
            this.truthTableControl.Presenter = truthTablePresenter;

            LoadSettings();

            SetFocus();
        }

        private void mathPresenter_PropertyChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(mathPresenter.Base))
            {
                switch (mathPresenter.Base)
                {
                    case NumeralSystem.Binary:
                        octButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        binButton.IsChecked = true;
                        break;
                    case NumeralSystem.Octal:
                        binButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        octButton.IsChecked = true;
                        break;
                    case NumeralSystem.Decimal:
                        binButton.IsChecked = false;
                        octButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        decButton.IsChecked = true;
                        break;
                    case NumeralSystem.Hexidecimal:
                        binButton.IsChecked = false;
                        octButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = true;
                        break;
                }
            }
            else if (args.PropertyName == nameof(mathPresenter.OutputFormat))
            {
                switch (mathPresenter.OutputFormat)
                {
                    case OutputFormats.Auto:
                        autoButton.IsChecked = true;
                        normalButton.IsChecked = false;
                        exponentialButton.IsChecked = false;
                        break;
                    case OutputFormats.Normal:
                        autoButton.IsChecked = false;
                        normalButton.IsChecked = true;
                        exponentialButton.IsChecked = false;
                        break;
                    case OutputFormats.Exponential:
                        autoButton.IsChecked = false;
                        normalButton.IsChecked = false;
                        exponentialButton.IsChecked = true;
                        break;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveSettings();

            base.OnClosing(e);
        }

        private void LoadSettings()
        {
            if (Settings.Default.SaveUserFunction && Settings.Default.UserFunctions != null)
                foreach (var func in Settings.Default.UserFunctions)
                    processor.Solve(func);

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

            tabControl.SelectedIndex = Settings.Default.SelectedTabIndex;

            mathPresenter.Base = Settings.Default.NumberBase;
            mathPresenter.OutputFormat = Settings.Default.OutputFormat;

            numberToolBar.IsExpanded = Settings.Default.NumbersExpanded;
            standartMathToolBar.IsExpanded = Settings.Default.StandartMathExpanded;
            trigonometricToolBar.IsExpanded = Settings.Default.TrigonometricExpanded;
            hyperbolicToolBar.IsExpanded = Settings.Default.HyperbolicExpanded;
            matrixToolBar.IsExpanded = Settings.Default.MatrixExpanded;
            statisticalToolBar.IsExpanded = Settings.Default.StatisticalExpanded;
            complexNumberToolBar.IsExpanded = Settings.Default.ComplexNumberExpanded;
            bitwiseToolBar.IsExpanded = Settings.Default.BitwiseExpanded;
            progToolBar.IsExpanded = Settings.Default.ProgExpanded;
            constantsMathToolBar.IsExpanded = Settings.Default.ConstantsMathExpanded;
            additionalMathToolBar.IsExpanded = Settings.Default.AdditionalMathExpanded;
        }

        private void SaveSettings()
        {
            if (Settings.Default.SaveUserFunction)
            {
                if (processor.Parameters.Functions.Count > 0)
                    Settings.Default.UserFunctions = new System.Collections.Specialized.StringCollection();
                foreach (var item in processor.Parameters.Functions)
                    Settings.Default.UserFunctions.Add($"{item.Key}:={item.Value}");
            }

            if (Settings.Default.RememberSizeAndPosition)
            {
                if (WindowState != WindowState.Minimized)
                    Settings.Default.WindowState = WindowState;

                Settings.Default.WindowTop = Top;
                Settings.Default.WindowLeft = Left;

                Settings.Default.WindowWidth = Width;
                Settings.Default.WindowHeight = Height;

                Settings.Default.SelectedTabIndex = tabControl.SelectedIndex;
            }
            else
            {
                Settings.Default.WindowState = (WindowState)Enum.Parse(typeof(WindowState), Settings.Default.Properties["WindowState"].DefaultValue.ToString());

                Settings.Default.WindowTop = double.Parse(Settings.Default.Properties["WindowTop"].DefaultValue.ToString());
                Settings.Default.WindowLeft = double.Parse(Settings.Default.Properties["WindowLeft"].DefaultValue.ToString());

                Settings.Default.WindowWidth = double.Parse(Settings.Default.Properties["WindowWidth"].DefaultValue.ToString());
                Settings.Default.WindowHeight = double.Parse(Settings.Default.Properties["WindowHeight"].DefaultValue.ToString());

                Settings.Default.SelectedTabIndex = int.Parse(Settings.Default.Properties["SelectedTabIndex"].DefaultValue.ToString());
            }

            if (Settings.Default.RememberBaseAndAngle)
            {
                Settings.Default.NumberBase = mathPresenter.Base;
                Settings.Default.OutputFormat = mathPresenter.OutputFormat;
            }

            if (Settings.Default.RememberRightToolBar)
            {
                Settings.Default.NumbersExpanded = numberToolBar.IsExpanded;
                Settings.Default.StandartMathExpanded = standartMathToolBar.IsExpanded;
                Settings.Default.TrigonometricExpanded = trigonometricToolBar.IsExpanded;
                Settings.Default.HyperbolicExpanded = hyperbolicToolBar.IsExpanded;
                Settings.Default.MatrixExpanded = matrixToolBar.IsExpanded;
                Settings.Default.StatisticalExpanded = statisticalToolBar.IsExpanded;
                Settings.Default.ComplexNumberExpanded = complexNumberToolBar.IsExpanded;
                Settings.Default.BitwiseExpanded = bitwiseToolBar.IsExpanded;
                Settings.Default.ProgExpanded = progToolBar.IsExpanded;
                Settings.Default.ConstantsMathExpanded = constantsMathToolBar.IsExpanded;
                Settings.Default.AdditionalMathExpanded = additionalMathToolBar.IsExpanded;
            }
            else
            {
                Settings.Default.NumbersExpanded = bool.Parse(Settings.Default.Properties["NumbersExpanded"].DefaultValue.ToString());
                Settings.Default.StandartMathExpanded = bool.Parse(Settings.Default.Properties["StandartMathExpanded"].DefaultValue.ToString());
                Settings.Default.TrigonometricExpanded = bool.Parse(Settings.Default.Properties["TrigonometricExpanded"].DefaultValue.ToString());
                Settings.Default.HyperbolicExpanded = bool.Parse(Settings.Default.Properties["HyperbolicExpanded"].DefaultValue.ToString());
                Settings.Default.MatrixExpanded = bool.Parse(Settings.Default.Properties["MatrixExpanded"].DefaultValue.ToString());
                Settings.Default.StatisticalExpanded = bool.Parse(Settings.Default.Properties["StatisticalExpanded"].DefaultValue.ToString());
                Settings.Default.ComplexNumberExpanded = bool.Parse(Settings.Default.Properties["ComplexNumberExpanded"].DefaultValue.ToString());
                Settings.Default.BitwiseExpanded = bool.Parse(Settings.Default.Properties["BitwiseExpanded"].DefaultValue.ToString());
                Settings.Default.ProgExpanded = bool.Parse(Settings.Default.Properties["ProgExpanded"].DefaultValue.ToString());
                Settings.Default.ConstantsMathExpanded = bool.Parse(Settings.Default.Properties["ConstantsMathExpanded"].DefaultValue.ToString());
                Settings.Default.AdditionalMathExpanded = bool.Parse(Settings.Default.Properties["AdditionalMathExpanded"].DefaultValue.ToString());
            }

            Settings.Default.Save();
        }

        private void Serialize(string path)
        {
            var exps = new XElement("expressions", mathPresenter.Workspace.Select(exp => new XElement("expression", exp.StringExpression)));
            var vars = new XElement("variables",
                from @var in processor.Parameters.Variables
                where @var.Type != ParameterType.Constant
                select new XElement("add",
                        new XAttribute("key", @var.Key),
                        new XAttribute("value", Convert.ToString(@var.Value, CultureInfo.InvariantCulture)),
                        new XAttribute("readonly", @var.Type == ParameterType.ReadOnly)));
            var funcs = new XElement("functions",
                from func in processor.Parameters.Functions
                select new XElement("add",
                        new XAttribute("key", func.Key.ToString()),
                        new XAttribute("value", func.Value.ToString())));

            var root = new XElement("xfunc",
                                    exps.IsEmpty ? null : exps,
                                    vars.IsEmpty ? null : vars,
                                    funcs.IsEmpty ? null : funcs);
            var doc = new XDocument(new XDeclaration("1.0", "UTF-8", null), root);

            doc.Save(path);
        }

        private void Deserialize(string path)
        {
            var doc = XDocument.Load(path);
            var vars = doc.Root.Element("variables");
            if (vars != null)
                foreach (var item in vars.Elements("add"))
                    processor.Parameters.Variables.Add(new Parameter(item.Attribute("key").Value, double.Parse(item.Attribute("value").Value), bool.Parse(item.Attribute("readonly").Value) ? ParameterType.ReadOnly : ParameterType.Normal));

            var funcs = doc.Root.Element("functions");
            if (funcs != null)
                foreach (var item in funcs.Elements("add"))
                    processor.Solve($"{item.Attribute("key").Value}:={item.Attribute("value").Value}");

            var exps = doc.Root.Element("expressions");
            if (exps != null)
                foreach (var item in exps.Elements("expression").Select(exp => exp.Value))
                    mathPresenter.Add(item);
        }

        private void SetFocus()
        {
            if (tabControl.SelectedItem == mathTab)
                this.mathControl.mathExpressionBox.Focus();
            if (tabControl.SelectedItem == graphsTab)
                this.graphsControl.graphExpressionBox.Focus();
            if (tabControl.SelectedItem == truthTableTab)
                this.truthTableControl.truthTableExpressionBox.Focus();
        }

        #region Commands

        private void NewCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Clear();
            processor.Parameters.Variables.Clear();
            processor.Parameters.Functions.Clear();
            fileName = null;
        }

        private void OpenCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var ofd = new OpenFileDialog()
            {
                FileName = "xFunc Document",
                DefaultExt = ".xml",
                Filter = "xFunc File (*.xml)|*.xml|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (ofd.ShowDialog() == true)
            {
                Deserialize(ofd.FileName);
                fileName = ofd.FileName;
            }
        }

        private void SaveCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                SaveAsCommand_Execute(o, args);
            else
                Serialize(fileName);
        }

        private void SaveAsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var sfd = new SaveFileDialog()
            {
                FileName = "xFunc Document",
                DefaultExt = ".xml",
                Filter = "xFunc File (*.xml)|*.xml|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (sfd.ShowDialog() == true)
            {
                Serialize(sfd.FileName);
                fileName = sfd.FileName;
            }
        }

        private void AutoButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.OutputFormat = OutputFormats.Auto;
        }

        private void NormalButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.OutputFormat = OutputFormats.Normal;
        }

        private void ExponentialButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.OutputFormat = OutputFormats.Exponential;
        }

        private void FormatButtons_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void AngleButtons_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void BinCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Binary;
        }

        private void OctCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Octal;
        }

        private void DecCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Decimal;
        }

        private void HexCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Hexidecimal;
        }

        private void BaseCommands_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void DeleteExp_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
            {
                var item = (MathWorkspaceItemViewModel)this.mathControl.mathExpsListBox.SelectedItem;

                mathPresenter.Remove(item);
            }
            else if (tabControl.SelectedItem == graphsTab)
            {
                var item = (GraphItemViewModel)this.graphsControl.graphsList.SelectedItem;

                graphsPresenter.Remove(item);
            }
        }

        private void DeleteExp_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = (tabControl.SelectedItem == mathTab && this.mathControl.mathExpsListBox.SelectedItem != null) ||
                              (tabControl.SelectedItem == graphsTab && this.graphsControl.graphsList.SelectedItem != null);
        }

        private void Clear_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
                mathPresenter.Clear();
            else if (tabControl.SelectedItem == graphsTab)
                graphsPresenter.Clear();
        }

        private void Clear_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab ||
                              tabControl.SelectedItem == graphsTab;
        }

        private void VariablesCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (variableView == null)
            {
                variableView = new VariableView(this.processor)
                {
                    Owner = this,
                    Top = Settings.Default.VarWindowTop == -1 ? this.Top + 100 : Settings.Default.VarWindowTop,
                    Left = Settings.Default.VarWindowLeft == -1 ? this.Left + this.Width - 300 : Settings.Default.VarWindowLeft,
                    Width = Settings.Default.VarWindowWidth,
                    Height = Settings.Default.VarWindowHeight
                };
                variableView.Closed += (lo, larg) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.VarWindowTop = variableView.Top;
                        Settings.Default.VarWindowLeft = variableView.Left;
                        Settings.Default.VarWindowWidth = variableView.Width;
                        Settings.Default.VarWindowHeight = variableView.Height;
                    }
                    else
                    {
                        Settings.Default.VarWindowTop = double.Parse(Settings.Default.Properties["VarWindowTop"].DefaultValue.ToString());
                        Settings.Default.VarWindowLeft = double.Parse(Settings.Default.Properties["VarWindowLeft"].DefaultValue.ToString());

                        Settings.Default.VarWindowWidth = double.Parse(Settings.Default.Properties["VarWindowWidth"].DefaultValue.ToString());
                        Settings.Default.VarWindowHeight = double.Parse(Settings.Default.Properties["VarWindowHeight"].DefaultValue.ToString());
                    }
                    variableView = null;
                };
            }

            if (variableView.Visibility == Visibility.Visible)
                variableView.Activate();
            else
                variableView.Visibility = Visibility.Visible;
        }

        private void VariablesCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void FunctionsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (functionView == null)
            {
                functionView = new FunctionView(processor)
                {
                    Owner = this,
                    Top = Settings.Default.FuncWindowTop == -1 ? this.Top + 100 : Settings.Default.FuncWindowTop,
                    Left = Settings.Default.FuncWindowLeft == -1 ? this.Left + this.Width - 300 : Settings.Default.FuncWindowLeft,
                    Width = Settings.Default.FuncWindowWidth,
                    Height = Settings.Default.FuncWindowHeight
                };
                functionView.Closed += (lo, larg) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.FuncWindowTop = functionView.Top;
                        Settings.Default.FuncWindowLeft = functionView.Left;
                        Settings.Default.FuncWindowWidth = functionView.Width;
                        Settings.Default.FuncWindowHeight = functionView.Height;
                    }
                    else
                    {
                        Settings.Default.FuncWindowTop = double.Parse(Settings.Default.Properties["FuncWindowTop"].DefaultValue.ToString());
                        Settings.Default.FuncWindowLeft = double.Parse(Settings.Default.Properties["FuncWindowLeft"].DefaultValue.ToString());

                        Settings.Default.FuncWindowWidth = double.Parse(Settings.Default.Properties["FuncWindowWidth"].DefaultValue.ToString());
                        Settings.Default.FuncWindowHeight = double.Parse(Settings.Default.Properties["FuncWindowHeight"].DefaultValue.ToString());
                    }
                    functionView = null;
                };
            }

            if (functionView.Visibility == Visibility.Visible)
                functionView.Activate();
            else
                functionView.Visibility = Visibility.Visible;
        }

        private void FunctionsCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void ConverterCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (converterView == null)
            {
                converterView = new Converter(this.mathControl)
                {
                    Owner = this,
                    Top = Settings.Default.ConverterTop == -1 ? this.Top + 100 : Settings.Default.ConverterTop,
                    Left = Settings.Default.ConverterLeft == -1 ? this.Left + 300 : Settings.Default.ConverterLeft
                };
                converterView.Closed += (obj, args1) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.ConverterTop = converterView.Top;
                        Settings.Default.ConverterLeft = converterView.Left;
                    }
                    else
                    {
                        Settings.Default.ConverterTop = double.Parse(Settings.Default.Properties["ConverterTop"].DefaultValue.ToString());
                        Settings.Default.ConverterLeft = double.Parse(Settings.Default.Properties["ConverterLeft"].DefaultValue.ToString());
                    }

                    converterView = null;
                };
            }

            if (converterView.Visibility == Visibility.Visible)
                converterView.Activate();
            else
                converterView.Visibility = Visibility.Visible;
        }

        private void AboutCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var aboutView = new AboutView { Owner = this };
            aboutView.ShowDialog();
        }

        private void SettingsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var settingsView = new SettingsView
            {
                Owner = this
            };
            if (settingsView.ShowDialog() == true)
            {
                Settings.Default.Lang = settingsView.ProgramLanguage;
                Settings.Default.RememberSizeAndPosition = settingsView.RememberStateAndPosition;
                Settings.Default.RememberRightToolBar = settingsView.RememberRightToolBar;
                Settings.Default.RememberBaseAndAngle = settingsView.RememberNumberAndAngle;
                if (!settingsView.RememberNumberAndAngle)
                {
                    Settings.Default.NumberBase = settingsView.Base;
                    mathPresenter.Base = settingsView.Base;
                }
                Settings.Default.MaxCountOfExpressions = settingsView.MaxCountOfExps;
                Settings.Default.DefaultChartColor = settingsView.ChartColor;
                Settings.Default.SaveUserFunction = settingsView.SaveUserFunctions;
                Settings.Default.SaveDump = settingsView.SaveDump;

                Settings.Default.Save();
            }
            else
            {
                Settings.Default.Reload();
            }
        }

        private void ExitCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        #endregion Commands

        private TextBox GetSelectedTextBox()
        {
            if (tabControl.SelectedItem == mathTab)
                return this.mathControl.mathExpressionBox;
            if (tabControl.SelectedItem == graphsTab)
                return this.graphsControl.graphExpressionBox;
            if (tabControl.SelectedItem == truthTableTab)
                return this.truthTableControl.truthTableExpressionBox;

            return null;
        }

        private void InsertChar_Click(object o, RoutedEventArgs args)
        {
            var tag = ((Button)o).Tag.ToString();
            var tb = GetSelectedTextBox();

            var prevSelectionStart = tb.SelectionStart;
            tb.Text = tb.Text.Insert(prevSelectionStart, tag);
            tb.SelectionStart = prevSelectionStart + tag.Length;
            tb.Focus();
        }

        private void InsertFunc_Click(object o, RoutedEventArgs args)
        {
            var func = ((Button)o).Tag.ToString();
            var tb = GetSelectedTextBox();

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
            var func = ((Button)o).Tag.ToString();
            var tb = GetSelectedTextBox();

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
            var func = ((Button)o).Tag.ToString();
            var tb = GetSelectedTextBox();

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
            if (!string.IsNullOrWhiteSpace(this.mathControl.mathExpressionBox.Text))
                this.mathControl.MathExpEnter();
        }

    }

}