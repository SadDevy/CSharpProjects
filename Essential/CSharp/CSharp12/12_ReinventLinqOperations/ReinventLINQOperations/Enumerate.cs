using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LINQOperations
{
    public static class Enumerate
    {
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            for (int i = 0; i < count; i++)
                yield return element;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, 
                                                                IEnumerable<TSource> second)
        {
            CheckNull(first, nameof(first));
            CheckNull(second, nameof(second));

            foreach (TSource value in first)
                yield return value;

            foreach (TSource value in second)
                yield return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckNull<TSourse>(TSourse value, string name)
           where TSourse : class
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, 
                                                                 TSource element)
        {
            CheckNull(source, nameof(source));

            yield return element;
            foreach (TSource value in source)
                yield return value;
        }

        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            CheckNull(source, nameof(source));

            foreach (TSource value in source)
            {
                yield return value;

                if (--count == 0)
                    yield break;
            }
        }

        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, 
                                                                   Func<TSource, int, bool> predicate)
        {
            CheckNull(source, nameof(source));
            CheckNull(predicate, nameof(predicate));

            int index = -1;
            foreach (TSource value in source)
            {
                if (predicate(value, ++index))
                    continue;

                yield return value;
            }
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source,
                                                           Func<TSource, bool> predicate) //!!!
        {
            CheckNull(source, nameof(source));
            CheckNull(predicate, nameof(predicate));

            foreach (TSource value in source)
                if (predicate(value))
                    return value;

            return default;
        }

        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            CheckNull(source, nameof(source));

            foreach (object element in source)
                if (element is TResult)
                    yield return (TResult)element;
        }

        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            CheckNull(source, nameof(source));

            IEnumerable<TResult> typedSource = source as IEnumerable<TResult>;
            if (typedSource != null)
                return typedSource;

            return CastIterator<TResult>(source);
        }

        private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
        {
            foreach (object element in source)
                yield return (TResult)element;
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            CheckNull(source, nameof(source));

            return new List<TSource>(source);
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, 
                                                                                            Func<TSource, TKey> keySelector, 
                                                                                            Func<TSource, TElement> elementSelector, 
                                                                                            IEqualityComparer<TKey> comparer)
        {
            CheckNull(source, nameof(source));
            CheckNull(keySelector, nameof(keySelector));
            CheckNull(elementSelector, nameof(elementSelector));;
            CheckNull(comparer, nameof(comparer));

            Dictionary<TKey, TElement> result = new Dictionary<TKey, TElement>(comparer);
            foreach (TSource value in source)
                result.Add(keySelector(value), elementSelector(value));

            return result;
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            CheckNull(source, nameof(source));
            CheckNull(predicate, nameof(predicate));

            int index = -1;
            foreach (TSource value in source)
                if (predicate(value, ++index))
                    yield return value;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source, nameof(source));
            CheckNull(predicate, nameof(predicate));

            foreach (TSource value in source)
                if (predicate(value))
                    return true;

            return false;
        }

        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source, nameof(source));
            CheckNull(predicate, nameof(predicate));

            foreach (TSource value in source)
                if (!predicate(value))
                    return false;

            return true;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            CheckNull(source, nameof(source));

            ICollection<TSource> collection = source as ICollection<TSource>;
            return collection.Contains(value);
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source,
                                                  TSource value,
                                                  IEqualityComparer<TSource> comparer)
        {
            CheckNull(source, nameof(source));
            CheckNull(comparer, nameof(comparer));

            foreach (TSource element in source)
                if (comparer.Equals(value, element))
                    return true;

            return false;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, 
                                                                         Func<TSource, TResult> selector)
        {
            CheckNull(source, nameof(source));
            CheckNull(selector, nameof(selector));

            foreach (TSource value in source)
                yield return selector(value);
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, 
                                                                         Func<TSource, int, TResult> selector)
        {
            CheckNull(source, nameof(source));
            CheckNull(selector, nameof(selector));

            int index = -1;
            foreach (TSource value in source)
                yield return selector(value, ++index);
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, 
                                                                             Func<TSource, 
                                                                             IEnumerable<TResult>> selector)
        {
            CheckNull(source, nameof(source));
            CheckNull(selector, nameof(selector));

            foreach (TSource value in source)
                foreach (TResult element in selector(value))
                    yield return element;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, 
                                                                                          Func<TSource, IEnumerable<TCollection>> collectionSelector, 
                                                                                          Func<TSource, TCollection, TResult> resultSelector)
        {
            CheckNull(source, nameof(source));
            CheckNull(collectionSelector, nameof(collectionSelector));
            CheckNull(resultSelector, nameof(resultSelector));

            foreach (TSource value in source)
                foreach (TCollection collection in collectionSelector(value))
                    yield return resultSelector(value, collection);
        }

        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            ICollection collection = source as ICollection;
            if (collection != null)
                return collection.Count;

            int count = 0;
            foreach (TSource value in source)
                count++;

            return count;
        }

        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(predicate, nameof(predicate));

            int result = 0;
            foreach (TSource value in source)
                if (predicate(value))
                    result++;

            return result;
        }

        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            CheckNull(source, nameof(source));
            CheckNull(func, nameof(func));

            TSource result;
            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                    throw new InvalidOperationException("Пустая последовательность.");

                result = e.Current;
                while (e.MoveNext())
                    result = func(result, e.Current);
            }

            return result;
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, 
                                                                            TAccumulate seed, 
                                                                            Func<TAccumulate, TSource, TAccumulate> func, 
                                                                            Func<TAccumulate, TResult> resultSelector)
        {
            CheckNull(source, nameof(source));
            CheckNull(func, nameof(func));
            CheckNull(resultSelector, nameof(resultSelector));

            TAccumulate result = seed;
            foreach (TSource value in source)
                result = func(result, value);

            return resultSelector(result);
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            CheckNull(source, nameof(source));

            List<TSource> result = new List<TSource>();
            foreach (TSource value in source)
                if (!result.Contains(value))
                    result.Add(value);

            return result;
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, 
                                                                   IEnumerable<TSource> second)
        {
            CheckNull(first, nameof(first));
            CheckNull(second, nameof(second));

            HashSet<TSource> set = new HashSet<TSource>();
            foreach (TSource value in first)
                set.Add(value);

            foreach (TSource value in second)
                if (set.Remove(value))
                    yield return value;
        }

        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            CheckNull(first, nameof(first));
            CheckNull(second, nameof(second));

            EqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            using (IEnumerator<TSource> a = first.GetEnumerator())
            using (IEnumerator<TSource> b = second.GetEnumerator())
            {
                while (a.MoveNext())
                {
                    if (!b.MoveNext() || !comparer.Equals(a.Current, b.Current))
                        return false;
                }

                if (b.MoveNext())
                    return false;
            }

            return true;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, 
                                                                              IEnumerable<TSecond> second, 
                                                                              Func<TFirst, TSecond, TResult> resultSelector)
        {
            CheckNull(second, nameof(second));

            using (IEnumerator<TFirst> a = first.GetEnumerator())
            using (IEnumerator<TSecond> b = second.GetEnumerator())
            {
                while (a.MoveNext() && b.MoveNext())
                    yield return resultSelector(a.Current, b.Current);
            }
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, 
                                                                                          Func<TSource, TKey> keySelector, 
                                                                                          Func<TSource, TElement> elementSelector, 
                                                                                          Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            CheckNull(keySelector, nameof(keySelector));
            CheckNull(elementSelector, nameof(elementSelector));
            CheckNull(resultSelector, nameof(resultSelector));

            foreach (var value in source.GroupBy(keySelector, elementSelector))
                yield return resultSelector(value.Key, value.Value);
        }

        private static Dictionary<TKey, List<TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                              Func<TSource, TKey> keySelector,
                                                                                              Func<TSource, TElement> elementSelector)
        {
            Dictionary<TKey, List<TElement>> result = new Dictionary<TKey, List<TElement>>();
            foreach (TSource value in source)
            {
                if (!result.ContainsKey(keySelector(value)))
                    result.Add(keySelector(value), new List<TElement>());

                result.TryGetValue(keySelector(value), out List<TElement> e);
                e.Add(elementSelector(value));
            }

            return result;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            CheckNull(inner, nameof(inner));
            CheckNull(outerKeySelector, nameof(outerKeySelector));
            CheckNull(innerKeySelector, nameof(innerKeySelector));
            CheckNull(resultSelector, nameof(resultSelector));

            EqualityComparer<TKey> comparer = EqualityComparer<TKey>.Default;
            foreach (TOuter outerValue in outer)
            {
                foreach (TInner innerValue in inner)
                {
                    if (comparer.Equals(outerKeySelector(outerValue), innerKeySelector(innerValue)))
                        yield return resultSelector(outerValue, innerValue);
                }
            }
        }
    }
}
