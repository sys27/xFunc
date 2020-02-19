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
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using xFunc.Maths;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Presenters;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class MathControl : UserControl, IMathView
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof(Status), typeof(string), typeof(MathControl));

        public static RoutedCommand CopyExpToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyExpToInputCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToInputCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();

        private MathPresenter presenter;

        private int index;
        private Key prevKey;

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
            catch (TokenizeException mle)
            {
                Status = mle.Message;
            }
            catch (ParseException mpe)
            {
                Status = mpe.Message;
            }
            catch (ParameterIsReadOnlyException mpiroe)
            {
                Status = mpiroe.Message;
            }
            catch (BinaryParameterTypeMismatchException bptme)
            {
                Status = bptme.Message;
            }
            catch (DifferentParameterTypeMismatchException dptme)
            {
                Status = dptme.Message;
            }
            catch (ParameterTypeMismatchException ptme)
            {
                Status = ptme.Message;
            }
            catch (ResultIsNotSupportedException rinse)
            {
                Status = rinse.Message;
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
            else if (args.Key == Key.Up && presenter.Workspace.Count > 0)
            {
                if (prevKey != Key.None && prevKey != args.Key)
                    index++;

                var newIndex = presenter.Workspace.Count - 1 - index;
                if (newIndex >= 0 && newIndex < presenter.Workspace.Count)
                {
                    this.mathExpressionBox.Text = presenter.Workspace[newIndex].StringExpression;
                    this.mathExpressionBox.SelectionStart = this.mathExpressionBox.Text.Length;

                    if (index < presenter.Workspace.Count - 1)
                        index++;
                }

                prevKey = args.Key;
            }
            else if (args.Key == Key.Down && presenter.Workspace.Count > 0)
            {
                if (index > 0 || (prevKey != Key.None && prevKey != args.Key))
                    index--;

                var newIndex = presenter.Workspace.Count - 1 - index;
                if (newIndex >= 0 && newIndex < presenter.Workspace.Count)
                {
                    this.mathExpressionBox.Text = presenter.Workspace[newIndex].StringExpression;
                    this.mathExpressionBox.SelectionStart = this.mathExpressionBox.Text.Length;
                }

                prevKey = args.Key;
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
            Clipboard.SetText(((MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem).StringExpression);
        }

        private void CopyAnswerToClip_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText(((MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem).Answer);
        }

        private void CopyExpToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathExpressionBox.Text = ((MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem).StringExpression;
        }

        private void CopyAnswerToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathExpressionBox.Text = ((MathWorkspaceItemViewModel)mathExpsListBox.SelectedItem).Answer;
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