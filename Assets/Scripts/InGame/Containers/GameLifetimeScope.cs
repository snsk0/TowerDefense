using Cinemachine;
using InGame;
using InGame.Cameras;
using InGame.Cursors;
using InGame.DropItems;
using InGame.Enemies;
using InGame.Players;
using InGame.Players.Archers;
using InGame.Players.Fighters;
using InGame.Targets;
using InGame.UI.Enhancements;
using InGame.UI.Players;
using Prepare;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    //‰ðŒˆ‚·‚éMonoBehaviour
    [SerializeField] private EnhancementView enhancementView;
    [SerializeField] private CursorController cursorController;
    [SerializeField] private TargetPointerView targetPointerView;
    [SerializeField] private PlayerHPView playerHPView;
    [SerializeField] private AttackCoolTimeView attackCoolTimeView;

    //‰ðŒˆ‚µ‚È‚¢MonoBehaviour
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnemyGenerator enemyGenerator;
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private GameObject enhancementPointObjectPrefab;
    [SerializeField] private Transform enhancementPointObjectParent;
    [SerializeField] private EnhancementPointObjectGenerator enhancementPointObjectGenerator;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerGeneratePresenter>();
        //builder.RegisterEntryPoint<EnhancementPresenter>();
        //builder.RegisterEntryPoint<EnemyGeneratePresenter>();
        builder.RegisterEntryPoint<CameraSetUpPresenter>();
        builder.RegisterEntryPoint<CursorPresenter>();
        //builder.RegisterEntryPoint<PlayerHPPresenter>();
        //builder.RegisterEntryPoint<AttackCoolTimePresenter>();

        builder.Register<PlayerBackpack>(Lifetime.Singleton);
        builder.Register<TargetManager>(Lifetime.Singleton);

        builder.Register<PlayerManager>(Lifetime.Singleton)
            .WithParameter("playerGenerator", playerGenerator);
        builder.Register<EnemyManager>(Lifetime.Singleton)
            .WithParameter("enemyGenerator", enemyGenerator);
        builder.Register<CameraManager>(Lifetime.Singleton)
            .WithParameter("freeLookCamera", freeLookCamera);
        builder.Register<EnhancementPointObjectManager>(Lifetime.Singleton)
            .WithParameter("prefab", enhancementPointObjectPrefab)
            .WithParameter("poolParent", enhancementPointObjectParent)
            .WithParameter("generator", enhancementPointObjectGenerator);

        builder.Register<TargetSearcher>(Lifetime.Transient);

        var prepareSetting = Parent.Container.Resolve<PrepareSetting>();
        switch (prepareSetting.selectedPlayerCharacterType)
        {
            case PlayerCharacterType.Fighter:
                builder.Register<PlayerController, FighterController>(Lifetime.Transient);
                builder.RegisterEntryPoint<FighterAnimationSetting>();
                break;
            case PlayerCharacterType.Archer:
                builder.Register<PlayerController, ArcherController>(Lifetime.Transient);
                builder.RegisterEntryPoint<ArcherAnimationSetting>();
                builder.RegisterEntryPoint<TargetPointerPresenter>();
                break;
        }
        
        //builder.RegisterComponent(enhancementView);
        builder.RegisterComponent(cursorController);
        //builder.RegisterComponent(targetPointerView);
        //builder.RegisterComponent(playerHPView);
        //builder.RegisterComponent(attackCoolTimeView);
    }
}
