using Moq;
using ShadowrunTools.Characters.Internal;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Validation
{
    public class ParsedRequirementsTests
    {
        private static IDslParser<T> MakeParser<T>()
        {
            var expressionVisitor = new DslExpressionVisitor<T>();
            return new DslParser<T>(
            new DslVisitor<T>(expressionVisitor, new DslAugmentVisitor<T>(expressionVisitor)));
        }

        private static ICategorizedTraitContainer MakeContainer(IEnumerable<ITraitContainer> categories)
        {
            var container = new CategorizedTraitContainer();

            foreach (var category in categories)
            {
                container.Add(category.Name, category);
            }

            return container;
        }

        private static ICategorizedTraitContainer MakeContainer(IEnumerable<ITrait> traits)
        {
            var container = new CategorizedTraitContainer();

            var lookup = traits
                .GroupBy(x => x.Category)
                .ToDictionary(
                    group => group.Key,
                    group => new TraitContainer(
                        group.Key,
                        group.ToDictionary(t => t.Name, t => t)
                        ) as ITraitContainer
                    );

            foreach (var group in lookup)
            {
                container.Add(group);
            }

            return container;
        }

        public static IEnumerable<object[]> WhitespaceStrings()
        {
            return (new []
            {
                null,
                "",
                " ",
                "\r",
                "\n",
                "\t",
                "\v",
                " \r\n\t\v",
                "\x00A0",
                "\x2000",
                "\x2001",
                "\x2003",
                "\x2004",
                "\x2005"
            })
            .Select(s => new object[] { s });
        }


        public static IEnumerable<object[]> SimpleBooleanExpressions()
        {
            return new[]
            {
                new object[] { "true", true },
                new object[] { "TRUE", true },
                new object[] { "false", false },
                new object[] { "FALSE", false },
                new object[] { "true and false", false },
                new object[] { "false or true", true },
                new object[] { "1 = 1", true },
                new object[] { "1 = 2", false },
                new object[] { "1 < 2", true },
                new object[] { "1 < 1", false },
                new object[] { "1 <= 2", true },
                new object[] { "1 <= 1", true },
                new object[] { "not (1 = 1)", false },
            };
        }

        [Fact]
        public void NeedsDefaultsToTrue()
        {
            const string category = "foo";
            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            var root = MakeContainer(new[] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            Assert.True(trait.SafeNeeds());
        }

        [Fact]
        public void TabooDefaultsToFalse()
        {
            const string category = "foo";
            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            var root = MakeContainer(new[] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            Assert.False(trait.SafeTaboo());
        }

        [Theory]
        [MemberData(nameof(WhitespaceStrings))]
        public void NullOrWhitespaceNeedsWillAlwaysReturnTrue(string needs)
        {
            const string category = "foo";
            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            var root = MakeContainer(new [] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            trait.Needs = needs;

            Assert.True(trait.SafeNeeds());
        }

        [Theory]
        [MemberData(nameof(WhitespaceStrings))]
        public void NullOrWhitespaceTabooWillAlwaysReturnFalse(string taboo)
        {
            const string category = "foo";
            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            var root = MakeContainer(new[] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            trait.Taboo = taboo;

            Assert.False(trait.SafeTaboo());
        }

        [Theory]
        [MemberData(nameof(SimpleBooleanExpressions))]
        public void SimpleNeedsExpressionsParse(string needs, bool expected)
        {
            const string category = "foo";
            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            var root = MakeContainer(new[] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            trait.Needs = needs;

            Assert.Equal(expected, trait.SafeNeeds());
        }

        [Theory]
        [InlineData("[foo]bar = 5", true)]
        [InlineData("[foo]bar:AugmentedRating = 5", true)]
        [InlineData("[foo]bar > 5", false)]
        [InlineData("([foo]bar) < 5", false)]
        [InlineData("[foo]bar >= 5", true)]
        [InlineData("[foo]bar >= 4", true)]
        [InlineData("[foo]bar <= 5", true)]
        [InlineData("[foo]bar <= 4", false)]
        [InlineData("[foo]bar < 6", true)]
        [InlineData("[foo]bar > 4", true)]
        [InlineData("has([foo]bar)", true)]
        [InlineData("has([foo]baz)", false)]
        [InlineData("4>[foo]bar", false)]
        public void TraitDependentNeedsTest(string needs, bool expected)
        {
            const string category = "foo";

            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);
            obj.Setup(x => x.Name)
                .Returns("bar");
            obj.Setup(x => x.Category)
                .Returns(category);

            var id = new Guid("774EF17C-FBD3-403E-BA6F-44EB9E6F45CE");

            var container = new TraitContainer(category);
            container.Add("bar", obj.Object);
            var root = MakeContainer(new[] { container });

            var parser = MakeParser<ITrait>();

            var trait = new TraitWithRequirements(id,
                                                  1,
                                                  "bar",
                                                  category,
                                                  container,
                                                  root,
                                                  new GameRules(),
                                                  parser);

            trait.Needs = needs;

            Assert.Equal(expected, trait.SafeNeeds());
        }
    }
}
