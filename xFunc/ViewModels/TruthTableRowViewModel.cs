// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.ViewModels
{

    public class TruthTableRowViewModel
    {
        public TruthTableRowViewModel(int varsCount, int valuesCount)
        {
            VarsValues = new bool[varsCount];
            Values = new bool[valuesCount];
        }

        public int Index { get; set; }

        public bool[] VarsValues { get; }

        public bool[] Values { get; set; }

        public bool Result
        {
            get => Values[^1];
            set => Values[^1] = value;
        }

    }

}