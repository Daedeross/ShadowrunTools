namespace ShadowrunTools.Characters.Validators
{
    using ShadowrunTools.Characters.Traits;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class TraitCollectionValidator : ItemChangedBase, IValidatorItem, IDisposable
    {
        private readonly ITraitContainer _collection;
        private readonly ISet<string> _propertyNames;
        private readonly Func<ITrait, bool> _validation;

        private IDictionary<string, bool> _isValidMap;

        private bool mIsValid;
        public bool IsValid
        {
            get => mIsValid;
            set
            {
                if (RaiseAndSetIfChanged(ref mIsValid, value))
                {
                    RaiseItemChanged(nameof(Message));
                }
            }
        }

        private string mLabel;
        public string Label
        {
            get => mLabel;
            set => RaiseAndSetIfChanged(ref mLabel, value);
        }

        private string mValue;
        public string Value
        {
            get => mValue;
            set => RaiseAndSetIfChanged(ref mValue, value);
        }

        private string mMessage;
        public string Message
        {
            get => IsValid ? null : mMessage;
        }

        public TraitCollectionValidator(ITraitContainer collection,
                             string label,
                             string message,
                             Func<ITrait, bool> validation,
                             IEnumerable<string> propertyNames)
        {
            mLabel = label;
            mMessage = message;
            _validation = validation;
            _propertyNames = new HashSet<string>(propertyNames);

            _collection.CollectionChanged += OnCollectionChanged;
            foreach (var kvp in collection)
            {
                var notify = kvp.Value as INotifyItemChanged;
                if (notify != null)
                {
                    notify.ItemChanged += OnTraitChanged;
                }
            }

            Recalc();
        }

        private void Recalc()
        {
            _isValidMap = _collection
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => _validation(kvp.Value));

            IsValid = _isValidMap.Values.All(x => x);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newTraits = new Dictionary<string, INotifyItemChanged>(e.NewItems.Count);
            foreach (var item in e.NewItems)
            {
                if (item is KeyValuePair<string, ITrait> kvp && kvp.Value is INotifyItemChanged trait)
                {
                    newTraits[kvp.Key] = trait;
                }
            }

            var oldTraits = new Dictionary<string, INotifyItemChanged>(e.OldItems.Count);
            foreach (var item in e.OldItems)
            {
                if (item is KeyValuePair<string, ITrait> kvp && kvp.Value is INotifyItemChanged trait)
                {
                    oldTraits[kvp.Key] = trait;
                }
            }

            foreach (var kvp in oldTraits)
            {
                _isValidMap.Remove(kvp.Key);
                kvp.Value.ItemChanged -= OnTraitChanged;
            }
            foreach (var kvp in newTraits)
            {
                _isValidMap[kvp.Key] = _validation(kvp.Value as ITrait);
                kvp.Value.ItemChanged += OnTraitChanged;
            }

            IsValid = _isValidMap.Values.All(x => x);
        }

        private void OnTraitChanged(object sender, ItemChangedEventArgs e)
        {
            if (!(sender is ITrait trait))
            {
                return;
            }

            if (_propertyNames.Overlaps(e.PropertyNames))
            {
                bool? oldValue = null;
                if (_isValidMap.TryGetValue(trait.Name, out bool isValid))
                {
                    oldValue = isValid;
                }

                var newValue = _validation(trait);
                _isValidMap[trait.Name] = newValue;
                if (oldValue != newValue)
                {
                    IsValid = _isValidMap.Values.All(x => x);
                }
            };
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_collection != null)
                    {
                        _collection.CollectionChanged -= OnCollectionChanged;
                        foreach (var item in _collection.Values)
                        {
                            if (item is INotifyItemChanged notify)
                            {
                                notify.ItemChanged -= OnTraitChanged;
                            }
                        }
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
