using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

using UnityEngine;

using UniRx;
using UniRx.Triggers;

using Runtime.Enemy.State;

using Cysharp.Threading.Tasks;


namespace Runtime.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        //設定項目
        [SerializeField] private List<Transform> _generateLocationList;
        [SerializeField] private float sameLocationDelay;

        //Enemy生成器
        private IEnemyGenerator[] generatorList;

        //座標管理
        private List<Transform> generateLocationList;
        private ConcurrentBag<Transform> awaitLocationList;

        //敵の管理
        private List<EnemyController> _livingEnemyList;
        public IReadOnlyList<EnemyController> livingEnemyList => _livingEnemyList;
        public IEnumerable<Transform> LivingEnemyTransformList => _livingEnemyList.Select(x => x.transform);

        //生成イベント
        private Subject<EnemyController> onGenerateSubject;
        public IObservable<EnemyController> onGenerateEventHandler => onGenerateSubject;


        //初期化
        private void Awake()
        {
            generateLocationList = new List<Transform>(_generateLocationList);
            generatorList = new IEnemyGenerator[Enum.GetValues(typeof(EnemyType)).Cast<int>().Max() + 1];
            awaitLocationList = new ConcurrentBag<Transform>();
            _livingEnemyList = new List<EnemyController>();
            onGenerateSubject = new Subject<EnemyController>();

            //generatorを全取得
            foreach(IEnemyGenerator generator in GetComponents<IEnemyGenerator>())
            {
                generatorList[(int)generator.enemyType] = generator;
            }
        }
        private void OnDestroy()
        {
            onGenerateSubject.Dispose();
        }


        //特定の敵を生成して渡す
        public EnemyController GetInitialEnemy(EnemyType type)
        {
            //要素がなくなるまで実行
            Transform awaitTransform;
            while (awaitLocationList.Count != 0)
            {
                //要素を削除する
                bool isSuccess = awaitLocationList.TryTake(out awaitTransform);

                //失敗していたらbreak
                if (!isSuccess) break;

                //削除できていたらgenerateLocationに戻す
                generateLocationList.Add(awaitTransform);
            }

            
            //生成可能座標がない場合nullを返す
            if (generateLocationList.Count == 0) return null;

            //乱数生成
            int index = UnityEngine.Random.Range(0, generateLocationList.Count);

            //座標をリストから削除
            Transform transform = generateLocationList[index];
            generateLocationList.Remove(transform);

            //敵を生成
            EnemyController enemy = generatorList[(int)type].Generate(transform);
            onGenerateSubject.OnNext(enemy);
            _livingEnemyList.Add(enemy);

            //デスを監視する
            SingleAssignmentDisposable disposable = new SingleAssignmentDisposable();
            disposable.Disposable = enemy.UpdateAsObservable()
                .Where(_ => { return enemy.currentState is DeathState; })
                .Subscribe(_ =>
                {
                    //イベント登録の解除
                    disposable.Dispose();

                    //リストから消す
                    _livingEnemyList.Remove(enemy);
                });


            //一定時間後にリストに戻す(非同期)
            UniTask task = WaitLocationCoolTime(transform);

            return enemy;
        }

        //ロケーションのクールタイム待機
        private async UniTask WaitLocationCoolTime(Transform transform)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(sameLocationDelay), cancellationToken: this.GetCancellationTokenOnDestroy());
            awaitLocationList.Add(transform);
        }
    }
}
