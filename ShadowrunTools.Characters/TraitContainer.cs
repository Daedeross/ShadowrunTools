using ShadowrunTools.Characters.Traits;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters
{
    public class TraitContainer : ITraitContainer
    {
        protected IDictionary<ITrait, HashSet<string>> _refKeyMap = new Dictionary<ITrait, HashSet<string>>();
        protected IDictionary<string, ITrait> _traits = new Dictionary<string, ITrait>();

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
                    OnBeforeReplace(new KeyValuePair<string, ITrait>(key, oldTrait), new KeyValuePair<string, ITrait>(key, value));
                    _traits[key] = value;
                    OnAfterReplace(new KeyValuePair<string, ITrait>(key, oldTrait), new KeyValuePair<string, ITrait>(key, value));
                }
                else
                {
                    OnBeforeAdd(new KeyValuePair<string, ITrait>(key, value));
                    _traits[key] = value;
                    OnAfterAdd(new KeyValuePair<string, ITrait>(key, value));
                }
            }
        }

        public string Name { get; private set; }

        public virtual bool OwnsObjects { get; set; }

        public virtual bool Valid { get; }

        public virtual string Summary { get; }

        public ICollection<string> Keys => _traits.Keys;

        public ICollection<ITrait> Values => _traits.Values;

        public int Count => _traits.Count;

        public bool IsReadOnly => false;

        public void Add(string key, ITrait value)
        {
            var item = new KeyValuePair<string, ITrait>(key, value);
            OnBeforeAdd(item);
            _traits.Add(key, value);
            OnAfterAdd(item);
        }

        public void Add(KeyValuePair<string, ITrait> item)
        {
            OnBeforeAdd(item);
            _traits.Add(item.Key, item.Value);
            OnAfterAdd(item);
        }

        public void Clear()
        {
            foreach (var kvp in _traits)
            {
                OnBeforeRemove(kvp);
                OnAfterRemove(kvp);
            }
            _traits.Clear();
        }

        public bool Contains(KeyValuePair<string, ITrait> item)
        {
            return _traits.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _traits.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, ITrait>[] array, int arrayIndex)
        {
            _traits.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ITrait>> GetEnumerator()
        {
            return _traits.GetEnumerator();
        }

        public bool Remove(string key)
        {
            if (_traits.TryGetValue(key, out ITrait trait))
            {
                OnRemoveValue(trait);
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
                OnBeforeRemove(item);
                collection.Remove(item);
                OnAfterRemove(item);
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

        protected void OnRemoveValue(ITrait item)
        {
            if (OwnsObjects && !_refKeyMap.ContainsKey(item))
            {
                item.Dispose();
            }
        }

        protected void AddToReverseMap(KeyValuePair<string, ITrait> item)
        {
            if (!_refKeyMap.TryGetValue(item.Value, out var set))
            {
                set = new HashSet<string>();
                _refKeyMap[item.Value] = set;
            }
            set.Add(item.Key);
        }

        protected void RemoveFromReverseMap(KeyValuePair<string, ITrait> item)
        {
            var oldSet = _refKeyMap[item.Value];
            oldSet.Remove(item.Key);
            if (oldSet.Count == 0)
            {
                _refKeyMap.Remove(item.Value);
            }
        }

        #region INotifyCollectionChanged

        protected virtual bool OnBeforeAdd(KeyValuePair<string, ITrait> item) => true;

        protected virtual void OnAfterAdd(KeyValuePair<string, ITrait> item)
        {
            AddToReverseMap(item);
            RaiseAddedItems(new List<KeyValuePair<string, ITrait>> { item });
        }

        protected virtual bool OnBeforeReplace(KeyValuePair<string, ITrait> oldItem, KeyValuePair<string, ITrait> newItem)
            => true;

        protected virtual void OnAfterReplace(KeyValuePair<string, ITrait> oldItem, KeyValuePair<string, ITrait> newItem)
        {
            AddToReverseMap(newItem);
            RemoveFromReverseMap(oldItem);
            OnRemoveValue(oldItem.Value);
            RaiseReplace(oldItem, newItem);
        }

        protected virtual bool OnBeforeRemove(KeyValuePair<string, ITrait> item)
            => true;

        protected virtual void OnAfterRemove(KeyValuePair<string, ITrait> item)
        {
            RemoveFromReverseMap(item);
            OnRemoveValue(item.Value);
            RaiseRemove(item);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

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

        public TraitContainer() { }

        public TraitContainer(string name)
        {
            Name = name;
        }

        public TraitContainer(string name, Dictionary<string, ITrait> initial)
        {
            Name = name;
            _traits = initial;
        }
    }
}
