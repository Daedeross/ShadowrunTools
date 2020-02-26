using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowrunTools.Characters.Validators
{
    /// <summary>
    /// Interface for validating and sumarizing a character based on <seealso cref="IRules"/>
    /// </summary>
    public interface ICharacterValidator : INotifyItemChanged, INotifyCollectionChanged
    {
        bool IsValid { get; }

        IReadOnlyCollection<IValidatorItem> Items { get; }
    }
}
