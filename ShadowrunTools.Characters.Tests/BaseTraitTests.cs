using Moq;
using ShadowrunTools.Characters.Model;
using ShadowrunTools.Characters.Traits;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShadowrunTools.Characters.Tests
{
    public class BaseTraitTests
    {
        internal class TestBaseTrait : BaseTrait
        {
            public TestBaseTrait(Guid id, int prototypeHash, ITraitContainer container, ICategorizedTraitContainer root, IRules rules)
                : base(id, prototypeHash, "Trait", "Hai", container, root, rules)
            {
            }

            public override TraitType TraitType => throw new NotImplementedException();

            public override bool Independant => throw new NotImplementedException();
        }

        [Fact]
        public void ConstructorTest()
        {
            var rootMock = new Mock<ICategorizedTraitContainer>().Object;
            var ownerMock = new Mock<ITraitContainer>().Object;
            var rulesMock = new Mock<IRules>().Object;
            var hash = 42;

            var id = Guid.NewGuid();

            var trait = new TestBaseTrait(id, hash, ownerMock, rootMock, rulesMock);
            Assert.Throws<ArgumentNullException>(() => new TestBaseTrait(id, hash, null, rootMock, rulesMock));
            Assert.Throws<ArgumentNullException>(() => new TestBaseTrait(id, hash, ownerMock, null, rulesMock));
        }
    }
}
