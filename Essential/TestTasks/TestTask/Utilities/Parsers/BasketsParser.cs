using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Parsers
{
    public static class BasketsParser
    {
        public static bool TryParseBaskets(string line, out IEnumerable<Basket> baskets)
        {
            baskets = null;
            if (line == null || string.IsNullOrEmpty(line))
                return false;

            const string lineSeparator = "\r\n\r\n";
            string[] lineParts = line.Split(lineSeparator);
            if (lineParts.Length == 0)
                return false;

            List<Basket> result = new List<Basket>();
            foreach (string linePart in lineParts)
            {
                if (!TryParseBasket(linePart, out Basket basket))
                    continue;

                result.Add(basket);
            }

            baskets = result;
            return true;
        }

        private static bool TryParseBasket(string line, out Basket basket)
        {
            basket = null;
            if (line == null || string.IsNullOrEmpty(line))
                return false;

            const string lineSeparator = "\n";
            const int skippedNumber = 1;
            string[] lineParts = line.Split(lineSeparator).Skip(skippedNumber).ToArray();
            if (lineParts.Length == 0)
                return false;

            basket = new Basket();
            foreach (string linePart in lineParts)
            {
                if (!GoodsParser.TryParseGoods(linePart, out Goods goods))
                    continue;

                basket.Add(goods);
            }

            return true;
        }
    }
}
