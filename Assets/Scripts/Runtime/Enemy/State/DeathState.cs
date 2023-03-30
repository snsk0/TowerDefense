using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Animation;
using Runtime.Enemy.Component;


namespace Runtime.Enemy.State
{
    public class DeathState : StateBase<EnemyController>
    {
        //コンポーネント
        private EnemyDissolve dissolve;
        private EnemyAnimator animator;
        private EnemyItemDropper dropper;   //仮実装

        private float timer;
        private bool isExcuted;


        //コンストラクタ
        public DeathState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            dissolve = owner.GetComponent<EnemyDissolve>();
            animator = owner.GetComponent<EnemyAnimator>();
            dropper = owner.GetComponent<EnemyItemDropper>();
        }



        public override void Start()
        {
            animator.PlayDeath();
            isExcuted = false;
        }

        public override void Update()
        {
            timer += Time.deltaTime;

            if(timer > 1.5f)
            {
                if (!isExcuted)
                {
                    isExcuted = true;
                    if(dissolve != null) dissolve.StartDissolve();
                }
            }
            if(timer > 3.0f)
            {
                dropper.Drop();     //仮実装
                owner.gameObject.SetActive(false);  //Disable
            }
        }


        //死亡遷移から他へ遷移させない
        public override bool GuardChangeState(StateBase<EnemyController> nextState)
        {
            return false;
        }
    }
}
