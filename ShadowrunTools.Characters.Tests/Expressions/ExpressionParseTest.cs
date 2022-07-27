using ExpressionEvaluator;
using Moq;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System.Collections.Generic;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Expressions
{
    public class ExpressionParseTest
    {
        public class TestScope
        {
            public int IntProp { get; set; }
            public string StringProp { get; set; }
            public float FloatProp { get; set; }
            public TestScope RecursiveProp { get; set; }
        }

        [Fact]
        public void TestTree()
        {
            var exprText = "RecursiveProp.IntProp > 1";
            var expr = new CompiledExpression<bool>(exprText);
            var del = expr.ScopeCompile<TestScope>();
            var scope = new TestScope
            {
                IntProp = 2,
                RecursiveProp = new TestScope
                {
                    IntProp = 3
                }
            };

            var result = del(scope);

            Assert.True(result);
        }

        public class TraitScope : IExpressionScope<IAttribute>
        {
            public IAttribute Me { get; set; }
            public ITraitContainer<IAttribute> Attributes { get; set; }
            public ITraitContainer<ISkill> Skills { get; }
            public ITraitContainer<IQuality> Qualities { get; }
        }

        [Fact]
        public void CharacterScopeTest()
        {
            var mockRules = new Mock<IRules>();

            var mockMetaAttribute = new Mock<IMetatypeAttribute>();
            mockMetaAttribute.SetupGet(x => x.Name).Returns("Robert");
            mockMetaAttribute.SetupGet(x => x.Min).Returns(2);
            mockMetaAttribute.SetupGet(x => x.Max).Returns(7);
            var metaAttribute = mockMetaAttribute.Object;
            var attribList = new List<IMetatypeAttribute> { metaAttribute };

            var mockMetatype = new Mock<IMetavariantPrototype>();
            mockMetatype.SetupGet(x => x.Attributes).Returns(attribList);

            var metatype = new CharacterMetatype(mockMetatype.Object);

            var mockPriorities = new Mock<ICharacterPriorities>();

            var mockAttributePrototype = new Mock<IAttributePrototype>();
            mockAttributePrototype.SetupGet(x => x.Name).Returns("Robert");
            mockAttributePrototype.SetupGet(x => x.Book).Returns("Book");
            mockAttributePrototype.SetupGet(x => x.Category).Returns(TraitCategories.Attribute);
            mockAttributePrototype.SetupGet(x => x.Page).Returns(42);
            mockAttributePrototype.SetupGet(x => x.ShortName).Returns("BOB");
            mockAttributePrototype.SetupGet(x => x.SubCategory).Returns("Primary Attributes");
            mockAttributePrototype.SetupGet(x => x.TraitType).Returns(Model.TraitType.Attribute);

            var factory = new TraitFactory(mockRules.Object);

            var character = new Character(metatype, mockPriorities.Object);

            var attribute = factory.CreateAttribute(character, mockAttributePrototype.Object);
            character.AddAttribute(attribute);

            IExpressionScope<IAttribute> scope = new TraitScope
            {
                Attributes = character.Attributes,
                Me = attribute
            };

            var exprText = "Me.AugmentedRating < 5";
            var expr = new CompiledExpression<bool>(exprText);
            var del = expr.ScopeCompile<TraitScope>();
            var result = del(scope as TraitScope);

            Assert.True(result);
        }
    }
}
