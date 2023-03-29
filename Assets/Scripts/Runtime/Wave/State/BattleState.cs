using UnityEngine;

using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Wave.State
{
    public class BattleState : ParentStateBase<WaveManager>
    {
        //コンポーネント
        private GameObject player;
        private EnemyHealth towerHealth;

        //タワーヘルス

        public BattleState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard) 
        {
            //towerとplayerのインスタンスを取得
            towerHealth = GameObject.FindGameObjectWithTag("Tower").GetComponent<EnemyHealth>();
        }


        protected override void SelfStart()
        {
            blackBoard.SetValue<bool>("EndWave", false);
        }
        protected override void SelfEnd()
        {
            blackBoard.SetValue<bool>("Wait", true);
        }

        protected override void SelfUpdate()
        {
            //playerとtowerどちらかが死んだらゲームオーバーに
            if (towerHealth.currentHealth.Value == 0) blackBoard.SetValue<bool>("GameOver", true);

            //bossStateが終了したらEndWave
            if(innerStateMachine.currentState is BossState && !blackBoard.GetValue<bool>("Boss"))
            {
                blackBoard.SetValue<bool>("EndWave", true);
            }
        }
    }
}
