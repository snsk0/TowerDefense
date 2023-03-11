using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InGame.Enemies
{
    public class EnemyGeneratePresenter : ControllerBase, IStartable
    {
        private readonly EnemyManager enemyManager;

        [Inject]
        public EnemyGeneratePresenter(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        public void Start()
        {
            enemyManager.GenerateEnemy();
        }
    }
}

