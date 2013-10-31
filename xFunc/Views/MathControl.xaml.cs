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
using System.Windows.Navigation;
using System.Windows.Shapes;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Presenters;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class MathControl : UserControl, IMathView
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(MathControl));

        public static RoutedCommand CopyExpToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyExpToInputCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToInputCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();

        private MathPresenter presenter;

        public MathControl()
        {
            InitializeComponent();
        }

        public MathControl(MathPresenter presenter)
        {
            this.presenter = presenter;

            InitializeComponent();
        }

        internal void MathExpEnter()
        {
            try
            {
                presenter.Add(mathExpressionBox.Text);
                var count = mathExpsListBox.Items.Count;
                if (count > 0)
                    mathExpsListBox.ScrollIntoView(mathExpsListBox.Items[count - 1]);
                Status = string.Empty;
            }
            catch (MathLexerException mle)
            {
                Status = mle.Message;
            }
            catch (MathParserException mpe)
            {
                Status = mpe.Message;
            }
            catch (MathParameterIsReadOnlyException mpiroe)
            {
                Status = mpiroe.Message;
            }
            catch (DivideByZeroException dbze)
            {
                Status = dbze.Message;
            }
            catch (ArgumentNullException ane)
            {
                Status = ane.Message;
            }
            catch (ArgumentException ae)
            {
                Status = ae.Message;
            }
            catch (FormatException fe)
            {
                Status = fe.Message;
            }
            catch (OverflowException oe)
            {
                Status = oe.Message;
            }
            catch (KeyNotFoundException knfe)
            {
                Status = knfe.Message;
            }
            catch (IndexOutOfRangeException)
            {
                Status = Resource.IndexOutOfRangeExceptionError;
            }
            catch (InvalidOperationException ioe)
            {
                Status = ioe.Message;
            }
            catch (NotSupportedException)
            {
                Status = Resource.NotSupportedOperationError;
            }

            mathExpressionBox.Text = string.Empty;
        }

        private void mathExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(mathExpressionBox.Text))
            {
                MathExpEnter();
            }
        }

        private void removeMath_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as MathWorkspaceItemViewModel;

            presenter.Remove(item);
        }

        private void Copy_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = mathExpsListBox.SelectedItem != null;
        }

        private void CopyExpToClip_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText((mathExpsListBox.SelectedItem as MathWorkspaceItemViewModel).StringExpression.ToString());
        }

        private void CopyAnswerToClip_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText((mathExpsListBox.SelectedItem as MathWorkspaceItemViewModel).Answer.ToString());
        }

        private void CopyExpToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathExpressionBox.Text = (mathExpsListBox.SelectedItem as MathWorkspaceItemViewModel).StringExpression.ToString();
        }

        private void CopyAnswerToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathExpressionBox.Text = (mathExpsListBox.SelectedItem as MathWorkspaceItemViewModel).Answer.ToString();
        }

        private void DeleteExp_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var item = (MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem;

            presenter.Remove(item);
        }

        private void DeleteExp_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = mathExpsListBox.SelectedItem != null;
        }

        public string Status
        {
            get
            {
                return (string)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        public MathPresenter Presenter
        {
            get
            {
                return presenter;
            }
            set
            {
                presenter = value;
            }
        }

        public IEnumerable<MathWorkspaceItemViewModel> MathExpressions
        {
            set
            {
                mathExpsListBox.ItemsSource = value;
            }
        }

    }

}
