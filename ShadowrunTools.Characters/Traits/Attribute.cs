namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShadowrunTools.Characters.Model;

    public class Attribute : LeveledTrait, IAttribute
    {
        protected readonly ICharacterMetatype Metatype;

        public Attribute(
            Guid id,
            string name,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            ICharacterMetatype metatype,
            IRules rules)
            : base(id, name, "Attribute", container, root, rules)
        {
            Metatype = metatype;
            Metatype.ItemChanged += OnMetatypeChanged;
            CalculateMinMax();
        }

        public string ShortName { get; set; }

        public override TraitType TraitType => TraitType.Attribute;

        protected int _min;
        public override int Min => _min;

        protected int _max;
        public override int Max => _max;
        public override int AugmentedMax => ImprovedRating + mRules.MaxAugment;

        public override bool Independant { get => true; }

        private string _customOrder;
        public string CustomOrder
        {
            get { return _customOrder; }
            set
            {
                if (_customOrder != value)
                {
                    _customOrder = value;
                    RaiseItemChanged(new[] { nameof(CustomOrder) });
                }
            }
        }


        protected void OnMetatypeChanged(object sender, ItemChangedEventArgs e)
        {
            if (ReferenceEquals(sender, Metatype))
            {
                if (e.PropertyNames.Contains(Name))
                {
                    CalculateMinMax();
                }
            }
            else
            {
                Logger.Warn("Unknown object sent to {0} event on Attribute:{1}", nameof(OnMetatypeChanged), Name);
            }
        }

        private void CalculateMinMax()
        {
            var propNames = new List<string>();

            var oldMin = _min;
            var oldMax = _max;
            var oldBase = BaseRating;
            var oldImproved = ImprovedRating;
            var oldAugmented = AugmentedRating;
            var oldAugMax = AugmentedMax;

            if (Metatype.TryGetAttribute(Name, out IMetatypeAttribute attribute))
            {

                _min = attribute.Min;
                _max = attribute.Max;

            }
            else // use (1/6) and log it
            {
                Logger.Error("Unable to retireve Attribute with Name:{0} info from Metatype:{1}", Name, Metatype.Name);
                _min = 1;
                _max = 6;
            }

            if (Min != oldMin)
                propNames.Add(nameof(Min));
            if (Max != oldMax)
                propNames.Add(nameof(Max));
            if (BaseRating != oldBase)
                propNames.Add(nameof(BaseRating));
            if (ImprovedRating != oldImproved)
                propNames.Add(nameof(ImprovedRating));
            if (AugmentedRating != oldAugmented)
                propNames.Add(nameof(AugmentedRating));
            if (AugmentedMax != oldAugmented)
                propNames.Add(nameof(AugmentedMax));

            if (propNames.Any())
            {
                RaiseItemChanged(propNames.ToArray());
            }
        }
    }
}
