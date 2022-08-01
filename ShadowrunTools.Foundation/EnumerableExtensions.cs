using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Foundation
{
    public static class EnumerableExtensions
    {
        public static Dictionary<K, V> Copy<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
        {
            return source.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value);
        }

        public static V GetOrAdd<K, V>(this IDictionary<K, V> dictionary, K key, V addValue)
        {
            if (dictionary.TryGetValue(key, out V value))
            {
                return value;
            }
            else
            {
                dictionary[key] = addValue;
                return addValue;
            }
        }

        public static V GetOrAdd<K, V>(this IDictionary<K, V> dictionary, K key, Func<V> addValueFactory)
        {
            if (dictionary.TryGetValue(key, out V value))
            {
                return value;
            }
            else
            {
                var addValue = addValueFactory();
                dictionary[key] = addValue;
                return addValue;
            }
        }

        public static V AddOrUpdate<K, V>(this IDictionary<K, V> dictionary, K key, V addValue, V updateValue)
        {
            if (dictionary.TryGetValue(key, out V oldValue))
            {
                dictionary[key] = updateValue;
                return updateValue;
            }
            else
            {
                dictionary[key] = addValue;
                return addValue;
            }
        }

        public static V AddOrUpdate<K, V>(this IDictionary<K, V> dictionary, K key, Func<K, V> addValueFactory, Func<K, V, V> updateValueFactory)
        {
            if (dictionary.TryGetValue(key, out V oldValue))
            {
                var updateValue = updateValueFactory(key, oldValue);
                dictionary[key] = updateValue;
                return updateValue;
            }
            else
            {
                var addValue = addValueFactory(key);
                dictionary[key] = addValue;
                return addValue;
            }
        }
    }
}
