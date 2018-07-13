using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization.Prototypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Serialization
{
    public class GenerateFilesTests
    {
        [Fact]
        public void GenerateAttributesFile()
        {
            var prototypes = Enumerable.Range(0, 8).Select(
                i => new AttributePrototype
                {
                    TraitType = TraitType.Attribute,
                    Book = "SR5",
                    Page = 51,
                    SubCategory = "Core Attributes",
                }).ToArray();

            prototypes[0].Name      = "Body";
            prototypes[0].ShortName = "BOD";
            prototypes[1].Name      = "Agility";
            prototypes[1].ShortName = "AGI";
            prototypes[2].Name      = "Reaction";
            prototypes[2].ShortName = "REA";
            prototypes[3].Name      = "Strength";
            prototypes[3].ShortName = "STR";
            prototypes[4].Name      = "Willpower";
            prototypes[4].ShortName = "WIL";
            prototypes[5].Name      = "Logic";
            prototypes[5].ShortName = "LOG";
            prototypes[6].Name      = "Intuition";
            prototypes[6].ShortName = "INT";
            prototypes[7].Name      = "Charisma";
            prototypes[7].ShortName = "Cha";

            var baseArray = prototypes.Cast<TraitPrototypeBase>().ToArray();

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());
            ser.TypeNameHandling = TypeNameHandling.All;
            ser.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            using (var stream = new StreamWriter("Attributes.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 } )
            {
                ser.Serialize(writer, baseArray);
            }

            IEnumerable<TraitPrototypeBase> output;

            using (var stream = new StreamReader("Attributes.json"))
            using (var reader = new JsonTextReader(stream))
            {
                output = ser.Deserialize<IEnumerable<TraitPrototypeBase>>(reader);
            }

            var array = output.ToArray();

            for (int i = 0; i < 8; i++)
            {
                var actual = array[i] as AttributePrototype;
                Assert.NotNull(actual);
                Assert.Equal(prototypes[i].Name, actual.Name);
                Assert.Equal(prototypes[i].ShortName, actual.ShortName);
                Assert.Equal(prototypes[i].TraitType, actual.TraitType);
                Assert.Equal(prototypes[i].Book, actual.Book);
                Assert.Equal(prototypes[i].Page, actual.Page);
                Assert.Equal(prototypes[i].SubCategory, actual.SubCategory);
            }
        }
    }
}
