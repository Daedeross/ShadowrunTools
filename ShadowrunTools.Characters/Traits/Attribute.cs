namespace ShadowrunTools.Characters.Traits
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using ReactiveUI;
    using ShadowrunTools.Characters.Model;

    public class Attribute : LeveledTrait, IAttribute
    {
        protected readonly ICharacterMetatype Metatype;

        public Attribute(
            Guid id,
            int prototypeHash,
            string name,
            ITraitContainer container,
            ICategorizedTraitContainer root,
            ICharacterMetatype metatype,
            IRules rules)
            : base(id, prototypeHash, name, "Attribute", container, root, rules)
        {
            Metatype = metatype;
            Metatype.ItemChanged += OnMetatypeChanged;
            CalculateMinMax();

            _min = this.WhenAnyValue(
                    me => me.MetatypeMin,
                    me => me.BonusMin,
                    (baseMin, extraMin) => baseMin + extraMin)
                .ToProperty(this, me => me.Min)
                .DisposeWith(Disposables);

            _max = this.WhenAnyValue(
                    me => me.MetatypeMax,
                    me => me.BonusMax,
                    (baseMax, extraMax) => baseMax + extraMax)
                .ToProperty(this, me => me.Max)
                .DisposeWith(Disposables);
        }

        public string ShortName { get; set; }

        public override TraitType TraitType => TraitType.Attribute;

        #region Min

        protected int m_MetatypeMin;
        public int MetatypeMin
        {
            get => m_MetatypeMin;
            protected set => this.RaiseAndSetIfChanged(ref m_MetatypeMin, value);
        }

        protected readonly ObservableAsPropertyHelper<int> _min;
        public override int Min => _min?.Value ?? 0;

        #endregion

        #region Max

        protected int m_MetatypeMax;
        public int MetatypeMax
        {
            get => m_MetatypeMax;
            protected set => this.RaiseAndSetIfChanged(ref m_MetatypeMax, value);
        }

        protected readonly ObservableAsPropertyHelper<int> _max;
        public override int Max => _max?.Value ?? 0;
        public override int AugmentedMax => ImprovedRating + mRules.MaxAugment;

        #endregion

        public override bool Independant { get => true; }

        private string m_CustomOrder;
        public string CustomOrder
        {
            get => m_CustomOrder;
            set => this.RaiseAndSetIfChanged(ref m_CustomOrder, value);
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
            // TODO: Test that these are called by ObservableAsProperty<>
            //var propNames = new List<string>();

            //var oldMin = MetatypeMin;
            //var oldMax = MetatypeMax;
            //var oldBase = BaseRating;
            //var oldImproved = ImprovedRating;
            //var oldAugmented = AugmentedRating;
            //var oldAugMax = AugmentedMax;

            if (Metatype.TryGetAttribute(Name, out IMetatypeAttribute attribute))
            {
                MetatypeMin = attribute.Min;
                MetatypeMax = attribute.Max;
            }
            else // use (1/6) and log it
            {
                Logger.Error("Unable to retireve Attribute with Name:{0} info from Metatype:{1}", Name, Metatype.Name);
                MetatypeMin = 1;
                MetatypeMax = 6;
            }

            // TODO: Test that these are called by ObservableAsProperty<>
            //if (Min != oldMin)
            //    propNames.Add(nameof(Min));
            //if (Max != oldMax)
            //    propNames.Add(nameof(Max));
            //if (BaseRating != oldBase)
            //    propNames.Add(nameof(BaseRating));
            //if (ImprovedRating != oldImproved)
            //    propNames.Add(nameof(ImprovedRating));
            //if (AugmentedRating != oldAugmented)
            //    propNames.Add(nameof(AugmentedRating));
            //if (AugmentedMax != oldAugMax)
            //    propNames.Add(nameof(AugmentedMax));

            //foreach (var name in propNames)
            //{
            //    this.RaisePropertyChanged(name);
            //}
        }
    }
}
