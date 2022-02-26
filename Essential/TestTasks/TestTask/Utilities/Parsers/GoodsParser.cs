using Entities;

namespace Utilities.Parsers
{
    public static class GoodsParser
    {
        public static bool TryParseGoods(string line, out Goods goods)
        {
            goods = null;
            if (line == null || string.IsNullOrEmpty(line))
                return false;

            if (!TryParseGoodsName(line, out string name))
                return false;

            if (!TryParseGoodsPrice(line, out decimal price))
                return false;

            goods = new Goods(name, price);
            return true;
        }

        private static bool TryParseGoodsPrice(string line, out decimal price)
        {
            price = default;
            if (line == null ||
                string.IsNullOrEmpty(line))
                return false;

            const string lineSeparator = "at ";
            string[] lineParts = line.Split(lineSeparator);
            if (lineParts.Length != 2)
                return false;

            string linePart = lineParts[1];
            if (!decimal.TryParse(linePart, out decimal result))
                return false;

            price = result;
            return true;
        }

        private static bool TryParseGoodsName(string line, out string name)
        {
            name = null;
            if (line == null || string.IsNullOrEmpty(line))
                return false;

            const string lineSeparator = " at";
            string[] lineParts = line.Split(lineSeparator);
            if (lineParts.Length != 2)
                return false;

            name = lineParts[0];
            return true;
        }
    }
}
