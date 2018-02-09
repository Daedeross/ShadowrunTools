namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class TraitContainerBase : ITraitContainer
    {
        private Dictionary<string, ITrait> _traits = new Dictionary<string, ITrait>();

        public ITrait this[string key]
        {
            get => _traits[key];
            set
            {
                if (_traits.TryGetValue(key, out ITrait oldTrait))
                {
                    if (ReferenceEquals(oldTrait, value))
                    {
                        return;
                    }
                    OnRemoveItem(oldTrait);
                }
                _traits[key] = value;
            }
        }

        public bool OwnsObjects { get; set; }

        public ICollection<string> Keys => _traits.Keys;

        public ICollection<ITrait> Values => _traits.Values;

        public int Count => _traits.Count;

        public bool IsReadOnly => false;

        public void Add(string key, ITrait value)
        {
            _traits.Add(key, value);
        }

        public void Add(KeyValuePair<string, ITrait> item)
        {
            _traits.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            foreach (var trait in _traits.Values)
            {
                OnRemoveItem(trait);
            }
            _traits.Clear();
        }

        public bool Contains(KeyValuePair<string, ITrait> item)
        {
            return (_traits as IDictionary<string, ITrait>).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _traits.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, ITrait>[] array, int arrayIndex)
        {
            (_traits as IDictionary<string, ITrait>).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ITrait>> GetEnumerator()
        {
            return _traits.GetEnumerator();
        }

        public bool Remove(string key)
        {
            if (_traits.TryGetValue(key, out ITrait trait))
            {
                OnRemoveItem(trait);
            }
            return _traits.Remove(key);
        }

        public bool Remove(KeyValuePair<string, ITrait> item)
        {
            var collection = _traits as IDictionary<string, ITrait>;

            if (collection.Contains(item))
            {
                collection.Remove(item);
                OnRemoveItem(item.Value);
                return true;
            }
            return false;
        }

        public bool TryGetValue(string key, out ITrait value)
        {
            return _traits.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _traits.GetEnumerator();
        }

        private void OnRemoveItem(ITrait item)
        {
            if (OwnsObjects)
            {
                item.Dispose(); 
            }
        }
    }
}
