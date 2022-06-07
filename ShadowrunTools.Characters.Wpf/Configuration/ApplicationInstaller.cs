﻿using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowrunTools.Characters.ViewModels;
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
                Component.For<IDataLoader>()
                    .ImplementedBy<DataLoader>(),
                Component.For<IRules>()
                    .ImplementedBy<RulesPrototype>()
                    .LifestyleSingleton(),
                Component.For<DisplaySettings>()
                    .ImplementedBy<DisplaySettings>()
                    .LifestyleSingleton());

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