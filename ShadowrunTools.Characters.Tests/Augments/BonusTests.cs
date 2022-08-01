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
    public class BonusTests
    {
        [Fact]
        public void BonusRaisesAmountChangedEventWhenAugmentAmountChanges()
        {
            var augment = new Mock<IAugment>();
            augment.SetupGet(x => x.Amount).Returns(1.125d);

            string propName = "Foo";

            IBonus bonus = new Bonus(augment.Object, propName);

            Assert.PropertyChanged(bonus, nameof(IBonus.Amount), () =>
            {
                augment.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs(nameof(IAugment.Amount)));
            });
        }

        [Fact]
        public void BonusRecalulatesAmountWhenAugmentAmountChanges()
        {
            double initialAmount = 1.125d;
            double actualAmount = initialAmount;
            var augment = new Mock<IAugment>();
            augment.SetupGet(x => x.Amount).Returns(() => actualAmount);

            string propName = "Foo";

            IBonus bonus = new Bonus(augment.Object, propName);

            // Ensure initial bonus is correct;
            Assert.Equal(initialAmount, bonus.Amount);

            actualAmount = 2d;

            augment.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs(nameof(IAugment.Amount)));

            Assert.Equal(actualAmount, bonus.Amount);
        }

        [Theory]
        [InlineData("Foo", "BonusFoo")]
        [InlineData("Rating", "BonusRating")]
        [InlineData("BonusRating", "BonusRating")]
        public void BonusEnsuresBonusPrefixToTargetProperty(string input, string expected)
        {
            var augment = new Mock<IAugment>();
            augment.SetupGet(x => x.Amount).Returns(1.125d);

            IBonus bonus = new Bonus(augment.Object, input);

            Assert.Equal(expected, bonus.TargetProperty);
        }

        [Theory]
        [InlineData("AugmentedRating", "BonusRating")]
        public void BonusHandlesPropertySynonyms(string input, string expected)
        {
            var augment = new Mock<IAugment>();
            augment.SetupGet(x => x.Amount).Returns(1.125d);

            IBonus bonus = new Bonus(augment.Object, input);

            Assert.Equal(expected, bonus.TargetProperty);
        }
    }
}
