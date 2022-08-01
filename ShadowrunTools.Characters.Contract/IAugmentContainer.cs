using DynamicData.Binding;
using System;
using System.Collections.Generic;

namespace ShadowrunTools.Characters
{
    public interface IAugmentContainer<T>: IDisposable
    {
        /// <summary>
        /// The raw, unparsed, augments contained.
        /// </summary>
        IObservableCollection<string> Augments { get; }

        /// <summary>
        /// The parsed augments contained within
        /// </summary>
        IEnumerable<IAugment> ParsedAugments { get; }
    }
}
