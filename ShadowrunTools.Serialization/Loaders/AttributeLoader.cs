using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowrunTools.Serialization
{
    [DataContract(Name = "Attribute", Namespace = "http://schemas.shadowruntools.com/loaders")]
    public class AttributeLoader: LeveledTraitLoader
    {
        public static AttributeLoader Create(IAttribute attribute)
        {
            return new AttributeLoader
            {
                Id = attribute.Id,
                TraitType = TraitType.Attribute,
                Name = attribute.Name,
                Category = attribute.Category,
                SubCategory = attribute.SubCategory,
                UserNotes = attribute.UserNotes,
                Book = attribute.Book,
                Page = attribute.Page,
                //PrototypeHash = attribute.
                // LeveledTrait
                ExtraMin = attribute.ExtraMin,
                ExtraMax = attribute.ExtraMax,
                BaseRating = attribute.BaseRating,
                Improvement = attribute.Improvement
            };
        }
    }
}
