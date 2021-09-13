using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClubProject1.Common.Services;
using VideoClubProject1.Core.Interfaces;
using VideoClubProject1.Infrastructure.Data;
using VideoClubProject1.Infrastructure.Services;
using VideoClubProject1.Web.Mappings;
using VideoClubProject1.Web.Mappings.Profiles;

namespace VideoClubProject1.Web.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<MovieService>()
                   .As<IMovieService>().InstancePerRequest();

            builder.RegisterType<CopyService>()
                   .As<ICopyService>().InstancePerRequest();

            builder.RegisterType<CustomerService>()
                   .As<ICustomerService>().InstancePerRequest();

            builder.RegisterType<HistoryService>()
                   .As<IHistoryService>().InstancePerRequest();

            builder.RegisterType<MoviePagingService>()
                    .As<IMoviePagingService>().InstancePerRequest();

            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            builder.Register(c => MapperInit.Init())
                    .AsSelf()
                    .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                    .As<IMapper>()
                    .InstancePerLifetimeScope();

            var mapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MovieProfile());
                cfg.AddProfile(new HistoryProfile());
            });

            builder.RegisterType<LoggingService>()
                    .As<ILoggingService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}