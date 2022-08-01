using Moq;
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

namespace ShadowrunTools.Characters.Tests.Augments
{
    public class LeveledTraitBonusTests
    {
        public class TestLeveledTrait : LeveledTrait
        {
            public TestLeveledTrait(Guid id, int prototypeHash, string name, string category, ITraitContainer container, ICategorizedTraitContainer root, IRules rules)
                : base(id, prototypeHash, name, category, container, root, rules)
            {
            }

            public override int Min => 1;

            public override int Max => 6;

            public override int AugmentedMax => 10;

            public override TraitType TraitType => TraitType.None;

            public override bool Independant => true;
        }

        public static IEnumerable<object[]> BonusTestData()
        {
            return new[]
            {
                new object[] { 1d, 1 },
                new object[] {1.25d, 1},
                new object[] {2d, 2},
                new object[] {-1d, -1},
                new object[] {-1.25d, -2},
                new object[] {-2d, -2 },
            };
        }

        [Theory]
        [MemberData(nameof(BonusTestData))]
        public void LeveledTraitHandlesBonusMinTest(double input, int expected)
        {
            var bonus = new Mock<IBonus>();
            bonus.SetupGet(x => x.TargetProperty).Returns("BonusMin");
            bonus.SetupGet(x => x.Amount).Returns(input);

            var rules = new Mock<IRules>();

            var container = new TraitContainer("foo");
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var trait = new TestLeveledTrait(Guid.NewGuid(), 0, "trait", "foo", container, root, rules.Object);

            trait.AddBonus(bonus.Object);

            Assert.Equal(expected, trait.BonusMin);
        }

        [Theory]
        [MemberData(nameof(BonusTestData))]
        public void LeveledTraitHandlesBonusMaxTest(double input, int expected)
        {
            var bonus = new Mock<IBonus>();
            bonus.SetupGet(x => x.TargetProperty).Returns("BonusMax");
            bonus.SetupGet(x => x.Amount).Returns(input);

            var rules = new Mock<IRules>();

            var container = new TraitContainer("foo");
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var trait = new TestLeveledTrait(Guid.NewGuid(), 0, "trait", "foo", container, root, rules.Object);

            trait.AddBonus(bonus.Object);

            Assert.Equal(expected, trait.BonusMax);
        }

        [Theory]
        [MemberData(nameof(BonusTestData))]
        public void LeveledTraitHandlesBonusRatingTest(double input, int expected)
        {
            var bonus = new Mock<IBonus>();
            bonus.SetupGet(x => x.TargetProperty).Returns("BonusRating");
            bonus.SetupGet(x => x.Amount).Returns(input);

            var rules = new Mock<IRules>();

            var container = new TraitContainer("foo");
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var trait = new TestLeveledTrait(Guid.NewGuid(), 0, "trait", "foo", container, root, rules.Object);

            trait.AddBonus(bonus.Object);

            Assert.Equal(expected, trait.BonusRating);
        }

        [Theory]
        [InlineData(nameof(ILeveledTrait.BonusMin))]
        [InlineData(nameof(ILeveledTrait.BonusMax))]
        [InlineData(nameof(ILeveledTrait.BonusRating))]
        public void LeveledTraitRecalcsWhenBonusChanges(string propertyName)
        {
            double value = 0d;
            var bonus = new Mock<IBonus>();
            bonus.SetupGet(x => x.TargetProperty).Returns(propertyName);
            bonus.SetupGet(x => x.Amount).Returns(() => value);

            var rules = new Mock<IRules>();

            var container = new TraitContainer("foo");
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var trait = new TestLeveledTrait(Guid.NewGuid(), 0, "trait", "foo", container, root, rules.Object);

            trait.AddBonus(bonus.Object);

            Assert.PropertyChanged(trait, propertyName, () =>
            {
                value = 1d;
                bonus.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs(nameof(IBonus.Amount)));
            });
        }

        [Theory]
        [InlineData(nameof(ILeveledTrait.BonusMin))]
        [InlineData(nameof(ILeveledTrait.BonusMax))]
        [InlineData(nameof(ILeveledTrait.BonusRating))]
        public void LeveledTraitDoesNotRaiseChangeEventWhenRecaledValueIsTheSame(string propertyName)
        {
            double value = 0d;
            var bonus = new Mock<IBonus>();
            bonus.SetupGet(x => x.TargetProperty).Returns(propertyName);
            bonus.SetupGet(x => x.Amount).Returns(() => value);

            var rules = new Mock<IRules>();

            var container = new TraitContainer("foo");
            var root = new CategorizedTraitContainer();
            root.Add(container.Name, container);

            var trait = new TestLeveledTrait(Guid.NewGuid(), 0, "trait", "foo", container, root, rules.Object);

            bool isInvoked = false;
            trait.PropertyChanged += (s, e) => isInvoked = true;

            trait.AddBonus(bonus.Object);

            value = 0.25d;  // Rounds down, thus integer property's value is not changed.
            bonus.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs(nameof(IBonus.Amount)));

            Assert.False(isInvoked);
        }
    }
}
