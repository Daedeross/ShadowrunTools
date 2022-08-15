using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Contract.Model;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Serialization.Prototypes;
using ShadowrunTools.Serialization.Prototypes.Priorities;
using System;
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

            prototypes[0].Id = new Guid("972c38e9-c2dd-4693-b9d5-2298fedf0158");
            prototypes[0].Name = "Body";
            prototypes[0].ShortName = "BOD";
            prototypes[0].CustomOrder = "00";
            prototypes[0].SubCategory = "Physical Attributes";
            prototypes[0].Id = new Guid("1808c09d-e8f8-4972-ae4d-0ff7cae00d5e");
            prototypes[1].Name = "Agility";
            prototypes[1].ShortName = "AGI";
            prototypes[1].CustomOrder = "01";
            prototypes[1].SubCategory = "Physical Attributes";
            prototypes[0].Id = new Guid("0cd0c66d-efe8-4c34-8d2b-e75e278b6e98");
            prototypes[2].Name = "Reaction";
            prototypes[2].ShortName = "REA";
            prototypes[2].CustomOrder = "02";
            prototypes[2].SubCategory = "Physical Attributes";
            prototypes[0].Id = new Guid("e8207222-ab11-4d3d-a304-c7911711af63");
            prototypes[3].Name = "Strength";
            prototypes[3].ShortName = "STR";
            prototypes[3].CustomOrder = "03";
            prototypes[3].SubCategory = "Physical Attributes";
            prototypes[0].Id = new Guid("68200af9-9f70-4bee-b275-079290d6a026");
            prototypes[4].Name = "Willpower";
            prototypes[4].ShortName = "WIL";
            prototypes[4].CustomOrder = "10";
            prototypes[4].SubCategory = "Mental Attributes";
            prototypes[0].Id = new Guid("9f986025-2cf6-4663-a94d-1f022753df56");
            prototypes[5].Name = "Logic";
            prototypes[5].ShortName = "LOG";
            prototypes[5].CustomOrder = "11";
            prototypes[5].SubCategory = "Mental Attributes";
            prototypes[0].Id = new Guid("ec7e03f1-7bfe-4763-9ec9-d72291f2f2e1");
            prototypes[6].Name = "Intuition";
            prototypes[6].ShortName = "INT";
            prototypes[6].CustomOrder = "12";
            prototypes[6].SubCategory = "Mental Attributes";
            prototypes[0].Id = new Guid("fa5631da-b680-47ab-83a4-ef5149d7d729");
            prototypes[7].Name = "Charisma";
            prototypes[7].ShortName = "CHA";
            prototypes[7].CustomOrder = "13";
            prototypes[7].SubCategory = "Mental Attributes";
            prototypes[0].Id = new Guid("f473fd89-a4a6-46dd-85a6-6233957035b6");
            prototypes[8].Name = "Edge";
            prototypes[8].ShortName = "EDG";
            prototypes[8].CustomOrder = "20";
            prototypes[8].SubCategory = "Special Attributes";
            prototypes[0].Id = new Guid("100ceb2b-b424-41dc-a5ec-926aa3fd42e5");
            prototypes[9].Name = "Magic";
            prototypes[9].ShortName = "MAG";
            prototypes[9].CustomOrder = "21";
            prototypes[9].SubCategory = "Special Attributes";
            prototypes[0].Id = new Guid("b14750c5-6804-43ae-aa44-723b3f534773");
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
                Name = name,
                Min = min,
                Max = max
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
                    Id = new Guid("e16218e4-670d-44b4-80ec-c1384e401385"),
                    Name = "Human",
                    Metatype = "Human",
                    _attributes = MakeMetaAttributes(1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 2,7),
                },
                new MetavariantPrototype
                {
                    Id = new Guid("918ed7cc-85ba-4ccf-bf09-44b7bd48d5f8"),
                    Name = "Elf",
                    Metatype = "Elf",
                    _attributes = MakeMetaAttributes(1,6, 2,7, 1,6, 1,6, 1,6, 1,6, 1,6, 1,6, 3,8),
                },
                new MetavariantPrototype
                {
                    Id = new Guid("46ed84d0-4fa7-4226-b1ac-82aedc79c866"),
                    Name = "Dwarf",
                    Metatype = "Dwarf",
                    _attributes = MakeMetaAttributes(3,8, 1,6, 1,5, 3,8, 2,7, 1,6, 1,6, 1,6, 1,6),
                },
                new MetavariantPrototype
                {
                    Id = new Guid("facce70e-9ba9-46d7-8dca-504d3af89300"),
                    Name = "Ork",
                    Metatype = "Ork",
                    _attributes = MakeMetaAttributes(3,8, 1,6, 1,6, 3,8, 1,6, 1,5, 1,6, 1,6, 1,6),
                },
                new MetavariantPrototype
                {
                    Id = new Guid("8b2fa92d-16f3-4c63-96e4-c77afa074205"),
                    Name = "Troll",
                    Metatype = "Troll",
                    _attributes = MakeMetaAttributes(5,10, 1,5, 1,6, 5,10, 1,6, 1,5, 1,5, 1,4, 1,6),
                }
            };

            return prototypes;
        }

        [Fact]
        public void GenerateAttributesFile()
        {
            var prototypes = MakeAttributes();

            var prototypeFile = new PrototypeFile
            {
                Attributes = prototypes.ToList(),
            };

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());

            using (var stream = new StreamWriter("Attributes.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
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
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>
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
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>
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
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>
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
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>
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
                        _metavariantOptions = new List<PriorityMetavariantOptionPrototype>
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
                        _options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Magician",
                                AttributeName = "Magic",
                                AttributeRating = 6,
                                FreeSpells = 10,
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                        _options = new List<SpecialOptionPrototype>
                        {
                            new SpecialOptionPrototype
                            {
                                Quality = "Magician",
                                AttributeName = "Magic",
                                AttributeRating = 4,
                                FreeSpells = 7,
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillGroupOptions = new List<SpecialSkillChoicePrototype>
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
                        _options = new List<SpecialOptionPrototype>
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
                                _skillOptions = new List<SpecialSkillChoicePrototype>
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
                                _skillGroupOptions = new List<SpecialSkillChoicePrototype>
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
                        _options = new List<SpecialOptionPrototype>
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

        private SkillPrototype MakeSkill(SkillType type, Guid id, string name, int page, string group, string subCat, bool trained, string attribute, string limit, params string[] spec)
        {
            return new SkillPrototype
            {
                Id = id,
                Name = name,
                Book = Books.Core,
                Page = page,
                GroupName = group,
                SkillType = type,
                SubCategory = subCat,
                TrainedOnly = trained,
                TraitType = TraitType.Skill,
                UsualLimit = limit,
                LinkedAttribute = attribute,
                Specializations = spec.ToList()
            };
        }

        private List<SkillPrototype> MakeActiveSkills()
        {
            return new List<SkillPrototype>
            {
                // Combat Active
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Archery", 130, null, "Combat Active", false, "Agility",null, "Bow", "Crossbow", "Non-Standard Ammunition", "Slingshot"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Automatics", 130, "Firearms", "Combat Active", false, "Agility",null, "Assault Rifles", "Cyber-Implant", "Machine Pistols", "Submachine Guns"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Blades", 130, "Close Combat", "Combat Active", false, "Agility",null, "Axes", "Knives", "Swords", "Parrying"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Clubs", 131, "Close Combat", "Combat Active", false, "Agility",null, "Batons", "Hammers", "Saps", "Staves", "Parrying"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Heavy Weapons", 132, null, "Combat Active", false, "Agility",null, "Assault Cannons", "Grenade Launchers", "Guided Missiles", "Machine Guns", "Rocket Launchers"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Longarms", 132, "Firearms", "Combat Active", false, "Agility",null, "Extended-Range Shots", "Long-Range Shots", "Shotguns", "Sniper Rifles"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Pistols", 132, "Firearms", "Combat Active", false, "Agility",null, "Holdouts", "Revolvers", "Semi-Automatics", "Tasers"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Throwing Weapons", 132, null, "Combat Active", false, "Agility",null, "Aerodynamic", "Blades", "Non-Aerodynamic"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Unarmed Combat", 132, "Close Combat", "Combat Active", false, "Agility",null, "Blocking", "Cyber Implants", "Subduing Combat"),
                // Physical Active
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Disguise",      133, "Stealth",   "Physical Active", false, "Intuition", "Mental",   "Camouflage", "Cosmetic", "Theatrical", "Trideo & Video"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Diving",        133, null,        "Physical Active", false, "Body",      "Physical", "Liquid Breathing Apparatus", "Mixed Gas", "Oxygen Extraction", "SCUBA", "Arctic", "Cave", "Commercial", "Millitary"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Escape Artist", 133, null,        "Physical Active", false, "Agility",   "Physical", "Contorionism", "Cuffs", "Ropes", "Zip Ties"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Free-Fall",     133, null,        "Physical Active", false, "Body",      "Physical", "BASE Jumping", "Break-Fall", "Bungee", "HALO", "Low Altitude", "Parachute", "Static Line", "Wingsuit", "Zipline"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Gymnastics",    133, "Athletics", "Physical Active", false, "Agility",   "Physical", "Balance", "Climbing", "Dance", "Leaping", "Parkour", "Rolling"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Palming",       133, "Stealth",   "Physical Active", true , "Agility",   "Physical", "Legerdemain", "Pickpocket", "Pilfering"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Perception",    133, null,        "Physical Active", false, "Intuition", "Mental",   "Hearing", "Scent", "Searching", "Taste", "Touch", "Visual"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Running",       133, "Athletics", "Physical Active", false, "Strength",  "Physical", "Distance, Sprinting", "Desert", "Forest", "Jungle", "Mountain", "Polar", "Urban"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Sneaking",      133, "Stealth",   "Physical Active", false, "Agility",   "Physical", "Desert", "Forest", "Jungle", "Mountain", "Polar", "Urban"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Survival",      133, "Outdoors",  "Physical Active", false, "Willpower", "Mental",   "Desert", "Forest", "Jungle", "Mountain", "Polar", "Urban"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Swimming",      134, "Athletics", "Physical Active", false, "Strength",  "Physical", "Dash", "Long Dostance"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Tracking",      134, "Outdoors",  "Physical Active", false, "Intuition", "Mental",   "Desert", "Forest", "Jungle", "Mountain", "Polar", "Urban"),
                // Social Active
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Con"          , 138, "Acting"   , "Social", false, "Charisma", "Social", "Fast Talking", "Seduction"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Etiquette"    , 138, "Influence", "Social", false, "Charisma", "Social", "Corporate", "High Society", "Media", "Mercenary", "Street", "Yakuza"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Impersonation", 138, "Acting"   , "Social", false, "Charisma", "Social", "Dwarf", "Elf", "Human", "Ork", "Troll"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Instruction"  , 138, null       , "Social", false, "Charisma", "Social", "Combat", "Physical", "Technical", "Resonance", "Social", "Language", "Magical", "Academic Knowledge", "Street Knowledge"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Intimidation" , 139, null       , "Social", false, "Charisma", "Social", "Interrogation", "Mental", "Physical", "Torture"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Leadership"   , 139, "Influence", "Social", false, "Charisma", "Social", "Command", "Direct", "Inspire", "Rally"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Negotiation"  , 139, "Influence", "Social", false, "Charisma", "Social", "Bargaining", "Contracts", "Diplomacy"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Performance"  , 139, "Acting"   , "Social", false, "Charisma", "Social", "Presentation", "Acting", "Comedy"),
                // Technical Active
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Aeronautics Mechanic", 143, "Engineering", "Technical", true , "Logic"    , "Mental", "Aerospace", "Fixed Wing", "LTA (blimp)", "Rotary Wing", "Tilt Wing", "Vector Thrust"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Animal Handling"     , 143, null         , "Technical", false, "Charisma" , "Mental", "Cat", "Bird", "Hell Hound", "Horse", "Dolphin"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Armorer"             , 143, null         , "Technical", false, "Logic"    , "Mental", "Armor", "Artillery", "Explosives", "Firearms", "Melee Weapons", "Heavy Weapons", "Weapon Accessories"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Artisan"             , 143, null         , "Technical", true , "Intuition", "Mental", "Cooking", "Sculpting", "Drawing", "Carpentry", "Fashion", "Painting"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Automotive Mechanic" , 143, "Engineering", "Technical", true , "Logic"    , "Mental", "Walker", "Hover", "Tracked", "Wheeled"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Biotechnology"       , 144, null         , "Technical", true , "Logic"    , "Mental", "Bioinformatics", "Bioware", "Cloning", "Gene Therapy", "Vat Maintenance"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Chemistry"           , 144, null         , "Technical", true , "Logic"    , "Mental", "Analytical", "Biochemistry", "Inorganic", "Organic", "Physical"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Computer"            , 144, "Electronics", "Technical", false, "Logic"    , "Mental", "Edit File", "Matrix Perception", "Matrix Search"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Cybercombat"         , 144, "Cracking"   , "Technical", false, "Logic"    , "Mental", "Devices", "Grids", "IC", "Personas", "Sprites"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Cybertechnology"     , 144, "Biotech"    , "Technical", true , "Logic"    , "Mental", "Bodyware", "Cyberlimbs", "Headware", "Repair"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Demolitions"         , 144, null         , "Technical", false, "Logic"    , "Mental", "Commercial Explosives", "Defusing", "Improvised Explosives", "Plastic Explosives"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Electronic Warfare"  , 144, "Cracking"   , "Technical", true , "Logic"    , "Mental", "Communications", "Encryption", "Jamming", "Sensor Operations"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "First Aid"           , 144, "Biotech"    , "Technical", false, "Logic"    , "Mental", "Gunshot Wounds", "Resuscitation", "Broken Bones", "Burns", "Concussions"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Forgery"             , 144, null         , "Technical", false, "Logic"    , "Mental", "Counterfeiting", "Credstick Forgery", "False ID", "Image Doctoring", "Paper Forgery"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Hacking"             , 145, "Cracking"   , "Technical", false, "Logic"    , "Mental", "Devices", "Files", "Hosts", "Personas"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Hardware"            , 145, "Electronics", "Technical", true , "Logic"    , "Mental", "Commlinks", "Cyberdecks", "Smartguns", "RCCs"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Industrial Mechanic" , 145, "Engineering", "Technical", true , "Logic"    , "Mental", "Electrical Power Systems", "Hydraulics", "HVAC", "Industrial Robotics", "Structural", "Welding"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Locksmith"           , 145, null         , "Technical", true , "Agility"  , "Mental", "Combination", "Keypad", "Maglock", "Tumbler", "Voice Recognition"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Medicine"            , 145, "Biotech"    , "Technical", true , "Logic"    , "Mental", "Cosmetic Surgery", "Extended Care", "Implant Surgery", "Magical Health", "Organ Culture", "Trauma Surgery"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Nautical Mechanic"   , 145, "Engineering", "Technical", true , "Logic"    , "Mental", "Motorboat", "Sailboat", "Ship", "Submarine"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Navigation"          , 145, "Outdoors"   , "Technical", false, "Logic"    , "Mental", "Augmented Reality Markers", "Celestial", "Compass", "Maps", "GPS"),
                MakeSkill(SkillType.Active, Guid.NewGuid(), "Software"            , 145, "Electronics", "Technical", true , "Logic"    , "Mental", "Data Bombs"),
            };
        }

        [Fact]
        public void GenerateSkillsFile()
        {
            var prototypes = MakeActiveSkills();
            var prototypeFile = new PrototypeFile
            {
                Skills = prototypes.ToList(),
            };

            var ser = new JsonSerializer();
            ser.Converters.Add(new StringEnumConverter());

            using (var stream = new StreamWriter("Skills.json"))
            using (var writer = new JsonTextWriter(stream) { Formatting = Formatting.Indented, Indentation = 2 })
            {
                ser.Serialize(writer, prototypeFile);
            }

            PrototypeFile output;

            using (var stream = new StreamReader("Skills.json"))
            using (var reader = new JsonTextReader(stream))
            {
                output = ser.Deserialize<PrototypeFile>(reader);
            }
        }
    }
}
