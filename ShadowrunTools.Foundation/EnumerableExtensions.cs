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
    }
}
