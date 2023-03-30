using UnityEngine;

using Runtime.Enemy.Component;
using Runtime.Enemy.Animation;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State.Taunt
{
    public class TauntSpawnState : StateBase<EnemyController>
    {
        private EnemyAnimator animator;
        private Rigidbody rigidbody;
        private EnemyHate hate;

        private float waitTime;

        //コンストラクタ
        public TauntSpawnState(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            animator = owner.GetComponent<EnemyAnimator>();
            rigidbody = owner.GetComponent<Rigidbody>();
            hate = owner.GetComponent<EnemyHate>();
        }



        public override void Start()
        {
            animator.PlayUnique(0);
            rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.VelocityChange);

            GameObject tower = GameObject.Find("Tower");
            GameObject player = GameObject.Find("Player");
            hate.AddHate(Random.Range(0f, 2.3f), player);
            hate.AddHate(Random.Range(0.1f, 2.9f), tower);

            waitTime = 1.2f;
        }

        public override void Update()
        {
            waitTime -= Time.deltaTime;

            if (waitTime < 0) blackBoard.SetValue<bool>("Spawn", false);
        }
    }
}
