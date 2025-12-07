using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Dsl;
using System;

namespace ShadowrunTools.Characters.Tests.Mocks
{
    public class ParserFactory : IParserFactory
    {
        public void Release(object parser)
        {
            throw new NotImplementedException();
        }

        IDslParser<T> IParserFactory.Create<T>()
        {
            var expVisitor = new DslExpressionVisitor<T>();
            var augVisitor = new DslAugmentVisitor<T>(expVisitor);
            var visitor = new DslVisitor<T>(expVisitor, augVisitor);

            return new DslParser<T>(visitor);
        }
    }
}
