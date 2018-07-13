using ShadowrunTools.Characters.Traits;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters
{
    public class TraitContainer : ITraitContainer
    {
        protected Dictionary<string, ITrait> _traits = new Dictionary<string, ITrait>();

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
                    _traits[key] = value;
                    RaiseReplace(new KeyValuePair<string, ITrait>(key, oldTrait), new KeyValuePair<string, ITrait>(key, value));
                }
                else
                {
                    _traits[key] = value;
                    RaiseAddedItem(new KeyValuePair<string, ITrait>(key, value));
                }
            }
        }

        public virtual bool OwnsObjects { get; set; }

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
                _traits.Remove(key);
                RaiseRemove(new KeyValuePair<string, ITrait>(key, trait));
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<string, ITrait> item)
        {
            var collection = _traits as IDictionary<string, ITrait>;

            if (collection.Contains(item))
            {
                collection.Remove(item);
                OnRemoveItem(item.Value);
                RaiseRemove(item);
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

        protected void OnRemoveItem(ITrait item)
        {
            if (OwnsObjects)
            {
                item.Dispose(); 
            }
        }

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void RaiseAddedItem(KeyValuePair<string, ITrait> newItem)
        {
            RaiseAddedItems(new List<KeyValuePair<string, ITrait>> { newItem });
        }

        protected void RaiseAddedItems(List<KeyValuePair<string, ITrait>> newItems)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems));
        }

        protected void RaiseReplace(KeyValuePair<string, ITrait> oldItem, KeyValuePair<string, ITrait> newItem)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, newItem));
        }

        protected void RaiseRemove(KeyValuePair<string, ITrait> item)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        #endregion // INotifyCollectionChanged

        public TraitContainer() : base() { }

        public TraitContainer(Dictionary<string, ITrait> initial)
        {
            _traits = initial;
        }
    }
}
