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
using xFunc.Maths;
using xFunc.Maths.Expressions;
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
            if (!RememberNumberAndAngle)
            {
                Angle = Settings.Default.AngleMeasurement;
                Base = Settings.Default.NumberBase;
            }
            MaxCountOfExps = Settings.Default.MaxCountOfExpressions;
            CheckUpdates = Settings.Default.CheckUpdates;
        }

        private void OKCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            DialogResult = true;
            Close();
        }

        private void OKCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            int max;
            args.CanExecute = int.TryParse(this.maxCountOfExpsTextBox.Text, out max);
        }

        private void ResetCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            // todo: !!!
            //Settings.Default.Reset();
            Init();
        }

        public string ProgramLanguage
        {
            get
            {
                return (string)this.langComboBox.SelectedValue;
            }
            internal set
            {
                this.langComboBox.SelectedValue = value;
            }
        }

        public bool RememberStateAndPosition
        {
            get
            {
                return this.positionCheckBox.IsChecked.Value;
            }
            internal set
            {
                this.positionCheckBox.IsChecked = value;
            }
        }

        public bool RememberRightToolBar
        {
            get
            {
                return this.toolBarCheckBox.IsChecked.Value;
            }
            internal set
            {
                this.toolBarCheckBox.IsChecked = value;
            }
        }

        public bool RememberNumberAndAngle
        {
            get
            {
                return this.numAndAngleCheckBox.IsChecked.Value;
            }
            internal set
            {
                this.numAndAngleCheckBox.IsChecked = value;
            }
        }

        public AngleMeasurement Angle
        {
            get
            {
                return (AngleMeasurement)this.angleComboBox.SelectedValue;
            }
            internal set
            {
                this.angleComboBox.SelectedValue = value;
            }
        }

        public NumeralSystem Base
        {
            get
            {
                return (NumeralSystem)this.baseComboBox.SelectedValue;
            }
            internal set
            {
                this.baseComboBox.SelectedValue = value;
            }
        }

        public int MaxCountOfExps
        {
            get
            {
                return int.Parse(this.maxCountOfExpsTextBox.Text);
            }
            internal set
            {
                this.maxCountOfExpsTextBox.Text = value.ToString();
            }
        }

        public bool CheckUpdates
        {
            get
            {
                return this.updatesCheckBox.IsChecked.Value;
            }
            internal set
            {
                this.updatesCheckBox.IsChecked = value;
            }
        }

    }

}
