// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using xFunc.UnitConverters;

namespace xFunc.Views
{

    public partial class Converter : Window
    {
        private readonly MathControl mathControl;

        public static RoutedCommand CalculateCommand = new RoutedCommand();
        public static RoutedCommand CopyFromCommand = new RoutedCommand();
        public static RoutedCommand CopyToCommand = new RoutedCommand();

        public Converter(MathControl mathControl)
        {
            Converters = new IConverter[]
            {
                new AreaConverter(),
                new UnitConverters.LengthConverter(),
                new MassConverter(),
                new PowerConverter(),
                new TemperatureConverter(),
                new TimeConverter(),
                new VolumeConverter()
            };
            this.mathControl = mathControl;

            InitializeComponent();

            convertersComboBox.ItemsSource = Converters;
            SetUnits();
        }

        private void ConvertFromTo()
        {
            if (convertersComboBox.SelectedItem == null || fromComboBox.SelectedItem == null || toComboBox.SelectedItem == null)
                return;

            var conv = (IConverter)convertersComboBox.SelectedItem;
            var from = (KeyValuePair<object, string>)fromComboBox.SelectedItem;
            var to = (KeyValuePair<object, string>)toComboBox.SelectedItem;

            if (double.TryParse(fromTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                toTextBox.Text = conv.Convert(value, from.Key, to.Key).ToString(CultureInfo.InvariantCulture);
        }

        private void ConvertToFrom()
        {
            if (convertersComboBox.SelectedItem == null || fromComboBox.SelectedItem == null || toComboBox.SelectedItem == null)
                return;

            var conv = (IConverter)convertersComboBox.SelectedItem;
            var from = (KeyValuePair<object, string>)fromComboBox.SelectedItem;
            var to = (KeyValuePair<object, string>)toComboBox.SelectedItem;

            if (double.TryParse(toTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                fromTextBox.Text = conv.Convert(value, to.Key, from.Key).ToString(CultureInfo.InvariantCulture);
        }

        private void CalculateCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            var conv = (IConverter)convertersComboBox.SelectedItem;
            var from = (KeyValuePair<object, string>)fromComboBox.SelectedItem;
            var to = (KeyValuePair<object, string>)toComboBox.SelectedItem;
            double value;

            if (double.TryParse(fromTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                var result = conv.Convert(value, from.Key, to.Key);

                toTextBox.Text = result.ToString(CultureInfo.InvariantCulture);
            }
            else if (double.TryParse(toTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                var result = conv.Convert(value, to.Key, from.Key);

                fromTextBox.Text = result.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void CalculateCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            double d;
            args.CanExecute = (!string.IsNullOrWhiteSpace(fromTextBox.Text) || !string.IsNullOrWhiteSpace(toTextBox.Text))
                            && (double.TryParse(fromTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d) || double.TryParse(toTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d));
        }

        private void CopyFromCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            mathControl.mathExpressionBox.Text += this.fromTextBox.Text;
        }

        private void CopyFromCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !string.IsNullOrWhiteSpace(this.fromTextBox.Text);
        }

        private void CopyToCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            mathControl.mathExpressionBox.Text += this.toTextBox.Text;
        }

        private void CopyToCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !string.IsNullOrWhiteSpace(this.toTextBox.Text);
        }

        private void SetUnits()
        {
            fromComboBox.ItemsSource = Units;
            fromComboBox.SelectedIndex = 0;
            toComboBox.ItemsSource = Units;
            toComboBox.SelectedIndex = 0;
        }

        private void convertersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetUnits();
        }

        private void fromTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fromTextBox.IsFocused)
                ConvertFromTo();
        }

        private void toTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (toTextBox.IsFocused)
                ConvertToFrom();
        }

        private void fromToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertFromTo();
        }

        public IConverter[] Converters { get; }

        public dynamic Units
        {
            get
            {
                var conv = (IConverter)convertersComboBox.SelectedItem;

                return conv.Units;
            }
        }

    }

}