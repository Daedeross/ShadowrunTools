using DynamicData;
using DynamicData.Binding;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace ShadowrunTools.Characters.ViewModels
{
    public static class Extensions
    {
        public static IObservable<IChangeSet<T>> ToObservableChangeSet<T>(this ITraitContainer<T> source)
            where T : class, ITrait
        {
            return source.ToObservableChangeSet<ITraitContainer<T>, KeyValuePair<string, T>>()
                .Transform(kvp => kvp.Value);

        }
        public static IObservable<IChangeSet<ITrait>> ToObservableChangeSet(this ITraitContainer source)
        {
            return source.ToObservableChangeSet<ITraitContainer, KeyValuePair<string, ITrait>>()
                .Transform(kvp => kvp.Value);
        }

        /// <summary>
        /// Creates an Observable Changeset from a TraitContainer and wraps each trait
        /// in a ViewModel resolved from the provided factory.
        /// </summary>
        /// <typeparam name="TViewModel">The ViewModel type to resolve.</typeparam>
        /// <typeparam name="TModel">The Trait's type</typeparam>
        /// <param name="source">The <see cref="ITraitContainer{T}"/> to bind to.</param>
        /// <param name="factory"><see cref="IViewModelFactory"/></param>
        /// <returns>The observable changeset.</returns>
        /// <remarks>The ViewModel is released when the trait is removed from the collection.</remarks>
        public static IObservable<IChangeSet<TViewModel>> ToViewModelCollection<TViewModel, TModel>(this ITraitContainer<TModel> source, IViewModelFactory factory)
            where TModel : class, ITrait
            where TViewModel : class, IViewModel<TModel>
        {
            return source.ToObservableChangeSet<ITraitContainer<TModel>, KeyValuePair<string, TModel>>()
                .Filter(kvp => kvp.Value != null)
                .Transform(kvp =>
                {
                    return factory.For<TViewModel, TModel>(kvp.Value);
                })
                .OnItemRemoved(factory.Release);
        }
        public static IObservableCache<TViewModel, string> ToViewModelCache<TViewModel, TModel>(this ITraitContainer<TModel> source, IViewModelFactory factory)
            where TModel : class, ITrait
            where TViewModel : class, IViewModel<TModel>
        {
            return source
                .AsObservableChangeSet(kvp => kvp.Key)
                .Transform(kvp =>
                {
                    return factory.For<TViewModel, TModel>(kvp.Value);
                })
                .OnItemRemoved(factory.Release)
                .AsObservableCache();
        }
    }
}
