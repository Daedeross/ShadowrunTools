namespace ShadowrunTools.Characters.Tests
{
    using Moq;
    using ShadowrunTools.Characters.Contract;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class BaseTraitTests
    {
        internal class TestBaseTrait : BaseTrait
        {
            public TestBaseTrait(Guid id, ITraitContainer container, ICategorizedTraitContainer root)
                : base(id, "Hai", container, root)
            {
            }
        }

        [Fact]
        public void ConstructorTest()
        {
            var rootMock = new Mock<ICategorizedTraitContainer>().Object;
            var ownerMock = new Mock<ITraitContainer>().Object;
            var id = Guid.NewGuid();

            var trait = new TestBaseTrait(id, ownerMock, rootMock);
            Assert.Throws<ArgumentNullException>(() => new TestBaseTrait(id, null, rootMock));
            Assert.Throws<ArgumentNullException>(() => new TestBaseTrait(id, ownerMock, null));
        }
    }
}
