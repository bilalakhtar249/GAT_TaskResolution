using GAT_TaskkResolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace GAT_TaskkResolution.DI
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<TaskResolutionContext, TaskResolutionContext>(new HierarchicalLifetimeManager());

            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }
    }
}