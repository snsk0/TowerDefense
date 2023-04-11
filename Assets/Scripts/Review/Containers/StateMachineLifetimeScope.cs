using Review.StateMachines;
using VContainer;
using VContainer.Unity;

namespace Review.Containers
{
    public class StateMachineLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<StateMachineFactory>(Lifetime.Scoped);
        }
    }
}

