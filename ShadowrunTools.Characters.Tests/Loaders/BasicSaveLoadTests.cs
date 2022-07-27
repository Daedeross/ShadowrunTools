using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Factories;
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

            var rules = new GameRules
            {
                GenerationMethod = GenerationMethod.Priority
            };

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

            var prototypes = dataLoader.ReloadAll();
            var defaultMeta = prototypes.DefaultMetavariant;
            var traitFactory = new TraitFactory(rules);
            var characterFactory = new CharacterFactory(rules, traitFactory);
            var character = characterFactory.Create(prototypes);

            character.Name = "Test Character";

            character.Priorities.AttributePriority = PriorityLevel.A;
            character.Priorities.MetatypePriority = PriorityLevel.B;
            character.Priorities.SpecialPriority = PriorityLevel.C;
            character.Priorities.SkillPriority = PriorityLevel.D;
            character.Priorities.ResourcePriority = PriorityLevel.E;

            ICharacterPersistence loader = dataLoader;
            var characterLoader = new CharacterLoader(new TraitLoader(prototypes, rules), prototypes);

            var filename = "testChar.sr5";

            loader.SaveCharacter(filename, characterLoader, character);

            var newChar = loader.LoadCharacter(filename, characterLoader);

            Assert.Equal(character.Name, newChar.Name);
            Assert.Equal(character.Priorities.AttributePriority, newChar.Priorities.AttributePriority);
            Assert.Equal(character.Priorities.MetatypePriority, newChar.Priorities.MetatypePriority);
            Assert.Equal(character.Priorities.SpecialPriority, newChar.Priorities.SpecialPriority);
            Assert.Equal(character.Priorities.SkillPriority, newChar.Priorities.SkillPriority);
            Assert.Equal(character.Priorities.ResourcePriority, newChar.Priorities.ResourcePriority);
        }
    }
}
