using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using UniRx;
using System;
using UniRx.Triggers;
using System.Linq;

namespace InGame.Enemies
{
    public class EnemyManager : ControllerBase
    {
        private readonly EnemyGenerator enemyGenerator;

        private readonly List<GameObject> currentEnemyObjects = new List<GameObject>();
        public IEnumerable<GameObject> CurrentEnemyObjects => currentEnemyObjects.Where(x=>!x.GetComponent<EnemyHealth>().HadDeadReactiveProperty.Value);

        private readonly ISubject<int> dropedEnhancementPointSubject = new Subject<int>();
        public IObservable<int> DropedEnhancementPointObservable => dropedEnhancementPointSubject;

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

            enemy.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    currentEnemyObjects.Remove(enemy);
                })
                .AddTo(this);
        }

        //プレイヤーの死亡を監視
        public void ObserveEnemyDeath(EnemyHealth enemyHealth)
        {
            enemyHealth.HadDeadReactiveProperty
                .Where(value => value)
                .Take(1)
                .Delay(TimeSpan.FromSeconds(2f))
                .Subscribe(_ =>
                {
                    //TODO: 敵の生成タイミングは別で設定する
                    GenerateEnemy();
                    //TODO: Enemy側に落とすポイントを設定しておく
                    dropedEnhancementPointSubject.OnNext(1);
                })
                .AddTo(this);
        }
    }
}

