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
using xFunc.Logics;
using xFunc.Logics.Expressions;
using xFunc.Presenters;
using xFunc.Resources;

namespace xFunc.Views
{

    public partial class TruthTableControl : UserControl
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(TruthTableControl));

        private TruthTablePresenter presenter;

        public TruthTableControl()
        {
            InitializeComponent();
        }

        public TruthTableControl(TruthTablePresenter presenter)
        {
            this.presenter = presenter;

            InitializeComponent();
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

        private void truthTableExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(truthTableExpressionBox.Text))
            {
                try
                {
                    presenter.Generate(truthTableExpressionBox.Text);
                    GenerateTruthTable(presenter.Expressions, presenter.Parameters);
                    truthTableList.ItemsSource = presenter.Table;

                    Status = string.Empty;
                }
                catch (LogicLexerException lle)
                {
                    Status = lle.Message;
                }
                catch (LogicParserException lpe)
                {
                    Status = lpe.Message;
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
            }
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

        public TruthTablePresenter Presenter
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

    }

}
