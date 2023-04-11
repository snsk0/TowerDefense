using Review.Enemies;
using Review.StateMachines;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Review.Containers
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private EnemyGenerateView enemyGenerateView;
        [SerializeField] private EnemyGenerator enemyGenerator;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EnemyGeneratePresenter>();

            builder.Register<EnemyManager>(Lifetime.Singleton);

            builder.Register<EnemyControllerFactory>(Lifetime.Scoped);

            builder.RegisterComponent(enemyGenerateView);
            builder.RegisterComponent(enemyGenerator);
        }
    }
}

