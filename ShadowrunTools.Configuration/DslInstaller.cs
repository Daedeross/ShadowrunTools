using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Dsl;

namespace ShadowrunTools.Configuration
{
    public class DslInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IDslParser<>))
                    .ImplementedBy(typeof(DslParser<>))
                    .LifestyleSingleton(),
                Component.For(typeof(IDslVisitor<>))
                    .ImplementedBy(typeof(DslVisitor<>))
                    .LifestyleSingleton(),
                Component.For(typeof(IDslExpressionVisitor<>))
                    .ImplementedBy(typeof(DslExpressionVisitor<>))
                    .LifestyleSingleton(),
                Component.For(typeof(IDslAugmentVisitor<>))
                    .ImplementedBy(typeof(DslAugmentVisitor<>))
                    .LifestyleSingleton(),
                Component.For<IParserFactory>()
                    .AsFactory()
                    );
        }
    }
}
