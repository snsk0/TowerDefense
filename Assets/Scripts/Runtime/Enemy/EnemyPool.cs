using System.Collections.Generic;

using UnityEngine;

using UniRx;
using UniRx.Triggers;


namespace Runtime.Enemy
{
    public class EnemyPool : MonoBehaviour, IEnemyGenerator
    {
        //パラメータ
        [SerializeField] private float maxInitializeAmount;
        [SerializeField] private EnemyController enemyPrefab;
        [SerializeField] private EnemyType _enemyType;

        //type参照
        public EnemyType enemyType => _enemyType;

        //Pool用
        private Stack<EnemyController> disableEnemyStack;



        //初期化
        private void Awake()
        {
            //スタックを生成
            disableEnemyStack = new Stack<EnemyController>();

            //初期化回数分Stackに積む
            for(int i = 0; i < maxInitializeAmount; i++)
            {
                PushInitialPrefab();
            }
        }


        //スタックに新しいPrefabを積む
        private void PushInitialPrefab()
        {
            EnemyController enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            enemy.transform.parent = gameObject.transform;
            disableEnemyStack.Push(enemy);
        }


        //生成関数
        public EnemyController Generate(Transform transform)
        {
            //スタックの残りがない場合
            if(disableEnemyStack.Count == 0)
            {
                PushInitialPrefab();
            }

            //スタックから取り出して有効化
            EnemyController enemy = disableEnemyStack.Pop();
            enemy.transform.position = transform.transform.position;
            enemy.transform.rotation = transform.transform.rotation;
            enemy.gameObject.SetActive(true);

            //Disable検知
            SingleAssignmentDisposable disposable = new SingleAssignmentDisposable();
            disposable.Disposable = enemy.gameObject.OnDisableAsObservable()
                .Subscribe(_ =>
                {
                    //イベント登録の解除
                    disposable.Dispose();

                    //スタックに積みなおす
                    disableEnemyStack.Push(enemy);
                });

            return enemy;
        }


    }
}
