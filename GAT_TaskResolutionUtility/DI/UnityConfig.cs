using GAT_TaskResolutionUtility.Logging;
using GAT_TaskResolutionEntity.Models;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace GAT_TaskResolutionUtility.DI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<TaskResolutionContext, TaskResolutionContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogger, Logger>(new HierarchicalLifetimeManager());

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}