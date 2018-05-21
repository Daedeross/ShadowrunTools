namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using ShadowrunTools.Characters.Model;

    public class Attribute : LeveledTrait
    {
        protected ICharacterMetatype Metatype { get; private set; }

        public Attribute(Guid id, ITraitContainer container, ICategorizedTraitContainer root, ICharacterMetatype metatype)
            : base(id, "Attribute", container, root)
        {
            Metatype = metatype;
            Metatype.ItemChanged += OnMetatypeChanged;
        }

        protected int _min;
        public override int Min => _min;

        protected int _max;
        public override int Max => _max;

        public override int AugmentedMax => throw new NotImplementedException();

        public override void OnAugmentChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnAugmentRemoving(AugmentKind kind)
        {
            throw new NotImplementedException();
        }

        protected override void OnAugmentAdded(IAugment augment)
        {
            throw new NotImplementedException();
        }

        protected override void OnAugmentRemoved(IAugment augment)
        {
            throw new NotImplementedException();
        }

        protected void OnMetatypeChanged(object sender, ItemChangedEventArgs e)
        {

        }

        private void CalculateMinMax()
        {
            if (Metatype.TryGetAttribute(Name, out IMetatypeAttribute attribute))
            {

            }
            else // use (1/6) and log it
            {

            }
        }
    }
}
