namespace ShadowrunTools.Characters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using ShadowrunTools.Characters.Model;

    public class Attribute : LeveledTrait
    {
        public Attribute(Guid id, ITraitContainer container, ICategorizedTraitContainer root)
            : base(id, "Attribute", container, root)
        {

        }

        public override int Min => throw new NotImplementedException();

        public override int Max => throw new NotImplementedException();

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
    }
}
