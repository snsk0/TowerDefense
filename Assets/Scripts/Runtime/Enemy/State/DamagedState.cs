using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Component;




namespace Runtime.Enemy.State
{
    public class DamagedState : StateBase<EnemyController>
    {
        private EnemyDamaged damaged;


        public DamagedState(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            damaged = owner.GetComponent<EnemyDamaged>();
        }



        public override void Start()
        {
            //ノックバック方向を取得しコンポーネントに渡す
            Vector3 knockBack = blackBoard.GetValue<Vector3>("Damaged");
            damaged.KnockBack(knockBack, knockBack.magnitude);

            //BlackBoardの数値を管理
            blackBoard.SetValue<bool>("DamgedEnd", false);
            blackBoard.SetValue<Vector3>("Damaged", Vector3.zero);
        }

        public override void Update()
        {
            if (!damaged.isDamaging) blackBoard.SetValue<bool>("DamagedEnd", true);
        }


        
        public override bool GuardChangeState(StateBase<EnemyController> nextState)
        {
            if (damaged.isDamaging)
            {
                if (!(nextState.GetType() == typeof(DeathState))　&& !(nextState.GetType() == typeof(DamagedState))) return false;
            }
            return true;
        }
    }
}
