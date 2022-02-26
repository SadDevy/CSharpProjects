using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Taxes
{
    public class BasicTax : ITax
    {
        private const int defaultTax = 0;
        private const int basicTax = 10;

        public int GetPercent(string goodsName)
        {
            bool isCandy = GoodsIsCandy(goodsName);
            bool isPopcorn = GoodsIsPopcorn(goodsName);
            bool isCoffee = GoodsIsCoffee(goodsName);

            if (isCandy || isPopcorn || isCoffee)
                return defaultTax;

            return basicTax;
        }

        private bool GoodsIsCoffee(string goodsName)
        {
            const string keyWord = "coffee";
            return goodsName.ToLower().Contains(keyWord);
        }

        private bool GoodsIsPopcorn(string goodsName)
        {
            const string keyWord = "popcorn";
            return goodsName.ToLower().Contains(keyWord);
        }

        private bool GoodsIsCandy(string goodsName)
        {
            IEnumerable<Candies> candies = Enum.GetValues(typeof(Candies)).Cast<Candies>();
            foreach (Candies candy in candies)
            {
                string candyName = candy.ToString().ToLower();

                bool isCandy = goodsName.ToLower().Contains(candyName);
                if (isCandy)
                    return true;
            }

            return false;
        }
    }
}
