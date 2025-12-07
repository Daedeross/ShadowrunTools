using Moq;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Factories
{
    public class TraitFactoryTests
    {
        const string AttributeName = "Agility";

        internal class CharacterMocks
        {
            public Mock<IRules> Rules { get; set; }
            public Mock<ICharacter> Character { get; set; }
            public Mock<ITraitContainer<IAttribute>> Attributes { get; set; }
            public Mock<ITraitContainer<ISkill>> Skills { get; set; }
            public Mock<ITraitContainer<ISkillGroup>> SkillGroups { get; set; }
        }

        internal CharacterMocks SetupMocks()
        {
            var mockCharacter = new Mock<ICharacter>();
            var mockAttributes = new Mock<ITraitContainer<IAttribute>>();
            var mockSkillsContainer = new Mock<ITraitContainer<ISkill>>();
            var mockSkillsObject = mockSkillsContainer.As<ITraitContainer>().Object;
            var mockGroupsContainer = new Mock<ITraitContainer<ISkillGroup>>();
            var mockGroupsObject = mockGroupsContainer.As<ITraitContainer>().Object;
            var mockRules = new Mock<IRules>();
            var mockAttribute = new Mock<IAttribute>();

            mockCharacter.SetupGet(x => x.Skills).Returns(mockSkillsContainer.Object);
            mockCharacter.Setup(
                x => x.TryGetValue(It.Is<string>(s => Equals(s, TraitCategories.Skill)),
                                   out mockSkillsObject));
            mockCharacter.SetupGet(x => x.SkillGroups).Returns(mockGroupsContainer.Object);
            mockCharacter.Setup(
                x => x.TryGetValue(It.Is<string>(s => Equals(s, TraitCategories.SkillGroup)),
                                   out mockGroupsObject));
            mockCharacter.SetupGet(x => x.Attributes).Returns(mockAttributes.Object);

            mockAttributes.SetupGet(x => x[It.Is<string>(s => Equals(s, AttributeName))])
                .Returns(mockAttribute.Object);

            return new CharacterMocks
            {
                Rules = mockRules,
                Character = mockCharacter,
                Skills = mockSkillsContainer,
                SkillGroups = mockGroupsContainer,
            };
        }

        [Fact]
        public void CreateSkillWithGroupTest()
        {
            var mocks = SetupMocks();
            var hash = nameof(CreateSkillWithGroupTest).GetHashCode();

            TraitFactory factory = new TraitFactory(mocks.Rules.Object, new Mocks.ParserFactory());

            var prototype = new SkillPrototype
            {
                Id = new Guid("C371490E-64A1-462D-BBC9-94223C725F68"),
                Name = "Test",
                SkillType = Contract.Model.SkillType.Active,
                SubCategory = "Combat Active",
                LinkedAttribute = AttributeName,
                GroupName = "TestGroup"
            };

            var skill = factory.CreateSkill(mocks.Character.Object, prototype);
        }
    }
}
