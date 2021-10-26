// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using xFunc.Properties;

namespace xFunc.Views
{

    public partial class SettingsView : Window
    {

        public static RoutedCommand OKCommand = new RoutedCommand();
        public static RoutedCommand ResetCommand = new RoutedCommand();

        public SettingsView()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            ProgramLanguage = Settings.Default.Lang;
            RememberStateAndPosition = Settings.Default.RememberSizeAndPosition;
            RememberRightToolBar = Settings.Default.RememberRightToolBar;
            RememberNumberAndAngle = Settings.Default.RememberBaseAndAngle;
            MaxCountOfExps = Settings.Default.MaxCountOfExpressions;
            ChartColor = Settings.Default.DefaultChartColor;
            SaveUserFunctions = Settings.Default.SaveUserFunction;
            SaveDump = Settings.Default.SaveDump;
        }

        private void OKCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            DialogResult = true;
            Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            int max;
            args.CanExecute = int.TryParse(this.maxCountOfExpsTextBox.Text, out max) && max > 0;
        }

        private void ResetCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Settings.Default.Reset();
            DialogResult = false;
            Close();
        }

        public string ProgramLanguage
        {
            get => (string)this.langComboBox.SelectedValue;
            internal set => this.langComboBox.SelectedValue = value;
        }

        public bool RememberStateAndPosition
        {
            get => this.positionCheckBox.IsChecked.Value;
            internal set => this.positionCheckBox.IsChecked = value;
        }

        public bool RememberRightToolBar
        {
            get => this.toolBarCheckBox.IsChecked.Value;
            internal set => this.toolBarCheckBox.IsChecked = value;
        }

        public bool RememberNumberAndAngle
        {
            get => this.numAndAngleCheckBox.IsChecked.Value;
            internal set => this.numAndAngleCheckBox.IsChecked = value;
        }

        public int MaxCountOfExps
        {
            get => int.Parse(this.maxCountOfExpsTextBox.Text);
            internal set => this.maxCountOfExpsTextBox.Text = value.ToString();
        }

        public Color ChartColor
        {
            get => this.chartColorGallery.SelectedColor.Value;
            internal set => this.chartColorGallery.SelectedColor = value;
        }

        public bool SaveUserFunctions
        {
            get => this.saveUserFuncCheckBox.IsChecked.Value;
            internal set => this.saveUserFuncCheckBox.IsChecked = value;
        }

        public bool SaveDump
        {
            get => this.saveDumpCheckBox.IsChecked.Value;
            internal set => this.saveDumpCheckBox.IsChecked = value;
        }

    }

}