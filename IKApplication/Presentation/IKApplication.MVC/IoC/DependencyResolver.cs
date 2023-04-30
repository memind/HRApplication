using Autofac;
using Autofac.Features.ResolveAnything;
using AutoMapper;
using DotNetOpenAuth.OAuth;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.Mappings;
using IKApplication.Domain.Entites;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.MVC.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserServices>().As<IAppUserServices>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
			})).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
