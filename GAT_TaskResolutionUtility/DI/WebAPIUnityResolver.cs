using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace GAT_TaskResolutionUtility.DI
{
    public class WebAPIUnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public WebAPIUnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new WebAPIUnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException exception)
            {
                throw new InvalidOperationException(
                    $"Unable to resolve service for type {serviceType}.",
                    exception);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException exception)
            {
                throw new InvalidOperationException(
                    $"Unable to resolve service for type {serviceType}.",
                    exception);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}