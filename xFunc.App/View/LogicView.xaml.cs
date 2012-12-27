using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using xFunc.Library.Expressions.Logics;

namespace xFunc.App.View
{

    public partial class LogicView : Window
    {

        private LogicParameterCollection parameters;
        private List<Row> truthTable;
        private List<ILogicExpression> exps;

        private GridView gridView;

        public LogicView(List<Row> truthTable, List<ILogicExpression> exps, LogicParameterCollection parameters)
        {
            this.parameters = parameters;
            this.exps = exps;
            this.truthTable = truthTable;

            InitializeComponent();

            Generate();
        }

        private void Generate()
        {
            gridView = new GridView();

            gridView.Columns.Add(new GridViewColumn
                                 {
                                     Header = "#",
                                     DisplayMemberBinding = new Binding("Index")
                                 });
            for (int i = 0; i < parameters.Count; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                                     {
                                         Header = parameters[i],
                                         DisplayMemberBinding = new Binding(string.Format("VarsValues[{0}]", i))
                                     });
            }
            for (int i = 0; i < exps.Count - 1; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                                     {
                                         Header = exps[i],
                                         DisplayMemberBinding = new Binding(string.Format("Values[{0}]", i))
                                     });
            }
            if (exps.Count != 0)
                gridView.Columns.Add(new GridViewColumn
                                     {
                                         Header = exps[exps.Count - 1],
                                         DisplayMemberBinding = new Binding("Result")
                                     });

            tableList.View = gridView;

            tableList.ItemsSource = truthTable;
        }

    }

    public class Row
    {

        private int index;
        private bool[] varsValues;
        private bool[] values;

        public Row(int varsCount, int valuesCount)
        {
            varsValues = new bool[varsCount];
            values = new bool[valuesCount];
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public bool[] VarsValues
        {
            get { return varsValues; }
        }

        public bool[] Values
        {
            get { return values; }
            set { values = value; }
        }

        public bool Result
        {
            get { return values[values.Length - 1]; }
            set { values[values.Length - 1] = value; }
        }

    }

}
