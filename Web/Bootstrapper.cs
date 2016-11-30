using System;
using System.Collections.Generic;
using Infrastructure.CQRS.Commands.Bus;
using Infrastructure.CQRS.Commands.Handlers;
using Infrastructure.CQRS.Commands.Repository;
using Infrastructure.CQRS.Events.Publisher;
using Infrastructure.CQRS.Events.Subscribers;
using Infrastructure.CQRS.EventStore;
using Infrastructure.CQRS.Queries.Handlers;
using Infrastructure.CQRS.Queries.Processor;
using Infrastructure.CQRS.ReadStore.DocumentDb;
using Infrastructure.InMemory.Commands.Bus;
using Infrastructure.InMemory.Events.Publisher;
using Infrastructure.InMemory.EventStore;
using Infrastructure.InMemory.ReadStore.DocumentDb;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using Nancy.Responses.Negotiation;
using Nancy.ViewEngines.Razor;
using Newtonsoft.Json;
using StructureMap;
using Web.Api.Infrastructure;

namespace Web
{
    public class Bootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            InternalConfiguration.ResponseProcessors.Clear();
            InternalConfiguration.ResponseProcessors.Add(typeof(JsonProcessor));
            InternalConfiguration.ResponseProcessors.Add(typeof(RazorViewEngine));

            InternalConfiguration.Serializers.Clear();
            InternalConfiguration.Serializers.Insert(0, typeof(CustomJsonSerializer));
        }

        protected override void ConfigureApplicationContainer(IContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Configure(containerConfiguration =>
            {
                containerConfiguration.Scan(cfg => {
                     cfg.AssembliesFromApplicationBaseDirectory();
                     cfg.WithDefaultConventions();

                     cfg.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                     cfg.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                     cfg.ConnectImplementationsToTypesClosing(typeof(ISubscriber<>));
                     cfg.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>));
                 });

                containerConfiguration.For<JsonSerializer>().Use<CustomJsonSerializer>();

                containerConfiguration.For<ICommandBus>().Singleton().Use(context => new InMemoryCommandBus(context.GetInstance));
                containerConfiguration.For<IEventPublisher>().Singleton().Use(context => new InMemoryEventPublisher(context.GetAllInstances));

                containerConfiguration.For<IEventStore>().Singleton().Use<InMemoryEventStore>();
                containerConfiguration.For(typeof(IRepository<>)).Singleton().Use(typeof(Repository<>));

                containerConfiguration.For<IDocumentDb>().Singleton().Use<InMemoryDocumentDb>();

                containerConfiguration.For<IQueryProcessor>().Singleton().Use(context => new QueryProcessor(context.GetInstance));
            });
        }

        protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.BeforeRequest += nancyContext =>
            {
                var acceptHeaders = new List<Tuple<string, decimal>>(nancyContext.Request.Headers.Accept);
                acceptHeaders.Insert(0, new Tuple<string, decimal>("application/vnd.siren+json", 1m));

                nancyContext.Request.Headers.Accept = acceptHeaders;

                return null;
            };

            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
            {
                x.Response.Headers.Add("Access-Control-Allow-Headers", "AUTHORIZATION");
                x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
            });
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Website/Views/", viewName));

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Styles", "Website/Styles"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Fonts", "Website/Fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", "Website/Scripts"));
        }
    }
}