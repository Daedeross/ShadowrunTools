using ShadowrunTools.Characters.Traits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class CategorizedTraitContainer : ICategorizedTraitContainer
    {
        private Dictionary<string, ITraitContainer> _categories;
        private Dictionary<string, List<ITrait>> _traits;

        public CategorizedTraitContainer()
        {
            _categories = new Dictionary<string, ITraitContainer>();
            _traits = new Dictionary<string, List<ITrait>>();
        }

        public ITraitContainer this[string key]
        {
            get => _categories[key];
            set
            {
                bool replace = false;
                if (_categories.TryGetValue(key, out ITraitContainer container))
                {
                    OnRemoveContainer(container);
                    replace = true;
                }
                _categories[key] = value;
                OnAddContainer(value);

                if (replace)
                {
                    RaiseReplace(new KeyValuePair<string, ITraitContainer>(key, container), new KeyValuePair<string, ITraitContainer>(key, value));
                }
                else
                {
                    RaiseAddedItems(new List<KeyValuePair<string, ITraitContainer>> { new KeyValuePair<string, ITraitContainer>(key, value) });
                }
            }
        }

        public ICollection<string> Keys => _categories.Keys;

        public ICollection<ITraitContainer> Values => _categories.Values;

        public int Count => _categories.Count;

        public bool IsReadOnly => false;

        public void Add(string key, ITraitContainer value)
        {
            _categories.Add(key, value);
            OnAddContainer(value);
            RaiseAddedItems(new List<KeyValuePair<string, ITraitContainer>> { new KeyValuePair<string, ITraitContainer>(key, value) });
        }

        public void Add(KeyValuePair<string, ITraitContainer> item)
        {
            (_categories as IDictionary<string, ITraitContainer>).Add(item);
            OnAddContainer(item.Value);
            RaiseAddedItems(new List<KeyValuePair<string, ITraitContainer>> { item });
        }

        public void Clear()
        {
            foreach (var kvp in _categories)
            {
                OnRemoveContainer(kvp.Value);
                RaiseRemove(kvp);
            }
            _categories.Clear();
        }

        public bool Contains(KeyValuePair<string, ITraitContainer> item)
        {
            return (_categories as IDictionary<string, ITraitContainer>).Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _categories.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, ITraitContainer>[] array, int arrayIndex)
        {
            (_categories as IDictionary<string, ITraitContainer>).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, ITraitContainer>> GetEnumerator()
        {
            return _categories.GetEnumerator();
        }

        public IEnumerable<ITrait> GetTraitsByName(string name)
        {
            if (!_traits.TryGetValue(name, out List<ITrait> list))
            {
                list = new List<ITrait>();
            }
            return list;
        }

        public bool Remove(string key)
        {
            if (_categories.TryGetValue(key, out ITraitContainer container))
            {
                OnRemoveContainer(container);
                RaiseRemove(new KeyValuePair<string, ITraitContainer>(key, container));
            }
            return _categories.Remove(key);
        }

        public bool Remove(KeyValuePair<string, ITraitContainer> item)
        {
            if (_categories.TryGetValue(item.Key, out ITraitContainer container))
            {
                OnRemoveContainer(container);
                RaiseRemove(item);
            }
            return _categories.Remove(item.Key);
        }

        public bool TryGetValue(string key, out ITraitContainer value)
        {
            return _categories.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetTrait(string category, string name, out ITrait trait)
        {
            trait = default;

            return _categories.TryGetValue(category, out var container)
                && container.TryGetValue(name, out trait);
        }

        #region Watches

        private void OnAddTrait(string name, ITrait trait)
        {
            if (!_traits.TryGetValue(name, out List<ITrait> list))
            {
                list = new List<ITrait>();
                _traits[name] = list;
            }
            list.Add(trait);
        }

        private void OnRemoveTrait(string name, ITrait trait)
        {
            if (_traits.TryGetValue(name, out List<ITrait> list))
            {
                list.Remove(trait);
            }
            if (list == null || list.Count == 0)
            {
                _traits.Remove(name);
            }
        }

        private void OnAddContainer(ITraitContainer container)
        {
            foreach (var kvp in container)
            {
                OnAddTrait(kvp.Key, kvp.Value);
            }
            container.CollectionChanged += OnContainerChanged;
            NotifyTraitsAdded(container.Select(kvp => (container.Name, kvp.Key, kvp.Value)).ToList());
        }

        private void OnRemoveContainer(ITraitContainer container)
        {
            foreach (var kvp in container)
            {
                OnRemoveTrait(kvp.Key, kvp.Value);
            }
            container.CollectionChanged -= OnContainerChanged;
            NotifyTraitsRemoved(container.Select(kvp => (container.Name, kvp.Key, kvp.Value)).ToList());
        }

        private void OnContainerChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var container = sender as ITraitContainer;
            if (container is null)
            {
                throw new InvalidOperationException("Non ITraitContainer subscribed to!?");
            }

            if (e.NewItems != null)
            {
                List<(string, string, ITrait)> added = new();
                foreach (var newItem in e.NewItems)
                {
                    if (newItem is KeyValuePair<string, ITrait> kvp)
                    {
                        OnAddTrait(kvp.Key, kvp.Value);
                        added.Add((container.Name, kvp.Key, kvp.Value));
                    }
                }
                NotifyTraitsAdded(added);
            }
            if (e.OldItems != null)
            {
                List<(string, string, ITrait)> removed = new();
                foreach (var oldItem in e.OldItems)
                {
                    if (oldItem is KeyValuePair<string, ITrait> kvp)
                    {
                        OnRemoveTrait(kvp.Key, kvp.Value);
                        removed.Add((container.Name, kvp.Key, kvp.Value));
                    }
                }
                NotifyTraitsRemoved(removed);
            }
        }

        #endregion

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void RaiseAddedItems(IList<KeyValuePair<string, ITraitContainer>> newItems)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems));
        }

        protected void RaiseReplace(KeyValuePair<string, ITraitContainer> oldItem, KeyValuePair<string, ITraitContainer> newItem)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItem, newItem));
        }

        protected void RaiseRemove(KeyValuePair<string, ITraitContainer> item)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }
        #endregion

        public event EventHandler<TraitsChangedEventArgs> TraitsChanged;

        protected void NotifyTraitsAdded(IList<(string category, string name, ITrait trait)> newTraits)
        {
            TraitsChanged?.Invoke(this, new TraitsChangedEventArgs(NotifyCollectionChangedAction.Add, newTraits));
        }

        protected void NotifyTraitReplaced(string category, string name, ITrait newTrait, ITrait oldTrait)
        {
            TraitsChanged?.Invoke(this, new TraitsChangedEventArgs(NotifyCollectionChangedAction.Replace, (category, name, newTrait), (category, name, oldTrait)));
        }

        protected void NotifyTraitRemoved(string category, string name, ITrait trait)
        {
            TraitsChanged?.Invoke(this, new TraitsChangedEventArgs(NotifyCollectionChangedAction.Remove, (category, name, trait)));
        }

        protected void NotifyTraitsRemoved(IList<(string category, string name, ITrait trait)> removedTraits)
        {
            TraitsChanged?.Invoke(this, new TraitsChangedEventArgs(NotifyCollectionChangedAction.Remove, removedTraits));
        }
    }
}
