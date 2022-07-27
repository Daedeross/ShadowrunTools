using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunTools.Foundation
{
    public static class FNV1aHash
    {
        private const int FNVBasis32bit = unchecked((int)0x811C9DC5);
        private const long FNVBasis64bit = unchecked((long)0xcbf29ce484222325);

        private const int FNVPrime32bit = unchecked((int)0x01000193);
        private const long FNVPrime64bit = unchecked((long)0x00000100000001B3);

        public static int CalculateHash32(params object[] values) => CalculateHash32(values.AsEnumerable());

        public static int CalculateHash32(IEnumerable<object> values)
        {
            int hash = FNVBasis32bit;

            foreach (var value in values)
            {
                hash ^= value?.GetHashCode() ?? 0;
                hash *= FNVPrime32bit;
            }

            return hash;
        }

        public static int AppendHash32(int hash, params object[] values) => AppendHash32(hash, values.AsEnumerable());
        public static int AppendHash32(int hash, IEnumerable<object> values)
        {
            foreach (var value in values)
            {
                hash ^= value?.GetHashCode() ?? 0;
                hash *= FNVPrime32bit;
            }

            return hash;
        }
    }
}
