using Review.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace Review.StateMachines
{
    public class EnemyGeneratePresenter : ControllerBase, IStartable
    {
        private EnemyManager enemyManager;
        private EnemyGenerateView enemyGenerateView;

        [Inject]
        public EnemyGeneratePresenter(EnemyManager enemyManager, EnemyGenerateView enemyGenerateView)
        {
            this.enemyManager = enemyManager;
            this.enemyGenerateView = enemyGenerateView;
        }

        public void Start()
        {
            enemyGenerateView.GenerateTypeObservable
                .Subscribe(x =>
                {
                    enemyManager.GenerateEnemy(x);
                })
                .AddTo(this);
        }
    }
}

