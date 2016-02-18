// Copyright 2012-2016 Dmitry Kischenko
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
