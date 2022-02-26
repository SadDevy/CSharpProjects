using System;
using System.Collections.Generic;

namespace BinaryTee
{
    public class GeneralComparer<TItem> : IComparer<TItem> where TItem : IComparable<TItem>
    {
        public bool IsDirect { get; private set; }

        private IComparer<TItem> comparer;

        public GeneralComparer(bool isDirect) 
            : this((Comparison<TItem>)null, isDirect) { }

        public GeneralComparer(IComparer<TItem> comparer, bool isDirect) 
            : this((Comparison<TItem>)comparer.Compare, isDirect) { }

        public GeneralComparer(Func<TItem, TItem, int> func, bool isDirect) 
            : this((Comparison<TItem>)func.Invoke, isDirect) { }

        public GeneralComparer(Comparison<TItem> comparison, bool isDirect)
        {
            if (comparison == null)
                comparer = Comparer<TItem>.Default;
            else
                comparer = Comparer<TItem>.Create(comparison);

            IsDirect = isDirect;
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

            result *= IsDirect ? 1 : -1;
            return result;
        }
    }
}
