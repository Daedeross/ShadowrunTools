using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Configuration
{
    public class ViewModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyNamed("ShadowrunTools.Characters.ViewModels")
                        .BasedOn<IViewModel>()
                        .WithServiceDefaultInterfaces()
                        .LifestyleTransient(),
                Component.For<IViewModelFactory>()
                        .AsFactory()
                    );
        }
    }
}
