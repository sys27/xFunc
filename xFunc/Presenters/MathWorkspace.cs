// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;

namespace xFunc.Presenters
{

    public class MathWorkspace : IList<MathWorkspaceItem>
    {
        private readonly List<MathWorkspaceItem> expressions;

        public MathWorkspace()
            : this(20)
        {
        }

        public MathWorkspace(int maxCountOfExps)
        {
            this.MaxCountOfExpressions = maxCountOfExps;
            expressions = new List<MathWorkspaceItem>(maxCountOfExps >= 20 ? 20 : maxCountOfExps);
        }

        public MathWorkspaceItem this[int index]
        {
            get => expressions[index];
            set => expressions[index] = value;
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
            if (expressions.Count >= MaxCountOfExpressions)
                expressions.RemoveAt(0);

            expressions.Add(item);
        }

        public void AddRange(IEnumerable<MathWorkspaceItem> items)
        {
            while (expressions.Count >= MaxCountOfExpressions)
                expressions.RemoveAt(0);

            expressions.AddRange(items);
        }

        public int IndexOf(MathWorkspaceItem item)
        {
            return expressions.IndexOf(item);
        }

        public void Insert(int index, MathWorkspaceItem item)
        {
            if (expressions.Count >= MaxCountOfExpressions)
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

        public int Count => expressions.Count;

        public bool IsReadOnly => false;

        public int MaxCountOfExpressions { get; set; }

    }

}