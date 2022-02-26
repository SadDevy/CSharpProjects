using System;
using System.Collections.Generic;
using System.Linq;

namespace Methods
{
    public static class Methods
    {
        public static Dictionary<int, int> GetNumbersWithLinq(int[] arr)
        {
            var grouping = arr.Where(n => n > 0)
                .GroupBy(n => n)
                .ToDictionary(v => v.Key, n => n.Count());

            return grouping;
        }

        public static Dictionary<int, int> GetNumbersWithArrays(int[] arr)
        {
            int[] keys = new int[arr.Length];
            int[] values = new int[arr.Length];

            int j = 0;
            for (int i = 0; j < keys.Length; i++)
            {
                for (; j < arr.Length; j++)
                {
                    if (arr[j] <= 0)
                        continue;

                    if (Array.FindAll(keys, n => n == arr[j]).Length != 0)
                    {
                        int index = Array.IndexOf(keys, arr[j]);
                        values[index] += 1;

                        continue;
                    }

                    keys[i] = arr[j];
                    break;
                }
                
            }

            Dictionary<int, int> result = new Dictionary<int, int>();
            for (int k = 0; k < keys.Length; k++)
            {
                if (keys[k] == 0)
                    break;

                result.Add(keys[k], values[k]);
            }

            return result;
        }

        public static Dictionary<int, int> GetNumbersWithLists(int[] arr)
        {
            List<int> keys = new List<int>();
            List<int> values = new List<int>();

            foreach (var item in arr)
            {
                if (item <= 0)
                    continue;

                if (keys.Contains(item))
                {
                    int index = keys.IndexOf(item);
                    values[index] += 1;
                    
                    continue;
                }
                
                keys.Add(item);
                values.Add(1);
            }
          
            Dictionary<int, int> result = new Dictionary<int, int>();
            for (int k = 0; k < keys.Count; k++)
            {
                if (keys[k] == 0)
                    break;

                result.Add(keys[k], values[k]);
            }

            return result;
        }
        
        private static int GetTwoCountWithLinq(int n)
        {
            var numbers = GetNumbers(n);

            return numbers.SelectMany(n => n.ToString())
                .GroupBy(n => n)
                .Select(n => new
                {
                    key = n.Key,
                    value = n.Count()
                }).Where(n => n.key == '2')
                .Select(n => n.value)
                .First();
        }

        private static List<int> GetNumbers(int n)
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i <= n; i++)
                numbers.Add(i);
            
            return numbers;
        }

        private static int GetTwoCountWithoutLinq(int n)
        {
            var numbers = GetNumbers(n);

            int two = 0;
            foreach (var item in numbers)
            {
                foreach (var val in item.ToString().ToCharArray())
                {
                    if (val == '2')
                        two++;
                }
            }

            return two;
        }
    }
}