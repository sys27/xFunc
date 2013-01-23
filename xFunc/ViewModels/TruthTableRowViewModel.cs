using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.ViewModels
{

    public class TruthTableRowViewModel
    {

        private int index;
        private bool[] varsValues;
        private bool[] values;

        public TruthTableRowViewModel(int varsCount, int valuesCount)
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
