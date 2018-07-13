using Moq;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using Xunit;
using Attribute = ShadowrunTools.Characters.Traits.Attribute;

namespace ShadowrunTools.Characters.Tests
{
    public class AttributeTests
    {
        private const string AttributeName = "Reaction";

        delegate void TryGetAttributeCallback(string name, out IMetatypeAttribute attr);

        public static IEnumerable<object[]> MetatypeTestData()
        {
            var attributes = new(int min, int max)[]
            {
                (1, 6),
                (2, 6),
                (3, 6),
                (4, 6),
                (5, 6),
                (6, 6),
                (2, 7),
                (3, 8),
                (4, 9),
                (5, 10),
                (6, 11),
                (0, 5),
                (-2, 0),
                (-1, 1),
            };

            foreach (var (min, max) in attributes)
            {
                var mock = new Mock<ICharacterMetatype>();
                mock.Setup(x => x.TryGetAttribute(It.IsAny<string>(), out It.Ref<IMetatypeAttribute>.IsAny))
                    .Callback(new TryGetAttributeCallback((string name, out IMetatypeAttribute attr) =>
                    {
                        if (name == AttributeName)
                        {
                            var attrMock = new Mock<IMetatypeAttribute>();
                            attrMock.SetupGet(x => x.Name).Returns(AttributeName);
                            attrMock.SetupGet(x => x.Min).Returns(min);
                            attrMock.SetupGet(x => x.Max).Returns(max);

                            attr = attrMock.Object;
                        }
                        else
                        {
                            attr = null;
                        }
                    }))
                    .Returns<string, IMetatypeAttribute>((name, attr) => name == AttributeName);

                yield return new object[] { mock.Object, min, max };
            }
        }

        [Theory]
        [MemberData(nameof(MetatypeTestData))]
        public void TestAttributeMinMax(ICharacterMetatype metatype, int min, int max)
        {
            var mockContainer = new Mock<ITraitContainer>();
            var mockCharacter = new Mock<ICharacter>();
            var mockRules = new Mock<IRules>();
            mockRules.SetupGet(x => x.MaxAugment)
                .Returns(4);

            var id = new Guid();

            var attribute = new Attribute(id, AttributeName, mockContainer.Object, mockCharacter.Object, metatype, mockRules.Object);

            Assert.Equal(min, attribute.Min);
            Assert.Equal(max, attribute.Max);
        }

        [Fact]
        public void TestChangeNotification()
        {
            var mockContainer = new Mock<ITraitContainer>();
            var mockCharacter = new Mock<ICharacter>();
            var mockRules = new Mock<IRules>();
            mockRules.SetupGet(x => x.MaxAugment)
                .Returns(4);

            var id = new Guid();

            var mock = new Mock<ICharacterMetatype>();

            int min = 1;
            int max = 6;

            mock.Setup(x => x.TryGetAttribute(It.IsAny<string>(), out It.Ref<IMetatypeAttribute>.IsAny))
                .Callback(new TryGetAttributeCallback((string name, out IMetatypeAttribute attr) =>
                {
                    if (name == AttributeName)
                    {
                        var attrMock = new Mock<IMetatypeAttribute>();
                        attrMock.SetupGet(x => x.Name).Returns(AttributeName);
                        attrMock.SetupGet(x => x.Min).Returns(min);
                        attrMock.SetupGet(x => x.Max).Returns(max);

                        attr = attrMock.Object;
                    }
                    else
                    {
                        attr = null;
                    }
                }))
                .Returns<string, IMetatypeAttribute>((name, attr) => name == AttributeName);

            var attribute = new Attribute(id, AttributeName, mockContainer.Object, mockCharacter.Object, mock.Object, mockRules.Object);

            var raised = Assert.Raises<ItemChangedEventArgs>(
                h => attribute.ItemChanged += h,
                h => attribute.ItemChanged -= h,
                () =>
                {
                    min = 3;
                    max = 8;
                    mock.Raise(x => x.ItemChanged += null, new ItemChangedEventArgs(new string []
                    {
                        AttributeName
                    }));
                });

            Assert.Equal(attribute, raised.Sender);

            var propNames = raised.Arguments.PropertyNames;
            Assert.Contains(nameof(ILeveledTrait.Min), propNames);
            Assert.Contains(nameof(ILeveledTrait.BaseRating), propNames);
            Assert.Contains(nameof(ILeveledTrait.ImprovedRating), propNames);
            Assert.Contains(nameof(ILeveledTrait.AugmentedRating), propNames);
            Assert.Contains(nameof(ILeveledTrait.Max), propNames);
            Assert.Contains(nameof(ILeveledTrait.AugmentedMax), propNames);
        }
    }
}
