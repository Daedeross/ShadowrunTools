using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Wpf.Resources.Prototypes;
using ShadowrunTools.Serialization;
using ShadowrunTools.Serialization.Prototypes;
using ShadowrunTools.Serialization.Prototypes.Priorities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Loaders
{
    public class BasicSaveLoadTests
    {
        [Fact]
        public void SimpleCharacterSaveLoadTest()
        {
            var logger = new Castle.Core.Logging.NullLogger();
            var serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            var dataLoader = new TestData(serializer, logger)
            {
                CurrentFiles = new List<string>
                {
                    @"TestArtifacts\Prototypes\Attributes.json",
                    @"TestArtifacts\Prototypes\Metatypes.json",
                    @"TestArtifacts\Prototypes\merge.json",
                    @"TestArtifacts\Prototypes\Priorities.json",
                }
            };

            var rules = new RulesPrototype();

            var prototypes = dataLoader.ReloadAll();
            var defaultMeta = prototypes.DefaultMetavariant;
            var traitFactory = new Factories.TraitFactory(rules);
            var character = CharacterFactory.Create(rules, prototypes, traitFactory);

            character.Name = "Test Character";

            character.Priorities.AttributePriority = PriorityLevel.A;
            character.Priorities.MetatypePriority = PriorityLevel.B;
            character.Priorities.SpecialPriority = PriorityLevel.C;
            character.Priorities.SkillPriority = PriorityLevel.D;
            character.Priorities.ResourcePriority = PriorityLevel.E;

            ICharacterPersistence loader = dataLoader;

            var filename = "testChar.sr5";

            loader.SaveCharacter(filename, character);

            var newChar = loader.LoadCharacter(filename, prototypes);

            Assert.Equal(character.Name, newChar.Name);
            Assert.Equal(character.Priorities.AttributePriority, newChar.Priorities.AttributePriority);
            Assert.Equal(character.Priorities.MetatypePriority, newChar.Priorities.MetatypePriority);
            Assert.Equal(character.Priorities.SpecialPriority, newChar.Priorities.SpecialPriority);
            Assert.Equal(character.Priorities.SkillPriority, newChar.Priorities.SkillPriority);
            Assert.Equal(character.Priorities.ResourcePriority, newChar.Priorities.ResourcePriority);
        }
    }
}
