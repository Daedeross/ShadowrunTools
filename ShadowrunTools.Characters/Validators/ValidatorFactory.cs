namespace ShadowrunTools.Characters.Validators
{
    using ShadowrunTools.Characters.Priorities;
    using ShadowrunTools.Characters.Traits;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ValidatorFactory
    {
        public static IValidatorItem AttributePointsValidator(ICharacter character)
        {
            if (character.Priorities is null)
            {
                throw new ArgumentNullException(nameof(character.Priorities));
            }
            return TraitCollectionAggregateValidator.Create<IAttribute, int>(
                character.Attributes,
                "Attributes",
                "Attributes are invalid",
                new[] { nameof(IAttribute.BaseRating), nameof(IAttribute.Min) },
                (left, attr) => left + (attr.BaseRating - attr.Min),
                points => points <= character.Priorities.AttributePoints,
                0,
                (INotifyItemChanged)character.Priorities,
                nameof(ICharacterPriorities.AttributePoints)
                );
        }
    }
}
