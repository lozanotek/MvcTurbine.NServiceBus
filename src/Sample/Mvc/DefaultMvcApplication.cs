namespace Mvc {
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Releasers;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.NServiceBus;
    using MvcTurbine.Windsor;
    using MvcTurbine.Web;
    using Services;

    public class DefaultMvcApplication : TurbineApplication {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication() {
            var container = CreateContainer();
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        static IWindsorContainer CreateContainer() {
            IWindsorContainer container = new WindsorContainer();

            // Add support for injection of arrays and IList<T> into
            // the components
            var kernel = container.Kernel;
            kernel.Resolver.AddSubResolver(new ArrayResolver(kernel));
            kernel.Resolver.AddSubResolver(new ListResolver(kernel));
            kernel.ReleasePolicy = new NoTrackingReleasePolicy();

            // Register the components into the container
            container.Register(Component.For<IBroadcastService>().ImplementedBy<BroadcastService>());
            container.Register(Component.For<LogService>().ImplementedBy<LogService>());
            ContainerManager.SetContainerProvider(() => new WindsorObjectBuilder(container));

            return container;
        }
    }
}
