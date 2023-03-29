using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy;

using Runtime.Wave.State;

namespace Runtime.Wave
{
    public class WaveManager : MonoBehaviour
    {
        //ステートマシンとブラックボード
        public StateMachine<WaveManager> stateMachine { get; private set; }
        private IBlackBoard blackBoard;

        //wave数
        public int wave { get; set; }


        //初期化
        private void Awake()
        {
            //ステートマシン関連の生成
            blackBoard = new BlackBoard();
            stateMachine = new StateMachine<WaveManager>(this, blackBoard);

            //BattleStateの構築
            BattleState battle = new BattleState(this, blackBoard);
            MainState main = new MainState(this, blackBoard);
            BossState boss = new BossState(this, blackBoard);

            //変数の設定
            main.waveTime = 30.0f;
            main.generateSpanTime = 10.0f;
            main.generateNumber = 3;
            main.enemiesNumber[(int)EnemyType.Taunt] = 1;

            //遷移の設定
            battle.innerStateMachine.AddTransition(main, boss, blackBoard => { return blackBoard.GetValue<bool>("Boss") == true; });

            //ブラックボードの初期化
            blackBoard.SetValue<bool>("Boss", false);

            //初期化
            battle.innerStateMachine.Initialize(main);



            //ステートの生成
            WaitState wait = new WaitState(this, blackBoard);
            GameOverState gameOver = new GameOverState(this, blackBoard);

            //各ステートの変数設定
            wait.waitTime = 5.0f;

            //遷移を設定
            stateMachine.AddTransition(wait, battle, blackBoard => { return blackBoard.GetValue<bool>("Wait") == false; });
            stateMachine.AddTransition(battle, wait, blackBoard => { return blackBoard.GetValue<bool>("EndWave") == true; });
            stateMachine.AddTransition(battle, gameOver, blackBoard => { return blackBoard.GetValue<bool>("GameOver") == true; });

            //ブラックボードの初期化
            blackBoard.SetValue<bool>("Wait", true);
            blackBoard.SetValue<bool>("EndWave", false);
            blackBoard.SetValue<bool>("GameOver", false);

            //初期化
            stateMachine.Initialize(wait);
        }


        private void Update()
        {
            stateMachine.Tick();
        }
    }
}
