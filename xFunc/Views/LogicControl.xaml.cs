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
using xFunc.Presenters;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class LogicControl : UserControl, ILogicView
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(LogicControl));

        public static RoutedCommand CopyExpToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToClipCommand = new RoutedCommand();
        public static RoutedCommand CopyExpToInputCommand = new RoutedCommand();
        public static RoutedCommand CopyAnswerToInputCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();

        private LogicPresenter presenter;

        public LogicControl()
        {
            InitializeComponent();
        }

        public LogicControl(LogicPresenter presenter)
        {
            this.presenter = presenter;

            InitializeComponent();
        }

        private void logicExpressionBox_KeyUp(object o, KeyEventArgs args)
        {
            if (args.Key == Key.Enter && !string.IsNullOrWhiteSpace(logicExpressionBox.Text))
            {
                try
                {
                    presenter.Add(logicExpressionBox.Text);
                    var count = logicExpsListBox.Items.Count;
                    if (count > 0)
                        logicExpsListBox.ScrollIntoView(logicExpsListBox.Items[count - 1]);
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

                logicExpressionBox.Text = string.Empty;
            }
        }

        private void removeLogic_Click(object o, RoutedEventArgs args)
        {
            var item = ((Button)o).Tag as LogicWorkspaceItemViewModel;

            presenter.Remove(item);
        }

        private void Copy_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = logicExpsListBox.SelectedItem != null;
        }

        private void CopyExpToClip_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText((logicExpsListBox.SelectedItem as LogicWorkspaceItemViewModel).StringExpression.ToString());
        }

        private void CopyAnswerToClip_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText((logicExpsListBox.SelectedItem as LogicWorkspaceItemViewModel).Answer.ToString());
        }

        private void CopyExpToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            logicExpressionBox.Text = (logicExpsListBox.SelectedItem as LogicWorkspaceItemViewModel).StringExpression.ToString();
        }

        private void CopyAnswerToInput_Execute(object o, ExecutedRoutedEventArgs args)
        {
            logicExpressionBox.Text = (logicExpsListBox.SelectedItem as LogicWorkspaceItemViewModel).Answer.ToString();
        }

        private void DeleteExp_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var item = (LogicWorkspaceItemViewModel)logicExpsListBox.SelectedItem;

            presenter.Remove(item);
        }

        private void DeleteExp_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = logicExpsListBox.SelectedItem != null;
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

        public LogicPresenter Presenter
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

        public IEnumerable<LogicWorkspaceItemViewModel> LogicExpressions
        {
            set
            {
                logicExpsListBox.ItemsSource = value;
            }
        }

    }

}
