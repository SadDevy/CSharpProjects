using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class StudentTestInfoComparer<TItem> : IComparer<TItem> where TItem : IComparable<TItem>
    {
        private IComparer<TItem> comparer;
        private int comparisonSign;

        public StudentTestInfoComparer(bool isDirect)
        {
            comparisonSign = isDirect ? 1 : -1;

            comparer = Comparer<TItem>.Default;
        }

        public StudentTestInfoComparer(IComparer<TItem> comparer, bool isDirect) : this(isDirect)
        {
            if (comparer == null)
                this.comparer = Comparer<TItem>.Default;
            else
                this.comparer = comparer;
        }

        public StudentTestInfoComparer(Comparison<TItem> comparison, bool isDirect) : this(isDirect)
        {
            if (comparison == null)
                comparer = Comparer<TItem>.Default;
            else
                comparer = Comparer<TItem>.Create(comparison);
        }

        public StudentTestInfoComparer(Func<TItem, TItem, int> func, bool isDirect) : this(isDirect)
        {
            if (func == null)
                comparer = Comparer<TItem>.Default;
            else
                comparer = Comparer<TItem>.Create(func.Invoke);
        }

        public int Compare(TItem a, TItem b)
        {
            int result;
            if (a == null)
            {
                result = (b == null) ? 0 : -1;
            }
            else if (b == null)
            {
                result = 1;
            }
            else
            {
                result = comparer.Compare(a, b);
            }

            return comparisonSign * result;

            //if (comparison != null)
            //{
            //    result = comparison(a, b);
            //}
            //else if (comparer != null)
            //{
            //    result = comparer.Compare(a, b);
            //}
            //else if (func != null)
            //{
            //    result = func(a, b);
            //}
            //else
            //{
            //    result = a.CompareTo(b);
            //}
        }
    }
}
