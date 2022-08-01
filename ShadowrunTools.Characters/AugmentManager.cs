using DynamicData.Binding;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters
{
    public class AugmentManager<T> : IAugmentContainer<T>
        where T : class, ITrait
    {
        private readonly Dictionary<string, IAugment> _augments = new Dictionary<string, IAugment>();
        private readonly Dictionary<string, IAugment> _tags = new Dictionary<string, IAugment>();
        private readonly IAugmentFactory<T> _factory;
        private readonly IScope<T> _scope;
        private bool disposedValue;

        public AugmentManager(IAugmentFactory<T> factory, IScope<T> scope)
        {
            _factory = factory;
            _scope = scope;

            Augments = new ObservableCollectionExtended<string>();
            Augments.CollectionChanged += OnAugmentsCollectionChanged;
        }

        public IEnumerable<IAugment> ParsedAugments => _augments.Values;

        public IObservableCollection<string> Augments { get; }


        public IEnumerable<(string script, string error)> Errors;


        private void OnAddAugment(string script)
        {
            var augment = _factory.Create(_scope, script);
            if (augment != null)
            {
                _augments.Add(script, augment);
            }
        }

        private void OnRemoveAugment(string script)
        {
            if (_augments.TryGetValue(script, out var augment))
            {
                augment.Dispose();
                _augments.Remove(script);
            }
        }

        public void OnAugmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (string script in e.OldItems)
                {
                    OnRemoveAugment(script);
                }
            }
            if (e.NewItems != null)
            {
                foreach (string script in e.NewItems)
                {
                    OnAddAugment(script);
                }
            }
        }
        protected void ClearAugments()
        {
            foreach (var augment in _augments.Values)
            {
                augment.Dispose();
            }
            _augments.Clear();
            Augments.Clear();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Augments.CollectionChanged -= OnAugmentsCollectionChanged;
                    ClearAugments();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
