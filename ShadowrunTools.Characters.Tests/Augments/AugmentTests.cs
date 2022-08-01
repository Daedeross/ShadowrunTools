using Moq;
using ShadowrunTools.Characters.Internal;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ShadowrunTools.Characters.Tests.Augments.LeveledTraitBonusTests;

namespace ShadowrunTools.Characters.Tests.Augments
{
    public class AugmentTests
    {
        [Fact]
        public void AugmentFindsWatchedProperties()
        {
            string category = "foo";
            string name = "bar";

            var rules = new Mock<IRules>();
            var container = new TraitContainer(category);
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var owner = new Mock<ILeveledTrait>();

            var watched = new TestLeveledTrait(Guid.NewGuid(), 12345, name, category, container, root, rules.Object);
            container.Add(name, watched);
            var watches = new List<PropertyReference>
            {
                new PropertyReference(category, name, nameof(ILeveledTrait.AugmentedRating))
            };

            var scope = new Scope<ILeveledTrait>(null, owner.Object, root);


            var augment = new Augment<ILeveledTrait>(scope, new List<PropertyReference>(), watches, () => watched.AugmentedRating);

            Assert.Equal(1, augment.Amount);

            watched.BaseIncrease = 1;   // Raises PropertyChanged

            Assert.Equal(2, augment.Amount);
        }

        [Fact]
        public void AugmentFindsWatchedPropertiesAfterCreation()
        {
            string category = "foo";
            string name = "bar";

            var rules = new Mock<IRules>();
            var container = new TraitContainer(category);
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var owner = new Mock<ILeveledTrait>();

            var watched = new TestLeveledTrait(Guid.NewGuid(), 12345, name, category, container, root, rules.Object);
            var watches = new List<PropertyReference>
            {
                new PropertyReference(category, name, nameof(ILeveledTrait.AugmentedRating))
            };

            var scope = new Scope<ILeveledTrait>(null, owner.Object, root);

            var augment = new Augment<ILeveledTrait>(scope, new List<PropertyReference>(), watches, () => watched.AugmentedRating);

            Assert.Equal(1, augment.Amount);

            container.Add(name, watched);   // trait is added after Augment created

            watched.BaseIncrease = 1;   // Raises PropertyChanged

            Assert.Equal(2, augment.Amount);
        }

        [Fact]
        public void AugmentFindsTargetProperties()
        {
            string category = "foo";
            string watchedName = "bar";
            string targetName = "baz";

            var rules = new Mock<IRules>();
            var container = new TraitContainer(category);
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var owner = new Mock<ILeveledTrait>();

            var watched = new TestLeveledTrait(Guid.NewGuid(), 12345, watchedName, category, container, root, rules.Object);
            container.Add(watchedName, watched);
            var watches = new List<PropertyReference>
            {
                new PropertyReference(category, watchedName, nameof(ILeveledTrait.AugmentedRating))
            };

            var scope = new Scope<ILeveledTrait>(null, owner.Object, root);

            var target = new Mock<IAugmentable>();
            var targetTrait = target.As<ILeveledTrait>();
            container.Add(targetName, targetTrait.Object);
            var targets = new List<PropertyReference>
            {
                new PropertyReference(category, targetName, nameof(LeveledTrait.BonusRating))
            };

            var augment = new Augment<ILeveledTrait>(scope, targets, watches, () => 1);

            target.Verify(x => x.AddBonus(It.IsAny<IBonus>()));
        }

        [Fact]
        public void AugmentFindsTargetPropertiesAfterCreation()
        {
            string category = "foo";
            string watchedName = "bar";
            string targetName = "baz";

            var rules = new Mock<IRules>();
            var container = new TraitContainer(category);
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var owner = new Mock<ILeveledTrait>();

            var watched = new TestLeveledTrait(Guid.NewGuid(), 12345, watchedName, category, container, root, rules.Object);
            container.Add(watchedName, watched);
            var watches = new List<PropertyReference>
            {
                new PropertyReference(category, watchedName, nameof(ILeveledTrait.AugmentedRating))
            };

            var scope = new Scope<ILeveledTrait>(null, owner.Object, root);

            var target = new Mock<IAugmentable>();
            var targetTrait = target.As<ILeveledTrait>();
            var targets = new List<PropertyReference>
            {
                new PropertyReference(category, targetName, nameof(LeveledTrait.BonusRating))
            };

            var augment = new Augment<ILeveledTrait>(scope, targets, watches, () => 1);

            target.VerifyNoOtherCalls();

            container.Add(targetName, targetTrait.Object);

            target.Verify(x => x.AddBonus(It.IsAny<IBonus>()));
        }
    }
}
