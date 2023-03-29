using UnityEngine;

using Runtime.Enemy.Animation;
using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State.Taunt
{
    public class TauntIdleState : StateBase<EnemyController>
    {
        private EnemyAnimator animator;
        private EnemyLook look;
        private ITargetProvider targetProvider;
        private GameObject target;
        private float waitTime;

        public float seekRange;
        public float attackCoolTime;


        //コンストラクタ
        public TauntIdleState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            animator = owner.GetComponent<EnemyAnimator>();
            targetProvider = owner.GetComponent<ITargetProvider>();
            look = owner.GetComponent<EnemyLook>();
        }


        public override void Start()
        {
            animator.PlayIdle();
            target = targetProvider.target.Value;
            waitTime = 0;

            //ブラックボードのパラメータを初期化しておく
            blackBoard.SetValue<bool>("Seek", false);
            blackBoard.SetValue<bool>("Attack", false);
        }

        public override void Update()
        {
            waitTime += Time.deltaTime;

            look.Look(target.transform.position);

            if (owner.transform.position.DistanceIgnoreY(target.transform.position) > seekRange)
            {
                blackBoard.SetValue<bool>("Seek", true);
            }
            
            else if(waitTime > attackCoolTime)
            {
                blackBoard.SetValue<bool>("Attack", true);
            }
        }

    }
}
