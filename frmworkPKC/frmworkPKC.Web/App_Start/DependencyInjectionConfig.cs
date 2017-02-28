// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyInjectionConfig.cs" company="Alliance Global Services">
//   Copyright 2015
// </copyright>
// <summary>
//   The Filter Config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using WebActivatorEx;
[assembly: PreApplicationStartMethod(typeof(frmworkPKC.Web.DependencyInjectionConfig), "Start")]
[assembly: ApplicationShutdownMethodAttribute(typeof(frmworkPKC.Web.DependencyInjectionConfig), "Stop")]
namespace frmworkPKC.Web
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common; 
    using global::frmworkPKC.Repository.Pattern.UnitOfWork; 
    using global::frmworkPKC.Repository.Ef6;
    using Entities.Context;
    using global::frmworkPKC.Repository.Pattern.DataContext;    
	using global::frmworkPKC.Common.Logging;
    using global::frmworkPKC.Common.LoggingImplementation; 
    using Controllers;
    using Repository;
    using Services;

    public static class DependencyInjectionConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterComponents(kernel);
				RegisterRepositories(kernel);
				RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterComponents(IKernel kernel)
        { 
            kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>(); 
            kernel.Bind<IDataContextAsync>().To<NorthwindDataContext>();
            kernel.Bind<IDataContext>().To<NorthwindDataContext>();  
			kernel.Bind<ILogHelper>().To<NLogLogger>();   
        }
		 
        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterRepositories(IKernel kernel)
        {
			kernel.Bind<ISupplierRepository>().To<SupplierRepository>();
			kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
        }
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
			kernel.Bind<ISupplierService>().To<SupplierService>();
			kernel.Bind<ICategoryService>().To<CategoryService>();
        }        
    }
}
