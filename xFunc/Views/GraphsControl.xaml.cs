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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using xFunc.Maths;
using xFunc.Presenters;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class GraphsControl : UserControl, IGraphsView
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof(Status), typeof(string), typeof(GraphsControl));

        public GraphsControl()
        {
            InitializeComponent();
        }

        public GraphsControl(GraphsPresenter presenter)
        {
            this.Presenter = presenter;

            InitializeComponent();
        }

        private void graphExpBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(graphExpressionBox.Text))
            {
                try
                {
                    Presenter.Add(graphExpressionBox.Text);
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
                catch (KeyNotFoundException)
                {
                    Presenter.Remove(Presenter.CountOfGraphs - 1);

                    Status = Resource.VariableNotFoundExceptionError;
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
                    Presenter.Remove(Presenter.CountOfGraphs - 1);

                    Status = Resource.NotSupportedOperationError;
                }

                graphExpressionBox.Text = string.Empty;
            }
        }

        private void graphsList_SelectionChanged(object o, SelectionChangedEventArgs args)
        {
            plot.Expression = graphsList.SelectedIndex >= 0 ? graphsList.Items.Cast<GraphItemViewModel>() : null;
        }

        private void graphItem_Toggle(object o, RoutedEventArgs args)
        {
            plot.ReRender();
        }

        private void removeGraph_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as GraphItemViewModel;

            Presenter.Remove(item);
        }

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public GraphsPresenter Presenter { get; set; }

        public IEnumerable<GraphItemViewModel> Graphs
        {
            set
            {
                graphsList.SelectedIndex = -1;
                graphsList.ItemsSource = value;
                graphsList.SelectedIndex = value.Count() - 1;
            }
        }

    }

}