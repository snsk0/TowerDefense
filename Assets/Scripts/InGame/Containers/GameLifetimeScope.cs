using Cinemachine;
using InGame.Cameras;
using InGame.Cursors;
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
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CursorController cursorController;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerGeneratePresenter>();
        builder.RegisterEntryPoint<EnhancementPresenter>();
        builder.RegisterEntryPoint<EnemyGeneratePresenter>();
        builder.RegisterEntryPoint<CameraSetUpPresenter>();
        builder.RegisterEntryPoint<CursorPresenter>();

        builder.Register<PlayerManager>(Lifetime.Singleton);
        builder.Register<EnemyManager>(Lifetime.Singleton);
        builder.Register<PlayerBackpack>(Lifetime.Singleton);
        builder.Register<CameraManager>(Lifetime.Singleton);

        builder.Register<PlayerController>(Lifetime.Transient);

        builder.RegisterComponent(playerGenerator);
        builder.RegisterComponent(enhancementView);
        builder.RegisterComponent(enemyGenerator);
        builder.RegisterComponent(virtualCamera);
        builder.RegisterComponent(cursorController);
    }
}
