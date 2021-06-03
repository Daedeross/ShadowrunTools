using Moq;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using Xunit;
using Attribute = ShadowrunTools.Characters.Traits.Attribute;

namespace ShadowrunTools.Characters.Tests
{
    public class CharacterInitializationTests
    {
        [Fact]
        public void TestCreateAttribute()
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
            var characterMetatype = new CharacterMetatype(mockMetatype.Object);

            var mockAttributePrototype = new Mock<IAttributePrototype>();
            mockAttributePrototype.SetupGet(x => x.Name).Returns("Robert");
            mockAttributePrototype.SetupGet(x => x.Book).Returns("Book");
            mockAttributePrototype.SetupGet(x => x.Category).Returns(TraitCategories.Attribute);
            mockAttributePrototype.SetupGet(x => x.Page).Returns(42);
            mockAttributePrototype.SetupGet(x => x.ShortName).Returns("BOB");
            mockAttributePrototype.SetupGet(x => x.SubCategory).Returns("Primary Attributes");
            mockAttributePrototype.SetupGet(x => x.TraitType).Returns(Model.TraitType.Attribute);

            var mockPriorities = new Mock<ICharacterPriorities>();

            var factory = new TraitFactory(mockRules.Object);

            var character = new Character(factory, characterMetatype, mockPriorities.Object);

            var attribute = character.CreateAttribute(mockAttributePrototype.Object);
            character.AddAttribute(attribute);

            Assert.Equal(2, attribute.Min);
            Assert.Equal(7, attribute.Max);
            Assert.Equal("Robert", attribute.Name);
            Assert.Equal("BOB", attribute.ShortName);
            Assert.Equal(42, attribute.Page);
            Assert.Equal(TraitCategories.Attribute, attribute.Category);
        }
    }
}
