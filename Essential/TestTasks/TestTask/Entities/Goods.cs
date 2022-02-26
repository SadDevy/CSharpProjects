using System;

namespace Entities
{
    public class Goods : IEquatable<Goods>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Goods(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Goods))
                return false;

            Goods goods = (Goods)obj;
            return Equals(goods);
        }

        public bool Equals(Goods goods)
        {
            if (goods == null)
                return false;

            return Name == goods.Name && Price == goods.Price;
        }

        public override int GetHashCode()
        {
            return 33 * (Name.GetHashCode() >> 1) + 17 * (Price.GetHashCode() >> 2);
        }
    }
}
