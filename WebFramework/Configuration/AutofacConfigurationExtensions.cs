using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Data.Repositories;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        // رجیستر کردن کل سرویس ها
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //نوع ریجستر > به ازای > خود سرویس
            //RegisterType > As > Liftetime
            // معادل دستور پایین services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // services.AddTransient => InstancePerDependency
            // services.AddScoped => InstancePerLifetimeScope
            // services.AddSingleton => SingleInstance
            // InstancePerRequest : بارای هر درخواست یه نمونه دراختیار ما قرار میده
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            
            // بجای این روش که دونه دونه ریجستر کنیم از روش پایین اسمبلی استفاده می کنیم
            // containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            // لایه های پروژه های برنامه بعنوان اسمبلی
            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            var servicesAssembly = typeof(JwtService).Assembly;

            // جستجو برای اسمبلی هایی که از IScopedDependency ارثبری کردند
            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<IScopedDependency>() // که از این اینترفیس ارثبری کرده
                .AsImplementedInterfaces() // به ازای اینترفیس هایی که ارثبری کرده
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        //We don't need this since Autofac updates for ASP.NET Core 3.0+ Generic Hosting
        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            
            // Populate : یک حلقه هست که کل سرویس ها رو جمع کرده
            containerBuilder.Populate(services);

            //Register Services to Autofac ContainerBuilder
            containerBuilder.AddServices();

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
