using System;

namespace BinaryTee
{
    [Serializable]
    public class Node<TItem> : IEquatable<Node<TItem>>
        where TItem : IComparable<TItem>
    {
        public TItem Data { get; set; }
        public Node<TItem> Right { get; set; }
        public Node<TItem> Left { get; set; }

        public bool HasRight => Right != null;
        public bool HasLeft => Left != null;

        public Node() { }

        public Node(TItem data, Node<TItem> right = null, Node<TItem> left = null)
        {
            Data = data;
            Right = right;
            Left = left;
        }

        public bool Equals(Node<TItem> a)
        {
            if (a == null)
                return false;

            return Data.CompareTo(a.Data) == 0;
        }
    }
}
