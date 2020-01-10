using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization.Prototypes;
using ShadowrunTools.Serialization.Prototypes.Priorities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ShadowrunTools.Characters.Tests.Serialization
{
    public class GenerateFilesTests
    {
        internal static List<AttributePrototype> MakeAttributes()
        {
            var prototypes = Enumerable.Range(0, 11).Select(
                i => new AttributePrototype
                {
                    TraitType = TraitType.Attribute,
                    Book = "SR5",
                    Page = 51,
                }).ToArray();

            prototypes[0].Name = "Body";
            prototypes[0].ShortName = "BOD";
            prototypes[0].CustomOrder = "00";
            prototypes[0].SubCategory = "Physical Attributes";
            prototypes[1].Name = "Agility";
            prototypes[1].ShortName = "AGI";
            prototypes[1].CustomOrder = "01";
            prototypes[1].SubCategory = "Physical Attributes";
            prototypes[2].Name = "Reaction";
            prototypes[2].ShortName = "REA";
            prototypes[2].CustomOrder = "02";
            prototypes[2].SubCategory = "Physical Attributes";
            prototypes[3].Name = "Strength";
            prototypes[3].ShortName = "STR";
            prototypes[3].CustomOrder = "03";
            prototypes[3].SubCategory = "Physical Attributes";
            prototypes[4].Name = "Willpower";
            prototypes[4].ShortName = "WIL";
            prototypes[4].CustomOrder = "10";
            prototypes[4].SubCategory = "Mental Attributes";
            prototypes[5].Name = "Logic";
            prototypes[5].ShortName = "LOG";
            prototypes[5].CustomOrder = "11";
            prototypes[5].SubCategory = "Mental Attributes";
            prototypes[6].Name = "Intuition";
            prototypes[6].ShortName = "INT";
            prototypes[6].CustomOrder = "12";
            prototypes[6].SubCategory = "Mental Attributes";
            prototypes[7].Name = "Charisma";
            prototypes[7].ShortName = "CHA";
            prototypes[7].CustomOrder = "13";
            prototypes[7].SubCategory = "Mental Attributes";
            prototypes[8].Name = "Edge";
            prototypes[8].ShortName = "EDG";
            prototypes[8].CustomOrder = "20";
            prototypes[8].SubCategory = "Special Attributes";
            prototypes[9].Name = "Magic";
            prototypes[9].ShortName = "MAG";
            prototypes[9].CustomOrder = "21";
            prototypes[9].SubCategory = "Special Attributes";
            prototypes[10].Name = "Resonance";
            prototypes[10].ShortName = "RES";
            prototypes[10].CustomOrder = "22";
            prototypes[10].SubCategory = "Special Attributes";

            return prototypes.ToList();
        }

        internal static MetatypeAttributePrototype MakeMetaAttribute(string name, int min, int max)
        {
            return new MetatypeAttributePrototype
            {
                Name = name, Min = min, Max = max
            };
        }

        internal static List<MetatypeAttributePrototype> MakeMetaAttributes(
            int b1, int b2,
            int a1, int a2,
            int r1, int r2,
            int s1, int s2,
            int w1, int w2,
            int l1, int l2,
            int i1, int i2,
            int c1, int c2,
            int e1, int e2)
        {
            return new List<MetatypeAttributePrototype>
            {
                MakeMetaAttribute("Body", b1, b2),
                MakeMetaAttribute("Agility", a1, a2),
                MakeMetaAttribute("Reaction", r1, r2),
                MakeMetaAttribute("Strength", s1, s2),
                MakeMetaAttribute("Willpower", w1, w2),
                MakeMetaAttribute("Logic", l1, l2),
                MakeMetaAttribute("Intuition", i1, i2),
                MakeMetaAttribute("Charisma", c1, c2),
                MakeMetaAttribute("Edge", e1, e2)
            };
        }

        private List<MetavariantPrototype> MakeMetatypes()
        {
            var prototypes = new List<MetavariantPrototype>
            {
                new MetavariantPrototype
                {
                    Name = "Human",
                    Metatype = "Human",
                    _Attributes = MakeMetaAttributes(1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 2,7),
                },
                new MetavariantPrototype
                {
                    Name = "Elf",
                    Metatype = "Elf",
                    _Attributes = MakeMetaAttributes(1,6, 2,7, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 3,8),
                }
            };

            return prototypes;
        }

        [Fact]
        public void GenerateAttributesFile()
        {
            var prototypes = MakeAttributes();

            var baseArray = prototypes.Cast<TraitPrototypeBase>().ToArray();
            var prototypeFile = new PrototypeFile
            {
                Attributes = prototypes.ToList(),
            };

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());

            using (var stream = new StreamWriter("Attributes.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 } )
            {
                ser.Serialize(writer, prototypeFile);
            }

            PrototypeFile output;

            using (var stream = new StreamReader("Attributes.json"))
            using (var reader = new JsonTextReader(stream))
            {
                output = ser.Deserialize<PrototypeFile>(reader);
            }

            var array = output.Attributes.ToArray();

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

        [Fact]
        public void GenerateMetatypesFile()
        {
            var prototypes = MakeMetatypes();
            var prototypeFile = new PrototypeFile
            {
                Metavariants = prototypes.ToList(),
            };

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());

            using (var stream = new StreamWriter("Metatypes.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
            {
                ser.Serialize(writer, prototypeFile);
            }

            PrototypeFile output;

            using (var stream = new StreamReader("Metatypes.json"))
            using (var reader = new JsonTextReader(stream))
            {
                output = ser.Deserialize<PrototypeFile>(reader);
            }
        }

        [Fact]
        public void GeneratePrioritiesFile()
        {
            var priorities = new PrioritiesPrototype
            {
                AttributesPrototypes = new Dictionary<PriorityLevel, AttributesPriorityPrototype>
                {
                    [PriorityLevel.A] = new AttributesPriorityPrototype { AttibutePoints = 24 },
                    [PriorityLevel.B] = new AttributesPriorityPrototype { AttibutePoints = 20 },
                    [PriorityLevel.C] = new AttributesPriorityPrototype { AttibutePoints = 16 },
                    [PriorityLevel.D] = new AttributesPriorityPrototype { AttibutePoints = 14 },
                    [PriorityLevel.E] = new AttributesPriorityPrototype { AttibutePoints = 12 },
                },
                MetatypePrototypes = new Dictionary<PriorityLevel, MetatypePriorityPrototype>
                {
                    [PriorityLevel.A] = new MetatypePriorityPrototype
                    {
                        _MetavariantOptions = new List<PriorityMetavariantOptionPrototype>
                        {
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Human",
                                SpecialAttributePoints = 9
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Elf",
                                SpecialAttributePoints = 8
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Dwarf",
                                SpecialAttributePoints = 7
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Ork",
                                SpecialAttributePoints = 7
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Troll",
                                SpecialAttributePoints = 5
                            }
                        }
                    },
                    [PriorityLevel.B] = new MetatypePriorityPrototype
                    {
                        _MetavariantOptions = new List<PriorityMetavariantOptionPrototype>
                        {
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Human",
                                SpecialAttributePoints = 7
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Elf",
                                SpecialAttributePoints = 6
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Dwarf",
                                SpecialAttributePoints = 4
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Ork",
                                SpecialAttributePoints = 4
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Troll",
                                SpecialAttributePoints = 0
                            }
                        }
                    },
                    [PriorityLevel.C] = new MetatypePriorityPrototype
                    {
                        _MetavariantOptions = new List<PriorityMetavariantOptionPrototype>
                        {
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Human",
                                SpecialAttributePoints = 5
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Elf",
                                SpecialAttributePoints = 3
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Dwarf",
                                SpecialAttributePoints = 1
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Ork",
                                SpecialAttributePoints = 0
                            },
                        }
                    },
                    [PriorityLevel.D] = new MetatypePriorityPrototype
                    {
                        _MetavariantOptions = new List<PriorityMetavariantOptionPrototype>
                        {
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Human",
                                SpecialAttributePoints = 3
                            },
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Elf",
                                SpecialAttributePoints = 1
                            },
                        }
                    },
                    [PriorityLevel.E] = new MetatypePriorityPrototype
                    {
                        _MetavariantOptions = new List<PriorityMetavariantOptionPrototype>
                        {
                            new PriorityMetavariantOptionPrototype
                            {
                                Metavariant = "Human",
                                SpecialAttributePoints = 1
                            },
                        }
                    }
                },
                ResourcesPrototypes = new Dictionary<PriorityLevel, ResourcesPriorityPrototype>
                {
                    [PriorityLevel.A] = new ResourcesPriorityPrototype { Resources = 450000 },
                    [PriorityLevel.B] = new ResourcesPriorityPrototype { Resources = 275000 },
                    [PriorityLevel.C] = new ResourcesPriorityPrototype { Resources = 140000 },
                    [PriorityLevel.D] = new ResourcesPriorityPrototype { Resources = 50000 },
                    [PriorityLevel.E] = new ResourcesPriorityPrototype { Resources = 6000 },
                },
                SkillsPrototypes = new Dictionary<PriorityLevel, SkillsPriorityPrototype>
                {
                    [PriorityLevel.A] = new SkillsPriorityPrototype
                    {
                        SkillGroupPoints = 10,
                        SkillPoints = 46
                    },
                    [PriorityLevel.B] = new SkillsPriorityPrototype
                    {
                        SkillGroupPoints = 5,
                        SkillPoints = 36
                    },
                    [PriorityLevel.C] = new SkillsPriorityPrototype
                    {
                        SkillGroupPoints = 2,
                        SkillPoints = 28
                    },
                    [PriorityLevel.D] = new SkillsPriorityPrototype
                    {
                        SkillPoints = 22
                    },
                    [PriorityLevel.E] = new SkillsPriorityPrototype
                    {
                        SkillPoints = 18
                    },
                },
                SpecialsPrototyes = new Dictionary<PriorityLevel, SpecialsPriorityPrototype>
                {
                    [PriorityLevel.A] = new SpecialsPriorityPrototype
                    {
                        _Options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Magician",
                                AttributeName = "Magic",
                                AttributeRating = 6,
                                FreeSpells = 10,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 2,
                                        Rating = 5,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Mystic Adept",
                                AttributeName = "Magic",
                                AttributeRating = 6,
                                FreeSpells = 10,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 2,
                                        Rating = 5,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Technomancer",
                                AttributeName = "Resonance",
                                AttributeRating = 6,
                                FreeComplexForms = 5,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Resonance",
                                        Count = 2,
                                        Rating = 5,
                                        Kind = SkillChoiceKind.Category
                                    }
                                }
                            }
                        }
                    },
                    [PriorityLevel.B] = new SpecialsPriorityPrototype
                    {
                        _Options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Magician",
                                AttributeName = "Magic",
                                AttributeRating = 4,
                                FreeSpells = 7,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 2,
                                        Rating = 4,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Mystic Adept",
                                AttributeName = "Magic",
                                AttributeRating = 4,
                                FreeSpells = 7,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 2,
                                        Rating = 4,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Technomancer",
                                AttributeName = "Resonance",
                                AttributeRating = 4,
                                FreeComplexForms = 2,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Resonance",
                                        Count = 2,
                                        Rating = 4,
                                        Kind = SkillChoiceKind.Category
                                    }
                                }
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Adept",
                                AttributeName = "Magic",
                                AttributeRating = 6,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Active",
                                        Count = 1,
                                        Rating = 4,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Aspected Magician",
                                AttributeName = "Magic",
                                AttributeRating = 5,
                                _SkillGroupOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 1,
                                        Rating = 4,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                        }
                    },
                    [PriorityLevel.C] = new SpecialsPriorityPrototype
                    {
                        _Options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Magician",
                                AttributeName = "Magic",
                                AttributeRating = 3,
                                FreeSpells = 5
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Mystic Adept",
                                AttributeName = "Magic",
                                AttributeRating = 3,
                                FreeSpells = 5
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Technomancer",
                                AttributeName = "Resonance",
                                AttributeRating = 3,
                                FreeComplexForms = 1
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Adept",
                                AttributeName = "Magic",
                                AttributeRating = 4,
                                _SkillOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Active",
                                        Count = 1,
                                        Rating = 2,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Aspected Magician",
                                AttributeName = "Magic",
                                AttributeRating = 3,
                                _SkillGroupOptions = new List<SpecialSkillChoicePrototype>
                                {
                                    new SpecialSkillChoicePrototype
                                    {
                                        Choice = "Magical",
                                        Count = 1,
                                        Rating = 2,
                                        Kind = SkillChoiceKind.Category
                                    }
                                },
                            },
                        }
                    },
                    [PriorityLevel.D] = new SpecialsPriorityPrototype
                    {
                        _Options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Adept",
                                AttributeName = "Magic",
                                AttributeRating = 2
                            },
                            new SpecialOptionPrototype
                            {
                                Quality = "Aspected Magician",
                                AttributeName = "Magic",
                                AttributeRating = 2
                            },
                        }
                    }
                }
            };

            var prototypeFile = new PrototypeFile
            {
                Priorities = priorities
            };

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());

            using (var stream = new StreamWriter("Priorities.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
            {
                ser.Serialize(writer, prototypeFile);
            }

            PrototypeFile output;

            using (var stream = new StreamReader("Priorities.json"))
            using (var reader = new JsonTextReader(stream))
            {
                output = ser.Deserialize<PrototypeFile>(reader);
            }
        }
    }
}
