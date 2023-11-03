using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Configuration
{
    public class CharacterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<CharacterFactory>()
                    .BasedOn<IFactory>()
                    .WithServiceFromInterface()
                    .LifestyleSingleton(),
                Component.For(typeof(IAugmentFactory<>))
                    .ImplementedBy(typeof(IAugmentFactory<>))
                    .LifestyleSingleton(),
                Classes.FromAssemblyContaining<BaseTrait>()
                    .BasedOn<ITrait>()
                    .WithServiceFromInterface()
                    .LifestyleTransient()
                );
        }
    }
}
