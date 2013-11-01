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
using System.ComponentModel;
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
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class FunctionView : Window
    {

        private MathProcessor processor;

        #region Commands

        public static RoutedCommand AddCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand RefreshCommand = new RoutedCommand();

        #endregion

        public FunctionView(MathProcessor processor)
        {
            this.processor = processor;
            RefreshList();

            this.SourceInitialized += (o, args) => this.HideMinimizeAndMaximizeButtons();

            InitializeComponent();
        }

        private void RefreshList()
        {
            this.DataContext = processor.UserFunctions.Select(f => new FunctionViewModel(f.Key, f.Value));
        }

        #region Commands

        private void AddCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            AddFunctionView view = new AddFunctionView()
            {
                Owner = this
            };
            if (view.ShowDialog() == true)
            {
                try
                {
                    var userFunc = processor.Parse(view.FunctionName) as UserFunction;
                    if (userFunc == null)
                    {
                        // todo: !!!
                        MessageBox.Show(this, "", Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);

                        return;
                    }

                    var func = processor.Parse(view.Function);

                    processor.UserFunctions.Add(userFunc, func);

                    RefreshList();
                }
                catch (MathLexerException mle)
                {
                    MessageBox.Show(this, mle.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (MathParserException mpe)
                {
                    MessageBox.Show(this, mpe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (MathParameterIsReadOnlyException mpiroe)
                {
                    MessageBox.Show(this, mpiroe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(this, ane.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ArgumentException ae)
                {
                    MessageBox.Show(this, ae.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void EditCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            var selectedItem = funcList.SelectedItem as FunctionViewModel;

            AddFunctionView view = new AddFunctionView(selectedItem)
            {
                Owner = this
            };
            if (view.ShowDialog() == true)
            {
                try
                {
                    var userFunc = (funcList.SelectedItem as FunctionViewModel).Function;
                    var func = processor.Parse(view.Function);

                    processor.UserFunctions[userFunc] = func;

                    RefreshList();
                }
                catch (MathLexerException mle)
                {
                    MessageBox.Show(this, mle.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (MathParserException mpe)
                {
                    MessageBox.Show(this, mpe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (MathParameterIsReadOnlyException mpiroe)
                {
                    MessageBox.Show(this, mpiroe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(this, ane.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ArgumentException ae)
                {
                    MessageBox.Show(this, ae.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void DeleteCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            var selectedItem = funcList.SelectedItem as FunctionViewModel;
            processor.UserFunctions.Remove(selectedItem.Function);

            RefreshList();
        }

        private void RefreshCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            RefreshList();
        }

        private void SelectedCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = funcList.SelectedItem != null;
        }

        #endregion

    }

}
