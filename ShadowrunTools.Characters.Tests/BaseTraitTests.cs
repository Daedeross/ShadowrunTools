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
            public TestBaseTrait(ITraitContainer container, ICategorizedTraitContainer root) : base(container, root)
            {
            }
        }

        [Fact]
        public void ConstructorTest()
        {
            var rootMock = new Mock<ICategorizedTraitContainer>().Object;
            var ownerMock = new Mock<ITraitContainer>().Object;

            //var trait = new BaseTrait(ownerMock, rootMock);
            //Assert.Throws<ArgumentNullException>(() => new BaseTrait(null, rootMock));
            //Assert.Throws<ArgumentNullException>(() => new BaseTrait(ownerMock, null));
        }
    }
}
