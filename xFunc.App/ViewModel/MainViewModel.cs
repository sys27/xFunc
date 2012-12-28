using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using xFunc.App.Command;
using xFunc.App.Properties;
using xFunc.App.Resources;
using xFunc.App.View;
using xFunc.Library;
using xFunc.Library.Exceptions;
using xFunc.Library.Logics;
using xFunc.Library.Logics.Expressions;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.App.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        private MathParser mathParser;
        private LogicParser logicParser;

        private MainView view;
        private bool isOperationsVisibile;

        private string function = string.Empty;
        private int functionSelectionStart;
        private int functionSelectionLength;
        private string x = string.Empty;
        private string answer = string.Empty;

        private string status = string.Empty;
        private bool statusVisibility;

        private bool isMath;
        private bool isLogic;

        private RelayCommand mathCommand;
        private RelayCommand logicCommand;
        private RelayCommand degreeCommand;
        private RelayCommand radianCommand;
        private RelayCommand gradianCommand;
        private RelayCommand operationsViewCommand;
        private RelayCommand aboutCommand;
        private RelayCommand calcCommand;

        private RelayCommand plusCommand;
        private RelayCommand minusCommand;
        private RelayCommand multiplyCommand;
        private RelayCommand divideCommand;
        private RelayCommand squareCommand;
        private RelayCommand involutionCommand;
        private RelayCommand sqrtCommand;
        private RelayCommand rootCommand;
        private RelayCommand sinCommand;
        private RelayCommand cosCommand;
        private RelayCommand tanCommand;
        private RelayCommand cotCommand;
        private RelayCommand arcsinCommand;
        private RelayCommand arccosCommand;
        private RelayCommand arctanCommand;
        private RelayCommand arccotCommand;
        private RelayCommand lnCommand;
        private RelayCommand lgCommand;
        private RelayCommand logCommand;
        private RelayCommand absCommand;
        private RelayCommand plotCommand;
        private RelayCommand derivativeCommand;
        private RelayCommand piCommand;
        private RelayCommand eCommand;

        private RelayCommand notCommand;
        private RelayCommand andCommand;
        private RelayCommand orCommand;
        private RelayCommand implCommand;
        private RelayCommand eqCommand;
        private RelayCommand nandCommand;
        private RelayCommand norCommand;
        private RelayCommand xorCommand;
        private RelayCommand truthTableCommand;
        private RelayCommand trueCommand;
        private RelayCommand falseCommand;

        public MainViewModel()
        {
            mathParser = new MathParser();
            logicParser = new LogicParser();

            IsMath = Settings.Default.IsMath;
            IsLogic = Settings.Default.IsLogic;
            AngleMeasurement = Settings.Default.AngleMeasurement;
            IsOperationsVisibile = Settings.Default.IsOperationsVisibile;
            calcCommand = new RelayCommand(calcCommand_Execute, calcCommand_CanExecute);
        }

        private MathParameterCollection ParseStringToMathParams(string str)
        {
            MathParameterCollection par = new MathParameterCollection();
            if (string.IsNullOrWhiteSpace(str))
                return par;

            string[] vars = str.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in vars)
            {
                string[] part = s.Split('=');

                if (part.Length == 2)
                    par.Add(part[0][0], double.Parse(part[1], CultureInfo.InvariantCulture));
            }

            return par;
        }

        private LogicParameterCollection ParseStringToLogicParams(string str)
        {
            LogicParameterCollection par = new LogicParameterCollection();
            string[] vars = str.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in vars)
            {
                string[] part = s.Split('=');

                par.Add(part[0][0]);
                par[part[0][0]] = bool.Parse(part[1]);
            }

            return par;
        }

        private List<Row> GenerateTruthTable(ILogicExpression expression, List<ILogicExpression> exps, LogicParameterCollection parameters)
        {
            List<Row> table = new List<Row>();

            for (int i = (int)Math.Pow(2, parameters.Count) - 1; i >= 0; i--)
            {
                parameters.Bits = i;
                bool b = expression.Calculate(parameters);

                Row row = new Row(parameters.Count, exps.Count);

                row.Index = (int)Math.Pow(2, parameters.Count) - i;
                for (int j = 0; j < parameters.Count; j++)
                {
                    row.VarsValues[j] = parameters[parameters[j]];
                }

                for (int j = 0; j < exps.Count - 1; j++)
                {
                    row.Values[j] = exps[j].Calculate(parameters);
                }

                if (exps.Count != 0)
                    row.Result = b;

                table.Add(row);
            }

            return table;
        }

        private void aboutCommand_Execute(object o)
        {
            AboutView aboutView = new AboutView { Owner = view };
            aboutView.ShowDialog();
        }

        private void calcCommand_Execute(object o)
        {
            try
            {
                if (isMath)
                {
                    IMathExpression exp = mathParser.Parse(function);
                    if (exp is PlotMathExpression)
                    {
                        var oldValue = AngleMeasurement;
                        AngleMeasurement = AngleMeasurement.Radian;
                        exp = mathParser.Parse(function);
                        MathParameterCollection parameters = new MathParameterCollection();
                        parameters.Add('x', 0);

                        DrawingFunc drawingFunc = new DrawingFunc(((PlotMathExpression)exp).FirstMathExpression, parameters) { Owner = view };
                        AngleMeasurement = oldValue;
                        drawingFunc.ShowDialog();
                    }
                    else if (exp is DerivativeMathExpression)
                    {
                        Answer = exp.Derivative().ToString();
                    }
                    else
                    {
                        MathParameterCollection parameters = ParseStringToMathParams(x);

                        Answer = exp.Calculate(parameters).ToString(CultureInfo.InvariantCulture);
                    }
                }
                else if (isLogic)
                {
                    ILogicExpression exp = logicParser.Parse(function);

                    if (exp is TruthTableExpression)
                    {
                        LogicParameterCollection parameters = logicParser.GetLogicParameters(function);
                        var expression = ((TruthTableExpression)exp).FirstMathExpression;
                        List<ILogicExpression> exps = new List<ILogicExpression>(logicParser.ConvertLogicExpressionToCollection(expression));

                        Status = Resource.workingStatus;
                        StatusVisibility = true;
                        Task<List<Row>> taks = new Task<List<Row>>(() => GenerateTruthTable(expression, exps, parameters));
                        taks.ContinueWith(t => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            StatusVisibility = false;
                            LogicView logicView = new LogicView(t.Result, exps, parameters) { Owner = view };
                            logicView.ShowDialog();
                        })));
                        taks.Start();
                    }
                    else
                    {
                        LogicParameterCollection parameters = ParseStringToLogicParams(x);
                        Answer = exp.Calculate(parameters).ToString();
                    }
                }
            }
            catch (LexerException le)
            {
                Answer = le.Message;
            }
            catch (ParserException pe)
            {
                Answer = pe.Message;
            }
            catch (DivideByZeroException dbze)
            {
                Answer = dbze.Message;
            }
            catch (ArgumentNullException ane)
            {
                Answer = ane.Message;
            }
            catch (ArgumentException ae)
            {
                Answer = ae.Message;
            }
            catch (FormatException fe)
            {
                Answer = fe.Message;
            }
            catch (OverflowException oe)
            {
                Answer = oe.Message;
            }
            catch (KeyNotFoundException)
            {
                Answer = Resource.keyNotFoundExceptionMessage;
            }
            catch (IndexOutOfRangeException)
            {
                Answer = Resource.indexOutOfRangeexceptionMessage;
            }
            catch (InvalidOperationException ioe)
            {
                Answer = ioe.Message;
            }
            catch (NotSupportedException)
            {
                Answer = Resource.notSupported;
            }
        }

        private bool calcCommand_CanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(function);
        }

        private void ClearValues()
        {
            Function = string.Empty;
            X = string.Empty;
            Answer = string.Empty;
        }

        private void BinFuncInsert(string symbols)
        {
            var temp = functionSelectionStart;
            Function = function.Insert(functionSelectionStart, symbols);
            view.Activate();
            view.functionTextBox.SelectionStart = temp + symbols.Length;
        }

        private void UnFuncInsert(string startSymbols, string endSymbols)
        {
            var temp = functionSelectionStart + functionSelectionLength;
            var fsl = functionSelectionLength;
            Function = function.Insert(functionSelectionStart, startSymbols).Insert(temp + startSymbols.Length, endSymbols);
            view.Activate();
            if (fsl > 0)
                view.functionTextBox.SelectionStart = temp + startSymbols.Length + endSymbols.Length;
            else
                view.functionTextBox.SelectionStart = temp + startSymbols.Length;
        }

        public string Function
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
                OnPropertyChanged("Function");
            }
        }

        public int FunctionSelectionStart
        {
            get
            {
                return functionSelectionStart;
            }
            set
            {
                functionSelectionStart = value;
                OnPropertyChanged("FunctionSelectionStart");
            }
        }

        public int FunctionSelectionLength
        {
            get
            {
                return functionSelectionLength;
            }
            set
            {
                functionSelectionLength = value;
                OnPropertyChanged("FunctionSelectionLength");
            }
        }

        public string X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public bool StatusVisibility
        {
            get
            {
                return statusVisibility;
            }
            set
            {
                statusVisibility = value;
                OnPropertyChanged("StatusVisibility");
            }
        }

        public bool IsMath
        {
            get { return isMath; }
            set
            {
                if (isMath != value)
                {
                    ClearValues();
                    Settings.Default.IsMath = value;
                    isMath = value;
                }

                OnPropertyChanged("IsMath");
            }
        }

        public bool IsLogic
        {
            get { return isLogic; }
            set
            {
                if (isLogic != value)
                {
                    ClearValues();
                    Settings.Default.IsLogic = value;
                    isLogic = value;
                }

                OnPropertyChanged("IsLogic");
            }
        }

        public AngleMeasurement AngleMeasurement
        {
            get
            {
                return mathParser.AngleMeasurement;
            }
            set
            {
                mathParser.AngleMeasurement = value;
                Settings.Default.AngleMeasurement = value;
                OnPropertyChanged("AngleMeasurement");
            }
        }

        public MainView View
        {
            get { return view; }
            set { view = value; }
        }

        public bool IsOperationsVisibile
        {
            get
            {
                return isOperationsVisibile;
            }
            set
            {
                isOperationsVisibile = value;
                Settings.Default.IsOperationsVisibile = value;
                OnPropertyChanged("IsOperationsVisibile");
            }
        }

        public ICommand MathCommand
        {
            get
            {
                if (mathCommand == null)
                    mathCommand = new RelayCommand(o => IsMath = true);

                return mathCommand;
            }
        }

        public ICommand LogicCommand
        {
            get
            {
                if (logicCommand == null)
                    logicCommand = new RelayCommand(o => IsLogic = true);

                return logicCommand;
            }
        }

        public ICommand DegreeCommand
        {
            get
            {
                if (degreeCommand == null)
                    degreeCommand = new RelayCommand(o => AngleMeasurement = AngleMeasurement.Degree);

                return degreeCommand;
            }
        }

        public ICommand RadianCommand
        {
            get
            {
                if (radianCommand == null)
                    radianCommand = new RelayCommand(o => AngleMeasurement = AngleMeasurement.Radian);

                return radianCommand;
            }
        }

        public ICommand GradianCommand
        {
            get
            {
                if (gradianCommand == null)
                    gradianCommand = new RelayCommand(o => AngleMeasurement = AngleMeasurement.Gradian);

                return gradianCommand;
            }
        }

        public ICommand OperationsViewCommand
        {
            get
            {
                if (operationsViewCommand == null)
                    operationsViewCommand = new RelayCommand(o => IsOperationsVisibile = !isOperationsVisibile);

                return operationsViewCommand;
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                    aboutCommand = new RelayCommand(aboutCommand_Execute);

                return aboutCommand;
            }
        }

        public ICommand CalcCommand
        {
            get
            {
                return calcCommand;
            }
        }

        #region Math Operations

        public ICommand PlusCommand
        {
            get
            {
                if (plusCommand == null)
                    plusCommand = new RelayCommand(o => BinFuncInsert("+"));

                return plusCommand;
            }
        }

        public ICommand MinusCommand
        {
            get
            {
                if (minusCommand == null)
                    minusCommand = new RelayCommand(o => BinFuncInsert("-"));

                return minusCommand;
            }
        }

        public ICommand MultiplyCommand
        {
            get
            {
                if (multiplyCommand == null)
                    multiplyCommand = new RelayCommand(o => BinFuncInsert("*"));

                return multiplyCommand;
            }
        }

        public ICommand DivideCommand
        {
            get
            {
                if (divideCommand == null)
                    divideCommand = new RelayCommand(o => BinFuncInsert("/"));

                return divideCommand;
            }
        }

        public ICommand SquareCommand
        {
            get
            {
                if (squareCommand == null)
                    squareCommand = new RelayCommand(o =>
                    {
                        if (functionSelectionLength > 0)
                        {
                            UnFuncInsert("(", ")^2");
                        }
                        else
                        {
                            UnFuncInsert(string.Empty, "^2");
                        }
                    });

                return squareCommand;
            }
        }

        public ICommand InvolutionCommand
        {
            get
            {
                if (involutionCommand == null)
                    involutionCommand = new RelayCommand(o => BinFuncInsert("^"));

                return involutionCommand;
            }
        }

        public ICommand SqrtCommand
        {
            get
            {
                if (sqrtCommand == null)
                    sqrtCommand = new RelayCommand(o => UnFuncInsert("sqrt(", ")"));

                return sqrtCommand;
            }
        }

        public ICommand RootCommand
        {
            get
            {
                if (rootCommand == null)
                    rootCommand = new RelayCommand(o => UnFuncInsert("root(", ", )"));

                return rootCommand;
            }
        }

        public ICommand SinCommand
        {
            get
            {
                if (sinCommand == null)
                    sinCommand = new RelayCommand(o => UnFuncInsert("sin(", ")"));

                return sinCommand;
            }
        }

        public ICommand CosCommand
        {
            get
            {
                if (cosCommand == null)
                    cosCommand = new RelayCommand(o => UnFuncInsert("cos(", ")"));

                return cosCommand;
            }
        }

        public ICommand TanCommand
        {
            get
            {
                if (tanCommand == null)
                    tanCommand = new RelayCommand(o => UnFuncInsert("tan(", ")"));

                return tanCommand;
            }
        }

        public ICommand CotCommand
        {
            get
            {
                if (cotCommand == null)
                    cotCommand = new RelayCommand(o => UnFuncInsert("cot(", ")"));

                return cotCommand;
            }
        }

        public ICommand ArcsinCommand
        {
            get
            {
                if (arcsinCommand == null)
                    arcsinCommand = new RelayCommand(o => UnFuncInsert("arcsin(", ")"));

                return arcsinCommand;
            }
        }

        public ICommand ArccosCommand
        {
            get
            {
                if (arccosCommand == null)
                    arccosCommand = new RelayCommand(o => UnFuncInsert("arccos(", ")"));

                return arccosCommand;
            }
        }

        public ICommand ArctanCommand
        {
            get
            {
                if (arctanCommand == null)
                    arctanCommand = new RelayCommand(o => UnFuncInsert("arctan(", ")"));

                return arctanCommand;
            }
        }

        public ICommand ArccotCommand
        {
            get
            {
                if (arccotCommand == null)
                    arccotCommand = new RelayCommand(o => UnFuncInsert("arccot(", ")"));

                return arccotCommand;
            }
        }

        public ICommand LnCommand
        {
            get
            {
                if (lnCommand == null)
                    lnCommand = new RelayCommand(o => UnFuncInsert("ln(", ")"));

                return lnCommand;
            }
        }

        public ICommand LgCommand
        {
            get
            {
                if (lgCommand == null)
                    lgCommand = new RelayCommand(o => UnFuncInsert("lg(", ")"));

                return lgCommand;
            }
        }

        public ICommand LogCommand
        {
            get
            {
                if (logCommand == null)
                    logCommand = new RelayCommand(o => UnFuncInsert("log(", ", )"));

                return logCommand;
            }
        }

        public ICommand AbsCommand
        {
            get
            {
                if (absCommand == null)
                    absCommand = new RelayCommand(o => UnFuncInsert("abs(", ")"));

                return absCommand;
            }
        }

        public ICommand PlotCommand
        {
            get
            {
                if (plotCommand == null)
                    plotCommand = new RelayCommand(o => UnFuncInsert("plot(", ")"));

                return plotCommand;
            }
        }

        public ICommand DerivativeCommand
        {
            get
            {
                if (derivativeCommand == null)
                    derivativeCommand = new RelayCommand(o => UnFuncInsert("deriv(", ", )"));

                return derivativeCommand;
            }
        }

        public ICommand PiCommand
        {
            get
            {
                if (piCommand == null)
                    piCommand = new RelayCommand(o => BinFuncInsert("π"));

                return piCommand;
            }
        }

        public ICommand ECommand
        {
            get
            {
                if (eCommand == null)
                    eCommand = new RelayCommand(o => BinFuncInsert("E"));

                return eCommand;
            }
        }

        #endregion

        #region Logic Operations

        public ICommand NotCommand
        {
            get
            {
                if (notCommand == null)
                    notCommand = new RelayCommand(o =>
                    {
                        if (functionSelectionLength > 0)
                            UnFuncInsert("!(", ")");
                        else
                            UnFuncInsert("!", string.Empty);
                    });

                return notCommand;
            }
        }

        public ICommand AndCommand
        {
            get
            {
                if (andCommand == null)
                    andCommand = new RelayCommand(o => BinFuncInsert("&"));

                return andCommand;
            }
        }

        public ICommand OrCommand
        {
            get
            {
                if (orCommand == null)
                    orCommand = new RelayCommand(o => BinFuncInsert("|"));

                return orCommand;
            }
        }

        public ICommand ImplCommad
        {
            get
            {
                if (implCommand == null)
                    implCommand = new RelayCommand(o => BinFuncInsert("->"));

                return implCommand;
            }
        }

        public ICommand EqCommad
        {
            get
            {
                if (eqCommand == null)
                    eqCommand = new RelayCommand(o => BinFuncInsert("<->"));

                return eqCommand;
            }
        }

        public ICommand NAndCommand
        {
            get
            {
                if (nandCommand == null)
                    nandCommand = new RelayCommand(o => BinFuncInsert(" nand "));

                return nandCommand;
            }
        }

        public ICommand NOrCommand
        {
            get
            {
                if (norCommand == null)
                    norCommand = new RelayCommand(o => BinFuncInsert(" nor "));

                return norCommand;
            }
        }

        public ICommand XOrCommand
        {
            get
            {
                if (xorCommand == null)
                    xorCommand = new RelayCommand(o => BinFuncInsert("^"));

                return xorCommand;
            }
        }

        public ICommand TruthTableCommand
        {
            get
            {
                if (truthTableCommand == null)
                    truthTableCommand = new RelayCommand(o => UnFuncInsert("table(", ")"));

                return truthTableCommand;
            }
        }

        public ICommand TrueCommand
        {
            get
            {
                if (trueCommand == null)
                    trueCommand = new RelayCommand(o => BinFuncInsert("t"));

                return trueCommand;
            }
        }

        public ICommand FalseCommand
        {
            get
            {
                if (falseCommand == null)
                    falseCommand = new RelayCommand(o => BinFuncInsert("f"));

                return falseCommand;
            }
        }

        #endregion

    }

}
