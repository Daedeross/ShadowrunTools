using Castle.Core.Logging;
using Moq;
using Newtonsoft.Json;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization;
using System.Collections.Generic;

namespace ShadowrunTools.Characters.Wpf.Resources.Prototypes
{
    public class TestData : DataLoader
    {
        public TestData(JsonSerializer serializer, ILogger logger) : base(serializer, logger)
        {
        }

        public override IPrototypeRepository ReloadAll()
        {
            var repo = base.ReloadAll();
            repo.Priorities = TestPriorities();
            return repo;
        }

        #region Helpers

        public static IPriorities TestPriorities()
        {
            var attributePoints = new[] { 12, 14, 16, 20, 24 };
            var attributes = new Dictionary<PriorityLevel, IAttributesPriority>(5);

            var metaTuples = new (string, int)[][]
            {
                new[] { ("Human", 1) },
                new[] { ("Human", 3), ("Elf", 0) },
                new[] { ("Human", 5), ("Elf", 3) },
                new[] { ("Human", 7), ("Elf", 6) },
                new[] { ("Human", 9), ("Elf", 8) },
            };
            var metas = new Dictionary<PriorityLevel, IMetatypePriority>(5);

            var specials = new Dictionary<PriorityLevel, ISpecialsPriority>(5);

            var skillPoints = new[] { (18, 0), (22, 0), (28, 2), (36, 5), (46, 10) };
            var skills = new Dictionary<PriorityLevel, ISkillsPriority>(5);

            var resourceValues = new[] { 6000m, 50000m, 140000m, 275000m, 450000m };
            var resources = new Dictionary<PriorityLevel, IResourcesPriority>(5);

            for (int i = 0; i < 5; i++)
            {
                var level = (PriorityLevel)i;

                var p = new Mock<IAttributesPriority>();
                p.SetupGet(x => x.AttibutePoints).Returns(attributePoints[i]);

                attributes[level] = p.Object;

                var metaOptions = new List<IPriorityMetavariantOption>();
                foreach (var tuple in metaTuples[i])
                {
                    var m = new Mock<IPriorityMetavariantOption>();
                    m.SetupGet(x => x.AdditionalKarmaCost).Returns(0);
                    m.SetupGet(x => x.Metavariant).Returns(tuple.Item1);
                    m.SetupGet(x => x.SpecialAttributePoints).Returns(tuple.Item2);

                    metaOptions.Add(m.Object);
                }

                var metasMock = new Mock<IMetatypePriority>();
                metasMock.SetupGet(x => x.MetavariantOptions).Returns(metaOptions);
                metas[level] = metasMock.Object;

                var specialsMock = new Mock<ISpecialsPriority>();
                if (i == 4)
                {
                    var mage1 = new Mock<ISpecialOption>();
                    mage1.SetupGet(x => x.Quality).Returns("Magician");
                    mage1.SetupGet(x => x.AttributeName).Returns("Magic");
                    mage1.SetupGet(x => x.AttributeRating).Returns(6);
                    var option1 = new Mock<ISpecialSkillChoice>();
                    option1.SetupGet(x => x.Choice).Returns("Magical");
                    option1.SetupGet(x => x.Count).Returns(2);
                    option1.SetupGet(x => x.Rating).Returns(5);
                    option1.SetupGet(x => x.Kind).Returns(SkillChoiceKind.Category);
                    mage1.SetupGet(x => x.SkillOptions).Returns(new List<ISpecialSkillChoice> { option1.Object });
                    mage1.SetupGet(x => x.FreeSpells).Returns(10);

                    specialsMock.SetupGet(x => x.Options).Returns(new List<ISpecialOption> { mage1.Object });
                }
                else
                {
                    specialsMock.SetupGet(x => x.Options).Returns(new List<ISpecialOption>());
                }
                specials[level] = specialsMock.Object;

                var skillsMock = new Mock<ISkillsPriority>();
                skillsMock.SetupGet(x => x.SkillPoints).Returns(skillPoints[i].Item1);
                skillsMock.SetupGet(x => x.SkillGroupPoints).Returns(skillPoints[i].Item2);
                skills[level] = skillsMock.Object;

                var resourcesMock = new Mock<IResourcesPriority>();
                resourcesMock.SetupGet(x => x.Resources).Returns(resourceValues[i]);
                resources[level] = resourcesMock.Object;
            }

            var mock = new Mock<IPriorities>();

            mock.SetupGet(x => x.Attributes)
                .Returns(attributes);
            mock.SetupGet(x => x.Metatype)
                .Returns(metas);
            mock.SetupGet(x => x.Specials)
                .Returns(specials);
            mock.SetupGet(x => x.Skills)
                .Returns(skills);
            mock.SetupGet(x => x.Resources)
                .Returns(resources);

            return mock.Object;
        }

        #endregion
    }
}
