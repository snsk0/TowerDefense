using InGame.Enemies;
using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;
using InGame.Players.Input;

namespace InGame.Enhancements
{
    public class EnhancementPointPresenter : ControllerBase, IStartable
    {
        private readonly EnemyManager enemyManager;
        private readonly EnhancementView enhancementView;
        private readonly PlayerBackpack playerBackpack;
        
        [Inject]
        public EnhancementPointPresenter(EnemyManager enemyManager, PlayerBackpack playerBackpack, EnhancementView enhancementView)
        {
            this.enemyManager = enemyManager;
            this.playerBackpack = playerBackpack;
            this.enhancementView = enhancementView;
        }

        public void Start()
        {
            enemyManager.DropedEnhancementPointObservable
                .Subscribe(value =>
                {
                    playerBackpack.AddEnhancementPoint(value);
                })
                .AddTo(this);

            playerBackpack.ObserveEveryValueChanged(x => x.enhancementPoint)
                .Subscribe(point =>
                {
                    enhancementView.SetPointText(point);
                })
                .AddTo(this);
        }
    }
}

