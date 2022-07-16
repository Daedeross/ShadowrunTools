using ShadowrunTools.Characters.Model;
using System.Linq.Expressions;

namespace ShadowrunTools.Dsl
{
    public interface IVisitorFactory
    {
        ICharacterBuilderVisitor<Payload<Expression>> ForExpression<T>();

        ICharacterBuilderVisitor<IntermediateParsedAugment<T>> ForAugment<T>();

        ICharacterBuilderVisitor<Result<T>> Create<T>();

        void Release<T>(T obj);
    }
}
