using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization;
using ShadowrunTools.Serialization.Prototypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Serialization
{
    public class PrototypeRepositoryTests
    {
        //[Fact]
        //public void TestMergeFile()
        //{
        //    var prototypeFile1 = new PrototypeFile
        //    {
        //        Attributes = new List<AttributePrototype>
        //        {
        //            new AttributePrototype
        //            {
        //                Name = "Robert",
        //                Book = "Book",
        //                Page = 42,
        //                CustomOrder = "00",
        //                ShortName = "BOB",
        //                SubCategory = "Primary Attributes",
        //                TraitType = TraitType.Attribute
        //            }
        //        }
        //    };

        //    var prototypeFile2 = new PrototypeFile
        //    {
        //        Attributes = new List<AttributePrototype>
        //        {
        //            new AttributePrototype
        //            {
        //                Name = "Susan",
        //                Book = "Book",
        //                Page = 44,
        //                CustomOrder = "01",
        //                ShortName = "SUE",
        //                SubCategory = "Primary Attributes",
        //                TraitType = TraitType.Attribute
        //            }
        //        }
        //    };

        //    var prototypeFile3 = new PrototypeFile
        //    {
        //        Metavariants = new List<MetavariantPrototype>
        //        {
        //            new MetavariantPrototype
        //            {
        //                Name = "Human",
        //                Metatype = "Human",
        //                _Attributes = GenerateFilesTests.MakeMetaAttributes(1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 2,7),
        //            }
        //        }
        //    };

        //    var prototypeFile4 = new PrototypeFile
        //    {
        //        Metavariants = new List<MetavariantPrototype>
        //        {
        //            new MetavariantPrototype
        //            {
        //                Name = "Elf",
        //                Metatype = "Elf",
        //                _Attributes = GenerateFilesTests.MakeMetaAttributes(1,6, 2,7, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 3,8),
        //            }
        //        }
        //    };

        //    using (var mem1 = new MemoryStream())
        //    using (var mem2 = new MemoryStream())
        //    using (var mem3 = new MemoryStream())
        //    using (var mem4 = new MemoryStream())
        //    {
        //        var ser = new JsonSerializer();
        //        ser.Converters.Add(new StringEnumConverter());

        //        using (var stream = new StreamWriter(mem1, System.Text.Encoding.UTF8, 2048, true))
        //        using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
        //        {
        //            ser.Serialize(writer, prototypeFile1);
        //        }

        //        using (var stream = new StreamWriter(mem2, System.Text.Encoding.UTF8, 2048, true))
        //        using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
        //        {
        //            ser.Serialize(writer, prototypeFile2);
        //        }

        //        using (var stream = new StreamWriter(mem3, System.Text.Encoding.UTF8, 2048, true))
        //        using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
        //        {
        //            ser.Serialize(writer, prototypeFile3);
        //        }

        //        using (var stream = new StreamWriter(mem4, System.Text.Encoding.UTF8, 2048, true))
        //        using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
        //        {
        //            ser.Serialize(writer, prototypeFile4);
        //        }

        //        var loader = new DataLoader(ser, new Castle.Core.Logging.NullLogger());

        //        //loader.CurrentFiles =
        //    }
        //}
    }
}
