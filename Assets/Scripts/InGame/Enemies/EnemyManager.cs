using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System;

namespace InGame.Enemies
{
    public class EnemyManager : ControllerBase
    {
        private readonly EnemyGenerator enemyGenerator;

        private List<GameObject> currentEnemyObjects = new List<GameObject>();

        [Inject]
        public EnemyManager(EnemyGenerator enemyGenerator)
        {
            this.enemyGenerator = enemyGenerator;
        }

        public void GenerateEnemy()
        {
            var enemy = enemyGenerator.GenerateEnemy();
            currentEnemyObjects.Add(enemy);
            ObserveEnemyDeath(enemy.GetComponent<EnemyHealth>());
        }

        public void ObserveEnemyDeath(EnemyHealth enemyHealth)
        {
            enemyHealth.HadDeadReactiveProperty
                .Where(value => value)
                .Take(1)
                .Delay(TimeSpan.FromSeconds(2f))
                .Subscribe(_ =>
                {
                    GenerateEnemy();
                })
                .AddTo(this);
        }
    }
}

