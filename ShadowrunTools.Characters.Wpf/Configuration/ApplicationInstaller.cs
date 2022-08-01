using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.Factories;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Traits;
using ShadowrunTools.Characters.ViewModels;
using ShadowrunTools.Characters.Wpf.ViewModel;
using ShadowrunTools.Dsl;
using ShadowrunTools.Foundation;
using ShadowrunTools.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunTools.Characters.Wpf.Configuration
{
    public class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var propInjector = container.Kernel.ComponentModelBuilder
                         .Contributors
                         .OfType<PropertiesDependenciesModelInspector>()
                         .Single();
            container.Kernel.ComponentModelBuilder.RemoveContributor(propInjector);

            container.AddFacility<TypedFactoryFacility>();

            container.AddFacility<LoggingFacility>(f => f.LogUsing<NullLogFactory>());

            container.Register(
                Classes.FromAssemblyNamed("ShadowrunTools.Characters.ViewModels")
                    .BasedOn<IViewModel>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),
                Component.For<IViewModelFactory>()
                    .AsFactory());

            //container.Register(
            //    Component.For<IRoller>()
            //        .ImplementedBy<Roller>()
            //        .LifestyleSingleton());

            container.Register(
                Classes.FromAssemblyContaining<CharacterFactory>()
                    .BasedOn<IFactory>()
                    .WithServiceFromInterface()
                    .LifestyleSingleton(),
                Component.For(typeof(IDslParser<>))
                    .ImplementedBy(typeof(DslParser<>))
                    .LifestyleSingleton(),
                Component.For(typeof(IAugmentFactory<>))
                    .ImplementedBy(typeof(IAugmentFactory<>))
                    .LifestyleSingleton(),
                Classes.FromAssemblyContaining<BaseTrait>()
                    .BasedOn<ITrait>()
                    .WithServiceFromInterface()
                    .LifestyleTransient()
                );

            container.Register(
                Classes.FromAssemblyContaining<MainWindow>()
                    .BasedOn(typeof(ReactiveUI.IViewFor<>))
                    .WithServiceSelf()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient(),
                Component.For<IViewFactory>()
                    .AsFactory(),
                Component.For<ReactiveUI.IViewLocator>()
                    .ImplementedBy<WindsorViewLocator>()
                    .Named(nameof(WindsorViewLocator)),
                Component.For<IViewContainer>()
                    .ImplementedBy<ViewContainer>()
                    .LifestyleTransient(),
                Component.For<DisplaySettings>()
                    .ImplementedBy<DisplaySettings>()
                    .LifestyleSingleton());

            // TODO: The following should eventually be scoped to a settings file or the like.
            container.Register(
                Component.For<IDataLoader>()        // TODO: rplace with named dependency based on settings file
                    .ImplementedBy<DataLoader>(),
                Component.For<IRules>()             // TODO: replace with loading of settings
                    .UsingFactoryMethod(kernel =>
                    {
                        var pl = PropertyFactory.CreateFromObject(new RulesPrototype(), false);
                        var rules = new GameRules();
                        rules.CommitEdit(pl);

                        return rules;
                    })
                    .LifestyleSingleton(),
                Component.For<IPriorities>()
                    .UsingFactoryMethod(kernel =>
                    {
                        var loader = kernel.Resolve<IDataLoader>();
                        var repo = loader.ReloadAll();
                        kernel.ReleaseComponent(loader);

                        return repo.Priorities;
                    }));

            container.Register(
                Classes.FromAssemblyContaining<MainWindow>()
                    .BasedOn<ReactiveUI.IBindingTypeConverter>()
                    .WithServiceBase());

            container.Register(
                Classes.FromAssemblyContaining<ReactiveUI.CommandBinderImplementation>()
                    .IncludeNonPublicTypes()
                    .BasedOn<ReactiveUI.CommandBinderImplementation>()
                    .WithServiceDefaultInterfaces()
                    .Configure(r => r.Named("ICommandBinderImplementation"))
                    .LifestyleSingleton());

            #region Serialization

            container.Register(
                Component.For<JsonSerializer>()
                    .UsingFactoryMethod(kernel =>
                    {
                        var serializer = new JsonSerializer();
                        serializer.Converters.Add(new StringEnumConverter());
                        return serializer;
                    })
                    .LifestyleSingleton());

            #endregion
        }
    }
}
