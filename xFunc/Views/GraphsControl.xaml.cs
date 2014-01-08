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
using xFunc.Presenters;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class GraphsControl : UserControl, IGraphsView
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(GraphsControl));

        private GraphsPresenter presenter;

        public GraphsControl()
        {
            InitializeComponent();
        }

        public GraphsControl(GraphsPresenter presenter)
        {
            this.presenter = presenter;

            InitializeComponent();
        }

        private void graphExpBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(graphExpressionBox.Text))
            {
                try
                {
                    presenter.Add(graphExpressionBox.Text);
                    Status = string.Empty;
                }
                catch (LexerException mle)
                {
                    Status = mle.Message;
                }
                catch (ParserException mpe)
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
                    Status = Resource.NotSupportedOperationError;
                }

                graphExpressionBox.Text = string.Empty;
            }
        }

        private void graphsList_SelectionChanged(object o, SelectionChangedEventArgs args)
        {
            if (graphsList.SelectedIndex >= 0)
                plot.Expression = graphsList.Items.Cast<GraphItemViewModel>();
            else
                plot.Expression = null;
        }

        private void graphItem_Toggle(object o, RoutedEventArgs args)
        {
            plot.ReRender();
        }

        private void removeGraph_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as GraphItemViewModel;

            presenter.Remove(item);
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

        public GraphsPresenter Presenter
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
