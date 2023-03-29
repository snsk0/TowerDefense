using UnityEngine;

using Runtime.Enemy.Animation;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State.Taunt
{
    public class TauntSpawnState : StateBase<EnemyController>
    {
        private EnemyAnimator animator;
        private Rigidbody rigidbody;

        private float waitTime;

        //コンストラクタ
        public TauntSpawnState(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            animator = owner.GetComponent<EnemyAnimator>();
            rigidbody = owner.GetComponent<Rigidbody>();
        }



        public override void Start()
        {
            animator.PlayUnique(0);
            rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.VelocityChange);

            waitTime = 1.2f;
        }

        public override void Update()
        {
            waitTime -= Time.deltaTime;

            if (waitTime < 0) blackBoard.SetValue<bool>("Spawn", false);
        }
    }
}
