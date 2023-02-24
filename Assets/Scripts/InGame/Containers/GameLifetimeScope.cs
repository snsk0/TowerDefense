using InGame.Enemies;
using InGame.Enhancements;
using InGame.Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnhancementView enhancementView;
    [SerializeField] private EnemyGenerator enemyGenerator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerGeneratePresenter>();
        builder.RegisterEntryPoint<EnhancementPresenter>();
        builder.RegisterEntryPoint<EnemyGeneratePresenter>();

        builder.Register<PlayerManager>(Lifetime.Singleton);
        builder.Register<EnemyManager>(Lifetime.Singleton);
        builder.Register<PlayerBackpack>(Lifetime.Singleton);

        builder.Register<PlayerController>(Lifetime.Transient);

        builder.RegisterComponent(playerGenerator);
        builder.RegisterComponent(enhancementView);
        builder.RegisterComponent(enemyGenerator);
    }
}
