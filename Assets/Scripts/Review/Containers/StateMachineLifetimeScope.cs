using Review.StateMachines;
using VContainer;
using VContainer.Unity;

namespace Review.Containers
{
    public class StateMachineLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StateMachineProcessor>();

            builder.Register<StateMachineFactory>(Lifetime.Singleton);
            builder.Register<StateMachineManager>(Lifetime.Singleton);
        }
    }
}

