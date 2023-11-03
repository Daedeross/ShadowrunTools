using Moq;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Dsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace ShadowrunTools.Characters.Tests.Expressions
{
    public class ScopedExpressionTests
    {
        private static IDslParser<T> MakeParser<T>()
            where T : class, INamedItem
        {
            var expressionVisitor = new DslExpressionVisitor<T>();
            return new DslParser<T>(
                new DslVisitor<T>(expressionVisitor, new DslAugmentVisitor<T>(expressionVisitor)));
        }

        [Fact]
        public void VisitorGetsExpressionWatchedPropertiesTest()
        {
            var parser = MakeParser<ILeveledTrait>();

            string script = "[foo]bar:AugmentedRating > [foo]bar:Min";

            var result = parser.ParseExpression<bool>(script);

            Assert.True(result.HasValue);
            Assert.Equal(2, result.Value.WatchedProperties.Count);
            Assert.Contains(new PropertyReference("foo", "bar", "AugmentedRating"), result.Value.WatchedProperties);
            Assert.Contains(new PropertyReference("foo", "bar", "Min"), result.Value.WatchedProperties);
        }

        [Fact]
        public void VisitorGetAugmentTest()
        {
            var parser = MakeParser<ILeveledTrait>();

            string script = "[foo]bar to me";

            var result = parser.ParseAgument(script);

            Assert.True(result.HasValue);
            Assert.Equal(1, result.Value.Expression.WatchedProperties.Count);
            Assert.Contains(new PropertyReference("foo", "bar", "AugmentedRating"), result.Value.Expression.WatchedProperties);
            Assert.Contains(new PropertyReference("_me", "_me", "AugmentedRating"), result.Value.Targets);
            //Assert.Contains(new PropertyReference("foo", "bar", "Min"), result.Value.WatchedProperties);
        }

        [Fact]
        public void AndExpressionShortCircuits()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var dict1 = new Dictionary<string, ITrait> { { "bar", obj.Object } };

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);
            container.Setup(x => x.ContainsKey(It.IsAny<string>()))
                .Returns<string>(s => dict1.ContainsKey(s));

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var cat = container.Object;

            category.Setup(x => x.TryGetValue(It.Is<string>(s => s == "foo"), out cat))
                .Returns(true);

            var scope = new Mock<IScope<ILeveledTrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var parser = MakeParser<ILeveledTrait>();


            var script = "has([foo]baar) AND (2 / 0 > 0)"; // would cause exception if not short-circuted.

            var parseResult = parser.ParseExpression<bool>(script, scope.Object);

            Assert.True(parseResult.HasValue);

            var result = parseResult.Value.Scoped();

            Assert.False(result);
        }

        [Fact]
        public void OrExpressionShortCircuits()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var dict1 = new Dictionary<string, ITrait> { { "bar", obj.Object } };

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);
            container.Setup(x => x.ContainsKey(It.IsAny<string>()))
                .Returns<string>(s => dict1.ContainsKey(s));

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var cat = container.Object;

            category.Setup(x => x.TryGetValue(It.Is<string>(s => s == "foo"), out cat))
                .Returns(true);

            var scope = new Mock<IScope<ILeveledTrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var parser = MakeParser<ILeveledTrait>();


            var script = "has([foo]bar) OR (2 / 0 > 0)"; // would cause exception if not short-circuted.

            var parseResult = parser.ParseExpression<bool>(script, scope.Object);

            Assert.True(parseResult.HasValue);

            var result = parseResult.Value.Scoped();

            Assert.True(result);
        }

        [Fact]
        public void ExceptionInVariableLookupReturnsDefault()
        {
            var obj = new Mock<ILeveledTrait>();
            obj.SetupGet(x => x.AugmentedRating)
                .Returns(5);

            var dict1 = new Dictionary<string, ITrait> { { "bar", obj.Object } };

            var container = new Mock<ITraitContainer>();
            container.SetupGet(x => x[It.Is<string>(s => s == "bar")])
                .Returns(obj.Object);
            container.Setup(x => x.ContainsKey(It.IsAny<string>()))
                .Returns<string>(s => dict1.ContainsKey(s));

            var category = new Mock<ICategorizedTraitContainer>();
            category.SetupGet(x => x[It.Is<string>(s => s == "foo")])
                .Returns(container.Object);

            var cat = container.Object;

            category.Setup(x => x.TryGetValue(It.Is<string>(s => s == "foo"), out cat))
                .Returns(true);

            var scope = new Mock<IScope<ILeveledTrait>>();
            scope.SetupGet(x => x.Traits)
                .Returns(category.Object);

            var parser = MakeParser<ILeveledTrait>();


            // misspell the trait name which should throw a "IndexNotFoundException"
            // This should be caught and return the default value of 0;
            string script = "[foo]baar:AugmentedRating + 1";
            var result = parser.ParseExpression<double>(script, scope.Object);


            Assert.True(result.HasValue);
            Assert.Equal(1d, result.Value.Scoped());
        }
    }
}
