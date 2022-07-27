using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Dsl
{
    public class Payload<T>
    {
        public T Value { get; set; }

        public object Extra { get; set; }

        public Payload(T value)
        {
            Value = value;
        }

        public Payload(T value, object extra)
        {
            Value = value;
            Extra = extra;
        }

        public static implicit operator Payload<T>(T v) => new(v);
        public static implicit operator T(Payload<T> p) => p.Value;
    }
}
