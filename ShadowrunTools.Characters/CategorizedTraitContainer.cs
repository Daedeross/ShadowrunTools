namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Traits;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class CategorizedTraitContainer : ICategorizedTraitContainer
    {
        private Dictionary<string, ITraitContainer> _categories;
        private Dictionary<string, List<ITrait>> _traits;

        public CategorizedTraitContainer()
        {
            _categories = new Dictionary<string, ITraitContainer>();
        }

        public ITraitContainer this[string key]
        {
            get => _categories[key];
            set
            {
                if (_categories.TryGetValue(key, out ITraitContainer container))
                {
                    OnRemoveContainer(container);
                }
                _categories[key] = value;
                OnAddContainer(value);
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
        }

        public void Add(KeyValuePair<string, ITraitContainer> item)
        {
            (_categories as IDictionary<string, ITraitContainer>).Add(item);
            OnAddContainer(item.Value);
        }

        public void Clear()
        {
            foreach (var container in _categories.Values)
            {
                OnRemoveContainer(container);
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
            }
            return _categories.Remove(key);
        }

        public bool Remove(KeyValuePair<string, ITraitContainer> item)
        {
            if (_categories.TryGetValue(item.Key, out ITraitContainer container))
            {
                OnRemoveContainer(container);
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
        }

        private void OnRemoveContainer(ITraitContainer container)
        {
            foreach (var kvp in container)
            {
                OnRemoveTrait(kvp.Key, kvp.Value);
            }
        }

        private void OnContainerChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    if (newItem is KeyValuePair<string, ITrait> kvp)
                    {
                        OnAddTrait(kvp.Key, kvp.Value);
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    if (oldItem is KeyValuePair<string, ITrait> kvp)
                    {
                        OnRemoveTrait(kvp.Key, kvp.Value);
                    }
                }
            }
        } 
        #endregion
    }
}
