namespace ShadowrunTools.Characters
{
    using ShadowrunTools.Characters.Model;
    using ShadowrunTools.Foundation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameRules : IRules, IEditable
    {
        private IPropertyList _oldProperties;

        [Display(Editable = true)]
        public GenerationMethod GenerationMethod { get; set; }

        [Display(Editable = true)]
        public int StartingKarma { get; set; }

        [Display(Editable = true)]
        public int MaxAugment { get; set; }

        [Display(Editable = true)]
        public int StartingSkillCap { get; set; }

        [Display(Editable = true)]
        public int StartingMaxedSkillCount { get; set; }

        [Display(Editable = true)]
        public int InPlaySkillCap { get; set; }

        [Display(Editable = true)]
        public int StartingMaxedAttributeCount { get; set; }

        [Display(Editable = true)]
        public int MaxInitiationDiscounts { get; set; } = 3;

        [Display(Editable = true)]
        public int MaxSubmersionDiscounts { get; set; } = 3;

        [Display(Editable = true)]
        public int AttributeKarmaMult { get; set; }

        [Display(Editable = true)]
        public int SkillGroupKarmaMult { get; set; }

        [Display(Editable = true)]
        public int ActiveSkillKarmaMult { get; set; }

        [Display(Editable = true)]
        public int MagicSkillKarmaMult { get; set; }

        [Display(Editable = true)]
        public int ResonanceSkillKarmaMult { get; set; }

        [Display(Editable = true)]
        public int KnowledgeSkillKarmaMult { get; set; }

        [Display(Editable = true)]
        public int LanguageSkillKarmaMult { get; set; }

        [Display(Editable = true)]
        public int InitiationKarmaBase { get; set; }

        [Display(Editable = true)]
        public int InitiationKarmaMult { get; set; }

        [Display(Editable = true)]
        public int SubmersionKarmaBase { get; set; }

        [Display(Editable = true)]
        public int SubmersionKarmaMult { get; set; }

        [Display(Editable = true)]
        public int ComplexFormKarma { get; set; }

        [Display(Editable = true)]
        public int SpellKarma { get; set; }

        [Display(Editable = true)]
        public int PowerPointKarma { get; set; }

        [Display(Editable = true)]
        public int SpecializationKarma { get; set; }

        [Display(Editable = true)]
        public int MartialArtsStyleKarma { get; set; }

        [Display(Editable = true)]
        public int MartialArtsTechniqueKarma { get; set; }

        [Display(Editable = true)]
        public int InPlayQualityMult { get; set; }

        [Display(Editable = true)]
        public int StartingMaxSkillRating { get; set; }

        [Display(Editable = true)]
        public int InPlayMaxSkillRating { get; }

        public IPropertyList BeginEdit()
        {
            _oldProperties = PropertyFactory.CreateFromObject(this, false);
            return _oldProperties;
        }

        public bool ValidateEdit(IPropertyList newProperties) => true;

        public void CommitEdit(IPropertyList newProperties)
        {
            IPropertyList changed;

            if (_oldProperties is null)
            {
                changed = newProperties;
            }
            else
            {
                changed = new PropertyList(newProperties
                    .Join(_oldProperties, p => p.Key, p => p.Key, (newKvp, oldKvp) => new { Key = newKvp.Key, Old = oldKvp.Value, New = newKvp.Value })
                    .Where(x => !Equals(x.Old.Value, x.New.Value))
                    .ToDictionary(x => x.Key, x => x.New)
                    );
            }

            PropertyFactory.SetFromPropertyList(this, changed);
            _oldProperties = null;

            RaiseItemChanged(changed.Keys.ToArray());
        }

        #region IItemChanged Implementation

        public event EventHandler<ItemChangedEventArgs> ItemChanged;

        protected void RaiseItemChanged(string[] propNames)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(propNames));
        }

        #endregion
    }
}
