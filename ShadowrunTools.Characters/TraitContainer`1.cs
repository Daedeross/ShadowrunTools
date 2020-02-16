using ShadowrunTools.Characters.Traits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class TraitContainer<T> : TraitContainer, ITraitContainer<T>
        where T : class, ITrait
    {
        public TraitContainer() : base() { }

        public TraitContainer(string name) : base (name) { }

        public TraitContainer(string name, Dictionary<string, T> initial) 
            : base(name, initial.ToDictionary(x => x.Key, x => x.Value as ITrait))
        {
        }

        T IDictionary<string, T>.this[string key]
        {
            get => base[key] as T;
            set => base[key] = value;
        }

        ICollection<T> IDictionary<string, T>.Values => throw new NotImplementedException();

        public void Add(string key, T value)
        {
            base.Add(key, value);
        }

        public void Add(KeyValuePair<string, T> item)
        {
            base.Add(new KeyValuePair<string, ITrait>(item.Key, item.Value));
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return base.Contains(new KeyValuePair<string, ITrait>(item.Key, item.Value));
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            var kvpArray = array.Select(item => new KeyValuePair<string, ITrait>(item.Key, item.Value)).ToArray();
            base.CopyTo(kvpArray, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            return base.Remove(new KeyValuePair<string, ITrait>(item.Key, item.Value));
        }

        public bool TryGetValue(string key, out T value)
        {
            var result = base.TryGetValue(key, out ITrait trait);
            value = trait as T;
            return result;
        }

        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
        {
            return new GenericEnumerator<T>(base.GetEnumerator());
        }

        private class GenericEnumerator<TValue> : IEnumerator<KeyValuePair<string, TValue>>
            where TValue : class, ITrait
        {
            private readonly IEnumerator<KeyValuePair<string, ITrait>> _enumerator;

            public KeyValuePair<string, TValue> Current { get; private set; }

            object IEnumerator.Current => Current;

            public GenericEnumerator(IEnumerator<KeyValuePair<string, ITrait>> enumerator)
            {
                _enumerator = enumerator;
            }

            public void Dispose()
            {
                _enumerator?.Dispose();
            }

            public bool MoveNext()
            {
                if (_enumerator.MoveNext())
                {
                    Current = new KeyValuePair<string, TValue>(_enumerator.Current.Key, _enumerator.Current.Value as TValue);
                    return true;
                }
                else
                {
                    Current = default;
                    return false;
                }
            }

            public void Reset()
            {
                _enumerator?.Reset();
                Current = default;
            }
        }
    }
}
