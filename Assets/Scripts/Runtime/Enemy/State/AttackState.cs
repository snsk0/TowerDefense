using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State
{
    public class AttackState : StateBase<EnemyController>
    {
        public int index { private get; set; }

        private EnemyAttack attack;
        private ITargetProvider targetProvider;



        //コンストラクタ
        public AttackState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            attack = owner.GetComponent<EnemyAttack>();
            targetProvider = owner.GetComponent<ITargetProvider>();
        }




        public override void Start()
        {
            attack.Attack(index, targetProvider.target.Value);
        }


        public override void Update()
        {
            if (!attack.isAttacking) blackBoard.SetValue<bool>("Attack", false);
        }


        public override bool GuardChangeState(StateBase<EnemyController> nextState)
        {
            if (attack.isAttacking)
            {
                //ダメージを受けたときか死亡ステート以外は攻撃が終わるまでブロック
                if(!(nextState.GetType() == typeof(DamagedState) || nextState.GetType() == typeof(DeathState)))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
