namespace ShadowrunTools.Characters.Validators
{
    using ShadowrunTools.Characters.Traits;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public class TraitCollectionAggregateValidator : ItemChangedBase, IValidatorItem, IDisposable
    {
        private readonly INotifyItemChanged _watch;
        private readonly ITraitContainer _collection;
        private readonly ISet<string> _propertyNames;
        private readonly ISet<string> _watchPropertyNames;
        private readonly Func<ITraitContainer, bool> _validation;

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

        public static TraitCollectionAggregateValidator<T> Create<T, TAggregate>(
            ITraitContainer<T> collection,
            string label,
            string message,
            IEnumerable<string> propertyNames,
            Func<TAggregate, T, TAggregate> aggregate,
            Func<TAggregate, bool> validation,
            TAggregate seed,
            INotifyItemChanged additionalWatch = null,
            params string[] additionalPropertyNames)
            where T : class, ITrait
        {
            Func<ITraitContainer<T>, bool> func = c => validation(c.Values.Aggregate(seed, aggregate));
            return new TraitCollectionAggregateValidator<T>(
                collection,
                label,
                message,
                func,
                propertyNames,
                additionalWatch,
                additionalPropertyNames);
        }

        public static TraitCollectionAggregateValidator Create<TAggregate>(
            ITraitContainer collection,
            string label,
            string message,
            IEnumerable<string> propertyNames,
            Func<TAggregate, ITrait, TAggregate> aggregate,
            Func<TAggregate, bool> validation,
            TAggregate seed,
            INotifyItemChanged additionalWatch = null,
            params string[] additionalPropertyNames)
        {
            Func<ITraitContainer, bool> func = c => validation(c.Values.Aggregate(seed, aggregate));
            return new TraitCollectionAggregateValidator(
                collection,
                label,
                message,
                func,
                propertyNames,
                additionalWatch,
                additionalPropertyNames);
        }

        public TraitCollectionAggregateValidator(
            ITraitContainer collection,
            string label,
            string message,
            Func<ITraitContainer, bool> validation,
            IEnumerable<string> propertyNames,
            INotifyItemChanged additionalWatch = null,
            params string[] additionalPropertyNames)
        {
            _collection = collection;
            mLabel = label;
            mMessage = message;
            _validation = validation;
            _propertyNames = new HashSet<string>(propertyNames);

            _collection.CollectionChanged += OnCollectionChanged;
            foreach (var kvp in collection)
            {
                if (kvp.Value is INotifyItemChanged notify)
                {
                    notify.ItemChanged += OnTraitChanged;
                }
            }

            _watch = additionalWatch;
            if (_watch != null)
            {
                _watchPropertyNames = new HashSet<string>(additionalPropertyNames);
                _watch.ItemChanged += OnWatchChanged;
            }

            IsValid = _validation(_collection);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newTraits = new List<INotifyItemChanged>(e.NewItems.Count);
            foreach (var item in e.NewItems)
            {
                if (item is KeyValuePair<string, ITrait> kvp && kvp.Value is INotifyItemChanged trait)
                {
                    newTraits.Add(trait);
                }
            }

            var oldTraits = new List<INotifyItemChanged>(e.OldItems.Count);
            foreach (var item in e.OldItems)
            {
                if (item is KeyValuePair<string, ITrait> kvp && kvp.Value is INotifyItemChanged trait)
                {
                    oldTraits.Add(trait);
                }
            }

            foreach (var trait in oldTraits)
            {
                trait.ItemChanged -= OnTraitChanged;
            }
            foreach (var trait in newTraits)
            {
                trait.ItemChanged += OnTraitChanged;
            }

            IsValid = _validation(_collection);
        }

        private void OnTraitChanged(object sender, ItemChangedEventArgs e)
        {
            if (!(sender is ITrait trait))
            {
                return;
            }

            if (_propertyNames.Overlaps(e.PropertyNames))
            {
                IsValid = _validation(_collection);
            };
        }

        private void OnWatchChanged(object sender, ItemChangedEventArgs e)
        {
            if (_watchPropertyNames.Overlaps(e.PropertyNames))
            {
                IsValid = _validation(_collection);
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
                    if (_watch != null)
                    {
                        _watch.ItemChanged -= OnWatchChanged;
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
