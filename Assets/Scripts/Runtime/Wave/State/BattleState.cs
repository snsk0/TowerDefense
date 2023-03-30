using UnityEngine;

using Runtime.Enemy;
using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Wave.State
{
    public class BattleState : ParentStateBase<WaveManager>
    {
        //コンポーネント
        private EnemyHealth towerHealth;
        private EnemyManager manager;
        private PlayerManagerProvider provider;

        public int maxWave { private get; set; }

        public BattleState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard) 
        {
            //towerとplayerのインスタンスを取得
            towerHealth = GameObject.FindGameObjectWithTag("Tower").GetComponent<EnemyHealth>();

            this.manager = owner.GetComponent<EnemyManager>();

            provider = owner.GetComponent<PlayerManagerProvider>();
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
            if (towerHealth.currentHealth.Value == 0 || provider.playerManager.IsDead) blackBoard.SetValue<bool>("GameOver", true);

            //bossStateが終了したらEndWave
            if(innerStateMachine.currentState is BossState && !blackBoard.GetValue<bool>("Boss"))
            {
                blackBoard.SetValue<bool>("EndWave", true);

                //生きてる雑魚を強制的に死亡させる
                foreach (EnemyController enemy in manager.livingEnemyList)
                {
                    enemy.blackBoardWriter.SetValue<bool>("Death", true);
                }

                if (owner.wave >= maxWave) blackBoard.SetValue<bool>("Clear", true);
            }
        }


    }
}
