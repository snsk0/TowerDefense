using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Animation;
using Runtime.Enemy.Component;


namespace Runtime.Enemy.State
{
    public class SeekFailedState : StateBase<EnemyController>
    {
        //コンポーネント
        private EnemyHate hate;
        private EnemyAnimator animator;


        //コンストラクタ
        public SeekFailedState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            hate = owner.GetComponent<EnemyHate>();
            animator = owner.GetComponent<EnemyAnimator>();
        }



        public override void Start()
        {
            //プレイヤーのヘイトを0にする
            GameObject player = GameObject.FindWithTag("Player");
            hate.ClearHate(player);

            //立ちモーションを再生する
            animator.PlayIdle();
        }

        public override void Update()
        {
            //ヘイト値を変更して改善されない場合、Idleに戻す
            //blackBoard.SetValue<bool>("SeekFailed", false);
        }

        public override void End()
        {
            //変更される場合必ずブラックボードのフラグをfalseに
            blackBoard.SetValue<bool>("SeekFailed", false);
        }
    }
}
