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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Resources;

namespace xFunc.Presenters
{

    public class MathWorkspace : IList<MathWorkspaceItem>
    {

        private int maxCountOfExps;
        private List<MathWorkspaceItem> expressions;

        public MathWorkspace()
            : this(20)
        {
        }

        public MathWorkspace(int maxCountOfExps)
        {
            this.maxCountOfExps = maxCountOfExps;
            expressions = new List<MathWorkspaceItem>(maxCountOfExps >= 20 ? 20 : maxCountOfExps);
        }

        public MathWorkspaceItem this[int index]
        {
            get
            {
                return expressions[index];
            }
            set
            {
                expressions[index] = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return expressions.GetEnumerator();
        }

        public IEnumerator<MathWorkspaceItem> GetEnumerator()
        {
            return expressions.GetEnumerator();
        }

        public void Add(MathWorkspaceItem item)
        {
            if (expressions.Count >= maxCountOfExps)
                expressions.RemoveAt(0);

            expressions.Add(item);
        }

        public void AddRange(IEnumerable<MathWorkspaceItem> items)
        {
            while (expressions.Count >= maxCountOfExps)
                expressions.RemoveAt(0);

            expressions.AddRange(items);
        }

        public int IndexOf(MathWorkspaceItem item)
        {
            return expressions.IndexOf(item);
        }

        public void Insert(int index, MathWorkspaceItem item)
        {
            if (expressions.Count >= maxCountOfExps)
                expressions.RemoveAt(0);

            expressions.Insert(index, item);
        }

        public void Clear()
        {
            expressions.Clear();
        }

        public bool Remove(MathWorkspaceItem item)
        {
            return expressions.Remove(item);
        }

        public void RemoveAt(int index)
        {
            expressions.RemoveAt(index);
        }

        public bool Contains(MathWorkspaceItem item)
        {
            return expressions.Contains(item);
        }

        public void CopyTo(MathWorkspaceItem[] items, int index)
        {
            expressions.CopyTo(items, index);
        }

        public int Count
        {
            get
            {
                return expressions.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int MaxCountOfExpressions
        {
            get
            {
                return maxCountOfExps;
            }
            set
            {
                maxCountOfExps = value;
            }
        }

    }

}
