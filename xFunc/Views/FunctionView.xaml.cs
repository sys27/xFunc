// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using xFunc.Maths;
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class FunctionView : Window
    {

        private Processor processor;

        #region Commands

        public static RoutedCommand AddCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand RefreshCommand = new RoutedCommand();

        #endregion

        public FunctionView(Processor processor)
        {
            this.processor = processor;
            this.processor.Parameters.Functions.CollectionChanged += (o, args) => RefreshList();

            RefreshList();

            this.SourceInitialized += (o, args) => this.HideMinimizeAndMaximizeButtons();

            InitializeComponent();
        }

        private void RefreshList()
        {
            this.DataContext = processor.Parameters.Functions.Select(f => new FunctionViewModel(f.Key, f.Value));
        }

        #region Commands

        private void AddCommand_Executed(object o, ExecutedRoutedEventArgs args)
        {
            var view = new AddFunctionView
            {
                Owner = this
            };
            if (view.ShowDialog() == true)
            {
                try
                {
                    if (!(processor.Parse(view.FunctionName) is UserFunction userFunc))
                    {
                        MessageBox.Show(this, Resource.AddFuncError, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);

                        return;
                    }

                    var func = processor.Parse(view.Function);

                    processor.Parameters.Functions.Add(userFunc, func);

                    RefreshList();
                }
                catch (TokenizeException mle)
                {
                    MessageBox.Show(this, mle.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParseException mpe)
                {
                    MessageBox.Show(this, mpe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParameterIsReadOnlyException mpiroe)
                {
                    MessageBox.Show(this, mpiroe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (BinaryParameterTypeMismatchException bptme)
                {
                    MessageBox.Show(this, bptme.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (DifferentParameterTypeMismatchException dptme)
                {
                    MessageBox.Show(this, dptme.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParameterTypeMismatchException ptme)
                {
                    MessageBox.Show(this, ptme.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
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

            var view = new AddFunctionView(selectedItem)
            {
                Owner = this
            };
            if (view.ShowDialog() == true)
            {
                try
                {
                    var userFunc = ((FunctionViewModel)funcList.SelectedItem).Function;
                    var func = processor.Parse(view.Function);

                    processor.Parameters.Functions[userFunc] = func;

                    RefreshList();
                }
                catch (TokenizeException mle)
                {
                    MessageBox.Show(this, mle.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParseException mpe)
                {
                    MessageBox.Show(this, mpe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParameterIsReadOnlyException mpiroe)
                {
                    MessageBox.Show(this, mpiroe.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (ParameterTypeMismatchException ptme)
                {
                    MessageBox.Show(this, ptme.Message, Resource.ErrorHeader, MessageBoxButton.OK, MessageBoxImage.Warning);
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
            processor.Parameters.Functions.Remove(selectedItem.Function);

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