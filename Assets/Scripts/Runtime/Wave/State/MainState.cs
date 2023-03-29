using UnityEngine;

using System;
using System.Linq;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy;


namespace Runtime.Wave.State
{
    public class MainState : StateBase<WaveManager>
    {
        //コンポーネント
        private EnemyManager enemyManager;

        //変数
        public float waveTime { get; set; }    //フェーズの時間
        public float currentTime { get; private set; }  //現在の経過時間
        public float generateSpanTime { private get; set; } //敵の生成間隔
        public int generateNumber { private get; set; } //敵の同時生成数
        public int[] enemiesNumber { get; private set; }    //敵の生成割合
        private float spanCounter;  //生成間隔のカウント


        //コンストラクタ
        public MainState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard)
        {
            enemyManager = owner.GetComponent<EnemyManager>();

            enemiesNumber = new int[Enum.GetValues(typeof(EnemyType)).Cast<int>().Max() + 1];
        }



        public override void Start()
        {
            currentTime = 0;
            spanCounter = 0;
        }

        public override void Update()
        {
            currentTime += Time.deltaTime;
            spanCounter += Time.deltaTime;


            if (currentTime > waveTime)
            {
                blackBoard.SetValue<bool>("Boss", true);
                return;
            }


            //生成しないときは返す
            if (!(spanCounter > generateSpanTime)) return;

            //spanCounterを初期化
            spanCounter = 0;

            //指定回数生成
            for (int i = 0; i < generateNumber; i++)
            {
                //乱数生成
                int index = UnityEngine.Random.Range(1, enemiesNumber.Sum() + 1);

                //生成する敵を検索
                int valueSum = 0;
                foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
                {
                    valueSum += enemiesNumber[(int)type];

                    if (valueSum <= index)
                    {
                        //生成する
                        enemyManager.GetInitialEnemy(type);
                        break;
                    }
                }
            }
        }



    }
}
