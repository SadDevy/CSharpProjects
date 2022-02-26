using System;
using System.Collections.Generic;

namespace Entities
{
    public class Basket : IEquatable<Basket>
    {
        public List<Goods> Goods { get; set; }

        public Basket()
        {
            Goods = new List<Goods>();
        }

        public void Add(Goods item)
        {
            Goods.Add(item);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Basket))
                return false;

            Basket basket = (Basket)obj;
            return Equals(basket);
        }

        public bool Equals(Basket basket)
        {
            if (basket == null)
                return false;

            if (Goods.Count != basket.Goods.Count)
                return false;

            for (int i = 0; i < Goods.Count; i++)
                if (!Goods[i].Equals(basket.Goods[i]))
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return 33 * (Goods.GetHashCode() >> 1);
        }
    }
}
