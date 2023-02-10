using InGame.Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerGenerator playerGenerator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerGeneratePresenter>();

        builder.Register<PlayerManager>(Lifetime.Singleton);

        builder.RegisterComponent(playerGenerator);
    }
}
