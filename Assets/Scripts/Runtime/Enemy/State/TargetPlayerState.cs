using System;

using UnityEngine;

using UniRx;

using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State
{
    public class TargetPlayerState : ParentStateBase<EnemyController>
    {
        //フィールド
        private ITargetProvider targetProvider; //target
        private IDisposable disposable;         //イベント破棄用


        //コンストラクタ
        public TargetPlayerState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            targetProvider = owner.GetComponent<ITargetProvider>();
        }



        //初期化
        protected override void SelfStart()
        {
            disposable = targetProvider.target.Subscribe(targetObject => OnChangeTarget(targetObject));
        }

        //終了時
        protected override void SelfEnd()
        {
            disposable.Dispose();   //登録解除
        }


        //登録用
        private void OnChangeTarget(GameObject targetObject)
        {
            if (targetObject.tag != "Player") blackBoard.SetValue<bool>("Player", false);
        }
    }
}