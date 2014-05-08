// Copyright 2012-2014 Dmitry Kischenko
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
using xFunc.UnitConverters;

namespace xFunc.Views
{

    public partial class Converter : Window
    {

        private object[] converters;

        public static RoutedCommand CalculateCommand = new RoutedCommand();

        public Converter()
        {
            converters = new object[]
            {
                new AreaConverter(),
                new xFunc.UnitConverters.LengthConverter(),
                new MassConverter(),
                new PowerConverter(),
                new TemperatureConverter(),
                new TimeConverter(),
                new VolumeConverter()
            };

            InitializeComponent();

            convertersComboBox.ItemsSource = converters;
            SetUnits();
        }

        private void CalculateCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            dynamic conv = convertersComboBox.SelectedItem;
            dynamic from = fromComboBox.SelectedItem;
            dynamic to = toComboBox.SelectedItem;
            double value;

            if (double.TryParse(fromTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                double result = conv.Convert(value, from.Key, to.Key);

                toTextBox.Text = result.ToString(CultureInfo.InvariantCulture);
            }
            else if (double.TryParse(toTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                double result = conv.Convert(value, to.Key, from.Key);

                fromTextBox.Text = result.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void CalculateCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            double d;
            args.CanExecute = (!string.IsNullOrWhiteSpace(fromTextBox.Text) || !string.IsNullOrWhiteSpace(toTextBox.Text))
                           && (double.TryParse(fromTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d) || double.TryParse(toTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d));
        }

        private void SetUnits()
        {
            fromComboBox.ItemsSource = Units;
            toComboBox.ItemsSource = Units;
        }

        private void convertersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetUnits();
        }

        public object[] Converters
        {
            get
            {
                return converters;
            }
        }

        public dynamic Units
        {
            get
            {
                dynamic conv = convertersComboBox.SelectedItem;

                return conv.Units;
            }
        }

    }

}
