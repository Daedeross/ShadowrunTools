using ReactiveUI;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunTools.Characters
{
    public class Augment<T> : ReactiveObject, IAugment
        where T : class, INamedItem
    {
        private readonly IScope<T> _scope;
        private readonly Func<double> _delegate;

        private readonly Dictionary<(string category, string name), (ITrait trait, string property)> _watchedTraits;
        private readonly Dictionary<ITrait, List<string>> _watchedProperties;

        private readonly Dictionary<(string category, string name), (IAugmentable trait, string property)> _targetedTraits;
        private readonly Dictionary<IAugmentable, List<string>> _targetedProperties;

        private readonly Dictionary<IAugmentable, List<IBonus>> _bonuses;

        public Augment(IScope<T> scope, IEnumerable<PropertyReference> targets, IEnumerable<PropertyReference> watches, Func<double> bonus)
        {
            _scope = scope;
            _delegate = bonus;
            _amount = bonus();

            _scope.Traits.TraitsChanged += OnTraitsCollectionChanged;

            var _watches = watches.ToList();
            _watchedTraits = _watches
                .Select(pr =>
                {
                    _scope.Traits.TryGetTrait(pr.Category, pr.Name, out var trait);

                    return new { pr.Category, pr.Name, Trait = trait, pr.Property };
                })
                .ToDictionary(a => (a.Category, a.Name), a => (a.Trait, a.Property));

            var watchedProperties = _watchedTraits
                .Where(kvp => kvp.Value.trait != null)
                .Select(kvp => new { kvp.Value.trait, kvp.Value.property });

            _watchedProperties = new Dictionary<ITrait, List<string>>();
            foreach (var tuple in watchedProperties)
            {
                AddPropertyToWatched(tuple.trait, tuple.property);
            }

            var _targets = targets.ToList();
            _targetedTraits = _targets
                .Select(pr =>
                {
                    _scope.Traits.TryGetTrait(pr.Category, pr.Name, out var trait);

                    return new { pr.Category, pr.Name, Trait = trait as IAugmentable, pr.Property };
                })
                .ToDictionary(a => (a.Category, a.Name), a => (a.Trait, a.Property));

            _targetedProperties = _targetedTraits
                .Where(kvp => kvp.Value.trait != null)
                .Select(kvp => new { kvp.Value.trait, kvp.Value.property })
                .GroupBy(a => a.trait)
                .ToDictionary(g => g.Key, g => g.Select(a => a.property).ToList());

            _bonuses = _targetedProperties
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(name => (IBonus)new Bonus(this, name)).ToList());

            foreach (var kvp in _bonuses)
            {
                foreach (var item in kvp.Value)
                {
                    kvp.Key.AddBonus(item);
                }
            }
        }

        public IEnumerable<IAugmentable> Targets => _targetedProperties.Keys;

        private double _amount;
        public double Amount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        private void OnTraitsCollectionChanged(object sender, TraitsChangedEventArgs e)
        {
            var calc = false;
            if (e.OldItems != null)
            {
                foreach (var (category, name, trait) in e.OldItems)
                {
                    calc |= OnTraitRemoved(category, name, trait);
                }
                calc = true;
            }
            if (e.NewItems != null)
            {
                foreach (var (category, name, trait) in e.NewItems)
                {
                    calc |= OnTraitAdded(category, name, trait);
                }
            }
            if (calc)
            {
                Recalc();
            }
        }

        private bool OnTraitAdded(string category, string name, ITrait trait)
        {
            if (trait == null)
            {
                throw new ArgumentNullException(nameof(trait));
            }

            bool recalc = false;

            if (_watchedTraits.TryGetValue((category, name), out var watch))
            {
                if (watch.trait is not null)
                {
                    throw new InvalidOperationException("New trait has the same category and name as existing trait.");
                }

                watch.trait = trait;
                AddPropertyToWatched(trait, watch.property);
                recalc = true;
            }

            if (trait is IAugmentable augmentable)
            {
                if (_targetedTraits.TryGetValue((category, name), out var target))
                {
                    if (target.trait is not null)
                    {
                        throw new InvalidOperationException("New trait has the same category and name as existing trait.");
                    }

                    target.trait = augmentable;
                    AddPropertyToTargeted(augmentable, target.property);
                }
            }

            return recalc;
        }

        private bool OnTraitRemoved(string category, string name, ITrait trait)
        {
            bool recalc = false;

            if (_watchedTraits.TryGetValue((category, name), out var tuple))
            {
                if (tuple.trait != trait)
                {
                    throw new ArgumentException("Trait being removed does not exist");
                }

                RemovePropertyFromWatched(trait, tuple.property);
                tuple.trait = null;
                recalc = true;
            }

            if (trait is IAugmentable augmentable)
            {
                if (_targetedTraits.TryGetValue((category, name), out var target))
                {
                    if (target.trait is not null)
                    {
                        throw new InvalidOperationException("New trait has the same category and name as existing trait.");
                    }

                    target.trait = augmentable;
                    RemovePropertyFromTargeted(augmentable, target.property);
                }
            }

            return recalc;
        }

        private void AddPropertyToWatched(ITrait trait, string property)
        {
            _watchedProperties.AddOrUpdate(
                trait,
                k =>
                {
                    trait.PropertyChanged += OnTraitPropertyChanged;

                    return new List<string> { property };
                },
                (k, v) =>
                {
                    v.Add(property);
                    return v;
                });
        }

        private void RemovePropertyFromWatched(ITrait trait, string property)
        {
            if (_watchedProperties.TryGetValue(trait, out var properties))
            {
                properties.Remove(property);
                if (properties.Count == 0)
                {
                    _watchedProperties.Remove(trait);
                    trait.PropertyChanged -= OnTraitPropertyChanged;
                }
            }
        }

        private void OnTraitPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ITrait trait)
            {
                if (_watchedProperties.TryGetValue(trait, out var properties)
                    && properties.Contains(e.PropertyName))
                {
                    Recalc();
                }
            }
        }


        private void AddPropertyToTargeted(IAugmentable trait, string property)
        {
            _targetedProperties.AddOrUpdate(
                trait,
                k =>
                {
                    return new List<string> { property };
                },
                (k, v) =>
                {
                    v.Add(property);
                    return v;
                });

            _bonuses.AddOrUpdate(
                trait,
                k =>
                {
                    var bonus = new Bonus(this, property);
                    trait.AddBonus(bonus);
                    return new List<IBonus> { bonus };
                },
                (k, v) =>
                {
                    var bonus = new Bonus(this, property);
                    trait.AddBonus(bonus);
                    v.Add(bonus);
                    return v;
                });
        }

        private void RemovePropertyFromTargeted(IAugmentable trait, string property)
        {
            if (_targetedProperties.TryGetValue(trait, out var properties))
            {
                properties.Remove(property);
                if (properties.Count == 0)
                {
                    _targetedProperties.Remove(trait);
                }
            }

            if (_bonuses.TryGetValue(trait, out var bonuses))
            {
                var bonus = bonuses.Find(b => string.Equals(b.TargetProperty, property));
                bonus.Dispose();
                if(bonuses.Remove(bonus))
                {
                    trait.RemoveBonus(bonus);
                }
                if (bonuses.Count == 0)
                {
                    _bonuses.Remove(trait);
                }
            }
        }

        private void Recalc()
        {
            Amount = _delegate();
        }

        #region IDisposable

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var trait in _watchedProperties.Keys)
                    {
                        trait.PropertyChanged -= OnTraitPropertyChanged;
                    }
                    _watchedProperties.Clear();
                    _watchedTraits.Clear();

                    _scope.Traits.TraitsChanged -= OnTraitsCollectionChanged;

                    foreach (var kvp in _bonuses)
                    {
                        foreach (var bonus in kvp.Value)
                        {
                            kvp.Key.RemoveBonus(bonus);
                            bonus.Dispose();
                        }
                    }

                    _bonuses.Clear();
                    _targetedProperties.Clear();
                    _targetedTraits.Clear();
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
