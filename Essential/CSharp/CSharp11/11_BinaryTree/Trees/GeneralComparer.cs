using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class GeneralComparer<TItem> : IComparer<TItem> where TItem : IComparable<TItem>
    {
        public bool IsForward { get; private set; }

        private IComparer<TItem> comparer;

        public GeneralComparer(bool isDirect)
        {
            IsForward = isDirect;

            comparer = Comparer<TItem>.Default;
        }

        public GeneralComparer(IComparer<TItem> comparer, bool isDirect) : this(isDirect)
        {
            if (comparer != null)
                this.comparer = comparer;
        }

        public GeneralComparer(Comparison<TItem> comparison, bool isDirect) : this(isDirect)
        {
            if (comparison != null)
                comparer = Comparer<TItem>.Create(comparison);
        }

        public GeneralComparer(Func<TItem, TItem, int> func, bool isDirect) : this(isDirect)
        {
            if (func != null)
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

            result *= IsForward ? 1 : -1;
            return result;
        }
    }
}
